using GCS_CPC_EMEC.Dialog;
using GCS_CPC_EMEC.Interface;
using GCS_CPC_EMEC.Models;
using GCS_CPC_EMEC.Services;
using GCS_CPC_EMEC.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GCS_CPC_EMEC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class InformationPage : ContentPage
    {

        static  int vitri = 0;
        InformationViewModel viewModel;
       public bool EnbaleButton = false;
        public InformationPage(InformationViewModel information)
        {
            InitializeComponent();
            BindingContext = viewModel = information;
            
        }      
        protected override void OnAppearing()
        {
            base.OnAppearing();            
                viewModel.LoadItemsCommand.Execute(null);            
               InformationListView.SelectedItem = viewModel.Items.ToList()[vitri];


        }
        protected override void OnBindingContextChanged()
        {
            vitri = 0;
            base.OnBindingContextChanged();
        }

         
        protected async override void OnDisappearing()
        {            
           
            int status = -1;
            if (viewModel.Items.Where(p => p.STATUS == 1).ToList().Count == viewModel.Items.Count)
                status = 1;
            if (viewModel.Items.Where(p => p.STATUS == 1).ToList().Count > 0)
                status = 0;
            else if (viewModel.Items.Where(p => p.STATUS == 0).ToList().Count > 0)
                status = 0;
            if (viewModel._tram.STATUS != 2 )
            {
                viewModel._tram.STATUS = status;
                TRAMReposity tRAM = new TRAMReposity(App.gCS_Dbcontext);
               await tRAM.UpdateItemAsync(viewModel._tram);
            }
            base.OnDisappearing();

        }

        private void txtSoCongTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            string macongto = txtSoCongTo.Text;
            if (macongto != "")
            {
                InformationListView.ItemsSource = viewModel.Items.Where(p => p.SERY_CTO.ToLower().Contains(macongto.ToLower()) || p.TEN_KHANG.ToLower().Contains(macongto.ToLower())).ToList();
            }
            else
            {
                InformationListView.ItemsSource = viewModel.Items;
            }
        }

        private async  void imgConnect_Clicked(object sender, EventArgs e)
        {
            if (Preferences.Get(Config.bluetooth,"") == "")
            {
                await  DisplayAlert("Thông báo", "Vui lòng cài đặt tên thiết bị bluetooth", "OK");
                return;
            }
            if (viewModel._isConnected == true)
            {
                MessageYESNO q = new MessageYESNO("Thông báo", "Bạn có muốn huỷ kết nối không?");
                var result = await q.Show();
                if (result == DialogReturn.Cancel)
                {
                    return;
                }
                
            }
            viewModel.SelectedBthDevice = Preferences.Get(Config.bluetooth, "");
            viewModel._isConnected = await DependencyService.Get<IBth>().Start(viewModel.SelectedBthDevice, 250, true);
           
            if (viewModel._isConnected == true)
            {
                imgConnect.Source = "online.png";
            }
            else
            {
                imgConnect.Source = "offline.png";
            }
           
        }

        public async Task  ReadRF()
        {
            try
            {              
                List<Information> infoList = InformationListView.ItemsSource as List<Information>;
                if (infoList == null)
                    infoList = viewModel.Items.ToList();
                int index = (InformationListView.ItemsSource as IList).IndexOf(InformationListView.SelectedItem);
                if (index == -1) index = 0;
                for (int i = index ; i < infoList.Count;i++)
                {
                    vitri = i;
                    Information item = infoList[i];
                    //yêu cầu ngưng vòng for 
                    if (viewModel.IsBusy == false) return;
                    InformationListView.SelectedItem = item;
                    List<Information> list = Task.Run(async () => await  viewModel.Informations.GetItemAsync(item.SERY_CTO)).Result as List<Information>;
                    
                    foreach (Information value in list)
                    {                       
                      int ketqua =   await  ReadRF_CTO(value);  
                        if (ketqua != -1)
                        {
                            //capâpập nhật trạng thai của listivew
                            (InformationListView.SelectedItem  as Information).STATUS = ketqua;
                        }    
                    }                    
                }
                viewModel.IsBusy = false;
                btnDocRF.Text = "Đọc RF";
                btnNhap.IsVisible= true;
                MessageBox question = new MessageBox("Thông báo", "Đã đọc xong!");
                 await question.Show();
                viewModel.LoadItemsCommand.Execute(null);
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
            }
           finally
            {
              
            }
        }

        public async Task<int>  ReadRF_CTO (Information item)
        {
            try
            {
                
                Double sl1 = Convert.ToDouble(item.SLUONG_1);
                Double sl2 = Convert.ToDouble(item.SLUONG_2);
                Double sl3 = Convert.ToDouble(item.SLUONG_3);
                Double sltb = (sl1 + sl2 + sl3) / 3;
                Double socu = Convert.ToDouble(item.CS_CU);
                
                double chiso = await DependencyService.Get<IBth>().Read(Convert.ToUInt32(item.SERY_CTO), item.METER_TYPE, item.LOAI_BCS);

                if (chiso != -1)
                {
                    if (chiso < Convert.ToDouble(item.CS_CU) && viewModel.IsBusy == true)
                    {                       
                        MessageYESNO messageYESNO = new MessageYESNO("Thông Báo", "KH : " + item.TEN_KHANG + Environment.NewLine +
                                                                                 "BCS :" + item.LOAI_BCS + Environment.NewLine 
                                                                                + "CS_Mới :" + string.Format("{0:##0}", chiso) + Environment.NewLine +
                                                                                 "CS_Cũ :" + string.Format("{0:#,##0}", item.CS_CU) + Environment.NewLine +  
                                                                                 "Đây là công tơ quay vòng. bạn có muốn lưu không?"
                                                                        );

                        var result =await messageYESNO.Show();
                        if (result == DialogReturn.OK)
                        {
                            Double csquayvong = 0;
                            int chieudaiso = 0;
                            if (item.CS_CU.ToString().Contains(",") == true)
                            {
                                chieudaiso = item.CS_CU.ToString().Split(',')[0].Length;
                            }
                            else
                            {
                                chieudaiso = item.CS_CU.ToString().Length;
                            }
                            csquayvong = Convert.ToDouble(Math.Pow(10, chieudaiso));
                            item.CS_MOI = Convert.ToDouble(chiso);
                            item.SL_MOI = Convert.ToInt32((item.CS_MOI - item.CS_CU + csquayvong) * item.HSN);
                            item.SL_TONG = Convert.ToInt32(item.SL_MOI);
                            item.SL_TTIEP = 0;
                            item.NGAY_MOI = DateTime.Now;
                            item.NGAY_GIO = DateTime.Now;
                            item.STATUS = 1;
                           
                            item.NGAY_MOI = DateTime.Now;
                            await viewModel.Informations.UpdateItemAsync(item);
                            return 1;
                        }
                       
                         
                    }
                    else if (((Convert.ToDouble(chiso) - socu) > sltb * Convert.ToDouble( Preferences.Get(Config.canTren, "0")) || (Convert.ToDouble(chiso) < sltb * Convert.ToDouble( Preferences.Get(Config.canDuoi, "0")))) && viewModel.IsBusy == true)
                    {                       
                        DialogDocRF_Bat_Thuong messageYESNO = new DialogDocRF_Bat_Thuong(item.TEN_KHANG, 
                            "SL1     : " + string.Format("{0:##0}", item.SLUONG_1) + Environment.NewLine +
                            "SL2     : " + string.Format("{0:##0}", item.SLUONG_2) + Environment.NewLine +
                            "SL3     : " + string.Format("{0:##0}", item.SLUONG_3) + Environment.NewLine +
                            "SLTB    : " + string.Format("{0:##0}", (item.SLUONG_3+ item.SLUONG_3+ item.SLUONG_3 )/3) ,
                            "CS Mới  :" + string.Format("{0:##0}", chiso) + Environment.NewLine + 
                            "CS Cũ   : " + string.Format("{0:##0}", item.CS_CU) + Environment.NewLine +
                            "Tổng SL : " + string.Format("{0:##0}", (Convert.ToDouble(chiso) - socu) * item.HSN ) + Environment.NewLine +
                            "Chỉ số bất thường. bạn có muốn lưu không?"  );                       
                      
                        var result = await messageYESNO.Show();
                        if (result == DialogReturn.OK)
                        {
                            item.CS_MOI = Convert.ToDouble(chiso);                            
                            item.STATUS = 1;
                            item.SL_MOI = Convert.ToInt32((item.CS_MOI + -item.CS_CU) * item.HSN);
                            item.SL_TONG =Convert.ToInt32( item.SL_MOI);
                            item.SL_TTIEP = 0;
                            item.NGAY_MOI = DateTime.Now;
                            item.NGAY_GIO = DateTime.Now;

                            await viewModel.Informations.UpdateItemAsync(item);
                            return 1;
                        }
                        else if(result == DialogReturn.Stop)
                        {
                            viewModel.IsBusy = false;
                            btnDocRF.Text = "Đọc RF";
                            btnNhap.IsVisible= true;
                            viewModel.LoadItemsCommand.Execute(null);
                            InformationListView.SelectedItem = viewModel.Items.ToList()[vitri];
                            return -1;
                        }


                    }
                    else if (viewModel.IsBusy == true)
                    {
                        item.CS_MOI = Convert.ToDouble(chiso);
                        item.STATUS = 1;
                        item.SL_MOI = Convert.ToInt32((item.CS_MOI + -item.CS_CU) * item.HSN);
                        item.SL_TONG = Convert.ToInt32(item.SL_MOI);
                        item.SL_TTIEP = 0;
                        item.NGAY_MOI = DateTime.Now;
                        item.NGAY_GIO = DateTime.Now;
                        await viewModel.Informations.UpdateItemAsync(item);
                        return 1;
                    }
                }
                else if (viewModel.IsBusy == true)                {
                   
                    DialogDocRFNotOK notOK = new DialogDocRFNotOK("KH : " + item.TEN_KHANG, "Không đọc được công tơ");
                    var result = await notOK.Show();
                   
                        if (result == DialogReturn.Stop) // dừng đọc
                        {
                            viewModel.IsBusy = false;
                            btnDocRF.Text = "Đọc RF";
                            btnNhap.IsVisible= true;
                            viewModel.LoadItemsCommand.Execute(null);
                            InformationListView.SelectedItem = viewModel.Items.ToList()[vitri];
                           
                        }
                        if (result == DialogReturn.Repeat) // đọc lại
                        {
                                 await ReadRF_CTO(item);
                        }       
                    
                }
                return -1;
            }
            catch (Exception EX)
            {              
                MessageYESNO messageYESNO = new MessageYESNO("Lỗi", EX.Message + Environment.NewLine + "Bạn có muốn tiếp tục không?");
                var OK = await messageYESNO.Show();
                if (OK == DialogReturn.Cancel)
                {
                    viewModel.IsBusy = false;
                    btnDocRF.Text = "Đọc RF";
                    btnNhap.IsVisible= true;
                    viewModel.LoadItemsCommand.Execute(null);
                    InformationListView.SelectedItem = viewModel.Items.ToList()[vitri];
                }
                return -1;
            }
            
        }

      

        private async void btnFillter_Clicked(object sender, EventArgs e)
        {
            MenuGCS menu =  new MenuGCS();
            var result = await menu.Show();
            if (result == MenuDocRF.DaDoc )
            {
                InformationListView.ItemsSource = viewModel.Items.Where(p => p.STATUS == 1).ToList();
            }
            else if (result == MenuDocRF.ChuaDoc)
            {
                InformationListView.ItemsSource = viewModel.Items.Where(p => p.STATUS == null).ToList();
            }
            else if (result == MenuDocRF.DocChuaXong)
            {
                InformationListView.ItemsSource = viewModel.Items.Where(p => p.STATUS == 0).ToList();
            }
            else
            {
                InformationListView.ItemsSource = viewModel.Items;
            }
        }

        private async void btnDocRF_OnTouchesBegan(object sender, IEnumerable<NGraphics.Point> e)
        {
            if (viewModel._isConnected == false)
            {
                await DisplayAlert("Thông báo", "Chưa kết nối bluetooth với thiết bị", "OK");
                return;
            }
            if (viewModel.Items.Count == 0)
            {
                await DisplayAlert("Thông báo", "chưa có thông tin các điểm đo", "OK");
                return;
            }
            if (btnDocRF.Text.ToLower() == "đọc rf")
            {
                viewModel.IsBusy = true;
                btnDocRF.Text = "Dừng Đọc";
                btnNhap.IsVisible= false;
                await ReadRF();

            }
            else // yeu cau dung doc
            {
                MessageYESNO question = new MessageYESNO("Thông Báo", "Bạn có muốn dừng đọc không");
                var result = await question.Show();
                if (result == DialogReturn.OK)
                {
                    viewModel.IsBusy = false;
                    btnDocRF.Text = "Đọc RF";
                    btnNhap.IsVisible= true ;
                }
            }
        }

        private async void btnNhap_OnTouchesBegan(object sender, IEnumerable<NGraphics.Point> e)
        {
            if (viewModel.IsBusy == true)
            {
                MessageBox question = new MessageBox("Thông báo", "Đang đọc dữ liệu. không thể nhập được!");

                await question.Show();
                return;
            }

            var item = InformationListView.SelectedItem as Information;

            if (item == null)
                return;
             vitri = (InformationListView.ItemsSource as IList).IndexOf(InformationListView.SelectedItem);
            if (vitri == -1) vitri = 0;
            InfomationReposity Informations = new InfomationReposity(App.gCS_Dbcontext);
           var items = (await Informations.GetItemAsync(item.SERY_CTO)) as List<Information>;
            if (items.Count() == 1)
            {
                Model_3Phase model = new Model_3Phase();             
                model.CS_CU = Convert.ToDouble(items[0].CS_CU);
                model.SERY_CTO = items[0].SERY_CTO;
                model.LOAI_BCS = items[0].LOAI_BCS;
                model.SLUONG_1 = items[0].SLUONG_1;
                model.SLUONG_2 = items[0].SLUONG_2;
                model.SLUONG_3 = items[0].SLUONG_3;
                model.TEN_KHANG = items[0].TEN_KHANG;
                model.DIA_CHI = items[0].DIA_CHI;
                model.CS_MOI = Convert.ToDouble(items[0].CS_MOI);
                model.SL_TONG = Convert.ToInt32(items[0].SL_TONG);
                model.SL_TTIEP = Convert.ToInt32(items[0].SL_TTIEP);
                model.SL_MOI = Convert.ToInt32(items[0].SL_MOI);
                model.HSN = Convert.ToDouble(items[0].HSN);
                model.KY = items[0].KY;
                model.THANG = items[0].THANG;
                model.NAM = items[0].NAM;
                await Navigation.PushAsync(new InformationDetail(model));
            }
            else
            {
                InformationDetailViewModel viewmodel = new InformationDetailViewModel(items[0] as Information);
                await Navigation.PushAsync(new InfomationDetail_3Phase(viewmodel));
            }

        }

        private void InformationListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            InformationListView.ScrollTo(e.SelectedItem, ScrollToPosition.Center, true);
        }

        
    }
}