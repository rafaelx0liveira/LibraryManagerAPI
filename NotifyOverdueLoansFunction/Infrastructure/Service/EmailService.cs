﻿using HandleOverdueLoansFunction.Application.Interfaces;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HandleOverdueLoansFunction.Infrastructure.Service
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;

        public EmailService()
        {
            _apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
        }

        public async Task SendEmailAsync(string to, string subject, string templatePath, Dictionary<string, string> placeholders)
        {
            string body = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

            body = HttpUtility.HtmlDecode(body);

            foreach (var placeholder in placeholders)
            {
                body = body.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }

            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("rafaelaparecido.oliveirasilva@gmail.com", "Biblioteca Online");
            var toEmail = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(
                from,
                toEmail,
                subject,
                plainTextContent: null, 
                htmlContent: body 
            );

            var response = await client.SendEmailAsync(msg);

            // Verifica o status da resposta
            if ((int)response.StatusCode >= 400)
            {
                throw new Exception($"Failed to send email. Status Code: {response.StatusCode}");
            }
        }
    }
}
