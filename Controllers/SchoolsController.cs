using LabApp_.Enums;
using LabApp_.Models;
using LabApp_.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace LabApp_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolService _schoolService;

        public SchoolsController(SchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetAllSchools()
        {
            var schools = await _schoolService.GetAllSchoolsAsync();
            return Ok(schools);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchoolById(int id)
        {
            var school = await _schoolService.GetSchoolByIdAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            return Ok(school);
        }

        [HttpPost]
        public async Task<ActionResult<School>> AddSchool(School school)
        {
            if (!Enum.IsDefined(typeof(Province), school.Province))
            {
                return BadRequest("A província fornecida é inválida.");
            }

            await _schoolService.AddSchoolAsync(school);
            return CreatedAtAction(nameof(GetSchoolById), new { id = school.Id }, school);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id, School school)
        {
            if (id != school.Id)
            {
                return BadRequest();
            }
            if (!Enum.IsDefined(typeof(Province), school.Province))
            {
                return BadRequest("A província fornecida é inválida.");
            }

            await _schoolService.UpdateSchoolAsync(school);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            await _schoolService.DeleteSchoolAsync(id);
            return NoContent();
        }




        // Aqui estou a separar para o upload

        [HttpPost("upload-excel")]
        public async Task<IActionResult> UploadExcel([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Nenhum arquivo enviado.");

                // Ler os dados do arquivo Excel
                var schoolsData = await ReadExcelFile(file);

                // Validar e inserir os dados na base de dados
                var result = await InsertSchoolsFromExcel(schoolsData);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao processar o upload do Excel: {ex.Message}");
            }
        }

        private async Task<List<SchoolExcelModel>> ReadExcelFile(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var schoolsData = new List<SchoolExcelModel>();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        schoolsData.Add(new SchoolExcelModel
                        {
                            Name = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                            Email = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                            NumberOfClassrooms = int.Parse(worksheet.Cells[row, 3].Value?.ToString()),
                            Province = worksheet.Cells[row, 4].Value?.ToString().Trim()
                        });
                    }

                    return schoolsData;
                }
            }
        }

        private async Task<bool> InsertSchoolsFromExcel(List<SchoolExcelModel> schoolsData)
        {
            foreach (var schoolData in schoolsData)
            {
                if (!Enum.IsDefined(typeof(Province), schoolData.Province))
                {
                    // Retorne uma mensagem de erro indicando que a província fornecida é inválida.
                    return false;
                }

                var school = new School
                {
                    Name = schoolData.Name,
                    Email = schoolData.Email,
                    NumberOfClassrooms = schoolData.NumberOfClassrooms,
                    Province = schoolData.Province
                };

                await _schoolService.AddSchoolAsync(school);
            }

            return true;
        }
    
    }
}
