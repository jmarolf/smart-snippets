
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.SmartSnippets
{
    using static SyntaxFactory;

    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class ReadConsoleSnippet : ConsoleSnippetBase
    {
        protected override CompletionItem GetCompletionItem()
            => CompletionItem.Create("Read from the console");

        protected override SyntaxNode GetSnippet()
        {
            return InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("System"),
                        IdentifierName("Console")),
                     IdentifierName("ReadLine")));
            // TODO: determine if we are on the right hand side of an assignment
            // return SyntaxFactory.ParseExpression("string inputFromConsolex` = System.Console.ReadLine();");
        }
    }
}
