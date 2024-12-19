using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReservationSystem.Models;

namespace ReservationSystem.Repositories
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms();
        Room GetById(int id);
        int CreateRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int id);
    }
}