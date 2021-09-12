using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;


namespace Microsoft.CodeAnalysis.SmartSnippets
{
    internal abstract class SmartSnippet : CompletionProvider
    {
        protected abstract SyntaxNode GetSnippet();

        public override async Task<CompletionChange> GetChangeAsync(Document document, CompletionItem item, char? commitKey, CancellationToken cancellationToken)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            if (root is null)
            {
                throw new InvalidOperationException("Could not get syntax root");
            }
            var snippet = GetSnippet();
            var nodeToReplace = root.FindNode(item.Span);
            var newRoot = root.ReplaceNode(nodeToReplace, snippet);
            var newDoc = document.WithSyntaxRoot(newRoot);
            var changes = (await newDoc.GetTextChangesAsync(document, cancellationToken).ConfigureAwait(false)).ToImmutableArray();
            return CompletionChange.Create(changes[0], changes, includesCommitCharacter: true);
        }
    }
}
