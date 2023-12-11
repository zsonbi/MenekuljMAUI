#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menekulj.ViewModel
{
    public class StoredGameViewModel : ViewModelBase
    {
        /// <summary>
        /// Betöltés parancsa.
        /// </summary>
        public DelegateCommand? LoadGameCommand { get; set; }

        /// <summary>
        /// Mentés parancsa.
        /// </summary>
        public DelegateCommand? SaveGameCommand { get; set; }

        private string name;
        private DateTime modified;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; OnPropertyChanged(nameof(Name)); }
        }
        public DateTime Modified
        {
            get { return this.modified; }
            set { this.modified = value; OnPropertyChanged(nameof(Modified));}
        }

        public StoredGameViewModel(string name, DateTime modified)
        {
            this.Name= name;
            this.Modified = modified;   
        }
    }

}
