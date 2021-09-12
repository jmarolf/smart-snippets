﻿using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;


namespace Microsoft.CodeAnalysis.SmartSnippets
{
    [ExportCompletionProvider(nameof(ReadConsoleSnippet), LanguageNames.CSharp)]
    internal sealed class ReadConsoleSnippet : SnippetInsideMethod
    {
        protected override Task ProvideCompletionsCoreAsync(CompletionContext context)
        {
            if (context.IsInConsoleApp())
            {
                context.AddItem(CompletionItem.Create("Read from the console")
                       .WithRules(CompletionItemRules.Default.WithFormatOnCommit(true)));
            }

            return Task.CompletedTask;
        }

        protected override SyntaxNode GetSnippet()
        {
            return SyntaxFactory.ParseExpression("System.Console.ReadLine();");
            // TODO: determine if we are on the right hand side of an assignment
            // return SyntaxFactory.ParseExpression("string inputFromConsolex` = System.Console.ReadLine();");
        }
    }
}
