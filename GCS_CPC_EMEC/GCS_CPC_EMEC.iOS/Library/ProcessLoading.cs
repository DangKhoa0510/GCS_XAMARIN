using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigTed;
using Foundation;
using GCS_CPC_EMEC.Interface;
using GCS_CPC_EMEC.iOS.Library;
using UIKit;
using Xamarin.Forms;

[assembly : Dependency(typeof(ProcessLoading))]
namespace GCS_CPC_EMEC.iOS.Library
{
   public class ProcessLoading : IProcessLoader
    {
        public void Hide()
        {
            BTProgressHUD.Dismiss();
        }

        public void Show(string title = "Loading")
        {
            BTProgressHUD.Show(title, maskType: ProgressHUD.MaskType.Black);
        }
    }
}