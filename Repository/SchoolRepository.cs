using LabApp_.Context;
using LabApp_.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabApp_.Interface;
using LabApp_.Models;
using Microsoft.EntityFrameworkCore;

namespace LabApp_.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly AppDbContext _context;

        public SchoolRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await _context.Schools.ToListAsync();
        }

        public async Task<School> GetSchoolByIdAsync(int id)
        {
            return await _context.Schools.FindAsync(id);
        }

        public async Task AddSchoolAsync(School school)
        {
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSchoolAsync(School school)
        {
            _context.Schools.Update(school);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSchoolAsync(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school != null)
            {
                _context.Schools.Remove(school);
                await _context.SaveChangesAsync();
            }
        }
    }
}
