
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.SmartSnippets
{
    using static SyntaxFactory;

    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class ClearConsoleSnippet : ConsoleSnippetBase
    {
        protected override CompletionItem GetCompletionItem()
            => CompletionItem.Create("Clear console");

        protected override SyntaxNode GetSnippet()
            => ExpressionStatement(
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("Console"),
                        IdentifierName("Clear"))));


    }
}
