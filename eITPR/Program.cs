using eITPR.Services;
using eITPR.Components;
using eITPR.Data;
using eITPR.Models;
using BlazorDemo.AspNetCoreHost;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDevExpressBlazor(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<UploadController>();
builder.Services.AddMvc();

//add sql server access
builder.Services.AddSingleton<DapperRepository<Project>>(s =>new DapperRepository<Project>(builder.Configuration.GetConnectionString("localhost")));
builder.Services.AddSingleton<DapperRepository<Drawdown>>(s =>new DapperRepository<Drawdown>(builder.Configuration.GetConnectionString("localhost")));
builder.Services.AddSingleton<DapperRepository<PurchaseOrder>>(s => new DapperRepository<PurchaseOrder>(builder.Configuration.GetConnectionString("localhost")));
builder.Services.AddSingleton<DapperRepository<ItprMain>>(s => new DapperRepository<ItprMain>(builder.Configuration.GetConnectionString("localhost")));
builder.Services.AddSingleton<DapperRepository<ItprMainView>>(s => new DapperRepository<ItprMainView>(builder.Configuration.GetConnectionString("localhost")));
builder.Services.AddSingleton<DapperRepository<vendorMain>>(s => new DapperRepository<vendorMain>(builder.Configuration.GetConnectionString("localhost")));
builder.Services.AddSingleton<DapperRepository<VendorMain>>(s => new DapperRepository<VendorMain>(builder.Configuration.GetConnectionString("localhost")));
builder.Services.AddSingleton<DapperRepository<Personnel>>(s => new DapperRepository<Personnel>(builder.Configuration.GetConnectionString("localhost")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();