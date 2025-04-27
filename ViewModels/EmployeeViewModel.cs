using CompanyG02.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace CompanyG02.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required !!")]
        [MaxLength(50, ErrorMessage = "Max Lenth is 50 Chars")]
        [MinLength(10, ErrorMessage = "Min Lenth is 10 Chars")]

        public string Name { get; set; }
        [Range(22, 35, ErrorMessage = "Age Must be In Range From 22 to 35 ")]
        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address Mudt Be Like 123-Street-City-Country")]
        public string Adress { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; } //FK
        //FK Optional =>OnDelete=>Restrict
        //FK Required =>OnDelete=> Cascade
        public Department Department { get; set; }
      
    }
}
