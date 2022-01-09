using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("DrzavaVakcina")]
    public class DrzavaVakcina{

        [Key]
        public int ID { get; set; }

        [JsonIgnore]
        public Vakcina vakcina{get;set;}

        [JsonIgnore]
        public Drzava drzava{get;set;}

        [Range(1,4)]
        public int doza{get;set;}



    }

}
