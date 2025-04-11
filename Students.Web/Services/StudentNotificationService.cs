using Hangfire;
using Infrastructure.BackgroundJobs.Abstractions;
using Infrastructure.Email.Services;

namespace Students.Web.Services
{
	public class StudentNotificationService(
		IBackgroundJobService backgroundJobService, IEmailService emailService)
	{
		private readonly IBackgroundJobService _backgroundJobService = backgroundJobService;
		private readonly IEmailService _emailService = emailService;

		public void ScheduleReminderEmail(string studentEmail, DateTimeOffset scheduledTime)
		{
			_backgroundJobService.Schedule(() => 
			SendReminderEmailAsync(studentEmail), scheduledTime);
		}

		public void SetUpDailyReportJob()
		{
			_backgroundJobService.AddOrUpdateRecurringJob(
				"daily-student-report",
				() => SendDailyReportAsync(),
				() => Cron.Daily());
		}

		public async Task SendReminderEmailAsync(string studentEmail)
		{
			await _emailService.SendEmailAsync(
				studentEmail,
				"Upcoming Deadline",
				"This is a reminder about your upcoming deadline.");
		}

		public async Task SendDailyReportAsync()
		{

		}
	}
}
