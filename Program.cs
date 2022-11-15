using EmailSender.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false
        };

        var configurations = new Configuration()
        services.AddScoped<SmtpClient>(builder => smtp);
        services.AddScoped<IAppConfigurations, AppConfigurations>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();