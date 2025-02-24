using Microsoft.Maui.ApplicationModel.Communication;
using System.Threading.Tasks;

namespace Gestion_Bunny.Utils
{
    public static class EmailUtil
    {
        public static async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = new List<string> { to }
                };

                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException)
            {
                Console.WriteLine("L'envoi d'e-mails n'est pas pris en charge sur cet appareil.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'envoi de l'e-mail : {ex.Message}");
            }
        }
    }
}
