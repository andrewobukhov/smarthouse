using System;

namespace SmartHouseWeb.Models
{
    public class SensorState
    {
        public int SensorStateId { get; set; }

        public int SensorId { get; set; }

        public virtual Sensor Sensor { get; set; }

        public int Value { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}