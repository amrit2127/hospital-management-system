using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Data.Repository
{
    public class HospitalTypeRepository:Repository<HospitalType>,IHospitalTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public HospitalTypeRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
