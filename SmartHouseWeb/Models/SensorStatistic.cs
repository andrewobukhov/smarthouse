using System;
using System.ComponentModel;

namespace SmartHouseWeb.Models
{
    public class SensorStatistic
    {
        public int SensorStatisticId { get; set; }

        public int Value { get; set; }

        public int SensorId { get; set; }

        public virtual Sensor Sensor { get; set; }

        public DateTime CreateTime { get; set; }

    }
}