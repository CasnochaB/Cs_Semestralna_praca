using Database;

namespace Housing_Database_GUI
{
    internal class AdressExportPage : AddressPage
    {
        public AdressExportPage(HousingDatabase database) : base(database)
        {
            AddToExport_Button.Content = "Clear export";
        }

        override public void AddressListReset()
        {
            foreach (var personAddress in database.exportHousings)
            {
                AddressItem addressItem = new AddressItem(personAddress.Value, personAddress.Key.address);
                Address_ListBox.Items.Add(addressItem);
            }
            DisplayPeopleCount();
        }

        protected override void AddToExportButton()
        {
            database.ClearExport();
            Address_ListBox.Items.Clear();
            DisplayPeopleCount();
        }

        public override void DisplayPeopleCount()
        {
            Count_Label.Content = Address_ListBox.Items.Count + "/" + database.exportHousings.Count;
        }
    }
}
