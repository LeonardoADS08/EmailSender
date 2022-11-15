using EmailSender.Core;
using Microsoft.Extensions.Hosting;
using PanoramicData.ConsoleExtensions;

public class Worker : BackgroundService
{
    private readonly IEmailSenderService _emailSenderService;

    public Worker(IEmailSenderService emailSenderService)
    {
        _emailSenderService = emailSenderService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.Write("Password: ");
        var password = ConsolePlus.ReadPassword();
        Console.WriteLine();

        var result = await _emailSenderService.SendEmailBatchAsync(password);
    }
}