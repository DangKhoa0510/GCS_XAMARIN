using GCS_CPC_EMEC.Dialog;
using GCS_CPC_EMEC.Interface;
using GCS_CPC_EMEC.Models;
using GCS_CPC_EMEC.Services;
using GCS_CPC_EMEC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace GCS_CPC_EMEC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GCS : ContentPage
    {
        bool IsBusy = false;
        // GCS_MasterViewModel viewModel;
        List<DVI_QUANLY> dVI_QUANLies = new List<DVI_QUANLY>();
        List<TRAM> _TRAMS = new List<TRAM>();
        public ICommand LoadItemsCommand { get; set; }
        public GCS()
        {
            InitializeComponent();
            if (Preferences.Get(Config.urlService, "") == "")
            {
                Navigation.PushAsync(new Setting());
            }
            else
            {
                GetDVI_QUANLies();
                LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            }
           
        }

        private async Task ExecuteLoadItemsCommand()
        {
          
            if (IsBusy)
                return;
            DependencyService.Get<IProcessLoader>().Show("Đang load dữ liệu. vui lòng đợi");
            IsBusy = true;

            try
            {
                GetDVI_QUANLies();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                DependencyService.Get<IProcessLoader>().Hide();
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            DependencyService.Get<IProcessLoader>().Show("Đang load dữ liệu. vui lòng đợi");
            var item = args.SelectedItem as GCS_CPC_EMEC.Models.TRAM;
            if (item == null)
                return;
            InformationViewModel viewModel = new InformationViewModel(item);
            await Navigation.PushAsync(new InformationPage(viewModel));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
            DependencyService.Get<IProcessLoader>().Hide();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetDVI_QUANLies();
        }

        public async void GetDVI_QUANLies()
        {
            try
            {
                TRAMReposity tram = new TRAMReposity(App.gCS_Dbcontext);
                _TRAMS = tram.GetTramTheoDVQLAsync(Preferences.Get(Config.maDonVi, ""));
                ItemsListView.ItemsSource = "";
                ItemsListView.ItemsSource = _TRAMS;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Lỗi", ex.Message, "OK");

            }

        }
        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tram = search.Text;
            if (tram !="")
            {
                ItemsListView.ItemsSource = _TRAMS.Where(p => p.MA_TRAM.ToLower().Contains( tram.ToLower())).ToList();
            }
           else
            {
                ItemsListView.ItemsSource = _TRAMS;
            }
        }

       

        private async void btnNhap_Invoked(object sender, EventArgs e)
        {
            try
            {
                var si = sender as SwipeItem;
                var item = si.CommandParameter as TRAM;
                if (item == null)
                    return;
                //kiem tra xem có danh muc điểm đo chưa
                InfomationReposity Informations = new InfomationReposity(App.gCS_Dbcontext);
                var items = await Informations.GetItem_TramAsync(item.MA_TRAM);
                if (items.Count <= 0)
                {
                    MessageYESNO message = new MessageYESNO("Thông Báo", "Danh sách điểm đo chưa có, bạn có muốn tải về không?");
                    var OK = await message.Show();
                    if (OK == DialogReturn.OK)
                    {
                        await TaiDiemDo(item.MA_TRAM);
                        DependencyService.Get<IProcessLoader>().Show("Đang load dữ liệu. vui lòng đợi");
                        InformationViewModel viewModel = new InformationViewModel(item);
                        await Navigation.PushAsync(new InformationPage(viewModel));
                        DependencyService.Get<IProcessLoader>().Hide();
                    }
                }
                else
                {

                    DependencyService.Get<IProcessLoader>().Show("Đang load dữ liệu. vui lòng đợi");
                    InformationViewModel viewModel = new InformationViewModel(item);
                    await Navigation.PushAsync(new InformationPage(viewModel));
                    DependencyService.Get<IProcessLoader>().Hide();
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProcessLoader>().Hide();
                MessageBox message = new MessageBox("Lỗi", ex.Message);
                await message.Show();

            }
            

           
        }

        private async void btnDongBo_Invoked(object sender, EventArgs e)
        {
            try
            {
                MessageYESNO message = new MessageYESNO("Thông Báo", "Bạn có muốn đồng bộ dữ liệu lên server không?");
                var result = await message.Show();
                if (result == DialogReturn.OK)
                {

                    var si = sender as SwipeItem;
                    var item = si.CommandParameter as TRAM;
                    if (item == null)
                        return;

                    InfomationReposity infomationReposity = new InfomationReposity(App.gCS_Dbcontext);
                    List<Information> list = await infomationReposity.GetAllItem_TramAsync(item.MA_TRAM);

                    if (list.Count > 0)
                    {
                        DependencyService.Get<IProcessLoader>().Show("Đang đồng bộ dữ liệu, vui lòng đợi!");
                        using (var client = new HttpClient())
                        {

                            string contents = JsonConvert.SerializeObject(list);
                            var response = client.PostAsJsonAsync(Preferences.Get(Config.urlService, "") + "/api/gcs/UpdateListChiSoMoi?matram=" + item.MA_TRAM + "&ky=" + item.KY + "&thang=" + item.THANG + "&nam=" + item.NAM, list).Result;

                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                DependencyService.Get<IProcessLoader>().Hide();
                                MessageBox ms = new MessageBox("Thông báo", "Đồng bộ thành công");
                                await ms.Show();
                                //cập nhật lại trạng thái
                                item.STATUS = 2;
                                TRAMReposity reposity = new TRAMReposity(App.gCS_Dbcontext);
                                await reposity.UpdateItemAsync(item);
                            }
                            else
                            {

                                Task<string> a = response.Content.ReadAsStringAsync();
                                DependencyService.Get<IProcessLoader>().Hide();
                                MessageBox ms = new MessageBox("Thông báo", "Đồng bộ không thành công" + Environment.NewLine + "Lỗi :" + a.Result);
                                await ms.Show();
                            }
                        }
                    }
                    else
                    {
                        MessageBox ms = new MessageBox("Thông báo", "Trạm Này không có dữ liệu");
                        await ms.Show();
                    }




                }
            }
            catch (Exception EX)
            {

                throw;
            }
            
            
        }

        private async Task<bool> TaiDiemDo(string matram)
        {
            try
            {
                InfomationReposity Informations = new InfomationReposity(App.gCS_Dbcontext);
                DependencyService.Get<IProcessLoader>().Show("Đang nạp dữ liệu vui lòng đợi");
                HttpClient client = new HttpClient();
                var _json = await client.GetStringAsync(Preferences.Get(Config.urlService, "") + "/api/gcs/SP_GET_DIEMDO_MATRAM?matram=" + matram);
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                Int32 from = _json.IndexOf("[");
                Int32 to = _json.IndexOf("]");
                string result = _json.Substring(from, to - from + 1);
                List<Information> diemdos = JsonConvert.DeserializeObject<List<Information>>(result);
                DependencyService.Get<IProcessLoader>().Hide();

                if (diemdos.Count > 0)
                {
                    DependencyService.Get<IProcessLoader>().Show("Đang lưu dữ liệu. Vui lòng đợi");
                    foreach (Information info in diemdos)
                    {
                        try
                        {
                            info.NGAY_GIO = DateTime.Now;
                            info.NGAY_MOI = DateTime.Now;
                            await Informations.AddItemAsync(info);
                        }
                        catch (Exception)
                        {
                        }

                    }
                    DependencyService.Get<IProcessLoader>().Hide();
                    return true;

                }
                else
                {
                    MessageBox mess = new MessageBox("Thông Báo", "Trạm này chưa khai báo điểm đo.!");
                    await mess.Show();
                    return false;
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProcessLoader>().Hide();
                MessageBox mess = new MessageBox("lỗi", ex.Message);
                await mess.Show();
                return false;
            }
        }

       
    }
}