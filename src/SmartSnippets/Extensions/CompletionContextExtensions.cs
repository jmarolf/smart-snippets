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

        public static async Task<bool> IsInAsyncMethodAsync(this CompletionContext context, CancellationToken cancellationToken)
        {
            var method = await TryGetMethodAsync(context, cancellationToken).ConfigureAwait(false);
            if (method is null)
            {
                return false;
            }

            var model = await context.Document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
            if (model is null)
            {
                return false;
            }

            var symbolInfo = model.GetSymbolInfo(method, cancellationToken);
            return symbolInfo.Symbol is IMethodSymbol methodSymbol && methodSymbol.IsAsync;
        }

        public static async Task<bool> IsInMethodBodyAsync(this CompletionContext context, CancellationToken cancellationToken)
        {
            return (await TryGetMethodAsync(context, cancellationToken).ConfigureAwait(false)) is not null;
        }

        private static async Task<MethodDeclarationSyntax?> TryGetMethodAsync(CompletionContext context, CancellationToken cancellationToken)
        {
            var currentSpan = context.CompletionListSpan;
            var root = await context.Document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            if (root is null)
            {
                return null;
            }

            var node = root.FindNode(currentSpan);
            var method = node.FirstAncestorOrSelf<MethodDeclarationSyntax>();
            if (method is not null)
            {
                return method;
            }

            return null;
        }
    }
}
