using System;
using Message.Board.Models;
using Message.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Message.Board.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            this._messageService = messageService;
        }


        [HttpGet("list")]
        [Produces("application/json")]
        public IActionResult GetList()
        {
            /*for the sake of this test, just return the DTO object as it is
             Ideally should map with an appropriate model to be exposed
             */
            return new ObjectResult(this._messageService.GetList());
        }

        [HttpPost("send")]
        [Produces("application/json")]
        public  IActionResult Send([FromBody] MessageModel message)
        {
            if (message==null || string.IsNullOrWhiteSpace(message.Content)) return new ObjectResult("Message ceontent cannot be empty") { StatusCode = StatusCodes.Status400BadRequest };

            try
            {               
                 this._messageService.Send(message.Content);
                return new ObjectResult(message);
            }
            catch (Exception)
            {
                return new ObjectResult("An error has occurred") { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
