using System.Collections.Generic;

namespace WebApi.Dto
{
    public record SendMailDto(MailAddress From,
                              ICollection<MailAddress> To,
                              ICollection<MailAddress> Cc,
                              ICollection<MailAddress> Bcc,
                              string Subject,
                              string Body,
                              bool IsBodyHtml,
                              string Priority,
                              ICollection<Attachment> Attachments);
}
