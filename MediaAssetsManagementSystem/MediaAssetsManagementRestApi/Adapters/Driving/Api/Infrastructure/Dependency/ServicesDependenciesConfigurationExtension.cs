using Api.Infrastructure.ExceptionHandlers;
using ApplicationServices.Ports.MediaAssetsPersistence;
using ApplicationServices.Ports.Persistence;
using ApplicationServices.Requests.Queries.Folders.ReadFolder;
using Domain.Aggregates.Folders;
using Domain.Aggregates.ImageAssets;
using MediaAssetsPersistence;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Repositories;
using System.Reflection;

namespace Api.Infrastructure.Dependency
{
    public static class ServicesDependenciesConfigurationExtension
    {
        public static void ConfigureMediaAssetsManagementServices(this IServiceCollection services)
        {
            ConfigureCustomDependencies(services);
            ConfigureMediatR(services);
        }

        private static void ConfigureMediatR(IServiceCollection services)
        {
            services.AddScoped<MediaAssetManagementSystemDbContextFactory>();
            services.AddDbContext<MediaAssetManagementSystemDbContext>();
            services.AddScoped<IMediaAssetsBlobContainerFactory, MediaAssetsBlobContainerFactory>();

            services.AddLogging();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<FoldersRepository>), typeof(Logger<FoldersRepository>));
            services.AddSingleton(typeof(ILogger<ImageAssetsRepository>), typeof(Logger<ImageAssetsRepository>));

            services.AddScoped<IFoldersRepository, FoldersRepository>();
            services.AddScoped<IFoldersQueriesRepository, FoldersRepository>();
            services.AddScoped<IImageAssetsRepository, ImageAssetsRepository>();
            services.AddScoped<IImageAssetsQueriesRepository, ImageAssetsRepository>();
        }

        private static void ConfigureCustomDependencies(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(ReadFolderQuery)));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlerMediatorPipelineBehavior<,>));
        }
    }
}
