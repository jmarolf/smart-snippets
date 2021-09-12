
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.SmartSnippets.File
{
    using static SyntaxFactory;

    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class ReadTextFileSnippet : SyncFileSnippetBase
    {
        protected override CompletionItem GetCompletionItem()
            => CompletionItem.Create("Read text from a file");

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
                        IdentifierName("ReadAllText")))
                .WithArgumentList(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                LiteralExpression(
                                    SyntaxKind.StringLiteralExpression,
                                    Literal("TheFile.txt")))))));
    }
}
