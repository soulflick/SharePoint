using System.ComponentModel;

namespace SharePoint.Classes
{
    public class StockItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Raise(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                Raise(nameof(IsVisible));
            }
        }

        private string _partID = null;
        public string PartID
        {
            get => _partID;
            set
            {
                _partID = value;
                Raise(nameof(PartID));
            }
        }

        private string _partDescription = null;
        public string PartDescription
        {
            get => _partDescription;
            set
            {
                _partDescription = value;
                Raise(nameof(PartDescription));
            }
        }


        private string _zone = null;
        public string Zone
        {
            get => _zone;
            set
            {
                _zone = value;
                Raise(nameof(Zone));
            }
        }


        private string _address = null;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                Raise(nameof(Address));
            }
        }


        private string _location = null;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                Raise(nameof(Location));
            }
        }


        private string _itemNumber = null;
        public string ItemNumber
        {
            get => _itemNumber;
            set
            {
                _itemNumber = value;
                Raise(nameof(ItemNumber));
            }
        }


        private string _weight = null;
        public string Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                Raise(nameof(Weight));
            }
        }


        private string _size = null;
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                Raise(nameof(Size));
            }
        }


        private string _owner = null;
        public string Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                Raise(nameof(Owner));
            }
        }


        private string _phoneNumber = null;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                Raise(nameof(PhoneNumber));
            }
        }


        private string _eMail = null;
        public string eMail
        {
            get => _eMail;
            set
            {
                _eMail = value;
                Raise(nameof(eMail));
            }
        }

        private string _comment = null;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                Raise(nameof(Comment));
            }
        }
    }
}
