using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservaDate { get; set; }
        public int Duration { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string ReservedBy { get; set; }

        
    }
}