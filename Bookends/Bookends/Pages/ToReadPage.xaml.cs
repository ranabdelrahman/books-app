using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Bookends
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToReadPage : ContentPage
    {
        private bool? currentOrientationLandscape;
        public ToReadPage()
        {
            InitializeComponent();
            DB.OpenConnection();
            ResetListViewSources();

            List<VolumeInfo> books = new List<VolumeInfo>();
            Label tlabel = new Label();

            foreach (var entry in DB.conn.Table<VolumeInfo>())
            {

                tlabel.Text = entry.title;
                entry.title = tlabel.Text;
                books.Add(entry);

            }

            bookList.ItemsSource = books.ToList();
        }

        private void ResetListViewSources()
        {
            bookList.ItemsSource = DB.conn.Table<VolumeInfo>().ToList();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            VolumeInfo info = bookList.SelectedItem as VolumeInfo;
            if (info != null)
            {
                int v = DB.conn.Delete(info);
                if (v > 0)
                {
                    bookList.SelectedItem = null;
                    ResetListViewSources();
                }
            }
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
    }
}

