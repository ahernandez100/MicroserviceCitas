using MediatR;
using Newtonsoft.Json;
using RecetasServices.Application.Services;
using RecetasServices.Domain.Repositories;
using RecetasServices.Infrastructure;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RecetasServices
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
            GlobalConfiguration.Configuration.Filters.Add(new Api.Filters.ExeptionFilter());


            // Verificar la configuración
            container.Verify();

            // Configurar Web API
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            StartRabbitMQListener(container);
        }
        private void RegisterDependencies(Container container)
        {
            container.Register<DatabaseContext>(Lifestyle.Scoped);
            container.Register<IRecetaRepository, RecetaRepository>(Lifestyle.Scoped);
            container.Register<IPersonaService, PersonaService>(Lifestyle.Scoped);
            container.Register<ICitaService, CitaService>(Lifestyle.Scoped);
        }
        public static void Register(HttpConfiguration config)
        {
            // Configuración de JSON
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Otras configuraciones
        }
        private void StartRabbitMQListener(Container container)
        {

            // Inicia el listener en un hilo separado
            Thread listenerThread = new Thread(() =>
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    var _recetaService = container.GetInstance<IMediator>();
                    RabbitMQListener listener = new RabbitMQListener(_recetaService);
                    listener.StartListening();
                }
            });

            listenerThread.IsBackground = true; // Hacer que el hilo sea un hilo de fondo
            listenerThread.Start();
        }
    }
}
