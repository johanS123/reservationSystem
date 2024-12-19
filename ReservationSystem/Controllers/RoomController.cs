using ReservationSystem.Models;
using ReservationSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationSystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomRepository _repository;


        public RoomController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ReservateConnection"].ConnectionString;
            _repository = new RoomRepository(connectionString);
        }

        public ActionResult Index()
        {
            var rooms = _repository.GetRooms();
            return View(rooms);
        }

        public ActionResult Create()
        {
            var rooms = _repository.GetRooms();
            ViewBag.Rooms = new SelectList(rooms, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Room model)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateRoom(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var room = _repository.GetRooms().FirstOrDefault(s => s.Id == id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [HttpPost]
        public ActionResult Edit(Room model)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateRoom(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var room = _repository.GetRooms().FirstOrDefault(s => s.Id == id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteRoom(id);
            return RedirectToAction("Index");
        }
    }
}