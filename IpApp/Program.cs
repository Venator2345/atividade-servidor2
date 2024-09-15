using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configurar o pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", (HttpContext context) =>
{
    var ip = context.Connection.RemoteIpAddress;
    var ipAddress = ip?.ToString() ?? "Desconhecido";
    var html = $@"
        <html>
        <head><title>Seu IP</title></head>
        <body>
            <h1>Seu IP: {ipAddress}</h1>
        </body>
        </html>";

    context.Response.ContentType = "text/html";
    return context.Response.WriteAsync(html);
});

app.Run();
