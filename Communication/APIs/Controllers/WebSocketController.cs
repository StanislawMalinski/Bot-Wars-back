using Communication.Services.Websocket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Communication.APIs.Controllers
{
    public class WebSocketsController : ControllerBase
    {
        private new const int BadRequest = ((int)HttpStatusCode.BadRequest);
        private readonly WebSocketProxyService _webSocketService;

        public WebSocketsController(WebSocketProxyService webSocketService)
        {
            _webSocketService = webSocketService;
        }

        [HttpGet("/tournament_ws/{id}")]
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
}
