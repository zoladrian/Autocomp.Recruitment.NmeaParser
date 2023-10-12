using Autocomp.Nmea.Parsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Parsers.FieldParsers
{
    public class EnumFieldParser<TEnum> : IFieldParser<TEnum> where TEnum : struct
    {
        public bool TryParse(string field, out TEnum value)
        {
            return Enum.TryParse(field.Trim()[0].ToString(), out value);
        }
    }
    
}
