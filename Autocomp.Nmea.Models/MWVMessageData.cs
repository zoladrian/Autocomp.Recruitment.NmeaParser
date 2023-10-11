﻿using static Autocomp.Nmea.Models.NmeaEnums.MWVEnums;

namespace Autocomp.Nmea.Models
{
    public class MWVMessageData
    {
        public double WindAngle { get; set; }
        public WindSpeedUnits WindSpeedUnits { get; set; }
        public double WindSpeed { get; set; }
        public Reference Reference { get; set; }
        public Status Status { get; set; }

        public MWVMessageData(
            double windAngle,
            WindSpeedUnits windSpeedUnits,
            double windSpeed,
            Reference reference,
            Status status
        )
        {
            WindAngle = windAngle;
            WindSpeedUnits = windSpeedUnits;
            WindSpeed = windSpeed;
            Reference = reference;
            Status = status;
        }
    }
}