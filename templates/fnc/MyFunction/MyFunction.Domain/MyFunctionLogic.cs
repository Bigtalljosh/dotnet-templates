namespace MyFunction.Domain;

public class MyFunctionLogic : IMyFunctionLogic
{
    public async Task<string> DoSomething(string name) => $"Hello, {name}!";
}
