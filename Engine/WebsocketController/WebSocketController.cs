using System.Net;
using Engine.Services;
using Microsoft.AspNetCore.Mvc;

namespace Engine.WebsocketController;

public class WebSocketsController : ControllerBase
{
    private new const int BadRequest = (int)HttpStatusCode.BadRequest;
    private readonly WebSocketService _webSocketService;

    public WebSocketsController(WebSocketService webSocketService)
    {
        _webSocketService = webSocketService;
    }

    [HttpGet("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await _webSocketService.AddWebSocket(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = BadRequest;
        }
    }
}