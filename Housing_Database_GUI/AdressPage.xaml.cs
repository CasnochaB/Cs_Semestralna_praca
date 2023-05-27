﻿using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Housing_Database_GUI
{
    /// <summary>
    /// Interaction logic for AdressPage.xaml
    /// </summary>
    public partial class AddressPage : UserControl
    {
        public HousingDatabase database;
        private FilterManager filterManager;
        public class AddressItem
        {
            public Person Person { get; set; }

            public AddressItem(Person person,string address)
            {
                Person = person;
                Address = address;
            }

            public string Address { get; set; }
        }

        protected enum FilterPredicate
        {
            FirstName,
            LastName,
            PersonID,
            Address
        }

        public AddressPage() : this(new HousingDatabase())
        {
        }

        public AddressPage(HousingDatabase housingDatabase)
        {
            InitializeComponent();
            database = housingDatabase;
            AddressListReset();
            filterManager = new FilterManager();
        }

        public virtual void AddressListReset()
        {
            foreach (var housing in database)
            {
                foreach (var unit in housing)
                {
                    foreach (var person in unit)
                    {
                        AddressItem addressItem = new AddressItem(person,unit.unitIdentifier);
                        Address_ListBox.Items.Add(addressItem);
                    }
                }
            }
            Address_ListBox.Items.Filter = null;
            Address_ListBox.Items.Refresh();
            DiplayPeopleCount();
        }
          
        protected bool AddressFilterPredicate(object item)
        {
            AddressItem addressItem = (AddressItem)item;
            string filterText = Filter_TextBox.Text;
            return addressItem.Address.Contains(filterText);
        }

        protected void ApplyFilter(FilterPredicate filterPredicate)
        {
            var text = Filter_TextBox.Text;
            switch (filterPredicate)
            {
                case FilterPredicate.FirstName:
                    Address_ListBox.Items.Filter = item => { return filterManager.FirstNameFilterPredicate(((AddressItem)item).Person, text); }; break;
                case FilterPredicate.LastName:
                    Address_ListBox.Items.Filter = item => { return filterManager.LastNameFilterPredicate(((AddressItem)item).Person, text); }; break;
                case FilterPredicate.Address:
                    Address_ListBox.Items.Filter = AddressFilterPredicate; break;
                case FilterPredicate.PersonID:
                    Address_ListBox.Items.Filter = item => { return filterManager.PersonIDFilterPredicate(((AddressItem)item).Person, text); }; break;
            }
            Address_ListBox.Items.Refresh();
            DiplayPeopleCount();
        }

        public virtual void DiplayPeopleCount()
        {
            Count_Label.Content = Address_ListBox.Items.Count + "/" + database.GetNumberOfInstances();
        }

        protected void AddToExport_Button_Click(object sender, RoutedEventArgs e)
        {
            var addressItem = GetSelectedItem();
            if (addressItem != null)
            {
                database.AddToExport(addressItem.Person,addressItem.Address);
            }
        }

        protected void ResetFilters_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Filter_TextBox != null)
            {
                Filter_TextBox.Text = "";
            }
        }

        protected void FilterType_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Filter_TextBox != null)
            {
                Filter_TextBox.Text = "";
            }
        }

        protected AddressItem? GetSelectedItem()
        {
            return (AddressItem?)Address_ListBox.SelectedItem;
        }

        protected void Filter_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter((FilterPredicate)FilterType_ComboBox.SelectedIndex);
        }
    }
}
