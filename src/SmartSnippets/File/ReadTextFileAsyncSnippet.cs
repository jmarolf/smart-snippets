
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.SmartSnippets.File
{
    using static SyntaxFactory;

    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class ReadTextFileAsyncSnippet : AsyncFileSnippetBase
    {
        protected override CompletionItem GetCompletionItem()
            => CompletionItem.Create("Read text from a file");

        protected override SyntaxNode GetSnippet()
            => ExpressionStatement(
                AwaitExpression(
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
                            IdentifierName("ReadAllTextAsync")))
                    .WithArgumentList(
                        ArgumentList(
                            SingletonSeparatedList<ArgumentSyntax>(
                                Argument(
                                    LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        Literal("TheFile.txt"))))))));
    }
}
