using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Vakcina")]
    public class Vakcina
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Proizvodjac { get; set; }

        
        
        public List<DrzavaVakcina> PodrzaneDrzave{get;set;} //lista drzava u koje gradjani sa ovom primljenom vakcinom mogu da idu

    }
}