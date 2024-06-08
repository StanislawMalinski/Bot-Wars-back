using Shared.DataAccess.DataBaseEntities;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace Engine.Services
{
    public class WebSocketService
    {
        private static ConcurrentDictionary<long, ConcurrentDictionary<string, WebSocket>> _sockets = new ConcurrentDictionary<long, ConcurrentDictionary<string, WebSocket>>();

        public async Task AddWebSocket(WebSocket webSocket, long tournamentId)
        {
            _sockets.TryAdd(tournamentId, new ConcurrentDictionary<string, WebSocket>());
            var socketId = Guid.NewGuid().ToString();
            _sockets[tournamentId][socketId] = webSocket;
            Console.WriteLine("WebSocket connection established");
            await HandleConnection(socketId, webSocket);
        }

        public async Task SendUpdateToAllClients(string message, long tournamentId)
        {
            Console.WriteLine("Sending update to all websocket clients");
            var tasks = _sockets[tournamentId].Values.Select(async socket =>
            {
                if (socket.State == WebSocketState.Open)
                {
                    var serverMsg = Encoding.UTF8.GetBytes(message);
                    await socket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }).ToArray();
            await Task.WhenAll(tasks);
        }
        private async Task HandleConnection(string socketId, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            foreach (var keyValueSocket in _sockets)
            {
                keyValueSocket.Value.TryRemove(socketId, out _);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            Console.WriteLine("WebSocket connection closed");
        }
    }
}
