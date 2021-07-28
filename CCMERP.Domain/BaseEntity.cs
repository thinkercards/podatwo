using System.ComponentModel.DataAnnotations;

namespace CCMERP.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
