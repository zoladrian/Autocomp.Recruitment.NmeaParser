using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parsers.Interfaces;
using System.Reflection;

namespace Autocomp.Nmea.Parsers
{
    public class ReflectionBasedNmeaParsingStrategy : INmeaParsingStrategy
    {
        /// <summary>
        /// Metoda dynamicznie wywołuje odpowiedni parser NMEA na podstawie nagłówka wiadomości.
        /// </summary>
        /// <param name="message">Wiadomość NMEA do przetworzenia.</param>
        /// <param name="parsers">Słownik zawierający dostępne parsery.</param>
        /// <param name="parsedData">Referencja do stringa przechowującego wynik analizy.</param>
        /// </summary>
        public void Parse(NmeaMessage message, Dictionary<string, object> parsers, ref string parsedData)
        {
            try { 
            string headerKey = message.Header.Substring(message.Header.Length - 3);
            if (parsers.TryGetValue(headerKey, out object parserObj))
            {
                Type parserType = parserObj.GetType();
                MethodInfo parseMethod = parserType.GetMethod("Parse");
                if (parseMethod != null)
                {
                    var result = parseMethod.Invoke(parserObj, new object[] { message });
                    PropertyInfo successProp = result.GetType().GetProperty("Success");
                    if ((bool)successProp.GetValue(result))
                    {
                        PropertyInfo dataProp = result.GetType().GetProperty("Data");
                        parsedData = dataProp.GetValue(result).ToString();
                    }
                    else
                    {
                        PropertyInfo errorProp = result.GetType().GetProperty("ErrorMessage");
                        parsedData = (string)errorProp.GetValue(result);
                    }
                }
            }
            else
            {
                parsedData = "Unknown message";
            }
        }
            catch 
            { 
                parsedData = "Unknown message";
            }
        }
    }
}