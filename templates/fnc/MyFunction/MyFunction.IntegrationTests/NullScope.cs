using System;

namespace MyFunction.IntegrationTests;

public class NullScope : IDisposable
{
    public static NullScope Instance { get; } = new NullScope();

    private NullScope() { }

    public void Dispose() { }
}

