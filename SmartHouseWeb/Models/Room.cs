﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouseWeb.Models
{
    public class Room
    {
        public int RoomId { get; set; }
         
        public string Name { get; set; }

        public RoomIndex RoomIndex { get; set; }

        public virtual ICollection<Sensor> Sensors { get; set; } 
    }
}