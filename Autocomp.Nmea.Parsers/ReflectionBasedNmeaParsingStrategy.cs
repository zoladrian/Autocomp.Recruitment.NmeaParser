using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Autocomp.Nmea.Parsers
{
    public class ReflectionBasedNmeaParsingStrategy : INmeaParsingStrategy
    {
        /// <summary>
        /// Metoda dynamicznie wywołuje odpowiedni parser NMEA na podstawie nagłówka wiadomości.
        /// Parsuje wiadomość i przechowuje wynik w przekazanym przez referencję stringu.
        /// W przypadku niepowodzenia zwraca komunikat o błędzie.
        /// </summary>
        /// <param name="message">Wiadomość NMEA do przetworzenia. Nie może być null.</param>
        /// <param name="parsers">Słownik zawierający dostępne parsery. Nie może być null.</param>
        /// <param name="parsedData">Referencja do stringa przechowującego wynik analizy.</param>
        public void Parse(NmeaMessage message, Dictionary<string, object> parsers, ref string parsedData, out string errorMessage)
        {
            errorMessage = null;

            if (message == null)
            {
                errorMessage = "Nmea Message is empty.";
                return;
            }

            if (parsers == null)
            {
                errorMessage = "Parsers list is empty.";
                return;
            }

            try
            {
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
                            errorMessage = (string)errorProp.GetValue(result);
                        }
                    }
                    else
                    {
                        errorMessage = "Unknown message";
                    }
                }
                else
                {
                    errorMessage = "Unknown message";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
