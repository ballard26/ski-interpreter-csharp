using System;

[Serializable]
public class TokenizerException : Exception
{
    public TokenizerException() : base() { }
    public TokenizerException(string message) : base(message) { }
    public TokenizerException(string message, Exception inner) : base(message, inner) { }
}
