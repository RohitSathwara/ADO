using ADO.Models;
using System.Data.SqlClient;
using System.Data;

namespace ADO.Services.implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connStr;

        public EmployeeRepository(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection");
        }

        public List<Employee> GetAll()
        {
            var list = new List<Employee>();

            using SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand("GetAllEmployees", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                list.Add(new Employee
                {
                    Id = (int)rdr["Id"],
                    Name = rdr["Name"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Department = rdr["Department"].ToString()
                });
            }
            return list;
        }


        // data table 
        //public List<Employee> GetAll()
        //{
        //    var list = new List<Employee>();

        //    using SqlConnection conn = new SqlConnection(_connStr);
        //    using SqlCommand cmd = new SqlCommand("GetAllEmployees", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    DataTable dt = new DataTable();
        //    using SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        //    conn.Open();
        //    adapter.Fill(dt);

        //    // Loop through DataTable rows
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        list.Add(new Employee
        //        {
        //            Id = row.Field<int>("Id"), // Strongly-typed access
        //            Name = row.Field<string>("Name") ?? string.Empty,
        //            Email = row.Field<string>("Email") ?? string.Empty,
        //            Department = row.Field<string>("Department") ?? string.Empty
        //        });
        //    }
        //    return list;
        //}



        public void Insert(Employee emp)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand("InsertEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100, emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@Department", emp.Department);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(Employee emp)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand("UpdateEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@Department", emp.Department);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand("DeleteEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
