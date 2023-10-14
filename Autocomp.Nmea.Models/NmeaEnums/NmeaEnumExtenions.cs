using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;
using static Autocomp.Nmea.Models.NmeaEnums.MWVEnums;

namespace Autocomp.Nmea.Models.NmeaEnums
{
    public static class NmeaEnumExtensions
    {
        public static string ToDescription(this ModeIndicator modeIndicator)
        {
            switch (modeIndicator)
            {
                case ModeIndicator.A:
                    return "Autonomous mode";

                case ModeIndicator.D:
                    return "Differential mode";

                case ModeIndicator.E:
                    return "Estimated (dead reckoning) mode";

                case ModeIndicator.M:
                    return "Manual input mode";

                case ModeIndicator.S:
                    return "Simulator mode";

                case ModeIndicator.N:
                    return "Data not valid";

                default:
                    return "Unknown";
            }
        }

        public static string ToDescription(this GLLEnums.Status status)
        {
            switch (status)
            {
                case GLLEnums.Status.A:
                    return "Data valid";

                case GLLEnums.Status.V:
                    return "Data not valid";

                default:
                    return "Unknown";
            }
        }

        public static string ToDescription(this MWVEnums.Status status)
        {
            switch (status)
            {
                case MWVEnums.Status.A:
                    return "Data valid";

                case MWVEnums.Status.V:
                    return "Data not valid";

                default:
                    return "Unknown";
            }
        }

        public static string ToDescription(this WindSpeedUnits windSpeedUnits)
        {
            switch (windSpeedUnits)
            {
                case WindSpeedUnits.K:
                    return "Knots";

                case WindSpeedUnits.M:
                    return "m/s";

                case WindSpeedUnits.N:
                    return "Km/h";

                case WindSpeedUnits.S:
                    return "mph";

                default:
                    return "Unknown";
            }
        }

        public static string ToDescription(this Reference reference)
        {
            switch (reference)
            {
                case Reference.R:
                    return "Relative";

                case Reference.T:
                    return "Theoretical";

                default:
                    return "Unknown";
            }
        }
    }
}