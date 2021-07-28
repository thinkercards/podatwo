using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class Organization
    {
        [Key]
        public int Org_ID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int CountryID { get; set; }
        [NotMapped]
        public string country { get; set; }
        public int Currency_ID { get; set; }
        [NotMapped]
        public string currency { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string VATID { get; set; }
        public string TaxWording { get; set; }
        public string DBConnection { get; set; }
 
        public DateTime CreatedDate { get; set; }
        public int CreatedByUser { get; set; }
        public string CreatedByProgram { get; set; }

        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }
        public string LastModifiedByProgram { get; set; }
        public string ExternalReference { get; set; }
        public string Comments { get; set; }
        public string Zipcode { get; set; }
        [NotMapped]
        public int userId { get; set; }
        public int IsActive { get; set; }
    }
}
