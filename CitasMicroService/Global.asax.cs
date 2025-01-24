using CitasMicroService.Domain.Repositories;
using CitasMicroService.Infrastructure;
using MediatR;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CitasMicroService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {


            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            // Registro de MediatR
            container.RegisterSingleton<IMediator, Mediator>();
            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Collection.Register(typeof(IPipelineBehavior<,>), assemblies);
            // Registrar el ServiceFactory para MediatR
            container.Register(() => (ServiceFactory)container.GetInstance, Lifestyle.Singleton);
            // Registro de dependencias
            RegisterDependencies(container);
            // Registrar controladores
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            // Registrar el filtro de excepciones globalmente
            //GlobalConfiguration.Configuration.Filters.Add(new Api.Filters.ExeptionFilter());


            // Verificar la configuración
            container.Verify();

            // Configurar Web API
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
        private void RegisterDependencies(Container container)
        {
            container.Register<DatabaseContext>(Lifestyle.Scoped);
            container.Register<ICitaRepository, CitaRepository>(Lifestyle.Scoped);

        }
    }
}
