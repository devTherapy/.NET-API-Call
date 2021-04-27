using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorQuerier.UI
{
    class Program
    {
       public  static async Task Main(string[] args)
        {
            //declares the Service collection. 
            IServiceCollection services = new ServiceCollection();
            ///Initializes the application with the service collection.
            Init Start = new Init(services);
            /// declares  the service builder 
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ///gets the service from the service collection and makes it available to the appUi.
            await serviceProvider.GetService<AppFunctionUi>().HomePage();
        }
    }
}
