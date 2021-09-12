using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;

namespace Microsoft.CodeAnalysis.SmartSnippets
{
    internal abstract class ConsoleSnippetBase : SnippetInsideMethod
    {
        protected override async Task<bool> ShouldProvideCompletionsAsync(CompletionContext context)
        {
            return (await base.ShouldProvideCompletionsAsync(context).ConfigureAwait(false)) &&
                context.IsInConsoleApp();
        }
    }
}
