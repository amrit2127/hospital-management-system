using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string RegNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string Pincode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Display(Name = "HospitalType")]
        public int HospitalTypeId { get; set; }
        public HospitalType HospitalType { get; set; }

    }

}
