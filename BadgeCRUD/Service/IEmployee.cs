using BadgeCRUD.Model;

namespace BadgeCRUD.Service
{
    public interface IEmployee
    {
        Task<List<Employee>> GetEmployees();
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(int id,Employee employee);
        Task DeleteEmployee(int id);
    }
}
