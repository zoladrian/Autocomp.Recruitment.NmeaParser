using Autocomp.Nmea.PrismApp.Services.Interfaces;

namespace Autocomp.Nmea.PrismApp.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
