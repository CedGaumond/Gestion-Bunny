using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class InvoiceDocument : IDocument

{
    public InvoiceModel Model { get; }

    public InvoiceDocument(InvoiceModel model)
    {
        Model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

     public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(57);
                page.MarginHorizontal(40);
            
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

                 page.Footer().AlignCenter().Height(50).PaddingBottom(10).Text(x =>
            {
                x.CurrentPageNumber();
                x.Span(" / ");
                x.TotalPages();
            });
            });
    }

    void ComposeHeader(QuestPDF.Infrastructure.IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item()
                    .Text($"Reçu #{Model.InvoiceNumber}")
                    .FontSize(20).SemiBold().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

                column.Item().Text(text =>
                {
                    text.Span("Date payé: ").SemiBold();
                    text.Span($"{Model.IssueDate:d}");
                });
                
                column.Item().Text(text =>
                {
                    text.Span("Date approx. de réception : ").SemiBold();
                    text.Span($"{Model.DueDate:d}");
                });
            });

            row.ConstantItem(100).AlignLeft().Height(100).Image("Source/Resources/Images/logo.jpg");
        });
    }

    void ComposeContent(QuestPDF.Infrastructure.IContainer container)
{
    container.PaddingVertical(40).Column(column =>
    {
        column.Spacing(5);

        column.Item().Element(ComposeTable);
        column.Item().Element(ComposeTotal);
    });
}

void ComposeTotal(QuestPDF.Infrastructure.IContainer container)
{
    container.PaddingVertical(0).Column(column => {
        column.Spacing(5);
        column.Item().AlignCenter().Text($"Grand total: {Model.Items.Sum(x => x.Price * x.Quantity):C}").SemiBold();
});
}

void ComposeTable(QuestPDF.Infrastructure.IContainer container)
{
    container.Table(table =>
{
    table.ColumnsDefinition(columns =>
    {
        columns.ConstantColumn(35);  // Fixed width for the # column
        columns.RelativeColumn(3);   // Flexible column for the product name
        columns.RelativeColumn();    // Unit price column
        columns.RelativeColumn();    // Quantity column
        columns.RelativeColumn();    // Total column
    });

    table.Header(header =>
    {
        header.Cell().Element(CellStyle).Text("#").Bold().FontColor("#fff");
        header.Cell().Element(CellStyle).Text("Ingrédient").Bold().FontColor("#fff");
        header.Cell().Element(CellStyle).AlignLeft().Text("Prix Unitaire").Bold().FontColor("#fff");
        header.Cell().Element(CellStyle).AlignLeft().Text("Quantité").Bold().FontColor("#fff");
        header.Cell().Element(CellStyle).AlignLeft().Text("Total").Bold().FontColor("#fff");
    });

    foreach (var item in Model.Items)
    {
        bool isEvenRow = Model.Items.IndexOf(item) % 2 == 0;
        var rowBackground = isEvenRow ? QuestPDF.Helpers.Colors.Grey.Lighten2 : QuestPDF.Helpers.Colors.White;

        table.Cell().Background(rowBackground).Text(Model.Items.IndexOf(item) + 1);  // # Column
        table.Cell().Background(rowBackground).Text(item.Name);  // Product Name
        table.Cell().Background(rowBackground).AlignLeft().Text($"{item.Price}$");  // Unit price
        table.Cell().Background(rowBackground).AlignLeft().Text(item.Quantity);  // Quantity
        table.Cell().Background(rowBackground).AlignLeft().Text($"{item.Price * item.Quantity}$");  // Total
    }

    static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
    {
        return container.Background("#1c1c1c").PaddingVertical(3);
    }

});

}
}
public class InvoiceModel
{
    public required int InvoiceNumber { get; set; }
    public required DateTime IssueDate { get; set; }
    public required DateTime DueDate { get; set; }

    public required Address SellerAddress { get; set; }
    public required Address CustomerAddress { get; set; }

    public required List<OrderItem> Items { get; set; }
    public required string Comments { get; set; }
}

public class OrderItem
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class Address
{
    public required string CompanyName { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required object Email { get; set; }
    public required string  Phone { get; set; }
}

public static class InvoiceDocumentDataSource
{
    private static Random Random = new Random();

    public static InvoiceModel GetInvoiceDetails()
    {
        var items = Enumerable
            .Range(1, 100)
            .Select(i => GenerateRandomOrderItem())
            .ToList();

        return new InvoiceModel
        {
            InvoiceNumber = Random.Next(1_000, 10_000),
            IssueDate = DateTime.Now,
            DueDate = DateTime.Now + TimeSpan.FromDays(14),

            SellerAddress = GenerateRandomAddress(),
            CustomerAddress = GenerateRandomAddress(),

            Items = items,
            Comments = Placeholders.Paragraph()
        };
    }

    private static OrderItem GenerateRandomOrderItem()
    {
        return new OrderItem
        {
            Name = Placeholders.Label(),
            Price = (decimal) Math.Round(Random.NextDouble() * 100, 2),
            Quantity = Random.Next(1, 10)
        };
    }

    private static Address GenerateRandomAddress()
    {
        return new Address
        {
            CompanyName = Placeholders.Name(),
            Street = Placeholders.Label(),
            City = Placeholders.Label(),
            State = Placeholders.Label(),
            Email = Placeholders.Email(),
            Phone = Placeholders.PhoneNumber()
        };
    }
}

// Usage 
/*
@using QuestPDF.Fluent
@using QuestPDF.Helpers
@using QuestPDF.Infrastructure
@using System.IO

<h1>PDF Generation Test</h1>

@code {
    private string filePath = "";
    private string errorMessage = "";

    protected override void OnInitialized()
    {
        try 
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            var document = new InvoiceDocument(model);
            document.GeneratePdfAndShow();
        
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
    }
}

@if (!string.IsNullOrEmpty(filePath))
{
    <p>PDF created at: @filePath</p>
}

@if (!string.IsNullOrEmpty(errorMessage))
{
    <p>Error: @errorMessage</p>
}
*/