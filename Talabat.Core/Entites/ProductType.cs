using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
    public class ProductType:BaseEntity
    {
        [Required]
        public string Name { get; set; }    
    }
}
