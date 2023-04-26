﻿using Inventario.Application.Contracts.DbContexts;
using Inventario.Domain.EntityModels.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Identity.DbContexts
{
    public class ApplicationIdentityDbContext
        : IdentityDbContext<Usuario>, IApplicationIdentityDbContext
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {

        }
        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
