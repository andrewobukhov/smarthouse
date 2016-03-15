using SmartHouseWeb.Models;
using SmartHouseWeb.Services;

namespace SmartHouseWeb.DataView
{
    public class SensorStateDto
    {
        public int Value { get; set; }

        public long Date { get; set; }

        public SensorStateDto(SensorState state)
        {
            Value = state.Value;
            Date = state.UpdateTime.ToUnixTimestamp();
        }
    }
}