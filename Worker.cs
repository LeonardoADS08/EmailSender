using EmailSender.Core;
using Microsoft.Extensions.Hosting;

public class Worker
    : IHostedService
{
    private readonly IEmailSenderService _emailSenderService;

    public Worker(IEmailSenderService emailSenderService)
    {
        _emailSenderService = emailSenderService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Initializing Email Sender :) ");

        var result = await _emailSenderService.SendEmailBatchAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}