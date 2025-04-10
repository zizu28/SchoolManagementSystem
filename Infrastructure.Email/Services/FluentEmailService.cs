using FluentEmail.Core;
using FluentEmail.Smtp;
using Infrastructure.Email.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Email.Services
{
	public class FluentEmailService : IEmailService
	{
		private readonly EmailSettings _settings;
		private readonly IFluentEmail _fluentEmail;
		private readonly ILogger<FluentEmailService> _logger;

		public FluentEmailService(IOptions<EmailSettings> settings, 
			IFluentEmail fluentEmail, ILogger<FluentEmailService> logger)
		{
			_settings = settings.Value;
			_fluentEmail = fluentEmail;
			_logger = logger;

			var smtpSender = new SmtpSender(new SmtpClient(_settings.SmtpServer)
			{
				Port = _settings.SmtpPort,
				Credentials = new NetworkCredential(_settings.SmtpUsername, _settings.SmtpPassword),
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
			});
		}

		public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
		{
			var email = await _fluentEmail
				.To(toEmail)
				.Subject(subject)
				.Body(body, isHtml)
				.SendAsync();

			if (email.Successful)
			{
				_logger.LogInformation($"Email sent to {toEmail} with subject: {subject}");
			}
			else
			{
				_logger.LogError($"Failed to send email to {toEmail} with subject: {subject}, " +
					$"Errors: {string.Join(",", email.ErrorMessages)}");
				throw new Exception($"Sending email failed: {string.Join(",", email.ErrorMessages)}");
			}
		}

		public Task SendToManyAsync(IEnumerable<string> toEmails, string subject, string body, bool isHtml = true)
		{
			var tasks = toEmails.Select(email => SendEmailAsync(email, subject, body, isHtml)).ToList();
			return Task.WhenAll(tasks);
		}

		public async Task SendWithAttachmentAsync(string toEmail, string subject, string body, string attachmentPath, string attachmntName = null, bool isHtml = true)
		{
			try
			{
				if(!File.Exists(attachmentPath))
				{
					throw new FileNotFoundException($"Attachment file not found: {attachmentPath}");
				}
				attachmntName ??= Path.GetFileName(attachmentPath);

				var email = await _fluentEmail
					.To(toEmail)
					.Subject(subject)
					.Body(body, isHtml)
					.AttachFromFilename(attachmntName, attachmentPath)
					.SendAsync();

				if (email.Successful)
				{
					_logger.LogInformation($"Email with attachment sent to {toEmail} with subject: {subject}");
				}
				else
				{
					_logger.LogError($"Failed to send email with attachment to {toEmail} with subject: {subject}, Errors: {string.Join(",", email.ErrorMessages)}");
					throw new Exception($"Sending email with attachment failed: {string.Join(",", email.ErrorMessages)}");
				}
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, $"Exception occurred while sending email with attachment to {toEmail}");
				throw;
			}
		}

		public Task SendWithAttachmentToMAnyAsync(IEnumerable<string> toEmails, string subject, string body, string attachmentPath, string attachmentName = null, bool isHtml = true)
		{
			var tasks = toEmails.Select(email => SendWithAttachmentAsync(email, subject, body, attachmentPath, attachmentName, isHtml)).ToList();
			return Task.WhenAll(tasks);
		}
	}
}
