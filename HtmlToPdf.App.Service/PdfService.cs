using DinkToPdf;
using DinkToPdf.Contracts;
using HtmlToPdf.App.Service.Contracts;
using System;

namespace HtmlToPdf.App.Service;

public class PdfService : IPdfService
{
    private readonly IConverter _convertor;

    public PdfService(IConverter convertor)
    {
        _convertor = convertor;
    }

    public async Task<byte[]> ConvertHtmlToPdf(string htmlContent)
    {
        return await Task.Run(() =>
        {
            var pdfDoc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = { new ObjectSettings { HtmlContent = htmlContent } }
            };

            return _convertor.Convert(pdfDoc);
        });
    }
}
