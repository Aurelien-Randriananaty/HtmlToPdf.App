namespace HtmlToPdf.App.Service.Contracts;

public interface IPdfService
{
    Task<byte[]> ConvertHtmlToPdf(string htmlContent);
}
