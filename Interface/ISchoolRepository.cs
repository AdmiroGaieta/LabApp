using LabApp_.Models;

namespace LabApp_.Interface
{
    public interface ISchoolRepository
    {
  
            Task<IEnumerable<School>> GetAllSchoolsAsync();
            Task<School> GetSchoolByIdAsync(int id);
            Task AddSchoolAsync(School school);
            Task UpdateSchoolAsync(School school);
            Task DeleteSchoolAsync(int id);
       
    }
}
