using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2.Models
{
    public class Office
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MaxLength(50)]
        public string? Country { get; set; }

        [StringLength(3)]
        public string? CountryCode { get; set; }

        [StringLength(3)]
        public string? Currency { get; set; }

        public ICollection<Asset> Assets { get; set; }   // Nav.property

    }
}
