using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.SmartSnippets.File
{
    internal abstract class AsyncFileSnippetBase : SnippetInsideMethod
    {
        protected override async Task<bool> ShouldProvideCompletionsAsync(CompletionContext context)
            => await base.ShouldProvideCompletionsAsync(context).ConfigureAwait(false) &&
               await context.IsInAsyncMethodAsync(context.CancellationToken).ConfigureAwait(false);
    }
}
