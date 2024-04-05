using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models.ViewModels
{
    public class DepartmentVM
    {
        public Department Department { get; set; }

        public IEnumerable<SelectListItem> HospitalList { get; set; }
    }
}
