using HtmlToPdf.App.Presentation.ActionFilters;
using HtmlToPdf.App.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace HtmlToPdf.App.Presentation.Controllers;

[Route("api/pdf")]
[ApiController]
public class PdfController : ControllerBase
{
    private readonly IPdfService _pdfService;

    public PdfController(IPdfService pdfService) => _pdfService = pdfService;

    /// <summary>
    /// Converts an HTML file to a PDF.
    /// </summary>
    /// <param name="file">The HTML file to convert.</param>
    /// <returns>The generated PDF file.</returns>
    [HttpPost]
    [Produces("application/pdf")]
    //[ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> ConvertHtmlToPdf([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is not uploaded.");

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        string htmlContent = Encoding.UTF8.GetString(stream.ToArray());

        var pdfBytes = await _pdfService.ConvertHtmlToPdf(htmlContent);

        return File(pdfBytes, "application/pdf", "converted-file.pdf");
    }

}
