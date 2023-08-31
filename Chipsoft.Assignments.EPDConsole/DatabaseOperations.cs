using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole
{
    public static class DatabaseOperations
    {
        public static int VoegPatientToe(Patient patient) 
        {
            try
            {
                using EPDDbContext eppdd = new();
                eppdd.Patients.Add(patient);

                return eppdd.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }

        public static int VerwijderPatient(Patient patient)
        {
            try
            {
                using EPDDbContext eppdd = new();
                eppdd.Entry(patient).State = EntityState.Deleted;
                return eppdd.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static List<Patient> OphalenPatienten()
        {
            using (EPDDbContext eppdd = new EPDDbContext())
            {
                var query = eppdd.Patients.ToList();

                return query;
            }
        }

        public static int VoegArtsToe(Arts arts)
        {
            try
            {
                using EPDDbContext eppdd = new();
                eppdd.Artsen.Add(arts);

                return eppdd.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static int VerwijderArts(Arts arts)
        {
            try
            {
                using EPDDbContext eppdd = new();
                eppdd.Entry(arts).State = EntityState.Deleted;
                return eppdd.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static List<Arts> OphalenArtsen()
        {
            using (EPDDbContext eppdd = new EPDDbContext())
            {
                var query = eppdd.Artsen.ToList();

                return query;
            }
        }

        public static int VoegAfspraakToe(Afspraak afspraak)
        {
            try
            {
                using EPDDbContext eppdd = new();
                eppdd.Afspraken.Add(afspraak);

                return eppdd.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static int VerwijderAfspraak(Afspraak afspraak)
        {
            try
            {
                using EPDDbContext eppdd = new();
                eppdd.Entry(afspraak).State = EntityState.Deleted;
                return eppdd.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static List<Afspraak> OphalenAfspraken()
        {
            using (EPDDbContext eppdd = new EPDDbContext())
            {
                var query = eppdd.Afspraken.ToList();

                return query;
            }
        }

        public static List<Afspraak> OphalenAfsprakenViaArts(int artsId)
        {
            using (EPDDbContext eppdd = new EPDDbContext())
            {
                var query = eppdd.Afspraken.Include(x => x.Arts).Include(x => x.Patient).ToList().Where(x => x.ArtsId == artsId);

                return query.ToList();
            }
        }

        public static List<Afspraak> OphalenAfsprakenViaPatient(int patientId)
        {
            using (EPDDbContext eppdd = new EPDDbContext())
            {
                var query = eppdd.Afspraken.Include(x => x.Arts).Include(x => x.Patient).ToList().Where(x => x.PatientId == patientId);

                return query.ToList();
            }
        }
    }
}
