using EmailSender.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using EmailSender.Core.Configurations;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                          ?? "Local";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .Build();

        var password = args[0];

        // Bind services
        services
            // Utility/Configuration
            .AddSingleton<IConfiguration>(configuration)

            // Dependencies
            .AddScoped<SmtpClient>(builder =>
            {
                var appConfigurationProvider = builder.GetRequiredService<IAppConfigurationProvider>();
                var email = appConfigurationProvider.Configurations.Email;

                return new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(email, password)
                };
            })
            .AddScoped<IAppConfigurationProvider, AppConfigurationProvider>()
            .AddScoped<IEmailSenderService, EmailSenderService>()

            // Hosted Services
            .AddHostedService<Worker>();
    })
    .Build();

host.Run();