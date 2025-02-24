using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class ChangePasswordModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
