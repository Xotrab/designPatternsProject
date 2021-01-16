namespace FileForgeDP
{
    using FileForgeDP.Database.Models;
    using FileForgeDP.Database.Repositories;
    using FileForgeDP.Facades;
    using FileForgeDP.Loggers;
    
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;

    public class Startup
    {
        private readonly string mCorsPolicy = "Konrad to powazny programista";
        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy => policy.RequireClaim("user_roles", "[Administrator]"));
                options.AddPolicy("User", policy => policy.RequireClaim("user_roles", "[User]"));
            });

            services.Configure<FileForgeDatabaseSettings>
                (Configuration.GetSection(nameof(FileForgeDatabaseSettings)));

            services.AddSingleton<IFileForgeDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<FileForgeDatabaseSettings>>().Value);

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
            services.AddScoped<KeycloakFacade>();
            // Set max size for uploaded files, since default is 1024 bytes :(

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue; //not recommended value
                options.MultipartBodyLengthLimit = int.MaxValue; //not recommended value
            });

            services.AddControllers();
            services.AddCors(x => x.AddPolicy(mCorsPolicy, builder =>
            {
                builder.SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = Configuration["Jwt:Authority"];
                o.Audience = Configuration["Jwt:Audience"];
                o.RequireHttpsMetadata = false;
                o.IncludeErrorDetails = true;
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = (x) =>
                    {
                        x.NoResult();
                        
                        x.Response.StatusCode = 500;
                        x.Response.ContentType = "text/plain";
                        return x.Response.WriteAsync(x.Exception.ToString());
                    }
                };
            });
        }

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
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode > 400)
                {
                    var logger = app.ApplicationServices.GetService<ILogger>();

                    ActionEnum actionEnum;

                    switch(context.Request.Method)
                    {
                        case "GET":
                            actionEnum = ActionEnum.GET;
                            break;
                        case "DELETE":
                            actionEnum = ActionEnum.DELETE;
                            break;

                        case "PUT":
                            actionEnum = ActionEnum.UPDATE;
                            break;
                        case "POST":
                            actionEnum = ActionEnum.UPLOAD;
                            break;
                        default:
                            actionEnum = ActionEnum.GET;
                            break;
                    }
                        
                    logger.Debug(context.User?.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "undefined", actionEnum, context.Request.Path.Value, HttpStatusCode.Unauthorized);
                   
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();
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
