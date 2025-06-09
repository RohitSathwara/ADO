using ADO.Models;

namespace ADO.Services
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        void Insert(Employee emp);
        void Update(Employee emp);
        void Delete(int id);
    }
}
