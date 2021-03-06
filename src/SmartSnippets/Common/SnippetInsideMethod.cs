using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;


namespace Microsoft.CodeAnalysis.SmartSnippets
{
    internal abstract class SnippetInsideMethod : SmartSnippet
    {

        protected override async Task<bool> ShouldProvideCompletionsAsync(CompletionContext context)
            => await context.IsInMethodBodyAsync(context.CancellationToken).ConfigureAwait(false);
    }
}
