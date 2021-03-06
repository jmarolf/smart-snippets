
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.SmartSnippets
{
    using static SyntaxFactory;

    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class WriteConsoleSnippet : ConsoleSnippetBase
    {
        protected override CompletionItem GetCompletionItem()
            => CompletionItem.Create("Write to the console");

        protected override SyntaxNode GetSnippet()
        {
            return ExpressionStatement(
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("System"),
                            IdentifierName("Console")),
                        IdentifierName("WriteLine"))));
            // TODO: grab data in scope and populate the Console.WriteLine call
        }
    }
}
