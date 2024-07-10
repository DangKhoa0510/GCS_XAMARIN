using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using GCS_CPC_EMEC.Interface;
using GCS_CPC_EMEC.Models;
using GCS_CPC_EMEC.Services;
using GCS_CPC_EMEC.Views;
using Xamarin.Forms;

namespace GCS_CPC_EMEC.ViewModels
{
    public class InformationViewModel : BaseViewModel
    {
       public TRAM _tram;
       public InfomationReposity Informations;
        public ObservableCollection<Information> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public InformationViewModel(TRAM tRAM)
        {
            _tram = tRAM;
            Items = new ObservableCollection<Information>();
            Informations = new InfomationReposity(App.gCS_Dbcontext);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Title = "Ghi chỉ số trạm :" + tRAM.MA_TRAM;
            MessagingCenter.Subscribe<InformationPage, Information>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Information;               
                await Informations.AddItemAsync(newItem);
            });
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

            
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();               
                var items = await Informations.GetItem_TramAsync(_tram.MA_TRAM);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
       
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
        public bool _isConnected { get; set; } = false;
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

       

        public  ICommand ConnectCommand { get; protected set; }
        public ICommand DisconnectCommand { get; protected set; }
        public ICommand ReadCommand { get; protected set; }
    }
}
