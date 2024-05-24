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
}
