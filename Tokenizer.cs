using System;
using System.Collections;
using System.Linq;

class Tokenizer : ITokenizer
{
    public IEnumerable<Token> GenerateTokens(IEnumerable<char> charStream)
        => charStream
            .SkipWhile(c => Char.IsWhiteSpace(c))
            .Select(c => Char.ToLower(c))
            .Select(c => c switch {
                's' => Token.S,
                'k' => Token.K,
                'i' => Token.I,
                '(' => Token.OpenParanthesis,
                ')' => Token.ClosedParanthesis,
                _ => throw new TokenizerException($"Invalid character used ${c}")
                });
}
