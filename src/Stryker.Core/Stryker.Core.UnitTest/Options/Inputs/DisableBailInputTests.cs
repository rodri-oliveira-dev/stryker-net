using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Stryker.Core.Options;
using Stryker.Core.Options.Inputs;
using Xunit;

namespace Stryker.Core.UnitTest.Options.Inputs
{
    public class DisableBailInputTests
    {
        [Theory]
        [InlineData(false, OptimizationModes.NoOptimization)]
        [InlineData(true, OptimizationModes.DisableBail)]
        [InlineData(null, OptimizationModes.NoOptimization)]
        public void ShouldValidate(bool? input, OptimizationModes expected)
        {
            var target = new DisableBailInput { SuppliedInput = input };

            var result = target.Validate();

            result.ShouldBe(expected);
        }
    }
}