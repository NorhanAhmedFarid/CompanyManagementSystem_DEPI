using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyG02.DAL.Models
{
    public class MyService
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Pricess { get; set; }


        public int Rate { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }


    }
}
