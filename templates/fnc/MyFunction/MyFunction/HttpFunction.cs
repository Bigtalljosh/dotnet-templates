using MyFunction.Domain;

namespace MyFunction;

public class HttpFunction
{
    private readonly IMyFunctionLogic _logic;

    public HttpFunction(IMyFunctionLogic logic)
    {
        _logic = logic;
    }

    [FunctionName(nameof(Run))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string name = req.Query["name"];

        string responseMessage = await _logic.DoSomething(name);

        return new OkObjectResult(responseMessage);
    }
}
