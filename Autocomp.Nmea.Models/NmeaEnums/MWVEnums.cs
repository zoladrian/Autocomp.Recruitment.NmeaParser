namespace Autocomp.Nmea.Models.NmeaEnums
{
    /// <summary>
    /// Klasa kontenerowa dla typów wyliczeniowych (enumów) używanych w komunikatach NMEA typu MWV.
    /// </summary>
    public static class MWVEnums
    {
        /// <summary>
        /// Enum WindSpeedUnits definiuje jednostki prędkości wiatru:
        /// - K: km/h (kilometry na godzinę)
        /// - M: m/h (metry na godzinę)
        /// - N: Węzły
        /// - S: mph (mile na godzinę)
        /// </summary>
        public enum WindSpeedUnits
        {
            K,
            M,
            N,
            S
        }

        /// <summary>
        /// Enum Reference definiuje odniesienie dla kąta i prędkości wiatru:
        /// - R: Względne (względem ruchu statku)
        /// - T: Teoretyczne (jakby statek był nieruchomy)
        /// </summary>
        public enum Reference
        {
            R,
            T
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
    }
}