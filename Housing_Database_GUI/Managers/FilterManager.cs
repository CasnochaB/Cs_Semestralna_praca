using Database;

namespace Housing_Database_GUI
{
    internal class FilterManager
    {
        public FilterManager()
        {

        }
        public bool LastNameFilterPredicate(Person person, string filterText)
        {
            return person.personalData.LastName.Contains(filterText);
        }

        public bool PersonIDFilterPredicate(Person person, string filterText)
        {
            return person.personalData.IdentificationNumber.StartsWith(filterText);
        }

        public bool FirstNameFilterPredicate(Person person, string filterText)
        {
            return person.personalData.FirstName.Contains(filterText);
        }

        public bool FullNameFilterPredicate(Person person, string filterText)
        {
            return person.fullName.Contains(filterText);
        }
    }
}