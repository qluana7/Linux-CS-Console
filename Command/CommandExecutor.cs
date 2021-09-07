using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsConsole
{
    public partial class CommandExecutor
    {
        public IEnumerable<string> Imports { get; set; }

        public CommandExecutor(CommandExecutorConfigure config)
        {
            Imports = config.Imports;
        }

        public async Task<AnalysisResult> Run(string cs)
        {
            try
            {
                var globals = new Variables();

                var sopts = ScriptOptions.Default;
                sopts = sopts.WithImports(Imports);
                sopts = sopts.WithReferences(AppDomain.CurrentDomain.GetAssemblies().Where(xa => !xa.IsDynamic && !string.IsNullOrWhiteSpace(xa.Location)));

                var script = CSharpScript.Create(cs, sopts, typeof(Variables));
                script.Compile();
                var result = await script.RunAsync(globals).ConfigureAwait(false);

                if (result != null && result.ReturnValue != null && !string.IsNullOrWhiteSpace(result.ReturnValue.ToString()))
                    return new AnalysisResult()
                    {
                        State = ResultState.Ok,
                        Result = result.ReturnValue.ToString(),
                        Command = cs,
                        Exception = null
                    };
                else
                    return new AnalysisResult()
                    {
                        State = ResultState.NoContent,
                        Result = null,
                        Command = cs,
                        Exception = null
                    };
            }
            catch (Exception ex)
            {
                return new AnalysisResult()
                {
                    State = ResultState.Error,
                    Result = null,
                    Command = cs,
                    Exception = ex
                };
            }
        }
    }

    public class Variables
    {

    }

    public enum ResultState
    {
        Ok,
        NoContent,
        Error
    }

    public class AnalysisResult
    {
        public ResultState State { get; init; }

        public string Result { get; init; }

        public Exception Exception { get; init; }

        public string Command { get; init; }
    }
}