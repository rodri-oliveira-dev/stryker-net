using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.FSharp.Collections;
using Stryker.Core.Logging;
using Stryker.Core.Mutators;
using Stryker.Core.Options;
using static FSharp.Compiler.SyntaxTree;

namespace Stryker.Core.Mutants
{
    internal class FsharpMutantOrchestrator : BaseMutantOrchestrator
    {
        private ILogger Logger { get; }
        private readonly FsharpBaseOrchestrator _base;

        public FsharpMutantOrchestrator(IEnumerable<IMutator> mutators = null, StrykerOptions options = null) : base(options)
        {
            Mutants = new Collection<Mutant>();
            Logger = ApplicationLogging.LoggerFactory.CreateLogger<MutantOrchestrator>();
            _base = new FsharpBaseOrchestrator();
        }

        public FSharpList<SynModuleOrNamespace> Mutate(FSharpList<SynModuleOrNamespace> treeroot)
        {
            var mutationContext = new MutationContext(this);
            var mutation = /*treeroot*/_base.Mutate(treeroot);

            if (mutationContext.HasStatementLevelMutant && _options?.DevMode == true)
            {
                // some mutants where not injected for some reason, they should be reviewed to understand why.
                Logger.LogError($"Several mutants were not injected in the project : {mutationContext.BlockLevelControlledMutations.Count + mutationContext.StatementLevelControlledMutations.Count}");
            }
            // mark remaining mutants as CompileError
            foreach (var mutant in mutationContext.StatementLevelControlledMutations.Union(mutationContext.BlockLevelControlledMutations))
            {
                mutant.ResultStatus = MutantStatus.CompileError;
                mutant.ResultStatusReason = "Stryker was not able to inject mutation in code.";
            }
            return mutation;
        }
    }
}