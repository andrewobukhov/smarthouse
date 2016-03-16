using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using SmartHouseWeb.DataView;
using SmartHouseWeb.Models;

namespace SmartHouseWeb.Controllers
{
    public class SensorController : CommonApiController
    {
        public IEnumerable<SensorStatisticDto> Get(int index)
        {
            var list = Context.SensorStatistics.
                OrderByDescending(x => x.CreateTime).Take(5);

            var dtos = new List<SensorStatisticDto>();

            foreach (var temperature in list)
            {
                dtos.Add(new SensorStatisticDto(temperature));
            }
            
            return dtos;
        }

        public IEnumerable<SensorStateDto> GetAllStates()
        {
            var list = new List<SensorStateDto>();
            foreach(var item in Context.SensorStates.Include("Sensor"))
            {
                list.Add(new SensorStateDto(item));
            }
            return list.AsEnumerable();
        }

        public SensorStateDto GetState(int index)
        {
            var state = Context.SensorStates.FirstOrDefault(x => x.Sensor.SensorIndex == (SensorIndex)index);

            if (state != null)
            {
                return new SensorStateDto(state);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage AddSensorValue(int index, int value)
        {
            var sensor = Context.Sensors.First(x => x.SensorIndex == (SensorIndex)index);
            if (sensor != null)
            {
                var lastSteta = Context.SensorStates.FirstOrDefault(x => x.Sensor.SensorIndex == (SensorIndex) index);

                if (lastSteta == null)
                {
                    Context.SensorStates.Add(new SensorState {Sensor = sensor, Value = value});
                }
                else
                {
                    lastSteta.Value = value;
                    lastSteta.UpdateTime = DateTime.Now;
                }

                var lastTemp = Context.SensorStatistics.Where(x => x.Sensor.SensorId == sensor.SensorId)
                    .OrderByDescending(x => x.CreateTime).FirstOrDefault();

                if (sensor.IsStatisticEnable &&
                    (lastTemp == null ||
                     (DateTime.Now - lastTemp.CreateTime >= TimeSpan.FromSeconds(sensor.StatisticFrequency))))
                {
                    var temp = new SensorStatistic()
                    {
                        Value = value,
                        CreateTime = DateTime.Now,
                        Sensor = sensor
                    };
                    Context.SensorStatistics.Add(temp);
                }
                Context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
