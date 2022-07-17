using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Data;
using WpfApp1.Model;

namespace WpfApp1
{
    public class PatientService
    {

        private UserWPFContext _userContext;
        private UserService userService;

        public PatientService()
        {
            _userContext = new UserWPFContext();
            userService = new UserService();
        }

       
        public List<Patient> refreshPatients()
        {
            return findAll();
        }

        public  Patient savePatient(Patient patient)
        {
            if (patient is null)
                return null;
            
            if(patient.creator == null)
            {
                var createdBy = userService.getLoggedInUser(_userContext);
                patient.creator = createdBy;
            }
          
            Patient saved = _userContext.patients.Add(patient).Entity;
            _userContext.SaveChanges();
            
            return saved;
        }

        public List<Patient> findAll()
        {
            return _userContext.patients.Select(e => e).Include(e => e.creator)
                .ToList();
        }

        public void deletePatient(Patient patient)
        {
            if (patient == null)
            {
                return;
            }
            _userContext.patients.Remove(patient);
            _userContext.SaveChanges();



        }
    }
}
