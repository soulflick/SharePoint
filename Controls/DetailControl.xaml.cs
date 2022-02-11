using SharePoint.Classes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace SharePoint.Controls
{
    public partial class DetailControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Raise(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        public static DetailControl Instance = null;
        public DetailControl()
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
        }

        public List<ItemDetail> Details { get; set; } = new List<ItemDetail>();

        private ItemDetail _detail = null;
        public ItemDetail Detail
        {
            get => _detail;
            set
            {
                _detail = value;
                Raise(nameof(Detail));
            }
        }

        public void Select(StockItem item)
        {
            if (!Details.Any(__detail => __detail.Item == item))
                Details.Add(new ItemDetail(item));

            var detail = Details.First(__detail => __detail.Item == item);

            Detail = detail;
        }
    }
}
