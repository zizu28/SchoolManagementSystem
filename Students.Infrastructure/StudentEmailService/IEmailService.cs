using Students.Domain.Entities;

namespace Students.Infrastructure.StudentEmailService
{
	public interface IEmailService
	{
		Task SendEmail(Student student);
		Task SendEmailWithAttachment(Student student);
	}
}
