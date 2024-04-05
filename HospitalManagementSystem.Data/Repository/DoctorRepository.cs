using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Data.Repository
{
    public class DoctorRepository:Repository<Doctor>,IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
