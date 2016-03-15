using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHouseWeb.Models;
using SmartHouseWeb.Services;

namespace SmartHouseWeb.DataView
{
    public class SensorStatisticDto
    {
        public SensorStatisticDto()
        { }

        public SensorStatisticDto(SensorStatistic sensorStatistic)
        {
            Temp = sensorStatistic.Value;
            Date = sensorStatistic.CreateTime.ToUnixTimestamp();
        }

        public int Temp { get; set; }

        public long Date { get; set; }
    }
}