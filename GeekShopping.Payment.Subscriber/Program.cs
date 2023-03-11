using GeekShopping.Payment.Subscriber.Config;
using GeekShopping.Payment.Subscriber.Models.Context;
using GeekShopping.Payment.Subscriber.Repository;
using GeekShopping.Payment.Subscriber.Services;
using GeekShopping.Payment.Subscriber.Subscribers;
using GeekShopping.Shared.Interfaces;
using GeekShopping.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<MySqlContext>(options =>
        {
            var connectionString = hostContext.Configuration.GetConnectionString("MySql");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        var mapper = MappingConfig.RegisterMappings().CreateMapper();

        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        services.AddSingleton<IPaymentGatewayService, StripeService>();
        services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        
        services.AddHostedService<ProcessPaymentSubscriber>();
        services.AddHostedService<UpdatePaymentSubscriber>();
    })
    .Build();

await host.RunAsync();
