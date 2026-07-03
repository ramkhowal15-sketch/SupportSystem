using Application.Dtos.MailDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services.MailServices;

public interface IMailService
{
    Task<string> Sendmail(MailDTO mail);
}
