using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SmartHouseWeb.Models;

namespace SmartHouseWeb.DAL
{
    public class ShContext : DbContext
    {
        public DbSet<SensorStatistic> SensorStatistics { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<SensorState> SensorStates { get; set; }

        public DbSet<SocketState> SocketStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}