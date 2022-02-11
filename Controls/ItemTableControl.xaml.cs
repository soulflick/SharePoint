using SharePoint.Classes;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace SharePoint.Controls
{
    public partial class ItemTableControl : UserControl
    {
        public ObservableCollection<StockItem> ItemCollection { get; set; } = new ObservableCollection<StockItem>();

        public ItemTableControl()
        {
            InitializeComponent();

            ItemCollection.Add(new StockItem()
            {
                Address = "Street 123, 7983",
                eMail = "phone@e.mail",
                ItemNumber = "1455637892",
                Location = "Shelve 123",
                Owner = "obi central",
                PartDescription = "new part, no errors",
                PartID = "abcdefgh",
                PhoneNumber = "03924 7983 2304",
                Size = "4m x 2m",
                Weight = "798kg",
                Zone = "798.2"
            });

            ItemCollection.Add(new StockItem());

            DataContext = this;
        }

        private void TrackTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (ItemCollection.Count <= 1)
                ItemCollection.Add(new StockItem());
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ItemCollection.Add(new StockItem());
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var str = SearchBox.Text;
            if (str.IndexOf(",") < 0)
            {
                foreach (var item in ItemCollection)
                    item.IsVisible =
                        (item.Address?.Contains(str) ?? true) ||
                        (item.eMail?.Contains(str) ?? true) ||
                        (item.ItemNumber?.Contains(str) ?? true) ||
                        (item.Location?.Contains(str) ?? true) ||
                        (item.Owner?.Contains(str) ?? true) ||
                        (item.PartDescription?.Contains(str) ?? true) ||
                        (item.PartID?.Contains(str) ?? true) ||
                        (item.PhoneNumber?.Contains(str) ?? true) ||
                        (item.Size?.Contains(str) ?? true) ||
                        (item.Weight?.Contains(str) ?? true) ||
                        (item.Zone?.Contains(str) ?? true);
                return;
            }

            string[] tokens = str.Split(',');

            for (int row = 0; row < ItemCollection.Count; row++)
            {
                var item = ItemCollection[row++];
                bool visible = true;
                for (int col = 0; col < ItemTable.Columns.Count; col++)
                {
                    if (col >= tokens.Length) break;
                    var token = tokens[col];
                    if (string.IsNullOrWhiteSpace(token)) continue;

                    var header = ItemTable.Columns[col].Header.ToString();

                    if (header == "PartID" && !item.PartID.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "Part Description" && !item.PartDescription.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "Zone" && !item.Zone.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "Address" && !item.Address.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "Location" && !item.Location.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "Item Number" && !item.ItemNumber.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "Weight" && !item.Weight.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "Size" && !item.Size.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "Owner" && !item.Owner.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "Phone Number" && !item.PhoneNumber.Contains(token))
                    {
                        visible = false;
                        break;
                    }
                    else if (header == "eMail" && !item.eMail.Contains(token))
                    {
                        visible = false;
                        break;
                    }

                }
                item.IsVisible = visible;
                row++;
            }
        }

        private void ItemTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ItemTable.SelectedItem as StockItem;
            if (item == null) return;

            DetailControl.Instance.Select(item);
        }
    }
}
