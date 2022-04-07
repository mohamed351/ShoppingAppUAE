using ShoppingAppUAE.Models.EmailViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Services.Mail
{
    public interface IMailService
    {

        Task SendEmailAsync(MailRequest mailRequest);
    }
}
