using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Housing_Database_GUI.HousingPageWindows
{
    public class FilterPersonWindow : FilterHousingUnitsWindow
    {
        public FilterPersonWindow() : base() 
        {
            Min_Label.Content = "Minimum age of person: ";
            Max_Label.Content = "Maximum age of person: ";
        }



    }
}
