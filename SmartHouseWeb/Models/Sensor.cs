namespace SmartHouseWeb.Models
{
    public class Sensor
    {
        public int SensorId { get; set; }

        public string Name { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public virtual SensorIndex SensorIndex { get; set; }

        public bool IsStatisticEnable { get; set; }

        public int StatisticFrequency { get; set; }
    }
}