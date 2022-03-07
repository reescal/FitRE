using FitRE;
using FitRE.Infrastructure;
using FitRE.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(nameof(Budget), x => x.BaseAddress = new Uri(builder.Configuration[$"{nameof(Budget)}APIPrefix"]));

builder.Services.AddScoped<IBudgetService, BudgetService>();

await builder.Build().RunAsync();
