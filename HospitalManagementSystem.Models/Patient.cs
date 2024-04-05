
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    public class Patient
    {
        public Patient()
        {
            AdultCount = 0;
            Age = 1;
            ChildrenSlipPrice = 700;
            AdultSlipPrice = 900;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime DateTime { get; set; }
        public int ChildrenCount { get; set; }
        public int ChildrenSlipPrice { get; set; }
        public int AdultCount { get; set; }
        public int AdultSlipPrice { get; set; }
        public int Amount { get; set; }

    }
}
