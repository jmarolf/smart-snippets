using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Simplification;

namespace Microsoft.CodeAnalysis.SmartSnippets
{
    internal abstract class SmartSnippet : CompletionProvider
    {
        protected abstract SyntaxNode GetSnippet();
        protected abstract CompletionItem GetCompletionItem();
        protected abstract Task<bool> ShouldProvideCompletionsAsync(CompletionContext context);

        public override async Task ProvideCompletionsAsync(CompletionContext context)
        {
            if (await ShouldProvideCompletionsAsync(context).ConfigureAwait(false))
            {
                context.AddItem(GetCompletionItem()
                       .WithRules(CompletionItemRules.Default.WithFormatOnCommit(true)));
            }
        }

        public override async Task<CompletionChange> GetChangeAsync(Document document, CompletionItem item, char? commitKey, CancellationToken cancellationToken)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            if (root is null)
            {
                throw new InvalidOperationException("Could not get syntax root");
            }
            var snippet = GetSnippet()
                .WithAdditionalAnnotations(
                    Simplifier.Annotation,
                    Formatter.Annotation);
            var nodeToReplace = root.FindNode(item.Span);
            var newRoot = root.ReplaceNode(nodeToReplace, snippet);
            var newDoc = document.WithSyntaxRoot(newRoot);
            var changes = (await newDoc.GetTextChangesAsync(document, cancellationToken).ConfigureAwait(false)).ToImmutableArray();
            return CompletionChange.Create(changes[0], changes, includesCommitCharacter: true);
        }
    }
}
