using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GCS_CPC_EMEC.Interface
{
    public interface IBth
    {
        bool Isconnected { get; set; }
    Task< bool>  Start(string name, int sleepTime, bool readAsCharArray);
        void Cancel();
        ObservableCollection<string> PairedDevices();
    Task<  double> Read(uint serial, string cloai, string bcs);
    }
}
