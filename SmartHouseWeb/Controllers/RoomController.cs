using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHouseWeb.DataView;

namespace SmartHouseWeb.Controllers
{
    public class RoomController : CommonApiController
    {
        // GET: Room
        public IEnumerable<RoomDto> Get()
        {
            return Context.Rooms.Select(x => new RoomDto() {Name = x.Name});
        }
    }
}