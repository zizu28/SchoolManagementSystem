using Infrastructure.PDFGeneration.Abstractions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.PDFGeneration.Services
{
	public class QuestPDFGenerationService : IPDFGenerationService
	{
		public byte[] GeneratePDF<T>(T data) where T : class
		{
			return GeneratePDF(data, options => { });
		}

		public byte[] GeneratePDF<T>(T data, Action<PDFGenerationOptions> configOptions) where T : class
		{
			// Set QuestPDF Licence
			QuestPDF.Settings.License = LicenseType.Community;

			// Create generation options
			var options = new PDFGenerationOptions();
			configOptions(options);

			// Create document based on the type
			var document = CreateDocument(data, options);

			return document.GeneratePdf();
		}

		private Document CreateDocument<T>(T data, PDFGenerationOptions options) where T : class
		{
			return Document.Create(container =>
			{
				container.Page(page =>
				{
					// Page setup
					page.Size(options.Landscape ? PageSizes.A4.Landscape() : PageSizes.A4);
					page.Margin(1, Unit.Centimetre);

					// Header
					page.Header().Height(50).Background(Colors.Grey.Lighten1)
					.AlignCenter()
					.Text(options.Title ?? typeof(T).Name)
					.FontSize(16)
					.SemiBold();

					// Content
					page.Content().PaddingVertical(1, Unit.Centimetre)
					.Column(col =>
					{
						// Dynamic content generation based on type
						GenerateContentForType(col, data, options);
					});

					// Footer with page numbers
					if (options.IncludePageNumbers)
					{
						page.Footer().Height(50).Background(Colors.Grey.Lighten1)
						.AlignCenter()
						.Text(text =>
						{
							text.CurrentPageNumber();
							text.Span(" / ");
							text.TotalPages();
						});
					}
				});
			});
		}

		private void GenerateContentForType<T>(ColumnDescriptor col, T data, PDFGenerationOptions options) where T : class
		{
			// Basic reflection-based content generation
			// You'll want to create more specific generation methods for different types
			foreach(var property in typeof(T).GetProperties())
			{
				col.Item()
					.Text($"{property.Name}: {property.GetValue(data)}")
					.FontSize(options.FontSize)
					.FontFamily(options.FontFamily);
			}
		}

		public async Task SavePDFAsync<T>(T data, string filePath) where T : class
		{
			var pdfBytes = GeneratePDF(data);
			await File.WriteAllBytesAsync(filePath, pdfBytes);
		}
	}
}
