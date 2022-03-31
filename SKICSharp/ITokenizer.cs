using System;
using System.Collections;

interface ITokenizer 
{
    IEnumerable<Token> GenerateTokens(IEnumerable<char> charStream);
}
