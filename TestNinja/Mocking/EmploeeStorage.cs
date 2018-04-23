namespace TestNinja.Mocking
{
    public interface IEmploeeStorage
    {
        void DeleteEmployee(int id);
    }

    public class EmploeeStorage : IEmploeeStorage
    {
        private EmployeeContext _db;
        
        public EmploeeStorage()
        {
            _db = new EmployeeContext();
        }
        
        public void DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if(employee == null) return;
            
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}