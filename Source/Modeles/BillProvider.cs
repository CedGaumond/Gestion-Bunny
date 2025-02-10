using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class BillProvider
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }
        public byte[] BillFile { get; set; }
        public bool IsDelivered { get; set; }
    }
}
