using CompanyG02.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyG02.PL.ViewModels
{
    public class ProjectsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required !!")]
        [MaxLength(50, ErrorMessage = "Max Lenth is 50 Chars")]
        [MinLength(10, ErrorMessage = "Min Lenth is 10 Chars")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Budget Is Required !!")]

        public decimal Budget { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
