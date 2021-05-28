using Serilog.Events;
using Stryker.Core.Exceptions;

namespace Stryker.Core.Options.Inputs
{
    public class VerbosityInput : InputDefinition<string, LogEventLevel>
    {
        public override string Default => "info";

        protected override string Description => "The verbosity (loglevel) for output to the console.";
        protected override string HelpOptions => FormatEnumHelpOptions();

        public LogEventLevel Validate()
        {
            if (SuppliedInput is not null)
            {
                return SuppliedInput.ToLower() switch
                {
                    "error" => LogEventLevel.Error,
                    "warning" => LogEventLevel.Warning,
                    "info" => LogEventLevel.Information,
                    "debug" => LogEventLevel.Debug,
                    "trace" => LogEventLevel.Verbose,
                    _ => throw new InputException($"Incorrect verbosity ({SuppliedInput}). The verbosity options are [Trace, Debug, Info, Warning, Error]")
                };
            }

            return LogEventLevel.Information;
        }
    }
}