using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Drzava")]
    public class Drzava
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(40)]
        public string Naziv{get;set;}

        public List<Vakcina> PodrzaneVakcine{get;set;} //lista vakcina koje drzava podrzava 
        public List<Pasos> PodrzaniPasosi{get;set;} //lista pasosa koji su dozvoljeni da udju u konkretnu drzavu

        public List<Test> PodrzaniTestovi{get;set;} //lista tipova testova koje drzava podrzava za ulazak u istu
        //info o testovima i vakcinama
        
    
    }
    
}