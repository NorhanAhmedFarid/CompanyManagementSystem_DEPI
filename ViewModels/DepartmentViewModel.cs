using CompanyG02.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace CompanyG02.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; } //By Convention => PK Identity
        [Required(ErrorMessage = "Code Is Required !!")]
        public string Code { get; set; } //Ref Type
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
