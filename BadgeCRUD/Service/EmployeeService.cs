using BadgeCRUD.Model;
using Microsoft.Data.SqlClient;

namespace BadgeCRUD.Service
{
    public class EmployeeService : IEmployee
    {
        private readonly IConfiguration _configuration;
        public string CString;
        
        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            CString = _configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using(SqlConnection conn = new SqlConnection(CString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EMPLOYEE", conn);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Employee employee = new Employee();
                    employee.EmployeeId = Convert.ToInt32(reader["EMPLOYEE_ID"]);
                    employee.EmployeeName = reader["EMPLOYEE_NAME"].ToString();
                    employee.EmployeeAge = Convert.ToInt32(reader["EMPLOYEE_AGE"]);
                    employee.EmployeeSalary = Convert.ToInt32(reader["SALARY"]);
                    employees.Add(employee);
                }
                return employees;
            }
        }

        public async Task AddEmployee(Employee employee)
        {
            using(SqlConnection conn = new SqlConnection(CString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE(EMPLOYEE_NAME,EMPLOYEE_AGE,SALARY) VALUES(@VAL1,@VAL2,@VAL3)",conn);
                cmd.Parameters.AddWithValue("@VAL1", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@VAL2", employee.EmployeeAge);
                cmd.Parameters.AddWithValue("@VAL3", employee.EmployeeSalary);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateEmployee(int id, Employee employee)
        {
            using(SqlConnection con = new SqlConnection(CString))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("UPDATE EMPLOYEE SET EMPLOYEE_NAME = @NAME, EMPLOYEE_AGE = @AGE, SALARY = @SALARY WHERE EMPLOYEE_ID = @ID", con);
                cmd.Parameters.AddWithValue("@NAME",employee.EmployeeName);
                cmd.Parameters.AddWithValue("@AGE", employee.EmployeeAge);
                cmd.Parameters.AddWithValue("@SALARY",employee.EmployeeSalary);
                cmd.Parameters.AddWithValue("@ID", employee.EmployeeId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteEmployee(int id)
        {
            using(SqlConnection connection = new SqlConnection(CString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("DELETE FROM EMPLOYEE WHERE EMPLOYEE_ID=@id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
