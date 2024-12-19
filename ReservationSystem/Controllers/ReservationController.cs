using ReservationSystem.Models;
using ReservationSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public ReservationController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ReservateConnection"].ConnectionString;
            _reservationRepository = new ReservationRepository(connectionString);
            _roomRepository = new RoomRepository(connectionString);
        }

        public ActionResult Index(string roomName, DateTime? startDate, DateTime? endDate)
        {
            var reservations = _reservationRepository.GetAll(roomName, startDate, endDate);
            return View(reservations);
        }

        public ActionResult Details(int id)
        {
            var reservation = _reservationRepository.GetById(id);
            if (reservation == null) return HttpNotFound();
            return View(reservation);
        }

        public ActionResult Create()
        {
            ViewBag.Rooms = new SelectList(_roomRepository.GetRooms().Where(s => s.Availability == true).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _reservationRepository.Add(reservation);
                return RedirectToAction("Index");
            }

            var rooms = _roomRepository.GetRooms();
            ViewBag.rooms = new SelectList(rooms, "Id", "Name");
            return View(reservation);
        }

        public ActionResult Edit(int id)
        {
            var reservation = _reservationRepository.GetById(id);
            if (reservation == null) return HttpNotFound();

            reservation.RoomId = reservation.Room.Id;

            var rooms = _roomRepository.GetRooms();
            ViewBag.Rooms = new SelectList(rooms, "Id", "Name", reservation.RoomId);

            return View(reservation);
        }

        [HttpPost]
        public ActionResult Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _reservationRepository.Update(reservation);
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        public ActionResult Delete(int id)
        {
            var reservation = _reservationRepository.GetById(id);
            if (reservation == null) return HttpNotFound();
            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _reservationRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}