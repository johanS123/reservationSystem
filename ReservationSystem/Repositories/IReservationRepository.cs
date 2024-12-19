using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReservationSystem.Models;

namespace ReservationSystem.Repositories
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll(string roomName, DateTime? startDate, DateTime? endDate);
        Reservation GetById(int id);
        int Add(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(int id);
    }
}