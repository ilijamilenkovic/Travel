using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Pasos")]
    public class Pasos
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Drzavljanstvo{get;set;}

        public List<Drzava> PodrzaneDrzave{get;set;} //u koje drzave moze da ide pasos


    }
}