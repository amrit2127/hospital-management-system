using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    public class Booking
    {
        public Booking()
        {
            AdultCount = 1;
            Adult = 900;
            Children = 700;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        public string Address { get; set; }
        public DateTime BookingDate { get; set; }
        [Required]
        public string  PhoneNumber { get; set; }
        public int AdultCount { get; set; }
        public int ChildrenCount { get; set; }
        public int TotalCount { get; set; }
        public int Adult { get; set; }
        public int Children { get; set; }
        public int Total { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
