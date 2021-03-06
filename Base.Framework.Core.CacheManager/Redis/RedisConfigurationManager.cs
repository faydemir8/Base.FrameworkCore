﻿using System;
using Base.Framework.Core.CacheManager.Objects.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Framework.Core.CacheManager.Redis
{
    public static class RedisConfigurationManager
    {
        public static IRedisDataAgent RedisDataAgent;
        public static bool IsConnected;
        public static void RedisConfiguration(this IServiceCollection services, IConfiguration configuration, string environmentName)
        {
            services.AddOptions();
            
            var redisOptions = new RedisOption();
            configuration.GetSection("RedisOption").Bind(redisOptions);
            if (!string.IsNullOrEmpty(redisOptions.ConnectionString))
            {
                services.AddDistributedRedisCache(option =>
                {
                    option.Configuration = $"{redisOptions.ConnectionString},defaultDatabase={redisOptions.DefaultDb}";
                    option.InstanceName = $"{environmentName}-{redisOptions.InstanceName}-";
                });
                services.AddScoped<IRedisDataAgent, RedisDataAgent>();
                RedisDataAgent = services.BuildServiceProvider().GetService<IRedisDataAgent>();
                IsConnected = true;
            }
            RedisDataAgent = null;
            IsConnected = false;
        }
    }
}
