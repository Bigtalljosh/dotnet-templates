using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFunction.Domain;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MyFunction.IntegrationTests;

public class HttpFunctionShould
{
    private IMyFunctionLogic? _functionLogic;
    private readonly ILogger logger = TestFactory.CreateLogger();

    [SetUp]
    public void Setup()
    {
        _functionLogic = new MyFunctionLogic();
    }

    [Test]
    public async Task Run_ShouldReturnHelloName()
    {
        var function = new HttpFunction(_functionLogic);
        var request = TestFactory.CreateHttpRequest("name", "Josh");

        var response = (OkObjectResult)await function.Run(request, logger);

        Assert.IsNotNull(response);
        Assert.AreEqual("Hello, Josh!", response.Value);
    }
}
