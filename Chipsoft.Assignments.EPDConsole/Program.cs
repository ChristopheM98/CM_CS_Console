using System;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Chipsoft.Assignments.EPDConsole
{
    public class Program
    {
        //Don't create EF migrations, use the reset db option
        //This deletes and recreates the db, this makes sure all tables exist

        private static void AddPatient()
        {
            //Do action
            Patient patient = null;

            do
            {
                Console.Clear();

                Console.Write("Voornaam van de patient: ");
                string voornaam = Console.ReadLine();
                Console.Write("Naam van de patient: ");
                string naam = Console.ReadLine();
                Console.Write("Adres van de patient: ");
                string adres = Console.ReadLine();
                Console.Write("Gemeente van de patient: ");
                string gemeente = Console.ReadLine();
                Console.Write("Email van de patient: ");
                string email = Console.ReadLine();
                Console.Write("Telefoonnummer van de patient: ");
                string telefoonnummer = Console.ReadLine();

                patient = new Patient(voornaam, naam, adres, gemeente, email, telefoonnummer);

                if (!string.IsNullOrEmpty(patient.Error)) 
                {
                    foreach (var e in patient.Error)
                    {
                        Console.Write(e);
                    }

                    Console.ReadLine();
                }
                    

            } while (!patient.IsGeldig());

            int oke = DatabaseOperations.VoegPatientToe(patient);

            if (oke > 0)
            {
                Console.WriteLine("De patient is toegevoegd.");
            }

            Console.Write("Druk op enter om terug naar het menu te gaan.");
            Console.ReadLine();
            //return to show menu again.
            
        }

        private static void ShowAppointment()
        {
            Console.Clear();
            Afspraak afspraak = null;
            Patient patient = null;
            Arts arts = null;
            int keuze;

            List<Arts> artsen = DatabaseOperations.OphalenArtsen();
            List<Patient> patients = DatabaseOperations.OphalenPatienten();

            Console.WriteLine("Maak een keuze:"
                + Environment.NewLine + "1 - Toon afspraken van specifieke patiënt"
                + Environment.NewLine + "2 - Toon afspraken van specifieke arts");

            string getal = Console.ReadLine();
            while (!int.TryParse(getal, out keuze) || keuze <= 0 || keuze > 2)
            {
                Console.WriteLine($"Uw keuze moet 1 of 2 zijn!");
                getal = Console.ReadLine();
            }

            if (keuze == 1)
            {
                ToonPatienten(patients);
                getal = Console.ReadLine();
                while (!int.TryParse(getal, out keuze) || keuze <= 0 || keuze > patients.Count)
                {
                    Console.WriteLine($"Uw keuze moet minstens 1 en maximum {patients.Count} zijn!");
                    getal = Console.ReadLine();
                }

                patient = patients[keuze - 1];
                List<Afspraak> afsPat = DatabaseOperations.OphalenAfsprakenViaPatient(patient.Id);
                if (afsPat.Count == 0)
                {
                    Console.WriteLine("Er zijn nog geen afspraken gevonden.");
                }
                else
                {
                    foreach (var afs in afsPat)
                    {
                        Console.WriteLine(afs.ToString());
                    }
                }
                
            }
            else
            {
                ToonArtsen(artsen);
                getal = Console.ReadLine();
                while (!int.TryParse(getal, out keuze) || keuze <= 0 || keuze > 2)
                {
                    Console.WriteLine($"Uw keuze moet minstens 1 en maximum {artsen.Count} zijn!");
                    getal = Console.ReadLine();
                }

                arts = artsen[keuze - 1];
                List<Afspraak> afsArts = DatabaseOperations.OphalenAfsprakenViaArts(arts.Id);
                if (afsArts.Count == 0)
                {
                    Console.WriteLine("Er zijn nog geen afspraken gevonden.");
                }
                else
                {
                    foreach (var afs in afsArts)
                    {
                        Console.WriteLine(afs.ToString());
                    }
                }
                
            }

            Console.Write("Druk op enter om terug naar het menu te gaan.");
            Console.ReadLine();
        }

        private static void AddAppointment()
        {
            Console.Clear();
            List<Patient> patients = DatabaseOperations.OphalenPatienten();
            List<Arts> artsen = DatabaseOperations.OphalenArtsen();

            if (artsen.Count == 0 || patients.Count == 0)
            {
                Console.WriteLine("Gelieve eerst minstens één patient en arts aan te maken.");
            }
            else
            {
                Afspraak afspraak = null;
                Patient patient = null;
                Arts arts = null;
                DateTime dataf;
                int uuraf, minaf, keuzep, keuzea;

                ToonPatienten(patients);
                Console.Write("Geef de nummer van de patient die een afspraak heeft: ");
                string getal = Console.ReadLine();

                while (!int.TryParse(getal, out keuzep) || keuzep <= 0 || keuzep > patients.Count)
                {
                    Console.WriteLine($"Uw keuze moet numeriek, minstens 1 en maximum {patients.Count} zijn");
                    Console.Write("Geef de nummer van de patient die een afspraak heeft: ");
                    getal = Console.ReadLine();
                }

                ToonArtsen(artsen);
                Console.Write("Geef de nummer van de arts: ");
                getal = Console.ReadLine();

                while (!int.TryParse(getal, out keuzea) || keuzea <= 0 || keuzea > artsen.Count)
                {
                    Console.WriteLine($"Uw keuze moet numeriek, minstens 1 en maximum {artsen.Count} zijn");
                    Console.Write("Geef de nummer van de arts: ");
                    getal = Console.ReadLine();
                }

                patient = patients[keuzep - 1];
                arts = artsen[keuzea - 1];

                Console.Write("Datum van de afspraak: ");
                string datum = Console.ReadLine();
                while (!DateTime.TryParse(datum, out dataf) || dataf < DateTime.Today)
                {
                    Console.Write("Gelieve een datum in te vullen die in de toekomst ligt: ");
                    datum = Console.ReadLine();
                }

                Console.Write("Uur van de afspraak: ");
                string uur = Console.ReadLine();
                while (!int.TryParse(uur, out uuraf))
                {
                    Console.WriteLine("Het uur moet numeriek zijn!");
                    uur = Console.ReadLine();
                }

                Console.Write("Minuten van de afspraak: ");
                string minuten = Console.ReadLine();
                while (!int.TryParse(minuten, out minaf))
                {
                    Console.WriteLine("De minuten moeten numeriek zijn!");
                    minuten = Console.ReadLine();
                }

                afspraak = new Afspraak(dataf, uuraf, minaf, patient.Id, arts.Id);

                int oke = DatabaseOperations.VoegAfspraakToe(afspraak);

                if (oke > 0)
                {
                    Console.WriteLine("De afspraak is toegevoegd.");
                }
            }

            Console.Write("Druk op enter om terug naar het menu te gaan.");
            Console.ReadLine();
        }

        private static void DeletePhysician()
        {
            Console.Clear();
            List<Arts> artsen = DatabaseOperations.OphalenArtsen();

            if (artsen.Count == 0)
            {
                Console.WriteLine("Er zijn nog geen artsen gevonden.");
            }
            else
            {
                int keuze;

                ToonArtsen(artsen);

                Console.Write("Geef de nummer van de arts die u wilt verwijderen: ");
                string getal = Console.ReadLine();

                while (!int.TryParse(getal, out keuze) || keuze <= 0 || keuze > artsen.Count)
                {
                    Console.WriteLine($"Uw keuze moet numeriek, minstens 1 en maximum {artsen.Count} zijn");
                    Console.Write("Geef de nummer van de arts die u wilt verwijderen: ");
                    getal = Console.ReadLine();
                }

                Arts arts = artsen[keuze - 1];

                int oke = DatabaseOperations.VerwijderArts(arts);

                if (oke > 0)
                {
                    Console.WriteLine($"Arts {arts.Naam} {arts.Voornaam} is verwijderd.");
                }
            }

            Console.Write("Druk op enter om terug naar het menu te gaan.");
            Console.ReadLine();
        }

        private static void AddPhysician()
        {
            Arts arts = null;

            do
            {
                Console.Clear();

                Console.Write("Voornaam van de arts: ");
                string voornaam = Console.ReadLine();
                Console.Write("Naam van de arts: ");
                string naam = Console.ReadLine();
                Console.Write("Adres van de arts: ");
                string adres = Console.ReadLine();
                Console.Write("Gemeente van de arts: ");
                string gemeente = Console.ReadLine();

                arts = new Arts(voornaam, naam, adres, gemeente);

                if (!string.IsNullOrEmpty(arts.Error))
                {
                    foreach (var e in arts.Error)
                    {
                        Console.Write(e);
                    }

                    Console.ReadLine();
                }


            } while (!arts.IsGeldig());

            int oke = DatabaseOperations.VoegArtsToe(arts);

            if (oke > 0)
            {
                Console.WriteLine("De arts is toegevoegd.");
            }

            Console.Write("Druk op enter om terug naar het menu te gaan.");
            Console.ReadLine();
        }

        private static void DeletePatient()
        {
            Console.Clear();
            List<Patient> patients = DatabaseOperations.OphalenPatienten();

            if (patients.Count == 0)
            {
                Console.WriteLine("Er zijn nog geen patienten gevonden.");
            }
            else
            {
                int i = 1;
                int keuze;

                ToonPatienten(patients);

                Console.Write("Geef de nummer van de patient die u wilt verwijderen: ");
                string getal = Console.ReadLine();

                while (!int.TryParse(getal, out keuze) || keuze <= 0 || keuze > patients.Count)
                {
                    Console.WriteLine($"Uw keuze moet numeriek, minstens 1 en maximum {patients.Count} zijn");
                    Console.Write("Geef de nummer van de patient die u wilt verwijderen: ");
                    getal = Console.ReadLine();
                }

                Patient patient = patients[keuze - 1];

                int oke = DatabaseOperations.VerwijderPatient(patient);

                if (oke > 0)
                {
                    Console.WriteLine($"Patient {patient.Naam} {patient.Voornaam} is verwijderd.");
                }
            }

            Console.Write("Druk op enter om terug naar het menu te gaan.");
            Console.ReadLine();
        }

        public static void ToonPatienten(List<Patient> patients) 
        {
            Console.WriteLine("Selecteer een patient: ");
            int i = 1;
            foreach (var pat in patients)
            {
                Console.WriteLine($"{i} - {pat.ToString()}");
                i++;
            }
        }

        public static void ToonArtsen(List<Arts> artsen)
        {
            Console.WriteLine("Selecteer een arts: ");
            int i = 1;
            foreach (var a in artsen)
            {
                Console.WriteLine($"{i} - {a.ToString()}");
                i++;
            }
        }

        #region FreeCodeForAssignment
        static void Main(string[] args)
        {
            while (ShowMenu())
            {
                //Continue
            }
        }

        public static bool ShowMenu()
        {
            Console.Clear();
            foreach (var line in File.ReadAllLines("logo.txt"))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("1 - Patient toevoegen");
            Console.WriteLine("2 - Patienten verwijderen");
            Console.WriteLine("3 - Arts toevoegen");
            Console.WriteLine("4 - Arts verwijderen");
            Console.WriteLine("5 - Afspraak toevoegen");
            Console.WriteLine("6 - Afspraken inzien");
            Console.WriteLine("7 - Sluiten");
            Console.WriteLine("8 - Reset db");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        AddPatient();
                        return true;
                    case 2:
                        DeletePatient();
                        return true;
                    case 3:
                        AddPhysician();
                        return true;
                    case 4:
                        DeletePhysician();
                        return true;
                    case 5:
                        AddAppointment();
                        return true;
                    case 6:
                        ShowAppointment();
                        return true;
                    case 7:
                        return false;
                    case 8:
                        EPDDbContext dbContext = new EPDDbContext();
                        dbContext.Database.EnsureDeleted();
                        dbContext.Database.EnsureCreated();
                        return true;
                    default:
                        return true;
                }
            }
            return true;
        }

        #endregion
    }
}