using Blazored.LocalStorage;
using Blazored.Toast;
using HiddenVilla_Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl") });

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
