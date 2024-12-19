using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Capacity { get; set; }
        public bool Availability { get; set; }
    }
}