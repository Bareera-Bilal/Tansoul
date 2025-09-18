using System;

namespace P1WEBMVC.Interfaces;

public interface IMailService
{
public Task SendEmailAsync(string emailAddress , string subject ,  string body, bool isHtml = true );
}
