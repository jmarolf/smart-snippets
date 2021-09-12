using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;


namespace Microsoft.CodeAnalysis.SmartSnippets
{
    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class WriteConsoleSnippet : SnippetInsideMethod
    {
        protected override Task ProvideCompletionsCoreAsync(CompletionContext context)
        {
            if (context.IsInConsoleApp())
            {
                context.AddItem(CompletionItem.Create("Write to the console")
                       .WithRules(CompletionItemRules.Default.WithFormatOnCommit(true)));
            }

            return Task.CompletedTask;
        }

        protected override SyntaxNode GetSnippet()
        {
            return SyntaxFactory.ParseExpression("System.Console.WriteLine();");
            // TODO: grab data in scope and populate the Console.WriteLine call
        }
    }
}
