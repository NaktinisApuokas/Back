using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using FobumCinema.Core.Entities;
using FobumCinema.Infrastructure.Repositories;
using FobumCinema.Infrastructure.Data;
using FobumCinema.API.Auth;
using FobumCinema.API.Auth.Model;
using FobumCinema.Core.Interfaces;


namespace FobumCinema.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FobumCinemaContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters.ValidAudience = _configuration["JWT:ValidAudience"];
                    options.TokenValidationParameters.ValidIssuer = _configuration["JWT:ValidIssuer"];
                    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.SameUser, policy => policy.Requirements.Add(new SameUserRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, AuthHandler>();

            services.AddCors(options =>
            {
                options.AddPolicy("corspolicy",
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                    });
            });
            services.AddDbContext<FobumCinemaContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddTransient<ICinemaRepository, CinemaRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IGeneralMovieRepository, GeneralMovieRepository>();
            services.AddTransient<IScreeningRepository, ScreeningRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentRatingRepository, CommentRatingRepository>();
            services.AddTransient<IMovieMarkRepository, MovieMarkRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<DatabaseSeeder, DatabaseSeeder>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseCors("corspolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
