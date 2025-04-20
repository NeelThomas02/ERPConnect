using ERPConnect.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ERPConnect.Services
{
    public interface IPdfService
    {
        byte[] GenerateInvoicePdf(Invoice invoice);
    }

    public class PdfService : IPdfService
    {
        public byte[] GenerateInvoicePdf(Invoice invoice)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Lambda overloads now available
                    page.Header()
                        .Text($"Invoice #{invoice.InvoiceId}")
                        .SemiBold()
                        .FontSize(20)
                        .FontColor(Colors.Blue.Medium);

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Customer: {invoice.Customer?.Name}");
                        col.Item().Text($"Date: {invoice.InvoiceDate:yyyy-MM-dd}");
                        col.Item().Text($"Total Amount: {invoice.TotalAmount:C}");
                    });

                    page.Footer().Row(footer =>
                    {
                        footer.ConstantColumn(100)
                            .AlignCenter()
                            .DefaultTextStyle(style => style.FontColor(Colors.Grey.Medium))
                            .Text(txt =>
                            {
                                txt.Span("Generated on ").FontSize(10);
                                txt.Span(DateTime.Now.ToString("yyyy-MM-dd HH:mm")).FontSize(10);
                            });
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
