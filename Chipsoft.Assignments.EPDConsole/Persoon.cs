using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole
{
    public class Persoon : BasisKlasse
    {
        [Key]
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public string Gemeente { get; set; }

        public Persoon(string voornaam, string naam, string adres, string gemeente) 
        {
            Voornaam = voornaam;
            Naam = naam;
            Adres = adres;
            Gemeente = gemeente;
        }

        public override string ToString()
        {
            return $"{Naam} {Voornaam} - {Adres} {Gemeente}";
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Voornaam" && string.IsNullOrWhiteSpace(Voornaam))
                {
                    return "Voornaam moet ingevuld zijn!";
                }
                if (columnName == "Naam" && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Naam moet ingevuld zijn!";
                }
                if (columnName == "Adres" && string.IsNullOrWhiteSpace(Adres))
                {
                    return "Adres moet ingevuld zijn!";
                }
                if (columnName == "Gemeente" && string.IsNullOrWhiteSpace(Gemeente))
                {
                    return "Gemeente moet ingevuld zijn!";
                }

                return "";
            }
        }
    }
}
