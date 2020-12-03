namespace FileForgeDP
{
    using FileForgeDP.Database;
    using FileForgeDP.Database.Models;
    using FileForgeDP.Database.Repositories;
    using FileForgeDP.Facades;
    using FileForgeDP.Loggers;
    using FileForgeDP.Mappers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="Startup" />.
    /// </summary>
    public class Startup
    {
        private readonly string mCorsPolicy = "Konrad to powazny programista";
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
		
        /// <summary>
        /// Gets the Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The ConfigureServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {
 

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.Configure<FileForgeDatabaseSettings>
                (Configuration.GetSection(nameof(FileForgeDatabaseSettings)));

            services.AddSingleton<IFileForgeDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<FileForgeDatabaseSettings>>().Value);

            services.AddScoped<FileModelMapper>();
            services.AddScoped<IOurMapper, OurMapper>();
            services.AddScoped<Mapper>();

            services.AddSingleton<FileRepository>();
            services.AddSingleton<WorkspaceRepository>();

            // Builder order reflect the log factors order
            services.AddSingleton(x => new AuditLoggerBuilder()
                                        .BuildTimeStamp()
                                        .BuildActor()
                                        .BuildAction()
                                        .BuildActionType()
                                        .BuildActionStatus()                                      
                                        .BuilLogPaths(Path.Combine(Environment.CurrentDirectory, @"..\logs\logs.txt"))
                                        .Build()
                                        );

            services.AddScoped<WorkspacesFacade>();
            // Set max size for uploaded files, since default is 1024 bytes :(

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue; //not recommended value
                options.MultipartBodyLengthLimit = int.MaxValue; //not recommended value
            });

            services.AddControllers();
            services.AddCors(x=>x.AddPolicy(mCorsPolicy, builder =>
                {
                    builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                }));
        }

        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseCors(mCorsPolicy);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}
