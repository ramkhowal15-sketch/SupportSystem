using Application.Dtos.MailDto;
using Application.Interfaces.Services.MailServices;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace Infrastracture.Services.MailServicves;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;


    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;

    }

    public async Task<string> Sendmail(MailDTO mail)
    {
        var from = _configuration["EmailSettings:Email"];
        var password = _configuration["EmailSettings:Password"];
        var port = Convert.ToInt32(_configuration["EmailSettings:Port"]);
        var host = _configuration["EmailSettings:Host"];

        var fromMail = new MailboxAddress("Ram", from);

        var message = new MimeMessage();
        message.From.Add(fromMail);

        message.To.Add(MailboxAddress.Parse(mail.To));


        message.Subject = mail.Subject;

        message.Body = new TextPart("html")
        {
            Text = mail.Body
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(from, password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);

        return "Email sent successfully";
    }
}

