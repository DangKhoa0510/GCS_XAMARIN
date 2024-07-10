using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GCS_CPC_EMEC.Droid.Library;
using GCS_CPC_EMEC.Interface;
using Xamarin.Forms;
[assembly : Dependency(typeof(ProcessLoading))]
namespace GCS_CPC_EMEC.Droid.Library
{ 
    public class ProcessLoading : IProcessLoader
    {
        public async void Hide()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndroidHUD.AndHUD.Shared.Dismiss();
            });
        }

        [Obsolete]
        public async void Show(string title = "Loading")
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndroidHUD.AndHUD.Shared.Show(Forms.Context, status: title, maskType: AndroidHUD.MaskType.Black);
            });

        }
    }
}