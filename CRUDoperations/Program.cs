using Common.FileHelper;
using Common.HttpClientHelpers;
using Common.Notification.Mail;
using Common.PasswordHash;
using Common.Resolver;
using Common.Validator;
using CRUDoperations;
using CRUDoperations.Filter;
using CRUDoperations.Repositories.Resolver;
using CRUDoperations.Services.Mapper;
using CRUDoperations.Services.Resolver;
using CRUDoperations.Services.Setting;
using Hangfire;
using Mapster;
using MapsterMapper;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPasswordHash, PasswordHash>();

CoreServicesResolver.ResolveCoreServices(builder.Services, builder.Configuration);
CoreServicesResolver.ResolveMapper(builder.Services);
CommonResolver.ResolveCommonServices(builder.Services, builder.Configuration);
UnitOfWorkResolver.ResolveUintOfWork(builder.Services, builder.Configuration);
UnitOfWorkResolver.ResolveLazier(builder.Services, builder.Configuration);
// Bind Unit Setting
MailSettings MailSetting = new();
builder.Configuration.Bind("MailSetting", MailSetting);
builder.Services.AddSingleton(MailSetting);

MailSetting emailSetting = new();
builder.Configuration.Bind("MailSetting", emailSetting);
builder.Services.AddSingleton(emailSetting);

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient(s =>
{
    IHttpContextAccessor contextAccessor = s.GetService<IHttpContextAccessor>();
    ClaimsPrincipal user = contextAccessor?.HttpContext?.User;
    return user;
});

builder.Services.AddControllers();
// add hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DBConString")));
builder.Services.AddHangfireServer();
//

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();//
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerHeaderFilter>();
});//



//builder.Services.AddMapster();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())//
{
    app.UseSwagger();//
    //app.UseSwaggerUI();//
    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        // this line to run swagger when publish api to IIS 
        string SwaggerUrl = builder.Configuration.GetValue<string>("SwaggerUrl");
        // Replace {...} with microservice name
        c.SwaggerEndpoint($"{SwaggerUrl}/swagger/v1/swagger.json", "CRUD Opertions Api V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();//

app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.UseAuthorization();//

#region Localization
List<CultureInfo> cultures = new()
{
    new CultureInfo("en"),
    new CultureInfo("ar")
};
app.UseRequestLocalization(option =>
{
    option.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
    option.SupportedCultures = cultures;
    option.SupportedUICultures = cultures;
});
#endregion

app.MapControllers();

app.UseHangfireDashboard();

app.Run();