using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers;
using Autocomp.Nmea.Parsers.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;

namespace Autocomp.Nmea.PrismApp.Modules.ModuleName.ViewModels
{
    public class ParserViewModel : BindableBase
    {
        private readonly Dictionary<string, object> _parsers;
        private readonly ReflectionBasedNmeaParsingStrategy _parsingStrategy;

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

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public ICommand ParseCommand { get; private set; }

        public ParserViewModel(INmeaParser<GLLMessageData> gllParser, INmeaParser<MWVMessageData> mwvParser, INmeaParsingStrategy parsingStrategy)
        {
            _parsers = new Dictionary<string, object>
            {
                { "GLL", gllParser },
                { "MWV", mwvParser }
            };
            _parsingStrategy = new ReflectionBasedNmeaParsingStrategy();
            ParseCommand = new DelegateCommand(ParseNmea);
        }

        private void ParseNmea()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrEmpty(NmeaInput)) return;

            NmeaMessage nmeaMessage = new NmeaMessage(NmeaInput);
            _parsingStrategy.Parse(nmeaMessage, _parsers, ref _parsedData, out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ErrorMessage = errorMessage;
            }

            RaisePropertyChanged(nameof(ParsedData));
            RaisePropertyChanged(nameof(ErrorMessage));
        }
    }
}