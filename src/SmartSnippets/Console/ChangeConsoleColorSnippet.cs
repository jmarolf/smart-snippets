
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.SmartSnippets
{
    using static SyntaxFactory;

    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class ChangeConsoleColorSnippet : ConsoleSnippetBase
    {
        protected override CompletionItem GetCompletionItem()
            => CompletionItem.Create("Change console color");

        protected override SyntaxNode GetSnippet()
            => ExpressionStatement(
                AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("Console"),
                        IdentifierName("BackgroundColor")),
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("ConsoleColor"),
                        IdentifierName("DarkRed"))));


    }
}
