class Program {
    static string PrintExpression(Expression e) => e switch {
        ApplicationExpression(var rhs, var lhs) => $"({PrintExpression(rhs)} {PrintExpression(lhs)})",
        AtomicExpression(Combinator.S) => "S",
        AtomicExpression(Combinator.K) => "K",
        AtomicExpression(Combinator.I) => "I",
        EOFExpression => ""
    };

    static void Main(string[] args) {
        ITokenizer tokenizer = new Tokenizer();
        IScanner scanner = new Scanner();

        var testCases = new (string, string)[] {
            ("ik", "k"),
            ("kis", "i"),
            ("k(ik)s", "k"),
            ("SKI(KIS)", "I"),
            ("SKiK", "K"),
            ("S(K(SI))KIK", "KI")
        };

        foreach (var (input, result) in testCases) {
            var resultExpression = Transformations.RemoveEOFExpressions(scanner.GenerateExpressionTree(tokenizer.GenerateTokens(result)));
            var inputExpression = Transformations.RemoveEOFExpressions(scanner.GenerateExpressionTree(tokenizer.GenerateTokens(input)));
            var evaluatedExpression = Transformations.EvaluateExpression(inputExpression, 100);
            
            if (evaluatedExpression != resultExpression) {
                Console.WriteLine(
                        $"(FAIL) The evaluated expression '{PrintExpression(evaluatedExpression)}' didn't match the expected expression '{PrintExpression(resultExpression)}'");
            } else {
                Console.WriteLine(
                        $"(SUCC) Evaluated '{PrintExpression(evaluatedExpression)}' from input '{PrintExpression(inputExpression)}'");
            }
        }
    }
}
