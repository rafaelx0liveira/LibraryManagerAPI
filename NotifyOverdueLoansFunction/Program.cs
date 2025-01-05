using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HandleOverdueLoansFunction.Infrastructure.Context;
using AutoMapper;
using HandleOverdueLoansFunction.Application.Interfaces;
using HandleOverdueLoansFunction.Persistance.Repository;
using HandleOverdueLoansFunction.Infrastructure.Service;
using System.Net.Mail;
using System.Net;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Obter a string de conexão
string connectionString = builder.Configuration["ConnectionStrings:MySqlConnectionString"] ?? throw new Exception();
var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");

builder.Services.AddDbContext<MySQLContext>(options =>
{
    // Configurar o provedor do banco (ajuste conforme seu banco de dados)
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Registering the repository
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

// Regustering the service
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<SmtpClient>(provider =>
{
    var smtpClient = new SmtpClient("smtp.sendgrid.net")
    {
        Port = 587,
        Credentials = new NetworkCredential("apikey", apiKey),
        EnableSsl = true
    };
    return smtpClient;
});


// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
