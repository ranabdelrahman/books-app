using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookends
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Results : ContentPage
    {
        RestService _restService;
        public string thisEntry;
        private bool? currentOrientationLandscape;
        public Results()
        {
            InitializeComponent();
            _restService = new RestService();
           
        }
        public void PlaySound(string file)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            String thisNameSpace = "Bookends";
            Stream audioStream = assembly.GetManifestResourceStream(thisNameSpace + "." + file);
            ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load(audioStream);
            player.Play();
        }
        string GenerateRequestUri(string endpoint, string title)
        {
            string requestUri = endpoint;
            requestUri += $"?q={title}";
            return requestUri;
        }

        protected override async void OnAppearing()
        {
            List<VolumeInfo> bookList = new List<VolumeInfo>();
            string uriRequest = GenerateRequestUri("https://www.googleapis.com/books/v1/volumes", thisEntry); 
          
            Book book = await _restService.GetBookData(uriRequest);
            Label tlabel = new Label();
            Label alabel = new Label();

                                            
                if (book.items == null)
                {
                    res.Text = null;
                    res.Text = "No books found";
                }

                //for (int i = 0; i < book.totalItems; i++)
                //{
                foreach (var citem in book.items)
                {
                    if (citem.volumeInfo.authors != null)
                    {
                        tlabel.Text = citem.volumeInfo.title;
                        citem.volumeInfo.title = tlabel.Text;

                        alabel.Text = citem.volumeInfo.authors[0];
                        citem.volumeInfo.authors[0] = alabel.Text;

                        //
                    }
                    bookList.Add(citem.volumeInfo);
                }
                //}
                lv.ItemsSource = bookList.ToList();
            }
               

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            bool isNowLandscape = width > height;
            if (isNowLandscape != currentOrientationLandscape)
            {
                layout.Orientation = isNowLandscape ? StackOrientation.Horizontal : StackOrientation.Vertical;
                currentOrientationLandscape = isNowLandscape;
            }
        }

        
        private void Button_Clicked(object sender, EventArgs e)
        {
            PlaySound("addrem.mp3");
            Button b = (Button)sender; 
            b.IsEnabled = false;
            DB.OpenConnection();
            VolumeInfo item = lv.SelectedItem as VolumeInfo;
            DB.conn.Insert(item);
        }

        private async void moreButton_Clicked(object sender, EventArgs e)
        {
            VolumeInfo item = lv.SelectedItem as VolumeInfo;
            Uri uri;
            try
            {
                uri = new Uri(item.infoLink);
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch(Exception ex) { }
        }
    }
}