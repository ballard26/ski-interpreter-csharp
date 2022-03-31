using System;

public abstract record Expression();
public sealed record AtomicExpression(Combinator combinator) : Expression();
public sealed record ApplicationExpression(Expression rhs, Expression lhs) : Expression();
public sealed record EOFExpression() : Expression();
