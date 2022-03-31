using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace RequestDelivery
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<DelivaryRequestsViewModel>();
            services.AddTransient<ChangeDeliveryRequestViewModel>();
            services.AddTransient<DeliveryRequest>();
            services.AddTransient<EventAggregator>();
            services.AddTransient<IEventAggregator, EventAggregator>();

            services.AddSingleton<RequestChangedEvent>();

            services.AddEntityFrameworkSqlite().AddDbContext<ApplicationDbContext>();

            using (var client = new ApplicationDbContext())
            {
                client.Database.EnsureCreated();
                client.Database.Migrate();
            }

            _provider = services.BuildServiceProvider();

        }

        public DelivaryRequestsViewModel MainViewModel => _provider.GetRequiredService<DelivaryRequestsViewModel>();
        public ChangeDeliveryRequestViewModel ChangeViewModel => _provider.GetRequiredService<ChangeDeliveryRequestViewModel>();
    }
}
