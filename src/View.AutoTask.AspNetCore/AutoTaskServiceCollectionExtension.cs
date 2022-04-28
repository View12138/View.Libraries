using Microsoft.Extensions.DependencyInjection;
using View.AutoTask.Options;

namespace View.Extensions.DependencyInjection
{
    public static class AutoTaskServiceCollectionExtension
    {
        public static IServiceCollection AddAutoTask(this IServiceCollection services, TaskOptions taskOptions = null)
        {
            services.AddOptions<TaskOptions>()
               .Configure(options =>
               {
                   // Specify default option values
                   taskOptions ??= new TaskOptions();
                   options.Cache = taskOptions.Cache;
                   options.CacheType = taskOptions.CacheType;
                   options.TaskScope = taskOptions.TaskScope;
               });
            return services;
        }
    }
}