using System.Net;
using Communication.Services.Websocket;
using Microsoft.AspNetCore.Mvc;

namespace Communication.APIs.Controllers;

public class WebSocketsController : ControllerBase
{
    private new const int BadRequest = (int)HttpStatusCode.BadRequest;
    private readonly WebSocketProxyService _webSocketService;

    public WebSocketsController(WebSocketProxyService webSocketService)
    {
        _webSocketService = webSocketService;
    }

    [HttpGet("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await _webSocketService.AddWebSocketClient(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = BadRequest;
        }
    }
}