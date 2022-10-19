using absolwent.Constants;
using absolwent.DAL;
using absolwent.Database;
using absolwent.Helpers;
using absolwent.Middlewares;
using absolwent.Models;
using absolwent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();
builder.Services
    .AddControllers(
        options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
            })
    .AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
        );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddDbContext<AbsolwentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Absolwent")), ServiceLifetime.Transient, ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<AbsolwentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Absolwent")));
builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();
builder.Services.AddTransient<IGraduateRepository, GraduateRepository>();
builder.Services.AddTransient<IDataRepository, DataRepository>();
builder.Services.AddTransient<IQuestionnaireRepository, QuestionnaireRepository>();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("public", new OpenApiInfo
    {
        Title = "Public",
        Version = "1.0"
    });
    option.SwaggerDoc("auth", new OpenApiInfo
    {
        Title = "Auth",
        Version = "1.0"
    });
    option.SwaggerDoc("admin", new OpenApiInfo
    {
        Title = "Admin",
        Version = "1.0"
    });
    option.SwaggerDoc("survey", new OpenApiInfo
    {
        Title = "Survey",
        Version = "1.0"
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Use bearer token to authorize",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    option.OperationFilter<AddAuthorizationHeaderOperationHeader>();
});

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IPoolService, PoolService>();
builder.Services.AddSingleton<Users>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true);
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseCors("AllowAll");
app.UseCors("corsapp");
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Test"))
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/public/swagger.json", "Public");
        option.SwaggerEndpoint("/swagger/auth/swagger.json", "Auth");
        option.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin");
        option.SwaggerEndpoint("/swagger/survey/swagger.json", "Survey");
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();
app.Run();
