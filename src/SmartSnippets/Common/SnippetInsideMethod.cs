using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;


namespace Microsoft.CodeAnalysis.SmartSnippets
{
    internal abstract class SnippetInsideMethod : SmartSnippet
    {
        protected abstract Task ProvideCompletionsCoreAsync(CompletionContext context);

        public override async Task ProvideCompletionsAsync(CompletionContext context)
        {
            if (await context.IsInMethodBodyAsync(context.CancellationToken))
            {
                await ProvideCompletionsCoreAsync(context);
            }
        }
    }
}
