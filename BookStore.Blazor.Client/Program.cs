using Blazored.LocalStorage;
using BookStore.Blazor.Client;
using BookStore.Blazor.Client.Providers;
using BookStore.Blazor.Client.Services;
using BookStore.Blazor.Client.Services.Authentication;
using BookStore.Blazor.Client.Services.Base;
using BookStore.Blazor.Client.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;





var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7039") });

builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IClient, Client>();



builder.Services.AddTransient(typeof(IHttpContacts<>), typeof(HttpContracts<>));
builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpInerceptorService>();







//builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p =>
                                                        p.GetRequiredService<ApiAuthenticationStateProvider>());



builder.Services.AddMudServices();








await builder.Build().RunAsync();
