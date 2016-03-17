using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHouseWeb.Models;

namespace SmartHouseWeb.DataView
{
    public class SocketStateDto
    {
        public string Name { get; set; }

        public bool IsTurnOn { get; set; }

        public int Index { get; set; }

        public string StateName { get; set; }

        public SocketStateDto()
        { }

        public SocketStateDto(SocketState state)
        {
            Name = "Бойлер";
            IsTurnOn = state.IsTurnOn;
            Index = (int)state.SocketIndex;
            StateName = IsTurnOn ? "Turn off" : "Turn on";
        }
    }
}