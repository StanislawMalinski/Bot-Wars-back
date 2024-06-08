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

    [HttpGet("/tournamentWs/{id}")]
    public async Task Get([FromRoute] long id)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await _webSocketService.AddWebSocketClient(webSocket, id);
        }
        else
        {
            HttpContext.Response.StatusCode = BadRequest;
        }
    }
}
