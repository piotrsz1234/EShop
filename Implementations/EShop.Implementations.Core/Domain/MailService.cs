using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using File = System.IO.File;

namespace EShop.Implementations.Core.Domain
{
    internal sealed class MailService : IMailService
    {
        private string SenderMail => ConfigurationManager.AppSettings["SenderMail"];
        private string SenderPassword => ConfigurationManager.AppSettings["SenderPassword"];
        private string MailServer => ConfigurationManager.AppSettings["MailServer"];
        private string TemplatesPath => ConfigurationManager.AppSettings["TemplatesPath"];

        public async Task<bool> SendOrderCreatedMailAsync(Order order)
        {
            var template = File.ReadAllText(Path.Combine(TemplatesPath, "OrderCreatedMail.html"));

            template = template.Replace("##OrderNumber##", order.OrderNumber);
            
            var builder = new StringBuilder();

            foreach (var orderProduct in order.OrderProduct) {
                builder.Append($"<p>{orderProduct.Count} x {orderProduct.Product.Name}</p>");
            }
            
            template = template.Replace("##OrderItems##", builder.ToString());

            return await SendEmailAsync(order.User.Email, "You created order", template);
        }

        public async Task<bool> SendOrderStatusChangedMailAsync(Order order)
        {
            var template = File.ReadAllText(Path.Combine(TemplatesPath, "OrderStatusChangedMail.html"));

            template = template.Replace("##OrderNumber##", order.OrderNumber);
            
            
            template = template.Replace("##CurrentStatus##", order.Status.GetDescription());

            return await SendEmailAsync(order.User.Email, "You order changed status", template);
        }
        
        private async Task<bool> SendEmailAsync(string recipient, string subject, string mailBody)
        {
            var message = new MailMessage(SenderMail, recipient, subject, mailBody);

            using (var client = new SmtpClient(MailServer)) {
                client.Credentials = new NetworkCredential(SenderMail, SenderPassword);

                try {
                    await client.SendMailAsync(message);
                } catch (Exception) {
                    return false;
                }
            }

            return true;
        }
    }
}