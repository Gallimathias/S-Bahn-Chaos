using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBahnChaosApp
{
    public class MainView : MasterDetailPage
    {
        public ObservableCollection<Vehicle> vehicles;
        public MainView()
        {
            vehicles = new ObservableCollection<Vehicle>();
            var footerbutton = new Button { Text = "+ Add" };
            footerbutton.Clicked += (s, e) =>
            {
                SubscribeView view = new SubscribeView();
                view.CloseWithOK += (b, ve) => { vehicles.Add(ve); Navigation.PopModalAsync(true); };
                NavigationPage page = new NavigationPage(view);
                Navigation.PushModalAsync(page);
            };

            var listview = new ListView { };
            listview.ItemsSource = vehicles;
            listview.HasUnevenRows = true;
            listview.ItemTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();
                Image image = new Image();
                Label label = new Label { FontSize = 30 };
                Label delayLabel = new Label { TextColor = Color.Red };
                Label messageLabel = new Label();

                image.SetBinding(Image.SourceProperty, nameof(Vehicle.Image));
                label.SetBinding(Label.TextProperty, nameof(Vehicle.Name));
                delayLabel.SetBinding(Label.TextProperty, nameof(Vehicle.Delay));
                //messageLabel.SetBinding(Label.TextProperty, nameof(Message.Header));

                var grid = new Grid
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    RowDefinitions = { new RowDefinition { Height = GridLength.Auto } },
                    ColumnDefinitions = {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                grid.Children.Add(image);
                grid.Children.Add(label, 1, 0);
                grid.Children.Add(delayLabel, 2, 0);
                grid.Children.Add(messageLabel, 3, 0);

                cell.View = grid;
                return cell;
            });

            listview.ItemTapped += (s, e) =>
             {
                 DetailView view = new DetailView((Vehicle)e.Item);
                 NavigationPage page = new NavigationPage(view);
                 Navigation.PushModalAsync(page);
                 ((ListView)s).SelectedItem = null;

             };


            listview.IsPullToRefreshEnabled = true;
#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
            listview.Refreshing += async (s, e) => { ((ListView)s).IsRefreshing = false; };
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
            for (int i = 1; i <= 6; i++)
            {
                Random r = new Random(i);
                int d = r.Next(0, 11);
                var vl = new Vehicle($"{i}", d, VehicleType.S);
                vehicles.Add(vl);
            }

            Message message = new Message("fährt erst ab S.-Hbf um 16:03",DateTime.Now);
            message.fromStop = new Stop("S.-Schwabstr.", new DateTime(2016, 5, 24, 15, 57, 0));
            message.toStop = new Stop("Böblingen", new DateTime(2016, 5, 24, 16, 56, 0));
            vehicles.First(sb => sb.Name == "6" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("fährt erst ab S.-Zuffenhausen um 16:27", DateTime.Now);
            message.fromStop = new Stop("S.-Schwabstr.", new DateTime(2016, 5, 24, 16, 12, 0));
            message.toStop = new Stop("Weil der Stadt", new DateTime(2016, 5, 24, 16, 57, 0));
            vehicles.First(sb => sb.Name == "6" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("Komplettausfall", DateTime.Now);
            message.fromStop = new Stop("Backnang", new DateTime(2016, 5, 24, 15, 56, 0));
            message.toStop = new Stop("S.-Vaihingen", new DateTime(2016, 5, 24, 16, 44, 0));
            vehicles.First(sb => sb.Name == "3" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("fährt erst ab Grunbach um 15:28", DateTime.Now);
            message.fromStop = new Stop("Schorndorf", new DateTime(2016, 5, 24, 15, 18, 0));
            message.toStop = new Stop("Filderstadt", new DateTime(2016, 5, 24, 16, 26, 0));
            vehicles.First(sb => sb.Name == "2" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("fährt erst ab S.-Rohr um 9:38", DateTime.Now);
            message.fromStop = new Stop("Herrenberg", new DateTime(2016, 5, 24, 9, 16, 0));
            message.toStop = new Stop("Kirchheim", new DateTime(2016, 5, 24, 10, 38, 0));
            vehicles.First(sb => sb.Name == "1" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("fährt erst ab S.-Feuerbach um 9:14", DateTime.Now);
            message.fromStop = new Stop("S.-Schwabstr.", new DateTime(2016, 5, 24, 9, 2, 0));
            message.toStop = new Stop("Backnang", new DateTime(2016, 5, 24, 9, 25, 0));
            vehicles.First(sb => sb.Name == "4" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            var u = new Vehicle("1", 1, VehicleType.U);
            vehicles.Add(u);

            Title = "SBahn Chaos App";
            

            this.Detail = new ContentPage
            {
                Content = new StackLayout
                {

                    Children = {
                    listview, footerbutton
                }
                }
            };

            Button save = new Button { Text = "Sync" };
            
            TableView settings = new TableView
            {
                Intent = TableIntent.Settings,
                Root = new TableRoot
                {
                    new TableSection ("Save and Load") {
                        new ViewCell { View = save},
                        new TextCell {
                            Text = "TextCell Text",
                            Detail = "TextCell Detail"
                        },
                        new EntryCell {
                            Label = "EntryCell:",
                            Placeholder = "default keyboard",
                            Keyboard = Keyboard.Default
                        }
                    },
                    new TableSection ("Section 2 Title") {
                        new EntryCell {
                            Label = "Another EntryCell:",
                            Placeholder = "phone keyboard",
                            Keyboard = Keyboard.Telephone
                        },
                        new SwitchCell {
                            Text = "SwitchCell:"
                        }
                    }
                }
            };
            ContentPage contenPage = new ContentPage { Title = "AppName", Content = new StackLayout { Children = { settings} } };
            contenPage.Icon = "ic_menu_white_48dp.png";
            this.Master = contenPage;

        }

    }
}
