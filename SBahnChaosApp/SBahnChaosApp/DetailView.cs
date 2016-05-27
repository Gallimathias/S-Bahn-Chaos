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
        private Line selectedItem;
        private ObservableCollection<Message> messages;

        ListView listView;

        public DetailView(Line selectedItem)
        {
            Title = selectedItem.ToString();
            this.selectedItem = selectedItem;

            messages = selectedItem.MessagesToOBSCollection();
            messages.OrderBy(m => m.DateTime);

            listView = new ListView();

            setupListView();

            listView.ItemSelected += (s, e) => { ((ListView)s).SelectedItem = null; };

            Content = listView;
        }

        private void setupListView()
        {
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
        }
    }
}
