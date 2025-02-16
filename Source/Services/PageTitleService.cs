using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public class PageTitleService
    {
        public event Action OnTitleChanged = delegate { };
        private string _title = "Titre par défaut";

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnTitleChanged?.Invoke();
                }
            }
        }
    }
}
