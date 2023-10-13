namespace Autocomp.Nmea.Parsers.Utilities
{
    public static class NmeaUtilities
    {
        /// <summary>
        /// Konwertuje wartość z formatu ddmm.mmmm
        /// </summary>
        public static decimal ConvertToDecimalDegrees(decimal ddmm)
        {
            decimal integralPart = Math.Floor(ddmm);
            decimal fractionalPart = ddmm - integralPart;

            decimal degrees = Math.Floor(integralPart / 100M);
            decimal minutes = (integralPart % 100M) + fractionalPart;

            return degrees + (minutes / 60M);
        }
    }
}