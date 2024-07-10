using GCS_CPC_EMEC.Models;
using GCS_CPC_EMEC.Services;
using GCS_CPC_EMEC.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GCS_CPC_EMEC.ViewModels
{
    public class InformationDetailViewModel : BaseViewModel
    {
        public bool IsCalculator { get; set; }
        public Information Info { get; set; }
        public InfomationReposity Informations;
        //ObservableCollection<Information> _Items = new ObservableCollection<Information>();
        ObservableCollection<Model_3Phase> _Items = new ObservableCollection<Model_3Phase>();
        public ObservableCollection<Model_3Phase> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
                OnPropertyChanged();
            }

        }
        public Command LoadItemsCommand { get; set; }


        public InformationDetailViewModel(Information info)
        {
            IsCalculator = false;
            Info = info;
            Items = new ObservableCollection<Model_3Phase>();
            Informations = new InfomationReposity(App.gCS_Dbcontext);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Title = "Công tơ:" + info.SERY_CTO;
            
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                _Items.Clear();
                var items = await Informations.GetItemAsync(Info.SERY_CTO);
                foreach (var item in items)
                {
                    Model_3Phase model = new Model_3Phase();
                    model.CS_CU = Convert.ToDouble(item.CS_CU);
                    model.SERY_CTO = item.SERY_CTO;
                    model.LOAI_BCS = item.LOAI_BCS;
                    model.SLUONG_1 = item.SLUONG_1;
                    model.SLUONG_2 = item.SLUONG_2;
                    model.SLUONG_3 = item.SLUONG_3;
                    model.TEN_KHANG = item.TEN_KHANG;
                    model.DIA_CHI = item.DIA_CHI;
                    model.CS_MOI = Convert.ToDouble(item.CS_MOI);
                    model.SL_TONG = Convert.ToInt32(item.SL_TONG);
                    model.SL_TTIEP = Convert.ToInt32(item.SL_TTIEP);
                    model.SL_MOI = Convert.ToInt32(item.SL_MOI);
                    model.HSN = Convert.ToDouble(item.HSN);
                    Items.Add(model);
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
    }
}
