using Inventario.Identity.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventario.Domain.EntityModels.Identity;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Identity.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class EnhancedAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public EnhancedAuthorizeAttribute(string section)
        {
            _section = section.ToLower().Trim();
        }
        readonly string _section;


        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext filterContext)
        {
            bool skipAuthorization =
                filterContext.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>().Any();

            if (skipAuthorization)
            {
                return;
            }

            var username = filterContext.HttpContext.User.Identity.Name;
            if (username == null)
            {
                //throw new UnauthorizedAccessException();
                filterContext.Result = new StatusCodeResult(401);
                return;
            }

            var dbContext = filterContext.HttpContext.RequestServices
                .GetService(typeof(ApplicationIdentityDbContext))
                as ApplicationIdentityDbContext;

            IQueryable<Permiso> query = dbContext.Permisos.Include(p => p.Usuarios);

            Permiso permiso =
                query.Where(w => w.Nombre.ToLower() == _section).FirstOrDefault();

            if (permiso == null)
            {
                filterContext.Result = new StatusCodeResult(401);
                return;
            }

            bool hasPermision =
                permiso.Usuarios.Where(w => w.UserName == username).Any();


            if (!hasPermision)
            {
                filterContext.Result = new StatusCodeResult(401);
            }
        }
    }
}
