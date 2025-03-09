using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans.IdentityStore;

namespace Orleans.Hosting
{
    /// <summary>
    /// Silo hosting extensions
    /// </summary>
    public static class SiloBuilderExtensions
    {
        /// <summary>
        /// Add identity store to orleans. Grain storage provider name can be found at <see
        /// cref="OrleansIdentityConstants.OrleansStorageProvider"/> ///
        /// </summary>
        /// <param name="builder">Silo builder</param>
        public static ISiloBuilder UseAzureTableOrleanIdentityStore(this ISiloBuilder builder, string storageName)
        {
            builder.ConfigureServices(s => s.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>());
            try
            {
                builder.AddMemoryGrainStorage(OrleansIdentityConstants.OrleansStorageProvider);
                builder.AddAzureTableGrainStorage(OrleansIdentityConstants.OrleansStorageProvider, options =>
                {
                    var connectionString = builder.Configuration.GetConnectionString(storageName);
                    options.TableServiceClient = new TableServiceClient(connectionString);
                });
            }
            catch
            {
                // Store was already added
            }

            return builder;
        }
        
        /// <summary>
        /// Add identity store to orleans. Grain storage provider name can be found at <see
        /// cref="OrleansIdentityConstants.OrleansStorageProvider"/> ///
        /// </summary>
        /// <param name="builder">Silo builder</param>
        public static ISiloBuilder UseAzureBlobOrleanIdentityStore(this ISiloBuilder builder, string storageName)
        {
            builder.ConfigureServices(s => s.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>());
            try
            {
                builder.AddMemoryGrainStorage(OrleansIdentityConstants.OrleansStorageProvider);
                builder.AddAzureBlobGrainStorage(OrleansIdentityConstants.OrleansStorageProvider, options =>
                {
                    var connectionString = builder.Configuration.GetConnectionString(storageName);
                    options.BlobServiceClient = new BlobServiceClient(connectionString);
                });
            }
            catch
            {
                // Store was already added
            }

            return builder;
        }


        /// <summary>
        /// Add identity store to orleans. Grain storage provider name can be found at <see
        /// cref="OrleansIdentityConstants.OrleansStorageProvider"/> ///
        /// </summary>
        /// <param name="builder">Silo builder</param>
        public static ISiloBuilder UseMemoryOrleanIdentityStore(this ISiloBuilder builder)
        {
            builder.ConfigureServices(s => s.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>());
            try
            {
                builder.AddMemoryGrainStorage(OrleansIdentityConstants.OrleansStorageProvider);
//                builder.Services.AddSerializer(sb => 
//                    sb.AddNewtonsoftJsonSerializer(
//                    sb.AddJsonSerializer(
//                        isSupported: type => type.Namespace.StartsWith("System.Security.Claims") || 
//                                     type.Namespace.StartsWith("Microsoft.AspNetCore.Identity")
//                    )
//                );
            }
            catch
            {
                // Store was already added
            }

            //JsonConvert.DefaultSettings = () =>
            //{
            //    return new JsonSerializerSettings()
            //    {
            //        Converters = new List<JsonConverter>() { new JsonClaimConverter(), new JsonClaimsPrincipalConverter(), new JsonClaimsIdentityConverter() }
            //    };
            //};

            return builder;
        }
    }
}