using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfCrud.Model;

namespace WpfCrud.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();
        public Contact SelectedContact { get; set; }

        // Properties for binding to the text boxes in the View
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public ICommand AddContactCommand { get; }
        public ICommand UpdateContactCommand { get; }
        public ICommand DeleteContactCommand { get; }

        public MainViewModel()
        {
            AddContactCommand = new RelayCommand(AddContact);
            UpdateContactCommand = new RelayCommand(UpdateContact, CanUpdateOrDelete);
            DeleteContactCommand = new RelayCommand(DeleteContact, CanUpdateOrDelete);
        }

        private void AddContact()
        {
            var newContact = new Contact
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Phone = this.Phone,
                Address = this.Address
            };
            Contacts.Add(newContact);
        }

        private bool CanUpdateOrDelete()
        {
            return SelectedContact != null;
        }

        private void UpdateContact()
        {
            if (SelectedContact != null)
            {
                // No need to re-assign, PropertyChanged will handle the update
                SelectedContact.FirstName = this.FirstName;
                SelectedContact.LastName = this.LastName;
                SelectedContact.Email = this.Email;
                SelectedContact.Phone = this.Phone;
                SelectedContact.Address = this.Address;
            }
        }


        private void DeleteContact()
        {
            if (SelectedContact != null)
            {
                Contacts.Remove(SelectedContact);
            }
        }
    }
}
