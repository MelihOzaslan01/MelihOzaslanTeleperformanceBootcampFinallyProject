using Microsoft.AspNetCore.Mvc.Testing;
using Shopping.API;

namespace Shopping.Tests.Controllers;

public class FakeApplication:WebApplicationFactory<Program>
{
    public FakeApplication()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
    }

}