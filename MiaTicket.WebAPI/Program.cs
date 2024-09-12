using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Mapper;
using MiaTicket.DataAccess;
using MiaTicket.Setting;
using MiaTicket.WebAPI.Middleware;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var setting = EnviromentSetting.GetInstance();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
});

builder.Services.AddTransient<IDataAccessFacade, DataAccessFacade>();
builder.Services.AddTransient<IAccountBusiness, AccountBusiness>();
builder.Services.AddTransient<ITokenBusiness, TokenBusiness>();
builder.Services.AddTransient<ICloudinaryBusiness, CloudinaryBusiness>();
builder.Services.AddTransient<IVerifyCodeBusiness, VerifyCodeBusiness>();
builder.Services.AddTransient<IEventBusiness, EventBusiness>();
builder.Services.AddTransient<ICategoryBusiness, CategoryBusiness>();
builder.Services.AddTransient<IBannerBusiness, BannerBusiness>();
builder.Services.AddTransient<IVnAddressBusiness, VnAddressBusiness>();
builder.Services.AddSingleton<IAuthorizationHandler, UserAuthorizeHandler>();
builder.Services.AddSingleton(setting);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddMemoryCache();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Angular UI", x =>
    {
    x.AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials();
    });

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = setting.GetIssuer(),
            ValidAudience = setting.GetAudience(),
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(setting.GetSecret())),
            ClockSkew = TimeSpan.Zero
        };
        opt.MapInboundClaims = false;
    });

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Angular UI");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
