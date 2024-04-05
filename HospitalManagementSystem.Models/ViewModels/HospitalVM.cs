using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class HospitalVM
    {
        public Hospital Hospital { get; set; }

        public IEnumerable<SelectListItem> HospitalTypeList { get; set; }
    }
}
