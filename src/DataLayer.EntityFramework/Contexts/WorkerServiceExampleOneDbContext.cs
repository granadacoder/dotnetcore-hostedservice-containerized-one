using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MyCompany.MyExamples.WorkerServiceExampleOne.Domain.Entities;
using MyCompany.MyExamples.WorkerServiceExampleOne.DomainDataLayer.EntityFramework.OrmMaps;

namespace MyCompany.MyExamples.WorkerServiceExampleOne.DomainDataLayer.EntityFramework.Contexts
{
    public class WorkerServiceExampleOneDbContext : DbContext
    {
        public const string ErrorMessageILoggerFactoryWrapperIsNull = "ILoggerFactoryWrapper is null";

        private readonly ILoggerFactory loggerFactory;

        public WorkerServiceExampleOneDbContext(DbContextOptions<WorkerServiceExampleOneDbContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(ErrorMessageILoggerFactoryWrapperIsNull, (Exception)null);

            this.Database.EnsureCreated();
        }

        //////public BoardGamesDbContext(DbContextOptions options) : base(options)
        //////{
        //////}

        public DbSet<BoardGameEntity> BoardGames { get; set; }

        public DbSet<MyParentEntity> MyParents { get; set; }

        public DbSet<MyChildEntity> MyChilds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoardGameMap());
            modelBuilder.ApplyConfiguration(new MyParentMap());
            modelBuilder.ApplyConfiguration(new MyChildMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Allow null in case you are using an IDesignTimeDbContextFactory
            if (this.loggerFactory != null)
            {
                /* this.loggerFactory is Microsoft.Extensions.Logging.ILoggerFactory */
                ////if (System.Diagnostics.Debugger.IsAttached)
                ////{
                ////    //// Probably shouldn't log sql statements in production reminder
                optionsBuilder.UseLoggerFactory(this.loggerFactory);
                ////}
            }
        }
    }
}
