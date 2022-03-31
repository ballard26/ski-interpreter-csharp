using System;

class Transformations {
    public static Expression RemoveEOFExpressions(Expression e) => e switch {
        ApplicationExpression(EOFExpression, var lhs) => RemoveEOFExpressions(lhs),
        ApplicationExpression(var rhs, EOFExpression) => RemoveEOFExpressions(rhs),
        ApplicationExpression(var rhs, var lhs) => new ApplicationExpression(RemoveEOFExpressions(rhs), RemoveEOFExpressions(lhs)),
        _ => e
    };

    private static Func<Expression, Expression> I = e => ReduceExpression(e);
    private static Func<Expression, Expression, Expression> K = (e1, e2) => ReduceExpression(e1);
    private static Func<Expression, Expression, Expression, Expression> S
        =  (e1, e2, e3) => ReduceExpression(
                new ApplicationExpression(
                    new ApplicationExpression(e1, e3), 
                    new ApplicationExpression(e2, e3)));

    public static Expression ReduceExpression(Expression e) => e switch {
        ApplicationExpression(AtomicExpression(Combinator.I), var lhs) 
            => I(lhs),
        ApplicationExpression(ApplicationExpression(AtomicExpression(Combinator.K), var lhs1), var lhs2) 
            => K(lhs1, lhs2),
        ApplicationExpression(ApplicationExpression(ApplicationExpression(AtomicExpression(Combinator.S), var lhs1), var lhs2), var lhs3) 
            => S(lhs1, lhs2, lhs3),
        ApplicationExpression(var rhs, var lhs) => new ApplicationExpression(ReduceExpression(rhs), ReduceExpression(lhs)),
        _ => e
    };

    public static Expression EvaluateExpression(Expression e, int loopLimit) {
        Expression lastOutput = null;
        var currentOutput = e;

        while(lastOutput != currentOutput && loopLimit != 0) {
            lastOutput = currentOutput;
            currentOutput = ReduceExpression(lastOutput);
            loopLimit = loopLimit - 1;
        }

        return currentOutput;
    }
}
