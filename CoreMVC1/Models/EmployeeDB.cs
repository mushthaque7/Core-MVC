using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace CoreMVC1.Models
{
    public class EmployeeDB
    {
        SqlConnection con;
        public EmployeeDB()
        {
            con = new SqlConnection(@"server=LAPTOP-C9KAO7JJ\SQLEXPRESS;database=ASP_Core;Integrated security=true");
        }
        public string InsertDB(Employee objcls)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_empinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", objcls.Name);
                cmd.Parameters.AddWithValue("@addr", objcls.Address);
                cmd.Parameters.AddWithValue("@salary", objcls.Salary);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Inserted Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }

        }
        public string LoginDB(Employee objcls)
        {
            try
            {
                string msg = "";
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", objcls.Id);
                cmd.Parameters.AddWithValue("@name", objcls.Name);
                con.Open();
                string i = cmd.ExecuteScalar().ToString();
                con.Close();
                if (i == "1")
                {
                    msg = "Success";
                }
                else
                {
                    msg = "Invalid Username or Password";
                }
                return msg;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
        public Employee ProfileDB(int id)
        {
            var getdata =new Employee();
            
            try
            {
                SqlCommand cmd = new SqlCommand("sp_profile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    getdata = new Employee
                    {
                        Id = Convert.ToInt32(dr["Emp_id"]),
                        Name = dr["Emp_name"].ToString(),
                        Address = dr["Emp_address"].ToString(),
                        Salary = dr["Emp_salary"].ToString()
                    };
                }
                con.Close();
                return getdata;
            }
            catch (NullReferenceException ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
           
        }
        public int UpdateProfile(int id,Employee emp)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eid", id);
                cmd.Parameters.AddWithValue("@esalary", emp.Salary);
                cmd.Parameters.AddWithValue("@eaddr", emp.Address);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }

        }
        public List<Employee> selectDB()
        {
            var list = new List<Employee>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_selectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var o = new Employee
                    {
                        Id = Convert.ToInt32(sdr["Emp_id"]),
                        Name = sdr["Emp_name"].ToString(),
                        Address = sdr["Emp_address"].ToString(),
                        Salary = sdr["Emp_salary"].ToString()
                    };
                    list.Add(o);
                }
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
    }
}
