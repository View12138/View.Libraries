using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using View.AutoTask;
using View.AutoTask.Options;

namespace View.AspNetCore.Builder
{
    public static class AutoTaskApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAutoTask(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<IOptions<TaskOptions>>();
            TaskInitializer.Start(options.Value);
            return builder;
        }
    }
}
