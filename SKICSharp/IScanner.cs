using System;
using System.Collections;

interface IScanner {
    Expression GenerateExpressionTree(IEnumerable<Token> tokens);
}
