using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole
{
    [Table("Patients")]
    public class Patient : Persoon
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }

        public Patient(string voornaam, string naam, string adres, string gemeente, string email, string telefoonnummer)
            : base(voornaam, naam, adres, gemeente)
        {
            Email = email;
            Telefoonnummer = telefoonnummer;
        }

        public List<Afspraak> Afspraken { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", {Email} - {Telefoonnummer}";
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Email" && string.IsNullOrWhiteSpace(Email))
                {
                    return "Email moet ingevuld zijn!";
                }
                if (columnName == "Telefoonnummer" && string.IsNullOrWhiteSpace(Telefoonnummer))
                {
                    return "Telefoonnummer moet ingevuld zijn!";
                }

                return base[columnName];
            }
        }
    }
}
