using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using ReservationSystem.Models;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace ReservationSystem.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;

        public ReservationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public IEnumerable<Reservation> GetAll(string roomName, DateTime? startDate, DateTime? endDate)
        {
            using (var db = Connection)
            {
                return db.Query<Reservation, Room, Reservation>("ListReservations", (reservation, room) =>
                {
                    reservation.Room = room;
                    return reservation;
                },
                new
                {
                    RoomName = string.IsNullOrEmpty(roomName) ? null : roomName,
                    startDate,
                    endDate
                },
                splitOn: "RoomId",
                commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Reservation GetById(int id)
        {
            using (var db = Connection)
            {
                return db.Query<Reservation, Room, Reservation>(
                    "GetReservationById",
                    (reservation, room) =>
                    {
                        reservation.Room = room;
                        return reservation;
                    },
                    new { ReservationId = id },
                    splitOn: "RoomId",
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }

        public int Add(Reservation reservation)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ReservedBy", reservation.ReservedBy);
            parameters.Add("@RoomId", reservation.RoomId);
            parameters.Add("@ReservaDate", reservation.ReservaDate);
            parameters.Add("@Duration", reservation.Duration);

            using (var db = Connection)
            {
                return db.QuerySingle<int>("CreateReservation", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(Reservation reservation)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", reservation.Id);
            parameters.Add("@ReservedBy", reservation.ReservedBy);
            parameters.Add("@RoomId", reservation.RoomId);
            parameters.Add("@ReservaDate", reservation.ReservaDate);
            parameters.Add("@Duration", reservation.Duration);


            using (var db = Connection)
            {
                db.Execute("UpdateReservation", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void Delete(int id)
        {
            using (var db = Connection)
            {
                db.Execute("DeleteReservation", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}