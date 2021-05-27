namespace Stryker.Core.Options.Inputs
{
    public class DisableBailInput : InputDefinition<bool?, OptimizationModes>
    {
        public override bool? Default => false;

        protected override string Description => "Disable abort unit testrun as soon as the first unit test fails.";

        public OptimizationModes Validate()
        {
            if (SuppliedInput is { })
            {
                return SuppliedInput.Value ? OptimizationModes.DisableBail : OptimizationModes.NoOptimization;
            }
            return OptimizationModes.NoOptimization;
        }
    }
}
