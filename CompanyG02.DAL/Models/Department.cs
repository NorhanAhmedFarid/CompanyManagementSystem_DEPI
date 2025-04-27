using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyG02.DAL.Models
{
    public class Department
    {
        public int Id { get; set; } //By Convention => PK Identity
        [Required]
        public string Code { get; set; } //Ref Type
         [MaxLength(50)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        //Navigation Poperty [Many]
    }
}
