using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IHospitalTypeRepository HospitalType { get; }
        public IHospitalRepository Hospital { get; }
        public IDepartmentRepository Department { get; }
        public IDoctorRepository Doctor {  get; }
        public IPatientRepository Patient { get; }
        public IBookingRepository Booking { get; }
        public IApplicationUserRepository ApplicationUser { get; }

        public void Save();
    }
}
