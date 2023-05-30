using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Twilio.Jwt.Taskrouter;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Helpers;
using VNVCWEBAPI.Middlewares;
using VNVCWEBAPI.REPO;
using VNVCWEBAPI.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("VNVCContext")));
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region "Register Config"
//Regiser Config
JWTConfig.JWTConfigurationSettings(builder.Configuration);
PagingConfig.PagingConfigurationSettings(builder.Configuration);
UploadConfig.UploadConfigurationSettings(builder.Configuration);
LoginConfig.LoginConfigurationSettings(builder.Configuration);
MailConfig.MailConfigurationSettings(builder.Configuration);
TwilioConfig.TwillioConfigurationSettings(builder.Configuration);
ReminderScheduleConfig.ReminderScheduleConfigurationSettings(builder.Configuration);
FirebaseSettings.FirebaseConfigurationSettings(builder.Configuration);
#endregion

builder.Services.AddDbContext<VNVCContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("VNVCContext"));
});


IServicesCollectionExtensions.AddServicesRepositories(builder.Services);
//IServicesCollectionExtensions.AddServicesRoles(builder.Services);

//JWT Config
var secretBytes = Encoding.UTF8.GetBytes(JWTConfig.SecretKey);
var tokenParameter = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,

    ValidateLifetime = true,

    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
    ClockSkew = TimeSpan.Zero
};
builder.Services.AddSingleton(tokenParameter);

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = tokenParameter;
    opt.Events = new JwtBearerEvents();
    opt.Events.OnTokenValidated = async (context) =>
    {
        var jwtServices = context.HttpContext.RequestServices.GetService<IJWTServices>();
        var jwtToken = context.SecurityToken as JwtSecurityToken;
        if (!(await jwtServices.isTokenLive(jwtToken.RawData)))
        {
            context.HttpContext.Response.Headers.Remove("Authorization");
            context.Fail("Invalid Token");
        }

    };
});
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Administrator", policy =>
    {
        policy.RequireRole(DefaultRoles.SuperAdmin, DefaultRoles.Admin);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHangfireDashboard("/jobs", new DashboardOptions()
{
    Authorization = new[] { new HangFireAuthorizationFilter() },
});
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Auto create folder Images in wwwroot
if (!Directory.Exists(Path.Combine(app.Environment.ContentRootPath + "//wwwroot//", UploadConfig.Images)))
{
    Directory.CreateDirectory(Path.Combine(app.Environment.ContentRootPath + "//wwwroot//", UploadConfig.Images));
}


app.UseStaticFiles();
app.UseMiddleware<GlobalExceptonHandlingMiddlewares>();



app.Run();
