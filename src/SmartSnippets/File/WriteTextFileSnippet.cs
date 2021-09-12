
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.SmartSnippets.File
{
    using static SyntaxFactory;

    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class WriteTextFileSnippet : SyncFileSnippetBase
    {
        protected override CompletionItem GetCompletionItem()
            => CompletionItem.Create("Write text to a file");

        protected override SyntaxNode GetSnippet()
            => ExpressionStatement(
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("System"),
                                IdentifierName("IO")),
                            IdentifierName("File")),
                        IdentifierName("WriteAllText")))
                .WithArgumentList(
                    ArgumentList(
                        SeparatedList<ArgumentSyntax>(
                            new SyntaxNodeOrToken[]{
                                Argument(
                                    LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        Literal("TheFile.txt"))),
                                Token(SyntaxKind.CommaToken),
                                Argument(
                                    LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        Literal("TextContents")))}))));
    }
}
