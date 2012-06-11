using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

using DomainStorage;
using DomainStorage.Abstracts;
using DomainRepos.Abstracts;
using DomainRepos.Concretes;
using Domain.Entities;
using Ninject.Web.Common;
using vinCMS.Infraestructure.Authentication;
using vinCMS.Infraestructure.Logging;


namespace vinCMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        private static IKernel _container;
        public static IKernel Container
        {
            get
            {
                if (_container == null)
                    _container = new StandardKernel(new SiteModule());
                return _container;
            }
        }


        protected override Ninject.IKernel CreateKernel()
        {
            return Container;
        }

        protected override void OnApplicationStarted()
        {
            //EF profiler
            // HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            // Area registration
            AreaRegistration.RegisterAllAreas();
            // Routing registration
            RegisterRoutes(RouteTable.Routes);

            base.OnApplicationStarted();
        }

        /// <summary>
        /// Catches errors at a high level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, System.EventArgs e)
        {
            // Get exception
            Exception ex = HttpContext.Current.Server.GetLastError();
            // Log Exception
            LoggerWrapper logger =
                ((ILogManager) MvcApplication.Container.GetService(typeof (ILogManager))).GetCurrentClassLogger();
            logger.FatalException("There has been an exception that hasn't been capture in the MVC app an has surfaced to Application_Error", ex);
            // Redirect
            HttpContext.Current.Response.Redirect("/error/handleerror/500");
        }

        /// <summary>
        /// Registers the routes to be used by the routing system
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //for extra security
            routes.RouteExistingFiles = true;
            routes.IgnoreRoute("public/{*path}");
            routes.IgnoreRoute("favicon.ico");


            // Routes for Admin Controller
            // Index page with paging parameter
            routes.MapRoute(
                null,
                "admin/index/{page}",
                new {controller = "admin", action = "index", page = UrlParameter.Optional});
            // any other actions in the admin controller
            routes.MapRoute(
                null,
                "admin/{action}/{id}",
                new {controller = "admin", action = "index", id = UrlParameter.Optional});
            
            // Route for Blog Posts
            // Route for Blog Posts Filtered by tags
            routes.MapRoute(
                null,
                "blog/tag/{tagname}/{page}",
                new {controller = "blog", action="tag", page=UrlParameter.Optional},
                new { tagname = @"(\d|[a-z]|\-)+" }
                );
            // Route for Blog Posts Filtered by category
            routes.MapRoute(
                null,
                "blog/category/{categoryname}/{page}",
                new { controller = "blog", action = "category", page=UrlParameter.Optional},
                new { categoryname = @"(\d|[a-z]|\-)+" });
            routes.MapRoute(
                null,
                "blog/{year}/{month}/{day}/{path}",
                new { controller = "blog", action = "details" },
                new { year = @"\d{4,4}", month = @"\d{1,2}", day = @"\d{1,2}", path = @"(\d|[a-z]|\-)+"}
                );
            routes.MapRoute(
                null,
                "blog/{action}/{page}",
                new {controller="blog", action = "index", page = UrlParameter.Optional});
            // Routes for errors
            routes.MapRoute(
                null,
                "error/{action}/{statuscode}",
                new {controller = "error", action = "handleError", statuscode = UrlParameter.Optional});
            // Routes for pages
            routes.MapRoute(
                null,
                "{path}",
                new { controller = "page", action = "details" },
                new { path = @"(\d|[a-z]|\-)+" }
                );

            // Default Route
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{page}", // URL with parameters
                new { controller = "page", action = "home", page = UrlParameter.Optional } // Parameter defaults
            );

        }


        public class SiteModule : Ninject.Modules.NinjectModule
        {

            public override void Load()
            {
                // Storage
                Bind<IContext>().To<VinCMSEntities>().InRequestScope();
                Bind<IBlogPostRepository>().To<BlogPostRepository>().InRequestScope();
                Bind<IEntityRepository<Category>>().To<CategoryRepository>().InRequestScope();
                Bind<IEntityRepository<Tag>>().To<TagRepository>().InRequestScope();
                Bind<IUserRepository>().To<UserRepository>().InRequestScope();
                Bind<IPageRepository>().To<PageRepository>().InRequestScope();
                // Authentication
                Bind<IFormsAuth>().To<FormsAuthWrapper>().InSingletonScope();
                Bind<IMembership>().To<MembershipWrapper>().InRequestScope();
                Bind<System.Web.Security.MembershipProvider>().To<VinCmsMembershipProvider>().InRequestScope();
                // Logging
                Bind<ILogManager>().To<LogManagerWrapper>().InSingletonScope();
            }
        }
        
    }
}