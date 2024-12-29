using Ambev.DeveloperEvaluation.Cache;
using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Common.Settings;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.MessagingBroker;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {

        builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("Redis"));
        
        var redisConfig = builder.Configuration.GetSection("Redis").Get<RedisSettings>();
        
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfig?.ConnectionString;
            options.InstanceName = redisConfig?.InstanceName;
        });

        builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfig?.ConnectionString ?? string.Empty));
        
        builder.Services.AddScoped<ICacheService, RedisCacheService>();
        
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ISaleItemRepository, SaleItemRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        
        builder.Services.AddScoped<IEventPublisher, MediatorEventPublisher>();
        builder.Services.AddScoped<IMessageService, MessageService>();
    }
}