namespace Infrastructure.Email.Services
{
	public interface IEmailService
	{
		Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true);
		Task SendToManyAsync(IEnumerable<string> toEmails, string subject, string body, bool isHtml = true);
		Task SendWithAttachmentAsync(string toEmail, string subject, string body,
			string attachmentPath, string attachmntName = null!, bool isHtml = true);

		Task SendWithAttachmentToMAnyAsync(IEnumerable<string> toEmails, string subject, string body,
			string attachmentPath, string attachmentName = null!, bool isHtml = true);
	}
}
