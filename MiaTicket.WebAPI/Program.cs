using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Mapper;
using MiaTicket.DataAccess;
using MiaTicket.DataCache;
using MiaTicket.Email;
using MiaTicket.Setting;
using MiaTicket.VNPay;
using MiaTicket.CloudinaryStorage;
using MiaTicket.VNPay.Config;
using MiaTicket.WebAPI.Middleware;
using MiaTicket.WebAPI.Policy;
using MiaTicket.ZaloPay;
using MiaTicket.ZaloPay.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using StackExchange.Redis;
using Hangfire;
using MiaTicket.Schedular.Service;

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
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    string connection = setting.GetRedisConnectionString();
    redisOptions.Configuration = connection;
});
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConnection = setting.GetRedisConnectionString();

    var config = new ConfigurationOptions
    {
        AbortOnConnectFail = false
    };
    config.EndPoints.Add(redisConnection);

    return ConnectionMultiplexer.Connect(config);
});

builder.Services.AddSingleton<IConnection>(sp =>
{
    var rabbitMQConnection = setting.GetRabbitMQConnectionString();
    var rabbitMQUserName = setting.GetRabbitMQUserName();
    var rabbitMQPassword = setting.GetRabbitMQPassword();
    var factory = new ConnectionFactory
    {
        HostName = rabbitMQConnection,
        UserName = rabbitMQUserName,
        Password = rabbitMQPassword,
        VirtualHost = "/",
    };
    return factory.CreateConnection();
});

builder.Services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                                                .UseSimpleAssemblyNameTypeSerializer()
                                                .UseRecommendedSerializerSettings()
                                                .UseSqlServerStorage(setting.GetHangFireConnectionString()));
builder.Services.AddHangfireServer();

builder.Services.Configure<VNPayConfig>(builder.Configuration.GetSection(VNPayConfig.ConfigName));
builder.Services.Configure<ZaloPayConfig>(builder.Configuration.GetSection(ZaloPayConfig.ConfigName));
builder.Services.AddTransient<IDataAccessFacade, DataAccessFacade>();
builder.Services.AddTransient<IAccountBusiness, AccountBusiness>();
builder.Services.AddTransient<ITokenBusiness, TokenBusiness>();
builder.Services.AddTransient<IVerificationCodeBusiness, VerificationCodeBusiness>();
builder.Services.AddTransient<IEventBusiness, EventBusiness>();
builder.Services.AddTransient<ICategoryBusiness, CategoryBusiness>();
builder.Services.AddTransient<IBannerBusiness, BannerBusiness>();
builder.Services.AddTransient<IVnAddressBusiness, VnAddressBusiness>();
builder.Services.AddTransient<IPaymentBusiness, PaymentBusiness>();
builder.Services.AddTransient<IOrderBusiness, OrderBusiness>();
builder.Services.AddTransient<IVoucherBusiness, VoucherBusiness>();
builder.Services.AddTransient<IOrganizerBusiness, OrganizerBusiness>();
builder.Services.AddTransient<ISummaryBusiness, SummaryBusiness>();
builder.Services.AddTransient<IVNPayService, VNPayService>();
builder.Services.AddTransient<IZaloPayService, ZaloPayService>();
builder.Services.AddTransient<IOrderCancellationService, OrderCancellationService>();
builder.Services.AddSingleton<IAuthorizationHandler, UserAuthorizeHandler>();
builder.Services.AddSingleton(setting);
builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();
builder.Services.AddSingleton<IEmailProducer, EmailProducer>();
builder.Services.AddSingleton<IEmailConsumer, EmailConsumer>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSingleton<IRedisCacheService, RedisCacherService>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHostedService<EmailBackgroundHandler>();

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

app.UseHangfireDashboard();
app.MapHangfireDashboard("/hangfire");

app.Run();
