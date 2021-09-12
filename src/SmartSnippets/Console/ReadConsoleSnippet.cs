using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.SmartSnippets
{
    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class ReadConsoleSnippet : CompletionProvider
    {
        public override async Task ProvideCompletionsAsync(CompletionContext context)
        {
            var token = context.CancellationToken;
            if (IsInExe(context) && await IsInMethodBodyAsync(context, token).ConfigureAwait(false))
            {
                context.AddItem(CompletionItem.Create("Read from the console")
                    .WithRules(CompletionItemRules.Default.WithFormatOnCommit(true)));
            }

            // todo: determine if we are on the right hand side of an assignment
        }

        public override async Task<CompletionChange> GetChangeAsync(Document document,
            CompletionItem item,
            char? commitKey,
            CancellationToken cancellationToken)
        {

            var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            if (root is null)
            {
                throw new InvalidOperationException("Could not get syntax root");
            }
            var snippet = SyntaxFactory.ParseExpression("System.Console.ReadLine();");
            var nodeToReplace = root.FindNode(item.Span);
            var newRoot = root.ReplaceNode(nodeToReplace, snippet);
            var newDoc = document.WithSyntaxRoot(newRoot);
            var changes = (await newDoc.GetTextChangesAsync(document, cancellationToken).ConfigureAwait(false)).ToImmutableArray();
            return CompletionChange.Create(changes[0], changes, includesCommitCharacter: true);
        }

        private static bool IsInExe(CompletionContext context)
        {
            return context.Document.Project.CompilationOptions?.MainTypeName is not null ||
                   context.Document.Project.CompilationOptions?.OutputKind == OutputKind.ConsoleApplication;
        }

        private static async Task<bool> IsInMethodBodyAsync(CompletionContext context, CancellationToken cancellationToken)
        {
            var currentSpan = context.CompletionListSpan;
            var root = await context.Document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            if (root is null)
            {
                return false;
            }

            var node = root.FindNode(currentSpan);
            var method = node.FirstAncestorOrSelf<MethodDeclarationSyntax>();
            if (method is not null)
            {
                return true;
            }

            return false;
        }
    }
}
