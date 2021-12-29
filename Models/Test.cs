
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public enum TipTesta
    {
        PCR,
        ANTIGENSKI,
        ANTITELA

    }
    [Table("Test")]
    public class Test
    {
        
        public int ID { get; set; }
        public TipTesta Tip{get;set;}

        public int Starost{get;set;} //starost testa izrazena u satima
        
        public List<Drzava> PodrzaneDrzave{get;set;}//za koje drzave vazi
    }
}