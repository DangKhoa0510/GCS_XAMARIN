using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Bluetooth;
using Android.Content;

namespace GCS_CPC_EMEC.Droid
{
    [Activity(Label = "GCS_CPC_EMEC", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const int RequestToEnableBluetooth = 1234;
        BluetoothDeviceReceiver receiver;
        IntentFilter filter1;
        IntentFilter filter2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            var dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "database.sqlite");
            var bluetooth = BluetoothAdapter.DefaultAdapter;
            if (bluetooth == null)
            {
                // device does not support Bluetooth
            }
            else
            {
                State state = bluetooth.State;
            }

            if (bluetooth?.IsEnabled == false)
            {
                var intent = new Intent(
                BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(intent, RequestToEnableBluetooth);
            }
            receiver = new BluetoothDeviceReceiver();
            filter1 = new IntentFilter(BluetoothDevice.ActionAclConnected);
            filter2 = new IntentFilter(BluetoothDevice.ActionAclDisconnected);

            LoadApplication(new App(dbpath));
        }
        protected override void OnResume()
        {
            this.RegisterReceiver(receiver, filter1);
            this.RegisterReceiver(receiver, filter2);
            base.OnResume();
        }

        protected override void OnPause()
        {
            UnregisterReceiver(receiver);
            base.OnPause();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == RequestToEnableBluetooth)
            {
                bool success = resultCode == Result.Ok;
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
 
}