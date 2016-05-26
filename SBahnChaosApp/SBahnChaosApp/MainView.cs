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
        public ObservableCollection<Line> Lines { get; set; }
        Button footerButton;
        ListView listView;

        public MainView()
        {
            Lines = new ObservableCollection<Line>();
            footerButton = new Button { Text = "+ Add" };
            listView = new ListView();
            
            Title = "SBahn Chaos App";

            setupMainView();
            initializedViews();
            subscribe();
            generatedDemoData();
            
        }

        private void setupMainView()
        {
            this.Detail = new ContentPage
            {
                Content = new StackLayout
                {

                    Children = {
                    listView, footerButton
                }
                }
            };

            ContentPage contenPage = new ContentPage { Title = "AppName", Content = new StackLayout { Children = { } } };
            contenPage.Icon = "ic_menu_white_48dp.png";
            this.Master = contenPage;
        }

        private void initializedViews()
        {
            listView.ItemTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();
                Image image = new Image();
                Label label = new Label { FontSize = 30 };
                Label delayLabel = new Label { TextColor = Color.Red };
                Label messageLabel = new Label();

                image.SetBinding(Image.SourceProperty, nameof(Line.Image));
                label.SetBinding(Label.TextProperty, nameof(Line.Name));
                delayLabel.SetBinding(Label.TextProperty, nameof(Line.Delay));
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

            listView.ItemsSource = Lines;
            listView.HasUnevenRows = true;
            listView.IsPullToRefreshEnabled = true;
        }

        private void subscribe()
        {
            footerButton.Clicked += (s, e) =>
            {
                SubscribeView view = new SubscribeView();
                view.CloseWithOK += (b, ve) => { Lines.Add(ve); Navigation.PopModalAsync(true); };
                NavigationPage page = new NavigationPage(view);
                Navigation.PushModalAsync(page);
            };

            listView.ItemTapped += (s, e) =>
            {
                DetailView view = new DetailView((Line)e.Item);
                NavigationPage page = new NavigationPage(view);
                Navigation.PushModalAsync(page);
                ((ListView)s).SelectedItem = null;

            };
            
            listView.Refreshing += async (s, e) => { ((ListView)s).IsRefreshing = false; };

        }

        private void generatedDemoData()
        {
            for (int i = 1; i <= 6; i++)
            {
                Random r = new Random(i);
                int d = r.Next(0, 11);
                var vl = new Line($"{i}", d, VehicleType.S);
                Lines.Add(vl);
            }

            Message message = new Message("fährt erst ab S.-Hbf um 16:03", DateTime.Now);
            message.fromStop = new Stop("S.-Schwabstr.", new DateTime(2016, 5, 24, 15, 57, 0));
            message.toStop = new Stop("Böblingen", new DateTime(2016, 5, 24, 16, 56, 0));
            Lines.First(sb => sb.Name == "6" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("fährt erst ab S.-Zuffenhausen um 16:27", DateTime.Now);
            message.fromStop = new Stop("S.-Schwabstr.", new DateTime(2016, 5, 24, 16, 12, 0));
            message.toStop = new Stop("Weil der Stadt", new DateTime(2016, 5, 24, 16, 57, 0));
            Lines.First(sb => sb.Name == "6" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("Komplettausfall", DateTime.Now);
            message.fromStop = new Stop("Backnang", new DateTime(2016, 5, 24, 15, 56, 0));
            message.toStop = new Stop("S.-Vaihingen", new DateTime(2016, 5, 24, 16, 44, 0));
            Lines.First(sb => sb.Name == "3" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("fährt erst ab Grunbach um 15:28", DateTime.Now);
            message.fromStop = new Stop("Schorndorf", new DateTime(2016, 5, 24, 15, 18, 0));
            message.toStop = new Stop("Filderstadt", new DateTime(2016, 5, 24, 16, 26, 0));
            Lines.First(sb => sb.Name == "2" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("fährt erst ab S.-Rohr um 9:38", DateTime.Now);
            message.fromStop = new Stop("Herrenberg", new DateTime(2016, 5, 24, 9, 16, 0));
            message.toStop = new Stop("Kirchheim", new DateTime(2016, 5, 24, 10, 38, 0));
            Lines.First(sb => sb.Name == "1" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            message = new Message("fährt erst ab S.-Feuerbach um 9:14", DateTime.Now);
            message.fromStop = new Stop("S.-Schwabstr.", new DateTime(2016, 5, 24, 9, 2, 0));
            message.toStop = new Stop("Backnang", new DateTime(2016, 5, 24, 9, 25, 0));
            Lines.First(sb => sb.Name == "4" && sb.VehicleType == VehicleType.S).Messages.Add(message);

            var u = new Line("1", 1, VehicleType.U);
            Lines.Add(u);
        }

    }
}
