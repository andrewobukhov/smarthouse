using System.Web.Http;

namespace SmartHouseWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "SensorAdd",
                routeTemplate: "api/sensor/add/{index}/{value}",
                defaults: new { controller = "Sensor", action ="AddSensorValue" }
            );

            config.Routes.MapHttpRoute(
                name: "SensorStates",
                routeTemplate: "api/sensor/states",
                defaults: new { controller = "Sensor", action = "GetAllStates" }
            );

            config.Routes.MapHttpRoute(
                name: "SensorLastValue",
                routeTemplate: "api/sensor/{index}",
                defaults: new { controller = "Sensor", action = "GetState" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultSensor",
                routeTemplate: "api/sensor/{action}/{index}",
                defaults: new { controller = "Sensor"}
            );

            config.Routes.MapHttpRoute(
                name: "SocketSetState",
                routeTemplate: "api/socket/{index}/{isTurnOn}",
                defaults: new { controller = "Socket", action = "SetState" }
            );

            config.Routes.MapHttpRoute(
                name: "SocketState",
                routeTemplate: "api/socket/{index}",
                defaults: new { controller = "Socket", action = "GetState" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
