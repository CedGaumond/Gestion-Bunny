using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Gestion_Bunny.Modeles;
using System.IO;
using QuestPDF.Previewer;
using QuestPDF.Companion;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Bunny.Services
{

    public class PDFService : IPDFService
    {
        private readonly string _logoPath;
        private readonly ApplicationDbContext _context;

        public PDFService(ApplicationDbContext context, string logoPath = "wwwroot/images/logo.png")
        {
            _logoPath = logoPath;
            _context = context;
        }

        public byte[] GenerateInvoicePdf(Bill bill, List<(Recipe Recipe, int Quantity)> items)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var model = CreateInvoiceModel(bill, items);

            var document = new InvoiceDocument(model, _logoPath);

            using (var stream = new MemoryStream())
            {
                document.GeneratePdf(stream);
                return stream.ToArray();
            }
        }

        public byte[] GenerateOrderPdf(Order order, List<(Ingredient ingredient, int Quantity)> items)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var model = CreateOrderModel(order, items);

            var document = new OrderDocument(model, _logoPath);

            using (var stream = new MemoryStream())
            {
                document.GeneratePdf(stream);
                return stream.ToArray();
            }
        }

        private InvoiceModel CreateInvoiceModel(Bill bill, List<(Recipe Recipe, int Quantity)> items)
        {
            var orderItems = items.Select(item => new OrderItem
            {
                Name = item.Recipe.Name,
                Price = item.Recipe.Price,
                Quantity = item.Quantity
            }).ToList();

            return new InvoiceModel
            {
                InvoiceNumber = bill.GenerateInvoiceNumber(),
                IssueDate = bill.OrderDate,
                Items = orderItems,
                Comments = "",

                // These could be populated from your actual data
                SellerAddress = new Address
                {
                    CompanyName = "Bunny&Co Joliette",
                    Street = "123 Rue Principale",
                    City = "Joliette",
                    State = "QC"
                }
            };
        }

        private OrderModel CreateOrderModel(Order order, List<(Ingredient ingredient, int Quantity)> items)
        {
            var orderItems = items.Select(item => new OrderItem
            {
                Name = item.ingredient.Name,
                Price = item.ingredient.Price,
                Quantity = item.Quantity,
                QuantityPerDeliveryUnit = item.ingredient.QuantityPerDeliveryUnit
            }).ToList();

            return new OrderModel
            {
                InvoiceNumber = order.GenerateInvoiceNumber(),
                IssueDate = order.OrderDate,
                Items = orderItems,
                Comments = "",

                // These could be populated from your actual data
                SellerAddress = new Address
                {
                    CompanyName = "Bunny&Co Joliette",
                    Street = "123 Rue Principale",
                    City = "Joliette",
                    State = "QC"
                }
            };
        }

        public byte[] GenerateSchedulePdf(DateTime weekStart)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var model = CreateScheduleModel(weekStart);

            var document = new ScheduleDocument(model, _logoPath);

            using (var stream = new MemoryStream())
            {
                document.GeneratePdf(stream);
                return stream.ToArray();
            }
        }


        private ScheduleModel CreateScheduleModel(DateTime weekStart)
        {
            DateTime weekEnd = weekStart.AddDays(7); 

            var schedules = _context.Schedules
                .Where(s => s.ShiftStart >= weekStart && s.ShiftStart < weekEnd)
                .Include(s => s.User)
                .ToList();

            var employees = schedules
                .GroupBy(s => s.User)
                .Select(group => new EmployeeSchedule
                {
                    Name = group.Key.GetFullName(),
                    Shifts = group.Select(s => new Shift
                    {
                        Date = s.ShiftStart.Date,
                        StartTime = s.ShiftStart.TimeOfDay,
                        EndTime = s.ShiftEnd.TimeOfDay
                    }).ToList()
                })
                .ToList();

            return new ScheduleModel
            {
                Title = $"Horaire de la semaine du {weekStart:dd/MM/yyyy} au {weekStart.AddDays(6):dd/MM/yyyy}",
                WeekStart = weekStart,
                Employees = employees
            };
        }

    }

    public class InvoiceDocument : IDocument
    {
        public InvoiceModel Model { get; }
        private readonly string _logoPath;

        public InvoiceDocument(InvoiceModel model, string logoPath)
        {
            Model = model;
            _logoPath = logoPath;
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
                        .Text($"{Model.InvoiceNumber}")
                        .FontSize(20).SemiBold().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

                    column.Item().Text(text =>
                    {
                        text.Span("Date d'achat: ").SemiBold();
                        text.Span($"{Model.IssueDate:d}");
                    });

                    column.Spacing(5);
                    column.Item().Height(5);

                    column.Item().Text(text =>
                    {
                        text.Span($"{Model.SellerAddress.CompanyName}");
                    });
                    column.Item().Text(text =>
                   {
                       text.Span($"{Model.SellerAddress.Street}");
                   });
                    column.Item().Text(text =>
                   {
                       text.Span($"{Model.SellerAddress.City}, {Model.SellerAddress.State}");
                   });
                });

                // Only try to load the logo if file exists
                if (File.Exists(_logoPath))
                {
                    row.ConstantItem(100).AlignLeft().Height(100).Image(_logoPath);
                }
            });
        }

        void ComposeContent(QuestPDF.Infrastructure.IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);
                column.Item().Element(ComposeTaxes);
                column.Item().Element(ComposeTotal);
            });
        }

        void ComposeTotal(QuestPDF.Infrastructure.IContainer container)
        {
            container.PaddingVertical(5).Column(column => {
                column.Spacing(5);
                column.Item().AlignCenter().Text($"Total: {Model.Items.Sum(x => x.Price * x.Quantity) + (Model.Items.Sum(x => x.Price * x.Quantity) * 0.1475m):C}").SemiBold();
            });
        }
        void ComposeTaxes(QuestPDF.Infrastructure.IContainer container)
        {
            container.PaddingVertical(0).Column(column => {
                column.Spacing(5);
                column.Item().AlignCenter().Text($"TPS + TVQ: {Model.Items.Sum(x => x.Price * x.Quantity) * 0.1475m:C}").SemiBold();
            });
        }

        void ComposeTable(QuestPDF.Infrastructure.IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(20);  // Fixed width for the # column
                    columns.RelativeColumn(2);   // Flexible column for the product name
                    columns.RelativeColumn();    // Unit price column
                    columns.RelativeColumn();    // Quantity column
                    columns.RelativeColumn();    // Total column
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#").Bold().FontColor("#fff");
                    header.Cell().Element(CellStyle).Text("Item").Bold().FontColor("#fff");
                    header.Cell().Element(CellStyle).AlignLeft().Text("Prix Unitaire").Bold().FontColor("#fff");
                    header.Cell().Element(CellStyle).AlignLeft().Text("Quantité").Bold().FontColor("#fff");
                    header.Cell().Element(CellStyle).AlignLeft().Text("Sous-total / Item").Bold().FontColor("#fff");
                });

                foreach (var item in Model.Items)
                {
                    bool isEvenRow = Model.Items.IndexOf(item) % 2 == 0;
                    var rowBackground = isEvenRow ? QuestPDF.Helpers.Colors.Grey.Lighten2 : QuestPDF.Helpers.Colors.White;

                    table.Cell().Background(rowBackground).Text(Model.Items.IndexOf(item) + 1);  // # Column
                    table.Cell().Background(rowBackground).Text(item.Name);  // Product Name
                    table.Cell().Background(rowBackground).AlignLeft().Text($"{item.Price:C}");  // Unit price
                    table.Cell().Background(rowBackground).AlignLeft().Text(item.Quantity);  // Quantity
                    table.Cell().Background(rowBackground).AlignCenter().Text($"{item.Price * item.Quantity:C}");  // Total
                }

                static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
                {
                    return container.Background("#1c1c1c").PaddingVertical(3);
                }
            });
        }
    }

    public class OrderDocument : IDocument
    {
        public OrderModel Model { get; }
        private readonly string _logoPath;

        public OrderDocument(OrderModel model, string logoPath)
        {
            Model = model;
            _logoPath = logoPath;
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
                        .Text($"{Model.InvoiceNumber}")
                        .FontSize(20).SemiBold().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

                    column.Item().Text(text =>
                    {
                        text.Span("Date d'achat: ").SemiBold();
                        text.Span($"{Model.IssueDate:d}");
                    });

                    column.Spacing(5);
                    column.Item().Height(5);

                    column.Item().Text(text =>
                    {
                        text.Span($"{Model.SellerAddress.CompanyName}");
                    });
                    column.Item().Text(text =>
                   {
                       text.Span($"{Model.SellerAddress.Street}");
                   });
                    column.Item().Text(text =>
                   {
                       text.Span($"{Model.SellerAddress.City}, {Model.SellerAddress.State}");
                   });
                });

                // Only try to load the logo if file exists
                if (File.Exists(_logoPath))
                {
                    row.ConstantItem(100).AlignLeft().Height(100).Image(_logoPath);
                }
            });
        }

        void ComposeContent(QuestPDF.Infrastructure.IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);
                column.Item().Element(ComposeTaxes);
                column.Item().Element(ComposeTotal);
            });
        }

        void ComposeTotal(QuestPDF.Infrastructure.IContainer container)
        {
            container.PaddingVertical(5).Column(column => {
                column.Spacing(5);
                column.Item().AlignCenter().Text($"Total: {Model.Items.Sum(x => x.Price * x.Quantity) + (Model.Items.Sum(x => x.Price * x.Quantity) * 0.14975m):C}").SemiBold();
            });
        }
        void ComposeTaxes(QuestPDF.Infrastructure.IContainer container)
        {
            container.PaddingVertical(0).Column(column => {
                column.Spacing(5);
                column.Item().AlignCenter().Text($"TPS + TVQ: {Model.Items.Sum(x => x.Price * x.Quantity) * 0.14975m:C}").SemiBold();
            });
        }

        void ComposeTable(QuestPDF.Infrastructure.IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(20);  // Fixed width for the # column
                    columns.RelativeColumn(2);
                    columns.RelativeColumn();   // Flexible column for the product name
                    columns.RelativeColumn();    // Unit price column
                    columns.RelativeColumn();    // Quantity column
                    columns.RelativeColumn();    // Total column
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#").Bold().FontColor("#fff");
                    header.Cell().Element(CellStyle).Text("Item").Bold().FontColor("#fff");
                    header.Cell().Element(CellStyle).AlignLeft().Text("Prix Unitaire").Bold().FontColor("#fff");
                    header.Cell().Element(CellStyle).AlignLeft().Text("Quantité").Bold().FontColor("#fff");
                    header.Cell().Element(CellStyle).AlignLeft().Text("Portions").Bold().FontColor("#fff");
                    header.Cell().Element(CellStyle).AlignCenter().Text("Sous-total").Bold().FontColor("#fff");
                });

                foreach (var item in Model.Items)
                {
                    bool isEvenRow = Model.Items.IndexOf(item) % 2 == 0;
                    var rowBackground = isEvenRow ? QuestPDF.Helpers.Colors.Grey.Lighten2 : QuestPDF.Helpers.Colors.White;

                    table.Cell().Background(rowBackground).Text(Model.Items.IndexOf(item) + 1);  // # Column
                    table.Cell().Background(rowBackground).Text(item.Name);  // Product Name
                    table.Cell().Background(rowBackground).AlignLeft().Text($"{item.Price:C}");  // Unit price
                    table.Cell().Background(rowBackground).AlignLeft().Text(item.Quantity);  // Quantity
                    table.Cell().Background(rowBackground).AlignLeft().Text(item.QuantityPerDeliveryUnit);  // Quantity
                    table.Cell().Background(rowBackground).AlignCenter().Text($"{item.Price * item.Quantity:C}");  // Total
                }

                static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
                {
                    return container.Background("#1c1c1c").PaddingVertical(3);
                }
            });
        }
    }

    public class ScheduleDocument : IDocument
    {
        public ScheduleModel Model { get; }
        private readonly string _logoPath;

        public ScheduleDocument(ScheduleModel model, string logoPath)
        {
            Model = model;
            _logoPath = logoPath;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(40);
                    page.MarginHorizontal(30);

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
                row.RelativeItem().Text($"{Model.Title}")
                    .FontSize(20).SemiBold().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
            });
        }

        void ComposeContent(QuestPDF.Infrastructure.IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Spacing(10);

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(150); 
                        for (int i = 0; i < 7; i++)
                        {
                            columns.RelativeColumn();
                        }
                    });
                  
                    table.Header(header =>
                    {
                        header.Cell().Border(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2)
                            .Padding(5).Text("Employé").SemiBold().FontSize(12);

                        for (int i = 0; i < 7; i++)
                        {
                            var day = Model.WeekStart.AddDays(i).ToString("ddd dd MMM");
                            header.Cell().Border(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2)
                                .Padding(5).Text(day).SemiBold().FontSize(12);
                        }
                    });

                    // Lignes pour chaque employé
                    foreach (var employee in Model.Employees)
                    {
                        table.Cell().Border(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2)
                            .Padding(5).Text(employee.Name).FontSize(11);

                        for (int i = 0; i < 7; i++)
                        {
                            var day = Model.WeekStart.AddDays(i).Date;
                            var shift = employee.Shifts.FirstOrDefault(s => s.Date.Date == day);

                            if (shift != null)
                            {
                                table.Cell().Border(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2)
                                    .Padding(5)
                                    .Text($"{shift.StartTime:hh\\:mm} - {shift.EndTime:hh\\:mm}")
                                    .FontSize(11).FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
                            }
                            else
                            {
                                table.Cell().Border(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2)
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(11).AlignCenter();
                            }
                        }
                    }
                });
            });
        }
    }

    public class InvoiceModel
    {
        public String InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public Address SellerAddress { get; set; }
        public List<OrderItem> Items { get; set; }
        public string Comments { get; set; }
    }

    public class OrderModel
    {
        public String InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public Address SellerAddress { get; set; }
        public Address CustomerAddress { get; set; }
        public List<OrderItem> Items { get; set; }
        public string Comments { get; set; }
    }

    public class OrderItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal QuantityPerDeliveryUnit { get; set; }
    }

    public class Address
    {
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public class ScheduleModel
    {
        public string Title { get; set; }
        public DateTime WeekStart { get; set; }
        public List<EmployeeSchedule> Employees { get; set; } = new();
    }

    public class EmployeeSchedule
    {
        public string Name { get; set; }
        public List<Shift> Shifts { get; set; } = new();
    }

    public class Shift
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

}