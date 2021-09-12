using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.SmartSnippets
{
    internal static class CompletionContextExtensions
    {
        public static bool IsInConsoleApp(this CompletionContext context)
        {
            return context.Document.Project.CompilationOptions?.MainTypeName is not null ||
                   context.Document.Project.CompilationOptions?.OutputKind == OutputKind.ConsoleApplication;
        }

        public static async Task<bool> IsInMethodBodyAsync(this CompletionContext context, CancellationToken cancellationToken)
        {
            var currentSpan = context.CompletionListSpan;
            var root = await context.Document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            if (root is null)
            {
                return false;
            }

            var node = root.FindNode(currentSpan);
            var method = node.FirstAncestorOrSelf<MethodDeclarationSyntax>();
            if (method is not null)
            {
                return true;
            }

            return false;
        }
    }
}
