using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers;
using Autocomp.Nmea.Parsers.FieldParsers;
using Autocomp.Nmea.Parsers.Interfaces;
using Autocomp.Nmea.Parsers.Validators;
using Autocomp.Nmea.PrismApp.Modules.ModuleName;
using Autocomp.Nmea.PrismApp.Modules.ModuleName.ViewModels;
using Autocomp.Nmea.PrismApp.Views;
using FluentValidation;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Windows;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;
using static Autocomp.Nmea.Models.NmeaEnums.MWVEnums;

namespace Autocomp.Nmea.PrismApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Rejestracja parserów dla GLLMessageData
            containerRegistry.Register<INmeaParser<GLLMessageData>, GLLParser>();
            containerRegistry.Register<IValidator<GLLMessageData>, GLLMessageDataValidator>();

            // Rejestracja parserów dla MWVMessageData
            containerRegistry.Register<INmeaParser<MWVMessageData>, MWVParser>();
            containerRegistry.Register<IValidator<MWVMessageData>, MWVMessageDataValidator>();

            containerRegistry.Register<INmeaParsingStrategy, ReflectionBasedNmeaParsingStrategy>();

            // Rejestracja parserów dla różnych typów pól
            containerRegistry.Register<IFieldParser<DateTime>, DateTimeFieldParser>();
            containerRegistry.Register<IFieldParser<LatitudeDirection>, EnumFieldParser<LatitudeDirection>>();
            containerRegistry.Register<IFieldParser<LongitudeDirection>, EnumFieldParser<LongitudeDirection>>();
            containerRegistry.Register<IFieldParser<Models.NmeaEnums.MWVEnums.Status>, EnumFieldParser<Models.NmeaEnums.MWVEnums.Status>>();
            containerRegistry.Register<IFieldParser<ModeIndicator>, EnumFieldParser<ModeIndicator>>();
            containerRegistry.Register<IFieldParser<double>, DoubleFieldParser>();
            containerRegistry.Register<IFieldParser<decimal>, DecimalFieldParser>();
            containerRegistry.Register<IFieldParser<WindSpeedUnits>, EnumFieldParser<WindSpeedUnits>>();
            containerRegistry.Register<IFieldParser<Reference>, EnumFieldParser<Reference>>();
            containerRegistry.Register<IFieldParser<Models.NmeaEnums.GLLEnums.Status>, EnumFieldParser<Models.NmeaEnums.GLLEnums.Status>>();

            containerRegistry.Register<ParserViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ParserModule>();
        }
    }
}