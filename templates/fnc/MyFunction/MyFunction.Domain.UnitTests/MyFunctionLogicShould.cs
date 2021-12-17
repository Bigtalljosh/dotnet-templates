using NUnit.Framework;
using System.Threading.Tasks;

namespace MyFunction.Domain.UnitTests;

public class MyFunctionLogicShould
{
    private IMyFunctionLogic? _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new MyFunctionLogic();
    }

    [Test]
    [TestCase("Josh", "Hello, Josh!")]
    [TestCase("NotJosh", "Hello, NotJosh!")]

    public async Task Return_HelloName(string name, string expectedResult)
    {
        var result = await _sut.DoSomething(name);
        Assert.AreEqual(result, expectedResult);
    }
}
