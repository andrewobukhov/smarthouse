using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouseWeb.Models
{
    public class SocketState
    {
        public int SocketStateId { get; set; }

        public SocketIndex SocketIndex { get; set; }

        public bool IsTurnOn { get; set; }
    }
}