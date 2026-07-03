using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.MailDto;

public class MailDTO
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
