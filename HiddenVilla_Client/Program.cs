using Blazored.LocalStorage;
using Blazored.Toast;
using HiddenVilla_Client;
using HiddenVilla_Client.Services;
using HiddenVilla_Client.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string? Url = builder.Configuration.GetValue<string>("BaseAPIUrl");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7137/") });

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider,AuthStateProvider>();

builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IHotelAmenityService, HotelAmenityService>();
builder.Services.AddScoped<IRoomOrderDetailsService, RoomOrderDetailsService>();
builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
