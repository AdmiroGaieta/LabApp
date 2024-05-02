using System.Collections.Generic;
using System.Threading.Tasks;
using LabApp_.Interface;
using LabApp_.Models;

namespace LabApp_.Services
{
    public class SchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await _schoolRepository.GetAllSchoolsAsync();
        }

        public async Task<School> GetSchoolByIdAsync(int id)
        {
            return await _schoolRepository.GetSchoolByIdAsync(id);
        }

        public async Task AddSchoolAsync(School school)
        {
            await _schoolRepository.AddSchoolAsync(school);
        }

        public async Task UpdateSchoolAsync(School school)
        {
            await _schoolRepository.UpdateSchoolAsync(school);
        }

        public async Task DeleteSchoolAsync(int id)
        {
            await _schoolRepository.DeleteSchoolAsync(id);
        }
    }
}
