using Menekulj.Model;

using Menekulj.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ViewModelCell : ViewModelBase
    {
  

        private Menekulj.Model.Cell cellType;

        public Menekulj.Model.Cell CellType { get { return cellType; } set { cellType = (Menekulj.Model.Cell)value; OnPropertyChanged();} }

   

        public int Row { get; private set; }
        public int Col { get;private  set; } 

        public int Id { get; private  set; }

        public ViewModelCell(int row, int col,int id )
        {
            this.Row = row;
            this.Col = col;
            this.Id = id;
        }

    }
}
