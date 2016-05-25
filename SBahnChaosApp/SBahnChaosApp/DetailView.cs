using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBahnChaosApp
{
    public class DetailView : ContentPage
    {
        private Vehicle selectedItem;
        private ObservableCollection<Message> messages;
        public DetailView(Vehicle selectedItem)
        {
            Title = selectedItem.Name;
            this.selectedItem = selectedItem;
            
            messages = selectedItem.MessagesToOBSCollection();
            messages.OrderBy(m => m.DateTime);

            var listView = new ListView { };
            listView.ItemSelected += (s, e) => { ((ListView)s).SelectedItem = null; };
            
            listView.ItemsSource = messages;
            listView.HasUnevenRows = true;
            listView.ItemTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();
                Label header = new Label();
                header.SetBinding(Label.TextProperty, nameof(Message.Header));

                Label body = new Label();
                body.SetBinding(Label.TextProperty, nameof(Message.Text));

                body.LineBreakMode = LineBreakMode.CharacterWrap;
                Label empty = new Label();

                cell.View = new StackLayout
                {
                    Children = { header, body, empty }
                };

                return cell;
            });

            Content = listView;
        }
    }
}
