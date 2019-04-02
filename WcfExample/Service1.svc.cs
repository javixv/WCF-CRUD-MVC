using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfExample
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IMisServicios
    {
        // Mi cadena de conexion example "Data Source=.;Initial Catalog=pruebas;User ID=sa;Password=123"
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=pruebas;Integrated Security=SSPI;");

        public bool DeleteEmployee(int EmployeeInfo)
        {
            con.Open();
            string query = "Delete from Employee where id = @ID";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@ID", EmployeeInfo);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res == 1) {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<EmployeeDetails> GetAllEmployee()
        {
            // Abrir Conexion
            con.Open();
            // Mi Query SQL
            SqlCommand cmd = new SqlCommand("Select * from Employee", con);
            // DataAdapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable
            DataTable dt = new DataTable();
            //DataFill
            da.Fill(dt);
            // List a inicializar
            List<EmployeeDetails> EmployeeList = new List<EmployeeDetails>();
            if (dt.Rows.Count > 0) {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EmployeeDetails employeeInfo = new EmployeeDetails();
                    employeeInfo.Id = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    employeeInfo.Name = dt.Rows[i]["name"].ToString();
                    employeeInfo.Salary = Convert.ToInt32(dt.Rows[i]["salary"].ToString());

                    EmployeeList.Add(employeeInfo);
                }
                con.Close();
            }

            return EmployeeList;
        }

        public List<EmployeeDetails> GetEmployeeDetails(string EmployeeName)
        {
            List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Employee where name Like '%'+@Name+'%'", con);
                cmd.Parameters.AddWithValue("@Name", EmployeeName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EmployeeDetails employeeInfo = new EmployeeDetails();
                        employeeInfo.Id = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                        employeeInfo.Name = dt.Rows[i]["name"].ToString();
                        employeeInfo.Salary = Convert.ToInt32(dt.Rows[i]["salary"].ToString());

                        employeeDetails.Add(employeeInfo);
                    }
                }
                con.Close();
            }
            return employeeDetails;
        }

        public string InsertEmployeeDetails(EmployeeDetails EmployeeInfo)
        {
            string strMessage = string.Empty;
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Employee(id,name,salary) values(@id,@name,@salary)", con);
            cmd.Parameters.AddWithValue("@id", EmployeeInfo.Id);
            cmd.Parameters.AddWithValue("@name", EmployeeInfo.Name);
            cmd.Parameters.AddWithValue("@salary", EmployeeInfo.Salary);
            
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                strMessage = EmployeeInfo.Name + " inserted successfully";
            }
            else
            {
                strMessage = EmployeeInfo.Name + " not inserted successfully";
            }
            con.Close();
            return strMessage;
        }

        public bool UpdateEmployee(EmployeeDetails EmployeeID)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Employee SET name = @Name, salary = @Salary WHERE id = @ID", con);
            cmd.Parameters.AddWithValue("@ID", EmployeeID.Id);
            cmd.Parameters.AddWithValue("@Name", EmployeeID.Name);
            cmd.Parameters.AddWithValue("@Salary", EmployeeID.Salary);

            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }
    }
}

