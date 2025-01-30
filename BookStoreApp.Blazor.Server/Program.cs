
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.Configurations;
using BookStoreApp.Blazor.Server.Providers;
using BookStoreApp.Blazor.Server.Services;
using BookStoreApp.Blazor.Server.Services.Authentication;
using BookStoreApp.Blazor.Server.Services.Base;
using BookStoreApp.Blazor.Server.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7039"));




builder.Services.AddTransient(typeof(IHttpContacts<>), typeof(HttpContracts<>));
builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpInerceptorService>();







builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped<Client, Client>(); // Register IClient
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p =>
                                                        p.GetRequiredService<ApiAuthenticationStateProvider>());



builder.Services.AddMudServices();

var app = builder.Build();
//
// Add services to the container.
//builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();
//builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7024"));
//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
//builder.Services.AddScoped<ApiAuthenticationStateProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(p =>
//                p.GetRequiredService<ApiAuthenticationStateProvider>());
//var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
