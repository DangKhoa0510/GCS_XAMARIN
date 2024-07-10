using GCS_CPC_EMEC.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GCS_CPC_EMEC.ViewModels
{
  public  class BluetoothViewModel :BaseViewModel
    {
        public ObservableCollection<string> ListOfDevices { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ListOfBarcodes { get; set; } = new ObservableCollection<string>();
        public string _SelectedBthDevice { get; set; } = "";
        public string SelectedBthDevice
        {
            get
            {
                return _SelectedBthDevice;
            }
            set
            {
                if (_SelectedBthDevice != value)
                {
                    _SelectedBthDevice = value;
                }
            }
        }
        public string SerialID { get; set; }
        private string _selectedBCS { get; set; }
        public string SelectedBCS
        {
            get { return _selectedBCS; }
            set
            {
                if (_selectedBCS != value)
                {
                    _selectedBCS = value;
                }
            }
        }
        private string _selectedCLoai { get; set; }
        public string SelectedCLoai
        {
            get { return _selectedCLoai; }
            set
            {
                if (_selectedCLoai != value)
                {
                    _selectedCLoai = value;
                }
            }
        }
        bool _isConnected { get; set; } = false;
        int _sleepTime { get; set; } = 250;

        public String SleepTime
        {
            get { return _sleepTime.ToString(); }
            set
            {
                try
                {
                    _sleepTime = int.Parse(value);
                }
                catch { }
            }
        }


        public bool IsPickerEnabled
        {
            get
            {
                return !_isConnected;
            }
        }

        public BluetoothViewModel() 
        {

            MessagingCenter.Subscribe<App>(this, "Sleep", (obj) =>
            {
                // When the app "sleep", I close the connection with bluetooth
                if (_isConnected)
                    DependencyService.Get<IBth>().Cancel();

            });

            MessagingCenter.Subscribe<App>(this, "Resume", (obj) =>
            {

                // When the app "resume" I try to restart the connection with bluetooth
                if (_isConnected)
                    DependencyService.Get<IBth>().Start(SelectedBthDevice, _sleepTime, true);

            });


            this.ConnectCommand = new Command(() => {
                // Try to connect to a bth device
                DependencyService.Get<IBth>().Start(SelectedBthDevice, _sleepTime, true);
                _isConnected = true;

                // Receive data from bth device
                //MessagingCenter.Subscribe<App, string>(this, "Barcode", (sender, arg) => {

                // Add the barcode to a list (first position)
                // ListOfBarcodes.Insert(0, arg);
                // });
            });

            this.DisconnectCommand = new Command(() => {

                // Disconnect from bth device
                DependencyService.Get<IBth>().Cancel();
                //MessagingCenter.Unsubscribe<App, string>(this, "Barcode");
                //_isConnected = false;
            });

            this.ReadCommand = new Command(() => {
                // đọc công tơ
                DependencyService.Get<IBth>().Read(Convert.ToUInt32(SerialID), SelectedCLoai, SelectedBCS);
            });

            try
            {
                // At startup, I load all paired devices
                ListOfDevices = DependencyService.Get<IBth>().PairedDevices();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }
        }

        public ICommand ConnectCommand { get; protected set; }
        public ICommand DisconnectCommand { get; protected set; }
        public ICommand ReadCommand { get; protected set; }
    }
}
