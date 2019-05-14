using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP2.Areas.Identity.Services
{
    public class EmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
