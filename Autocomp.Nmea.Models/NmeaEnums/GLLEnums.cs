namespace Autocomp.Nmea.Models.NmeaEnums
{
    /// <summary>
    /// Klasa kontenerowa dla typów wyliczeniowych (enumów) używanych w komunikatach NMEA typu GLL.
    /// </summary>
    public static class GLLEnums
    {
        /// <summary>
        /// Enum LatitudeDirection określa kierunek szerokości geograficznej:
        /// - N: Północ
        /// - S: Południe
        /// </summary>
        public enum LatitudeDirection
        {
            N,
            S
        }

        /// <summary>
        /// Enum LongitudeDirection określa kierunek długości geograficznej:
        /// - E: Wschód
        /// - W: Zachód
        /// </summary>
        public enum LongitudeDirection
        {
            E,
            W
        }

        /// <summary>
        /// Enum Status definiuje status ważności danych:
        /// - A: Dane są ważne
        /// - V: Dane są nieważne
        /// </summary>
        public enum Status
        {
            A,
            V
        }

        /// <summary>
        /// Enum ModeIndicator określa tryb pracy wskaźnika systemu nawigacji:
        /// - A: Tryb autonomiczny
        /// - D: Tryb różnicowy
        /// - E: Tryb szacowany (dead reckoning)
        /// - M: Tryb ręcznego wprowadzania
        /// - S: Tryb symulatora
        /// - N: Dane są nieważne
        /// </summary>
        public enum ModeIndicator
        {
            A,
            D,
            E,
            M,
            S,
            N
        }
    }
}