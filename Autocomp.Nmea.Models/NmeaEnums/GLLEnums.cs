using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Models.NmeaEnums
{
    // Klasa kontenerowa dla enumów używanych w komunikatach NMEA typu GLL.
    public static class GLLEnums
    {
        /// <summary>
        /// Określa kierunek szerokości geograficznej.
        /// </summary>
        public enum LatitudeDirection
        {
            N, // Północ
            S  // Południe
        }

        /// <summary>
        /// Określa kierunek długości geograficznej.
        /// </summary>
        public enum LongitudeDirection
        {
            E, // Wschód
            W  // Zachód
        }

        /// <summary>
        /// Status danych. A oznacza dane ważne, V oznacza dane nie ważne.
        /// </summary>
        public enum Status
        {
            A, // Dane są ważne
            V  // Dane są nieważne
        }

        /// <summary>
        /// Definiuje tryb wskaźnika systemu nawigacji.
        /// </summary>
        public enum ModeIndicator
        {
            A, // Tryb autonomiczny
            D, // Tryb różnicowy
            E, // Tryb szacowany (dead reckoning)
            M, // Tryb ręcznego wprowadzania
            S, // Tryb symulatora
            N  // Dane są nieważne
        }
    }
}
