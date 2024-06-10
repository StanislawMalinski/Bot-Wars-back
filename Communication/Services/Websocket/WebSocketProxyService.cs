using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace Communication.Services.Websocket;

public class WebSocketProxyService
{
        private readonly string _engineWebSocketEndpoint = "ws://bot_wars_engine:8080/ws/{0}";
        private static ConcurrentDictionary<long, ConcurrentDictionary<string, WebSocket>> _sockets = new ConcurrentDictionary<long, ConcurrentDictionary<string, WebSocket>>();
        private static ConcurrentDictionary<long, bool> _connectedTournaments = new ConcurrentDictionary<long, bool>();
        public WebSocketProxyService() 
        {
        }

        public async Task ConnectToEngine(long tournamentId)
        {
            Console.WriteLine($"Connecting to Engine WebSocket with id {tournamentId}");
            if (!_connectedTournaments.ContainsKey(tournamentId))
            {
                _connectedTournaments.TryAdd(tournamentId, false);
            }
            if (_connectedTournaments[tournamentId])
            {
                return;
            }
            using (var engineWebSocket = new ClientWebSocket())
            {
                try
                {
                    Uri serverUri = new Uri(string.Format(_engineWebSocketEndpoint, tournamentId));
                    await engineWebSocket.ConnectAsync(serverUri, CancellationToken.None);
                    Console.WriteLine($"Connected to engine WebSocket {serverUri}");
                    await ReceiveMessages(engineWebSocket, tournamentId);
                    _connectedTournaments[tournamentId] = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connecting to engine WebSocket: {ex.Message}");
                }
            }
        }

        private async Task ReceiveMessages(ClientWebSocket clientWebSocket, long tournamentId)
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
                    await SendUpdateToClients(message, tournamentId);
                }
            }
        }

        public async Task AddWebSocketClient(WebSocket webSocket, long tournamentId)
        {
            Console.WriteLine("Adding new client WebScoket");
            ConnectToEngine(tournamentId);
            _sockets.TryAdd(tournamentId, new ConcurrentDictionary<string, WebSocket>());
            var socketId = Guid.NewGuid().ToString();
            _sockets[tournamentId][socketId] = webSocket;
            Console.WriteLine("WebSocket connection established");
            await HandleClientConnection(socketId, webSocket);
        }

        private async Task SendUpdateToClients(string message, long tournamentId)
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

        private async Task HandleClientConnection(string socketId, WebSocket webSocket)
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