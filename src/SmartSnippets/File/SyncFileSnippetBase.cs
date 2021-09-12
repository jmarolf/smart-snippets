using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;

namespace Microsoft.CodeAnalysis.SmartSnippets.File
{
    internal abstract class SyncFileSnippetBase : SnippetInsideMethod
    {
        protected override async Task<bool> ShouldProvideCompletionsAsync(CompletionContext context)
            => await base.ShouldProvideCompletionsAsync(context).ConfigureAwait(false);
    }
}
