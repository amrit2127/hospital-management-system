using HospitalManagementSystem.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Data.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) 
        { 
            _context = context; 
            HospitalType=new HospitalTypeRepository(context);
            Hospital=new HospitalRepository(context);
            Department = new DepartmentRepostitory(context);
            Doctor=new DoctorRepository(context);
            Patient=new PatientRepository(context);
            ApplicationUser=new ApplicationUserRepository(context);
        }
        public IHospitalTypeRepository HospitalType { private set; get; }

        public IHospitalRepository Hospital { private set; get; }

        public IDepartmentRepository Department { private set; get; }
        public IDoctorRepository Doctor { private set; get; }
        public IPatientRepository Patient { private set; get; }

        public IBookingRepository Booking { private set; get; }
        public IApplicationUserRepository ApplicationUser { private set; get; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
