using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers;
using Autocomp.Nmea.Parsers.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace Autocomp.Nmea.PrismApp.Modules.ModuleName.ViewModels
{
    public class ParserViewModel : BindableBase
    {
        private string _message;
        private string _parsedData;
        private string _nmeaInput;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public string ParsedData
        {
            get { return _parsedData; }
            set { SetProperty(ref _parsedData, value); }
        }

        public string NmeaInput
        {
            get { return _nmeaInput; }
            set { SetProperty(ref _nmeaInput, value); }
        }

        public ICommand ParseCommand { get; private set; }

        private readonly INmeaParser<GLLMessageData> _gllParser;
        private readonly INmeaParser<MWVMessageData> _mwvParser;

        public ParserViewModel(INmeaParser<GLLMessageData> gllParser, INmeaParser<MWVMessageData> mwvParser)
        {
            _gllParser = gllParser;
            _mwvParser = mwvParser;
            ParseCommand = new DelegateCommand(ParseNmea);
        }

        private void ParseNmea()
        {
            if (string.IsNullOrEmpty(NmeaInput)) return;

            NmeaMessage nmeaMessage = new NmeaMessage(NmeaInput);

            if (_gllParser.CanParse(nmeaMessage.Header))
            {
                var result = _gllParser.Parse(nmeaMessage);
                if (result.Success)
                    ParsedData = result.Data.ToString();
                else if (result.ErrorMessage is not null)
                    ParsedData = result.ErrorMessage;
            }
            else if (_mwvParser.CanParse(nmeaMessage.Header))
            {

                var result = _mwvParser.Parse(nmeaMessage);
                if (result.Success)
                    ParsedData = result.Data.ToString();
                else if (result.ErrorMessage is not null)
                    ParsedData = result.ErrorMessage;
            }
            else
            {
                ParsedData = "Unknown header";
            }
        }
    }
}
