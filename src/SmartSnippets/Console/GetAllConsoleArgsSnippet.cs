
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.SmartSnippets
{
    using static SyntaxFactory;

    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class GetAllConsoleArgsSnippet : ConsoleSnippetBase
    {
        protected override CompletionItem GetCompletionItem()
            => CompletionItem.Create("Loop through all provided commandline arguments");

        protected override SyntaxNode GetSnippet()
            => ForEachStatement(
                PredefinedType(
                    Token(SyntaxKind.StringKeyword)),
                Identifier("argument"),
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("System"),
                            IdentifierName("Environment")),
                        IdentifierName("GetCommandLineArgs"))),
                Block());
    }
}
