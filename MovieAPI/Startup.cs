using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieAPI;
using MovieAPI.Helpers;
using MovieAPI.Models;
using MovieAPI.Services;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    // ConfigureServices: Servislerin ve middleware konfigürasyonlarının yapılandırıldığı yer
    public void ConfigureServices(IServiceCollection services)
    {
        // HttpClient tanımlaması ve MovieService için kayıt
        services.AddHttpClient<MovieService>();

        // Arka planda çalışacak servis kaydı
        services.AddHostedService<MovieUpdateService>();

        // JWT kimlik doğrulama ayarları
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var auth0Domain = $"https://{Configuration["Auth0:Domain"]}/";
                options.Authority = auth0Domain;
                options.Audience = Configuration["Auth0:Audience"];
            });

        services.AddControllers();

        // Swagger belgelendirme hizmeti
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Movie Recommendation API",
                Version = "v1"
            });
        });
        // Scoped olarak Email servisi kaydı ve DbContext ayarları
        services.AddScoped<IEmailService, SmtpEmailService>();
        services.AddDbContext<MovieDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Auth0 ayarlarını yükle
        var auth0Domain = Configuration["Auth0:Domain"];
        var auth0ClientId = Configuration["Auth0:ClientId"];

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{auth0Domain}/";
                options.Audience = auth0ClientId;
            });

        services.AddControllers();
    }
    // Configure: HTTP istek yönlendirmesi ve middleware'lerin eklenmesi
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Recommendation API v1");
            });
        }

        app.UseRouting();
        // Kimlik doğrulama ve yetkilendirme middleware'leri
        app.UseAuthentication();
        app.UseAuthorization();

        // Controller endpoint'leri tanımlaması
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
