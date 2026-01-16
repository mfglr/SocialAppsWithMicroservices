using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BlobService.Api
{
    internal class IdentityOptions
    {
        public required string Issuer { get; set; }
        public required string BaseUrl { get; set; }
        public required string Audience { get; set; }
    }

    internal static class ServiceRegistration
    {
        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>()!;
            services
                .AddAuthentication()
                .AddJwtBearer(
                    JwtBearerDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.Authority = option.BaseUrl;
                        options.Audience = option.Audience;
                        options.RequireHttpsMetadata = false;

                        options.TokenValidationParameters = new()
                        {
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,
                            ValidateIssuer = true,
                        };
                    }
                );

            services
                .AddAuthorization(
                    options =>
                    {
                        options
                           .AddPolicy(
                               "client",
                               p =>
                               {
                                   p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                                   p.RequireAuthenticatedUser();
                               }
                           );
                    }
                );
            return services;
        }
    }
}
