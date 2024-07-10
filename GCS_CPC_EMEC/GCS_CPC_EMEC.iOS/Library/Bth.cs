using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using GCS_CPC_EMEC.Interface;
using GCS_CPC_EMEC.iOS.Library;
using UIKit;
using Xamarin.Forms;

[assembly : Dependency(typeof(Bth))]
namespace GCS_CPC_EMEC.iOS.Library
{
    public class Bth : IBth
    {
        public bool Isconnected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<string> PairedDevices()
        {
            ObservableCollection<string> a = new ObservableCollection<string>() { "Tuan" };
            return a;
        }

        public Task<double> Read(uint serial, string cloai, string bcs)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Start(string name, int sleepTime, bool readAsCharArray)
        {
            throw new NotImplementedException();
        }
    }
}