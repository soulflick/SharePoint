using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace SharePoint.Classes
{
    public class ItemDetail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Raise(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        private StockItem _item = null;
        public StockItem Item
        {
            get => _item;
            set
            {
                _item = value;
                Raise(nameof(Item));
            }
        }

        public List<MyPicture> Images { get; set; } = new List<MyPicture>();
        public ItemDetail(StockItem item) => Item = item;
    }
}
