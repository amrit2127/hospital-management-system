using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Data.Repository
{
    public class HospitalRepository:Repository<Hospital>,IHospitalRepository
    {
        private readonly ApplicationDbContext _context;
        public HospitalRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
