using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole
{
    [Table("Afspraken")]
    public class Afspraak : BasisKlasse
    {
        [Key]
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Uur { get; set; }
        public int Minuut { get; set; }

        public int ArtsId { get; set; }

        public int PatientId { get; set; }

        public Afspraak() { }
        public Afspraak(DateTime datum, int uur, int minuut, int artsid, int patientid)
        {
            Datum = datum;
            Uur = uur;
            Minuut = minuut;
            ArtsId = artsid;
            PatientId = patientid;
        }

        public Arts Arts { get; set; }
        public Patient Patient { get; set; }

        public override string ToString()
        {
            return $"{Arts.Naam} {Arts.Voornaam}: {Patient.Naam} {Patient.Voornaam} op {Datum.ToShortDateString()} om {Uur}:{Minuut}";
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Naam" && string.IsNullOrWhiteSpace(Datum.ToString()))
                {
                    return "Datum moet ingevuld zijn!";
                }
                if (columnName == "Uur" && Uur == 0)
                {
                    return "Uur moet ingevuld zijn!";
                }
                if (columnName == "Minuut" && Minuut == 0)
                {
                    return "Uur moet ingevuld zijn!";
                }

                return "";
            }
        }
    }
}
