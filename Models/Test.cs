
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        
        [JsonIgnore]
        public List<Drzava> PodrzaneDrzave{get;set;}//za koje drzave vazi
    }
}