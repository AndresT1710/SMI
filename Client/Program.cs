using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SMI.Client;
using SMI.Client.Services;
using SMI.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using SMI.Shared.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Agregar servicios de autenticación
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

// Primero registrar el AuthStateProvider
builder.Services.AddScoped<AuthStateProvider>();

// Luego registrar AuthenticationStateProvider para que apunte a AuthStateProvider
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthStateProvider>());

// Ahora, registrar AuthService como el servicio que maneja la autenticación
builder.Services.AddScoped<IAuthService, AuthService>();

//servicio de la persona
builder.Services.AddScoped<PersonaService>();

//servicio de la profesion
builder.Services.AddScoped<ProfesionService>();

//servicio de tipo documento
builder.Services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();

await builder.Build().RunAsync();
