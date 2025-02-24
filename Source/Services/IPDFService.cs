using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
    public interface IPDFService
    {
        byte[] GenerateInvoicePdf(Bill bill, List<(Recipe Recipe, int Quantity)> items);
    }
}