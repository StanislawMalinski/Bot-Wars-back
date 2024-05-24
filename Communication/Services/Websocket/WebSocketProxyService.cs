using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Services.Websocket
{
    public class WebSocketProxyService
    {
        private readonly string _engineWebSocketEndpoint = "ws://host.docker.internal:7001/ws";
        private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public WebSocketProxyService() 
        {
            ConnectToEngine();
        }

        public async Task ConnectToEngine()
        {
            Console.WriteLine("Web Socket service created - waiting");
            using (var engineWebSocket = new ClientWebSocket())
            {
                try
                {
                    Uri serverUri = new Uri(_engineWebSocketEndpoint);
                    await engineWebSocket.ConnectAsync(serverUri, CancellationToken.None);
                    Console.WriteLine("Connected to engine WebSocket");
                    await ReceiveMessages(engineWebSocket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connecting to engine WebSocket: {ex.Message}");
                }
            }
        }

        private async Task ReceiveMessages(ClientWebSocket clientWebSocket)
        {
            var buffer = new byte[1024 * 4];

            while (clientWebSocket.State == WebSocketState.Open)
            {
                var result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    Console.WriteLine("WebSocket connection closed.");
                }
                else
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine("Received: " + message);
                    await SendUpdateToAllClients(message);
                }
            }
        }

        public async Task AddWebSocketClient(WebSocket webSocket)
        {
            var socketId = Guid.NewGuid().ToString();
            _sockets.TryAdd(socketId, webSocket);
            Console.WriteLine("WebSocket connection established");
            await HandleClientConnection(socketId, webSocket);
        }

        private async Task SendUpdateToAllClients(string message)
        {
            Console.WriteLine("Sending update to all websocket clients");
            var tasks = _sockets.Values.Select(async socket =>
            {
                if (socket.State == WebSocketState.Open)
                {
                    var serverMsg = Encoding.UTF8.GetBytes(message);
                    await socket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }).ToArray();
            await Task.WhenAll(tasks);
        }

        private async Task HandleClientConnection(string socketId, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            _sockets.TryRemove(socketId, out _);
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            Console.WriteLine("WebSocket connection closed");
        }
    }
}
