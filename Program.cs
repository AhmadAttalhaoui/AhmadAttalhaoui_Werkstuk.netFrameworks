using API3.Areas.Identity.Data;
using API3.Data;
using API3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("API3ContextConnection");
builder.Services.AddDbContext<API3Context>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDbContext<API3Context>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<API3User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<API3Context>();

builder.Services.AddControllersWithViews();

    builder.Services.AddSingleton<LanguagService>();
    builder.Services.AddCoreAdmin("Admin");

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

    builder.Services.AddMvc()
        .AddViewLocalization()
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            {

                var assemblyName = new AssemblyName(typeof(ShareResource).GetTypeInfo().Assembly.FullName);

                return factory.Create("ShareResource", assemblyName.Name);

            };


        });





    builder.Services.Configure<RequestLocalizationOptions>(
        options =>
        {
            var supportedCultures = new List<CultureInfo>
                {
                            new CultureInfo("en-US"),
                            new CultureInfo("nl-NL"),
                            new CultureInfo("fr-FR"),
                };



            options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());

        });

var app = builder.Build();

  /*          using var userManager = app.ServiceProvider.GetProvider.GetRequiredService<UserManager<>>()){

};*/
var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(locOptions.Value);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Films}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
