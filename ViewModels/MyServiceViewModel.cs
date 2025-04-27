using CompanyG02.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyG02.PL.ViewModels
{
    public class MyServiceViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required !!")]
        [MaxLength(50, ErrorMessage = "Max Lenth is 50 Chars")]
        [MinLength(10, ErrorMessage = "Min Lenth is 10 Chars")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price Is Required !!")]

        public int Pricess { get; set; }
        [Required(ErrorMessage = "Rate Is Required !!")]

        public int Rate { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
