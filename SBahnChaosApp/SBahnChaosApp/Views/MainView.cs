using SBahnChaosApp.Core;
using SBahnChaosApp.FileManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBahnChaosApp.Views
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

            MainFileManager.Initialize();

            setList();

            Title = "SBahn Chaos App";
            
            setupMainView();
            initializedViews();
            subscribe();
            
        }

        private void setList()
        {
            Lines = MainFileManager.GetSubscribedChannels();
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
                view.CloseWithOK += (b, ve) => {
                    Lines.Add(ve);
                    MainFileManager.SaveLine(ve);
                    Navigation.PopModalAsync(true); };
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
        

    }
}
