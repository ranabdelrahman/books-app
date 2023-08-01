using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System.Web.UI;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace Bookends
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage
    {
        RestService _restService;
        public string SearchEntry; // 
        public BooksPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Home");
            _restService = new RestService();
            SearchEntry = entry.Text; //
           
            //Image book1 = new Image { Source="girlOnTrain.jpg"};
            //Image book2 = new Image { Source = "SOC.jpg" };

            //List<Book> books = new List<Book>();
            //books.Add(new Book { items.volumeInfo.Title="The Girl on the Train", Author="Paula Hawkins", Picture=book1});
            //books.Add(new Book { Title="Six of Crows", Author="Leigh B. Ardugho", Picture =book2});
            //lv.ItemsSource = books;
        }
        Results resultPage;
        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    DB.OpenConnection();
        //    Button b = (Button)sender;  
        //    VolumeInfo item = lv.SelectedItem as VolumeInfo;
        //    //DB.OpenConnection();
        //    DB.conn.Insert(item); //maybe I only need this line. find a way to get sender button's info (or selected item since it's listview)
              
        //}

        private async void searchButton_Clicked(object sender, EventArgs e) 
        {
            //resultPage.thisEntry = entry.Text;
            resultPage =new Results();
           resultPage.thisEntry = entry.Text;
            if(entry.Text!=null)
                await Navigation.PushAsync(resultPage, true);

        }

        string GenerateRequestUri(string endpoint, string title)
        {
            string requestUri = endpoint;
            requestUri += $"?q={title}";
            return requestUri; 
        }


    }
}