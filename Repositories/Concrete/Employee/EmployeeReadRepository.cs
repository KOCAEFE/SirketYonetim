using SirketYonetim.Data;
using SirketYonetim.Repositories.Abstract.Employee;

namespace SirketYonetim.Repositories.Concrete.Employee
{
    public class EmployeeReadRepository : ReadRepository<Entities.Employee>, IEmployeeReadRepository
    {
        public EmployeeReadRepository(SirketYonetimContext context) : base(context)
        {
        }
    }
}
