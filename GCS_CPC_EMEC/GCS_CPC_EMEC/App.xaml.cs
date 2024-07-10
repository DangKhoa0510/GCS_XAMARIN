using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GCS_CPC_EMEC.Services;
using GCS_CPC_EMEC.Views;
using System.Diagnostics;

namespace GCS_CPC_EMEC
{
    public partial class App : Application
    {
        public static GCS_Dbcontext gCS_Dbcontext;
        public static string PathDatabase = "";


        public App(string dbPath)
        {
            InitializeComponent();
            PathDatabase = dbPath;
            Debug.WriteLine("$database local at : " + dbPath);
            gCS_Dbcontext = new GCS_Dbcontext(dbPath);
            Device.SetFlags(new[] { "SwipeView_Experimental", "CarouseView_Experimental","IndicatorView_Experimental" });
            MainPage = new AppShell(); 
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
