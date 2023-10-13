using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Models.NmeaEnums
{
    // Klasa kontenerowa dla enumów używanych w komunikatach NMEA typu MWV.
    public static class MWVEnums
    {

        /// <summary>
        /// Definiuje jednostki prędkości wiatru.
        /// </summary>
        public enum WindSpeedUnits
        {
            K, // km/h
            M, // m/h
            N, // Węzły
            S  // mph
        }

        /// <summary>
        /// Definiuje odniesienie dla kąta i prędkości wiatru.
        /// </summary>
        public enum Reference
        {
            R, // Względne (względem ruchu statku)
            T  // Teoretyczne (jakby statek był nieruchomy)
        }

        /// <summary>
        /// Definiuje status wiadomości.
        /// </summary>
        public enum Status
        {
            A, // Dane są ważne
            V  // Dane są nieważne
        }
    }

}
