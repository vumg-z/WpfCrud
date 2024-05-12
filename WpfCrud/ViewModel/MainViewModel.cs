using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Model;

namespace WpfCrud.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged

    {


        public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();
        public Contact SelectedContact { get; set; }

        // Properties for binding to the text boxes in the View
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        private bool _display3DCube;
        public bool Display3DCube
        {
            get => _display3DCube;
            set
            {
                if (_display3DCube != value)
                {
                    _display3DCube = value;
                    OnPropertyChanged(nameof(Display3DCubeVisibility));
                }
            }
        }

        public Visibility Display3DCubeVisibility => Display3DCube ? Visibility.Visible : Visibility.Collapsed;

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

            Display3DCube = CheckEasterEgg();

            if (CheckEasterEgg())
            {
                Display3DCube = true;
                MessageBox.Show("hola profe, si lee esto entonces estamos en una situacion complicada."); // This provides immediate feedback
            }
            else
            {
                Display3DCube = false;
            }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool CheckEasterEgg()
        {
            return FirstName?.Equals("chino", StringComparison.OrdinalIgnoreCase) == true;

                   //LastName?.Equals("chino", StringComparison.OrdinalIgnoreCase) == true &&
                   //Email?.Equals("chino", StringComparison.OrdinalIgnoreCase) == true &&
                   //Phone?.Equals("chino", StringComparison.OrdinalIgnoreCase) == true &&
                   //Address?.Equals("chino", StringComparison.OrdinalIgnoreCase) == true;
        }


    }
}
