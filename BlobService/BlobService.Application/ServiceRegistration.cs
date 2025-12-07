using BlobService.Application.UseCases.CreateContainer;
using BlobService.Application.UseCases.DeleteBlob;
using BlobService.Application.UseCases.GetBlob;
using BlobService.Application.UseCases.UploadBlob;
using BlobService.Application.UseCases.UploadSingleBlob;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BlobService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBlobApplicationServices(this IServiceCollection services) => 
            services
                .AddMediator(cfg => {
                    cfg.AddConsumer<UploadBlobConsumer>();
                    cfg.AddConsumer<UploadSingleBlobConsumer>();
                    cfg.AddConsumer<DeleteBlobConsumer>();
                    cfg.AddConsumer<GetBlobConsumer>();
                    cfg.AddConsumer<CreateContainerConsumer>();
                });
    }
}
