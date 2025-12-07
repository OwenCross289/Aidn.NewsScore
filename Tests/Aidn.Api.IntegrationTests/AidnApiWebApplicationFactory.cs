using Aidn.Api.IntegrationTests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

[assembly: AssemblyFixture(typeof(AidnApiWebApplicationFactory))]

namespace Aidn.Api.IntegrationTests;

public class AidnApiWebApplicationFactory : WebApplicationFactory<IApiMarker> { }
