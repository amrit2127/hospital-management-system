﻿using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Data.Repository
{
    public class BookingRepository:Repository<Booking>,IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
