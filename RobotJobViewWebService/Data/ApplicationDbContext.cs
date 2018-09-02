using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RobotJobViewWebService.Models;

namespace RobotJobViewWebService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RobotJobViewWebService.Models.Job> Job { get; set; }
        public DbSet<RobotJobViewWebService.Models.Result> Result { get; set; }
    }
}
