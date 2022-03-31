using System;

[Serializable]
public class ScannerException : Exception
{
    public ScannerException() : base() { }
    public ScannerException(string message) : base(message) { }
    public ScannerException(string message, Exception inner) : base(message, inner) { }
}
