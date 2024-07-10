using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GCS_CPC_EMEC.Droid;
using GCS_CPC_EMEC.Droid.Library;
using GCS_CPC_EMEC.Interface;
using GCS_CPC_EMEC.Services;
using Java.IO;
using Java.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(Bth))]
namespace GCS_CPC_EMEC.Droid
{
    public class Bth : IBth
    {
        const int RequestResolveError = 1000;
        BluetoothDevice device = null;
        BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
        BluetoothSocket BthSocket = null;
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        bool _isConnected = false;       
        public bool Isconnected { get =>  _isConnected; set => _isConnected = value ; }
      

        public Bth()
        {
        }

        #region IBth implementation

       
        public async Task< bool>  Start(string name, int sleepTime = 200, bool readAsCharArray = false)
        {
           await connect(name, sleepTime, readAsCharArray) ;
            return Isconnected;
            
        }

        [Obsolete]
        private async  Task connect(string name, int sleepTime, bool readAsCharArray)
        {
            ProcessLoading loader = new ProcessLoading();
            var result = "";
            adapter = BluetoothAdapter.DefaultAdapter;
            try
            {
                if (adapter == null)
                    result ="No Bluetooth adapter found.";               
                else
                    result = "Adapter found!!";

                if (!adapter.IsEnabled)
                    result = "Bluetooth adapter is not enabled.";
                else
                    System.Diagnostics.Debug.WriteLine("Adapter enabled!");

                loader.Show ("Try to connect to " + name);

                foreach (var bd in adapter.BondedDevices)
                {
                    loader.Show("Paired devices found: " + bd.Name.ToUpper());
                    if (bd.Name.ToUpper().IndexOf(name.ToUpper()) >= 0)
                    {

                        loader.Show("Found " + bd.Name + ". Try to connect with it!");
                        device = bd;
                        break;
                    }
                }

                if (device == null)
                    result = ("Named device not found.");
                else
                {
                    Java.Util.UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");
                    if ((int)Build.VERSION.SdkInt >= 10) // Gingerbread 2.3.3 2.3.4
                        BthSocket = device.CreateInsecureRfcommSocketToServiceRecord(uuid);
                    else
                        BthSocket = device.CreateRfcommSocketToServiceRecord(uuid);

                    if (BthSocket != null)
                    {
                      await   BthSocket.ConnectAsync();
                        Isconnected = BthSocket.IsConnected;
                    }
                    else
                        Isconnected = false;
                }
            }
            catch (Exception ex)
            {
                Isconnected = false;
                System.Diagnostics.Debug.WriteLine("EXCEPTION: " + ex.Message);
            }
            loader.Hide();
           
        }

        public void Cancel()
        {
            if (BthSocket != null)
                BthSocket.Close();
            device = null;
            adapter = null;
        }

        public ObservableCollection<string> PairedDevices()
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            ObservableCollection<string> devices = new ObservableCollection<string>();

            foreach (var bd in adapter.BondedDevices)
                devices.Add(bd.Name);

            return devices;
        }
         
       
        public async Task< double> Read(uint serial, string cloai, string bcs)
        {
            int error = -1;
            double cs_moi = -1 ;
            // Read data from the device
            string[] datareceive_arr = new string[1024];
            byte[] arrayOfByte = new byte[0];
            string datareceive = "";
            byte cmd_rf = 0;
            byte sub_cmd = 0;
            clsRFSpider.meter_info.Init();
            switch (bcs)
            {
                case "KT":
                    cmd_rf = clsRFSpider.CMD_READ;
                    break;
                case "KN":
                    cmd_rf = clsRFSpider.CMD_READ_ALL;
                    break;
                case "BT":
                    cmd_rf = clsRFSpider.CMD_READ_TARIFF_INFO;
                    sub_cmd = clsRFSpider.SUBCMD_READ_TARIFF_ALL;
                    clsRFSpider.meter_info.index_tariff = 0;
                    break;
                case "CD":
                    cmd_rf = clsRFSpider.CMD_READ_TARIFF_INFO;
                    sub_cmd = clsRFSpider.SUBCMD_READ_TARIFF_ALL;
                    clsRFSpider.meter_info.index_tariff = 1;
                    break;
                case "TD":
                    cmd_rf = clsRFSpider.CMD_READ_TARIFF_INFO;
                    sub_cmd = clsRFSpider.SUBCMD_READ_TARIFF_ALL;
                    clsRFSpider.meter_info.index_tariff = 2;
                    break;
                case "VC":
                    cmd_rf = clsRFSpider.CMD_READ_VAR_IMPORT;
                    break;
                case "BN":
                    cmd_rf = clsRFSpider.CMD_READ_TARIFF_INFO;
                    sub_cmd = clsRFSpider.SUBCMD_READ_TARIFF_ALL;
                    clsRFSpider.meter_info.index_tariff = 3;
                    break;
                case "CN":
                    cmd_rf = clsRFSpider.CMD_READ_TARIFF_INFO;
                    sub_cmd = clsRFSpider.SUBCMD_READ_TARIFF_ALL;
                    clsRFSpider.meter_info.index_tariff = 4;
                    break;
                case "TN":
                    cmd_rf = clsRFSpider.CMD_READ_TARIFF_INFO;
                    sub_cmd = clsRFSpider.SUBCMD_READ_TARIFF_ALL;
                    clsRFSpider.meter_info.index_tariff = 5;
                    break;
                case "VN":
                    cmd_rf = clsRFSpider.CMD_READ_VAR_EXPORT;
                    break;

            }
            string datasend = "";
            clsRFSpider.METER_TYPE _metertype = (clsRFSpider.METER_TYPE)Enum.Parse(typeof(clsRFSpider.METER_TYPE), cloai, true);
            if ((error = clsRFSpider.generate_message_rf(_metertype, serial, cmd_rf, sub_cmd, ref datasend, clsRFSpider.CMD_READ_METER)) != 0)
            {
                System.Diagnostics.Debug.WriteLine("Đọc không thành công.");
                return -1;
            }
            string stringhex = clsConvert.ToStringHex(datasend);
            System.Diagnostics.Debug.WriteLine(stringhex);
            arrayOfByte = clsConvert.StringToByteArray(stringhex);
            if (BthSocket.IsConnected)
            {
                // Write data to the device
                 BthSocket.OutputStream.Write(arrayOfByte, 0, arrayOfByte.Length);
                //await BthSocket.OutputStream.WriteAsync(arrayOfByte, 0, arrayOfByte.Length);
                // read data
                int timeout = 1000;
                string buffer_str = "";
                int total_read = 0;
                byte[] buffer = new byte[1024];
                System.Threading.Thread.Sleep(timeout);
                if (System.IO.AndroidExtensions.IsDataAvailable(BthSocket.InputStream) == false) return -1;

                total_read =  BthSocket.InputStream.Read(buffer, total_read, buffer.Length);
                
                if (total_read == 0)
                    return -1;
                //total_read = await BthSocket.InputStream.ReadAsync(buffer, total_read, buffer.Length);
                Array.Resize<byte>(ref buffer, total_read);
                buffer_str = clsConvert.ToStringLsb(buffer, total_read);
                if ((error = clsRFSpider.split_message(buffer_str, ref datareceive_arr)) != 0)
                {
                   
                    return -1;
                }
                error = -1;
                if (datareceive_arr.Length <= 0)
                {
                    
                    return -1;
                }
                for (int j = 0; j < datareceive_arr.Length; j++)
                {
                    if ((error = clsRFSpider.analyze_message_rf(_metertype, serial, cmd_rf, sub_cmd, datareceive_arr[j], ref datareceive)) == 0)
                    {
                        clsRFSpider.meter_info.raw_data_recv = clsConvert.ToStringHex(datareceive_arr[j]);
                        break;
                    }
                }
                if ((error == 0) && (serial == clsRFSpider.meter_info.serial))
                {
                    switch (bcs)
                    {
                        case "KT":
                            cs_moi = clsRFSpider.meter_info.kWh;
                            break;
                        case "KN":
                            cs_moi = clsRFSpider.meter_info.kWhExport;
                            break;
                        case "BT":
                        case "CD":
                        case "TD":
                        case "BN":
                        case "CN":
                        case "TN":
                            cs_moi = clsRFSpider.meter_info.tariff_reg_value[clsRFSpider.meter_info.index_tariff];
                            break;
                        case "VC":
                            cs_moi = clsRFSpider.meter_info.varImport;
                            break;
                        case "VN":
                            cs_moi = clsRFSpider.meter_info.varExport;
                            break;
                        default:
                            cs_moi = clsRFSpider.meter_info.kWh;
                            break;
                    }
                }
               
            }
            return cs_moi;
        }


        public static long CurrentTimeMillis()

        {

            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;

        }

        //public  int readInputStreamWithTimeout(Stream istr, byte[] b, int timeoutMillis)

        //{

        //    int bufferOffset = 0;

        //    long maxTimeMillis = CurrentTimeMillis() + timeoutMillis;

        //    while (CurrentTimeMillis() < maxTimeMillis && bufferOffset < b.Length)
        //    {

        //        int readLength = Math.Min(istr.(), b.Length - bufferOffset);

        //        // can alternatively use bufferedReader, guarded by isReady():

        //        int readResult = istr.Read(b, bufferOffset, readLength);

        //        if (readResult == -1) break;

        //        bufferOffset += readResult;

        //    }

        //    return bufferOffset;

        //}


        #endregion
    }

    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "android.bluetooth.adapter.action.STATE_CHANGED" })]
    public class BluetoothDeviceReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            String action = intent.Action;
            if (BluetoothDevice.ActionAclConnected.Equals(action))
            {
                //Connected...
            }
            else if (BluetoothDevice.ActionAclDisconnected.Equals(action))
            {
                //Disconnected...
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(context);
                alert.SetTitle("Thông Báo");
                alert.SetMessage("Bluetooth đã mất kết nối.!");
                alert.SetNeutralButton("OK", delegate {
                    alert.Dispose();
                });
                alert.Show();
            }
        }
    }
}