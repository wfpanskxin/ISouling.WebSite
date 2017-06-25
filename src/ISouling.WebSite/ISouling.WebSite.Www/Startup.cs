using Autofac;
using Autofac.Extensions.DependencyInjection;
using ISouling.WebSite.Www.Data;
using ISouling.WebSite.Www.Models.AccountViewModels;
using ISouling.WebSite.Www.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Reflection;

namespace ISouling.WebSite.Www
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                .AddEntityFrameworkStores<UserDbContext, int>()
                .AddDefaultTokenProviders();

            services.AddSession();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        if (type.GetTypeInfo().IsSubclassOf(typeof(RegisterViewModel)))
                            return factory.Create(typeof(RegisterViewModel));

                        return factory.Create(type);
                    };
                });

            #region autofac

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            builder.RegisterType<AuthMessageSender>().AsImplementedInterfaces();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.Populate(services);

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer = builder.Build());

            #endregion autofac
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime, IHttpContextAccessor httpContextAccessor)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            ISouling.Component.Web.HttpContext.Configure(httpContextAccessor);

            var supportedCultures = new[]
            {
                new CultureInfo("zh-CN"),
                new CultureInfo("en-US")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(supportedCultures[0]),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseSession();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                foreach (var area in new[] { "User" })
                {
                    MapAreaRoute(routes, area);
                }

                routes.MapRoute(
                    name: "Enneagram",
                    template: "Enneagram/{id:range(1,9)}",
                    defaults: new { controller = "Enneagram", action = "Index" });

                routes.MapRoute(
                    name: "controller",
                    template: "{controller=Home}",
                    defaults: new { action = "Index" });

                routes.MapRoute(
                    name: "action",
                    template: "{action=Index}/{id?}",
                    defaults: new { controller = "Home" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //DbInitializer.Initialize(context);

            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }

        private void MapAreaRoute(IRouteBuilder routes, string area)
        {
            routes.MapAreaRoute(
                name: area + "_controller",
                areaName: area,
                template: area + "/{controller=Home}",
                defaults: new { action = "Index" });

            routes.MapAreaRoute(
                name: area + "_action",
                areaName: area,
                template: area + "/{action=Index}/{id?}",
                defaults: new { controller = "Home" });

            routes.MapAreaRoute(
                name: area + "_default",
                areaName: area,
                template: area + "/{controller=Home}/{action=Index}/{id?}");
        }
    }
}