using System;
using Microsoft.AspNetCore.Mvc;
using P1WEBMVC.Services;

namespace P1WEBMVC.Controllers;

public class ChatController : Controller
{

    public IActionResult Assistant()
{
    return View();
}

    private readonly ChatGPTService _chatService;

    public ChatController(ChatGPTService chatService)
    {
        _chatService = chatService;
    }

   [HttpPost]
public async Task<JsonResult> GetResponse(string question)
{
    var answer = await _chatService.AskAsync(question);
    if (string.IsNullOrEmpty(answer))
    {
        answer = "Hmm, the AI seems quiet. Check if the backend is responding.";
    }
    return Json(answer);
}

}
