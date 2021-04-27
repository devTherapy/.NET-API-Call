using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
namespace AuthorQuerier.UI
{
    /// <summary>
    /// Adds nlog to the service collection and makes it available to the scope of the app function ui.
    /// </summary>
    public class Init
    {
        public Init(IServiceCollection services)
        {
            services.AddLogging(LoggingBuilder =>
            {
                LoggingBuilder.AddNLog("nlog.config");
            });
            services.AddScoped<AppFunctionUi>();
        }
    }
}
