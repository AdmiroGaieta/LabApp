using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabApp_.Models
{
    [Table("Schools")] // Define o nome da tabela no banco de dados
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int NumberOfClassrooms { get; set; }
        public string Province { get; set; }
    }
}
