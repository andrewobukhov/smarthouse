using SmartHouseWeb.Models;

namespace SmartHouseWeb.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.ShContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DAL.ShContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Rooms.AddOrUpdate(x => x.Name,
                new Room { Name = "Котельная", RoomIndex = RoomIndex.BoilerRoom },
                new Room { Name = "Кухня", RoomIndex = RoomIndex.Kitchen },
                new Room { Name = "Баня", RoomIndex = RoomIndex.BathHouse }
                );

            context.SaveChanges();

            var boilerRoom = context.Rooms.First(x => x.RoomIndex == RoomIndex.BoilerRoom);

            context.Sensors.AddOrUpdate(
                x => x.SensorIndex,
                new Sensor
                {
                    Name = "Температура воздуха в котельной",
                    RoomId = boilerRoom.RoomId,
                    SensorIndex = SensorIndex.TempSensorBoilerRoom,
                    IsStatisticEnable = true,
                    StatisticFrequency = 30
                }
            );

            context.Sensors.AddOrUpdate(
                x => x.SensorIndex,
                new Sensor
                {
                    Name = "Температура воды в котле",
                    RoomId = boilerRoom.RoomId,
                    SensorIndex = SensorIndex.TempOfWaterBoiler,
                    IsStatisticEnable = false,
                    StatisticFrequency = 0
                }
            );

            context.SaveChanges();
        }
    }
}
