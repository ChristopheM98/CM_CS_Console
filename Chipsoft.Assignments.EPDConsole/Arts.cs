using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole
{
    [Table("Artsen")]
    public class Arts : Persoon
    {
        [Key]
        public int Id { get; set; }

        public Arts(string voornaam, string naam, string adres, string gemeente) : base(voornaam, naam, adres, gemeente){}

        public List<Afspraak> Afspraken { get; set; }
    }    
}
