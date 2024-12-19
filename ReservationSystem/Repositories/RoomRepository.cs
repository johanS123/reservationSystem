using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Data.Common;

namespace ReservationSystem.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly string _connectionString;

        public RoomRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Room> GetRooms()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Room>("listRooms", commandType: CommandType.StoredProcedure);
            }
        }

        public int CreateRoom(Room room)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", room.Name);
            parameters.Add("@Capacity", room.Capacity);
            parameters.Add("@Availability", room.Availability);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QuerySingle<int>("CreateRoom", parameters, commandType: CommandType.StoredProcedure);
            }
                
        }

        public void UpdateRoom(Room room)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", room.Id);
            parameters.Add("@Name", room.Name);
            parameters.Add("@Capacity", room.Capacity);
            parameters.Add("@Availability", room.Availability);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute("UpdateRoom", parameters, commandType: CommandType.StoredProcedure);
            }
            
        }

        public void DeleteRoom(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("DeleteRoom", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        // Obtener una sala por su ID
        public Room GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QuerySingleOrDefault<Room>("SELECT * FROM Rooms WHERE Id = @Id", new { Id = id });
            }
        }
    }
}