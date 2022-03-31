using System;
using System.Collections;
using System.Linq;

class Scanner : IScanner {
    public Expression GenerateExpressionTree(IEnumerable<Token> tokens) => InternalGenerateExpressionTree(tokens.Reverse()).Item1;

    private (Expression, IEnumerable<Token>) InternalGenerateExpressionTree(IEnumerable<Token> tokens) {
        try {
            var currentToken = tokens.First();

            var (rhs, r1) = currentToken switch {
                Token.S => (new AtomicExpression(Combinator.S), tokens.Skip(1)),
                Token.K => (new AtomicExpression(Combinator.K), tokens.Skip(1)),
                Token.I => (new AtomicExpression(Combinator.I), tokens.Skip(1)),
                Token.ClosedParanthesis => OpenParanthesisState(tokens.Skip(1))
            };
            
            var (lhs, r2) = InternalGenerateExpressionTree(r1);

            return (new ApplicationExpression(lhs, rhs), r2);

        } catch (InvalidOperationException) {
            return (new EOFExpression(), tokens);
        }
    }

    private (Expression, IEnumerable<Token>) OpenParanthesisState(IEnumerable<Token> tokens) {
        var (rhs, r1) = InternalGenerateExpressionTree(tokens);
        var (lhs, r2) = InternalGenerateExpressionTree(r1);

        if (r2.FirstOrDefault(Token.ClosedParanthesis) != Token.OpenParanthesis) {
            throw new ScannerException($"Unexpected token; expected ')' got ${r2.First()}");
        }

        return (new ApplicationExpression(lhs, rhs), r2.Skip(1));
    }
}
