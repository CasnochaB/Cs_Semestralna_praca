using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Housing_Database_GUI
{
    internal class AdressExportPage : AddressPage
    {
        public AdressExportPage(HousingDatabase database) : base(database) {
            AddToExport_Button.Visibility = System.Windows.Visibility.Hidden;
        }

        override public void AddressListReset()
        {
            foreach (var personAddress in database.exportHousings)
            {
                AddressItem addressItem = new AddressItem(personAddress.Value, personAddress.Key.address);
                Address_ListBox.Items.Add(addressItem);
            }
            DiplayPeopleCount();
        }

        public override void DiplayPeopleCount()
        {
            Count_Label.Content = Address_ListBox.Items.Count + "/" + database.exportHousings.Count;
        }
    }
}
