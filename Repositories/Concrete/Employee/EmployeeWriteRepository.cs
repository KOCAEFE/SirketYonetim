using SirketYonetim.Data;
using SirketYonetim.Repositories.Abstract.Employee;

namespace SirketYonetim.Repositories.Concrete.Employee
{
    public class EmployeeWriteRepository : WriteRepository<Entities.Employee>, IEmployeeWriteRepository
    {
        public EmployeeWriteRepository(SirketYonetimContext context) : base(context)
        {
        }
    }
}
