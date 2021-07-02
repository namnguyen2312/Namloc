using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data;
using WBrand.Data.EF;
using WBrand.Data.EF.Repositories.User;
using WBrand.Services;
using WBrand.Services.Facade;
using WBrand.Services.Facade.Identity;
using WBrand.Services.Facade.User;
using WBrand.Web.Kernel.Mappers;

[assembly: OwinStartup(typeof(WBrand.Web.App_Start.Startup))]

namespace WBrand.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigAutofac(app);
            ConfigureAuth(app);
            ConfigAutoMapper();
            UpdateMigration();
        }

        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var assembliesGpotWebApi = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("WBrand.Web.Api"));
            builder.RegisterApiControllers(assembliesGpotWebApi.ToArray());

            var assembliesGpotMapper = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("WBrand.Web.Kernel"));
            builder.RegisterAssemblyTypes(assembliesGpotMapper.ToArray())
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();
            MapperConfiguration configMapper;
            builder.Register(c =>
            {
                var profiles = c.Resolve<IEnumerable<Profile>>();

                configMapper = new MapperConfiguration(x =>
               {
                    // Load in all our AutoMapper profiles that have been registered
                    foreach (var profile in profiles)
                   {
                       x.AddProfile(profile);
                   }
               });

                return configMapper;
            })
            .As<IConfigurationProvider>()
            //.As<IConfiguration>()
            .SingleInstance()
            .AsSelf();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

            builder.RegisterType<WBrandDbContext>().AsSelf().InstancePerRequest();

            builder.RegisterType<RoleStore<AppRole>>().As<IRoleStore<AppRole, string>>();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<AppUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IEfRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(BaseServices<>)).As(typeof(IBaseServices<>)).InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(AppGroupRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(AppGroupService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver
        }

        private void UpdateMigration()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WBrandDbContext, WBrand.Data.EF.Migrations.Configuration>());
        }

        private void ConfigAutoMapper()
        {

            Mapper.Initialize(mapperConfigurationExpression =>
            {
                mapperConfigurationExpression.AddProfile(new DomainToViewModelMappingProfile());
                mapperConfigurationExpression.AddProfile(new ViewModelToDomainMappingProfile());
                mapperConfigurationExpression.AddProfile(new CatalogMappingProfile());
                mapperConfigurationExpression.AddProfile(new BlogMappingProfile());
            });
        }
    }
}
