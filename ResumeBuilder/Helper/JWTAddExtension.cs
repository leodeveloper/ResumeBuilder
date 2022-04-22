using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeBuilder.Helper
{
    /// <summary>
    /// Extension class for JWT
    /// </summary>
    public static class JWTAddExtension
    {
        /// <summary>
        /// Create JWT Token Validation Mehanism
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environment"></param>
        /// <param name="builder"></param>
        public static void AddJwtAuthentication(this IServiceCollection services,
            IWebHostEnvironment environment,
            IConfiguration Configuration, bool IsHttpsMetaDataRequired)
        {
            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })// Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = IsHttpsMetaDataRequired;
                options.TokenValidationParameters = tokenValidationParameters(Configuration);
            });
        }

        public static TokenValidationParameters tokenValidationParameters(IConfiguration Configuration)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidAudience = Configuration["JWT:ValidAudience"],
                ValidIssuer = Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
            };
        }
    }
}
