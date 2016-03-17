using SmartHouseWeb.Models;
using SmartHouseWeb.Services;

namespace SmartHouseWeb.DataView
{
    public class SensorStateDto
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public long Date { get; set; }

        public SensorStateDto()
        {
        }

        public SensorStateDto(SensorState state)
        {
            Name = state.Sensor.Name;
            Value = state.Value;
            Date = state.UpdateTime.ToUnixTimestamp();
        }
    }
}