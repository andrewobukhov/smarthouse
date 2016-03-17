using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouseWeb.DataView
{
    public class SocketStateDto
    {
        public string Name { get; set; }

        public bool IsTurnOn { get; set; }

        public int Index { get; set; }
    }
}