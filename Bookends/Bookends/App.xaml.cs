using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;


namespace Bookends
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());

           // MainPage = new NavigationPage(new BooksPage());

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
        protected override void OnStart()
        {
            PlaySound("start.mp3");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
