namespace Infrastructure.PDFGeneration.Abstractions
{
	public interface IPDFGenerationService
	{
		byte[] GeneratePDF<T>(T data) where T: class;
		byte[] GeneratePDF<T>(T data, Action<PDFGenerationOptions> configOptions) where T : class;
		Task SavePDFAsync<T>(T data, string filePath) where T : class;
	}

	public class PDFGenerationOptions
	{
		public bool Landscape { get; set; } = false;
		public string Title { get; set; } = string.Empty;
		public bool IncludePageNumbers { get; set; } = false;
		public string FontFamily { get; set; } = "Arial";
		public int FontSize { get; set; } = 12;
	}
}
