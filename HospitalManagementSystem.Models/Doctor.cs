using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string About { get; set; }
        
        public string SpecialityName { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string ResidentOf { get; set; }
        [Required]
        public GenderType Gender { get; set; }
        public enum PerformanceLevel { Best, Good, Better, NeedImprovement }
        [Required]
        public PerformanceLevel Performance { get; set; }
        [Display(Name = "Image")]
        public string  ImageUrl { get; set; }
        [Required]
        public bool HasLicense { get; set; }
        [Required]
        public string LicenseNumber { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
