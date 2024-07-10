using GCS_CPC_EMEC.Dialog;
using GCS_CPC_EMEC.Interface;
using GCS_CPC_EMEC.Models;
using GCS_CPC_EMEC.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GCS_CPC_EMEC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Setting : ContentPage
    {

        public Setting()
        {
            InitializeComponent();
           
           if (Preferences.Get(Config.onLine, false) == true)
            {
                imgOn.Source= "icon_check_32.png";
            }
           else
            {
                imgOn.Source = "icon_uncheck_32.png";
            }

            if (Preferences.Get(Config.offLine, false) == true)
            {
                imgOff.Source = "icon_check_32.png";
            }
            else
            {
                imgOff.Source = "icon_uncheck_32.png";
            }
            txtService.Text = Preferences.Get(Config.urlService, "http://smart.cpc.vn/GCS");
            txtCanDuoi.Text = Preferences.Get(Config.canDuoi,"0");
            txtCanTren.Text = Preferences.Get(Config.canTren, "0");
            txtMaDonVi.Text = Preferences.Get(Config.maDonVi, "");
            txtBluetooth.ItemsSource = DependencyService.Get<IBth>().PairedDevices();
            txtBluetooth.SelectedItem = Preferences.Get(Config.bluetooth, "");
            txtky.Text = Preferences.Get(Config.ky, 0).ToString();
            txtthang.Text = Preferences.Get(Config.thang, 0).ToString();
            txtnam.Text = Preferences.Get(Config.nam, 0).ToString();
        }

        private async void btnSaveService_Clicked(object sender, EventArgs e)
        {
            if (txtService.Text == "")
            {
                DisplayAlert("Thông báo", "vui lòng nhập địa chỉ url", "OK");
                return;

            }
            Preferences.Set(Config.urlService, txtService.Text);
            MessageBox mess = new MessageBox("Thông Báo", "Lưu thành công!");
            await mess.Show();
        }
       
        

        private void chkOn_Tapped(object sender, EventArgs e)
        {
            imgOn.Source = "icon_check_32.png";
            imgOff.Source = "icon_uncheck_32.png";          
               Preferences.Set(Config.offLine, false);
               Preferences.Set(Config.onLine, true);
        }

        private void chkOff_Tapped(object sender, EventArgs e)
        {
            imgOff.Source = "icon_check_32.png";
            imgOn.Source = "icon_uncheck_32.png";
            Preferences.Set(Config.onLine, false);
            Preferences.Set(Config.offLine, true);
        }

        private async void btnSaveDonVi_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Config.maDonVi, txtMaDonVi.Text);
            MessageBox mess = new MessageBox("Thông Báo", "Lưu thành công!");
            await mess.Show();
        }

        private async void btnlayDiemDo_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Preferences.Get(Config.maDonVi, "") == "")
                {                 
                    MessageBox message = new MessageBox("Thông Báo", "Mã đơn vị chưa được cài đặt");
                    await message.Show();
                    return;
                }
                DependencyService.Get<IProcessLoader>().Show("Đang đọc dữ liệu vui lòng đợi");
                HttpClient client = new HttpClient();
                var _json = await client.GetStringAsync(Preferences.Get(Config.urlService, "") + "/api/gcs/get_diemdo_donvi?madonvi=" + Preferences.Get(Config.maDonVi, ""));
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                Int32 from = _json.IndexOf("[");
                Int32 to = _json.IndexOf("]");
                string result = _json.Substring(from, to - from + 1);
                List<Information> diemdos = JsonConvert.DeserializeObject<List<Information>>(result);
                DependencyService.Get<IProcessLoader>().Hide();
                InfomationReposity infomationReposity = new InfomationReposity(App.gCS_Dbcontext);
                if (diemdos.Count > 0)
                {
                    MessageYESNO message = new MessageYESNO("Thông Báo", "Dữ liệu sẽ xoá và tạo mới. bạn có muốn không");
                    var OK  =  await message.Show();
                    if (OK == DialogReturn.OK)
                    {
                        DependencyService.Get<IProcessLoader>().Show("Đang lưu dữ liệu. Vui lòng đợi");
                        await infomationReposity.TruncateTableAsync();
                        foreach (Information item in diemdos)
                        {
                            await infomationReposity.AddItemAsync(item);
                        }
                        DependencyService.Get<IProcessLoader>().Hide();
                        MessageBox mess = new MessageBox("Thông Báo", "Lưu thành công!");
                        await mess.Show();
                    }

                }
            }
            catch (Exception ex )
            {
                DependencyService.Get<IProcessLoader>().Hide();
                await DisplayAlert("Lỗi", ex.Message, "OK");
            }
          
        }

        private async void btnlayTram_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Preferences.Get(Config.maDonVi, "") == "")
                {
                    await DisplayAlert("Thông báo", "Mã đơn vị chưa được cài đặt", "OK");
                    return;
                }
                DependencyService.Get<IProcessLoader>().Show("Đang đọc dữ liệu vui lòng đợi");
                HttpClient client = new HttpClient();
                var _json = await client.GetStringAsync(Preferences.Get(Config.urlService, "") + "/api/gcs/get_tram?donvi=" + Preferences.Get(Config.maDonVi, ""));
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                Int32 from = _json.IndexOf("[");
                Int32 to = _json.IndexOf("]");
                string result = _json.Substring(from, to - from + 1);
                List<TRAM> _TRAMS = JsonConvert.DeserializeObject<List<TRAM>>(result);
              
                
                DependencyService.Get<IProcessLoader>().Hide();
                InfomationReposity infomationReposity = new InfomationReposity(App.gCS_Dbcontext);
                if (_TRAMS.Count > 0)
                {
                    MessageYESNO message = new MessageYESNO("Thông Báo", "Dữ liệu sẽ xoá và tạo mới. bạn có muốn không");
                    var OK = await message.Show();
                    if (OK == DialogReturn.OK)                        
                    {
                        DependencyService.Get<IProcessLoader>().Show("Đang lưu dữ liệu. Vui lòng đợi");
                        //lấy danh sách trạm về theo don vi
                        TRAMReposity tram = new TRAMReposity(App.gCS_Dbcontext);
                        await tram.DeleteTram_DonVi(Preferences.Get(Config.maDonVi, ""));
                        foreach (TRAM _tram in _TRAMS)
                        {
                            _tram.MA_DVIQLY = Preferences.Get(Config.maDonVi, "");
                            _tram.KY = Convert.ToInt32(Preferences.Get(Config.ky, 0));
                            _tram.THANG = Convert.ToInt32(Preferences.Get(Config.thang, 0));
                            _tram.NAM = Convert.ToInt32(Preferences.Get(Config.nam, 0));
                            _tram.STATUS = -1;
                            await tram.AddItemAsync(_tram);
                            await  infomationReposity.DeleteItem_TramAsync(_tram.MA_TRAM);
                        }
                        DependencyService.Get<IProcessLoader>().Hide();
                        MessageBox mess = new MessageBox("Thông Báo", "Lưu thành công!");
                        await mess.Show();
                    }

                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProcessLoader>().Hide();
                MessageBox mess = new MessageBox("Lỗi", ex.Message);
                await mess.Show();               
            }
        }

        private void txtCanDuoi_Completed(object sender, EventArgs e)
        {
            Preferences.Set(Config.canDuoi, txtCanDuoi.Text);
        }

        private void txtCanTren_Completed(object sender, EventArgs e)
        {
            Preferences.Set(Config.canTren, txtCanTren.Text);
        }

        private void txtBluetooth_Focused(object sender, FocusEventArgs e)
        {
            txtBluetooth.ItemsSource = DependencyService.Get<IBth>().PairedDevices();
        }

        private async void btnSaveBluetooth_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Config.bluetooth, txtBluetooth.SelectedItem.ToString());
            MessageBox mess = new MessageBox("Thông Báo", "Lưu thành công!");
            await mess.Show();
        }

        private async void btnSaveKyDuLieu_Clicked(object sender, EventArgs e)
        {
            try
            {
                Preferences.Set(Config.ky, Convert.ToInt32(txtky.Text));
                Preferences.Set(Config.thang, Convert.ToInt32(txtthang.Text));
                Preferences.Set(Config.nam, Convert.ToInt32(txtnam.Text));
                TRAMReposity reposity = new TRAMReposity(App.gCS_Dbcontext);
                TRAM _tram = App.gCS_Dbcontext.TRAMS.FirstOrDefault();
                if (_tram == null)
                {
                    MessageYESNO messageYESNO = new MessageYESNO("Thông báo", "Bạn có muôn nạp danh sách trạm không?");
                    var OK = await messageYESNO.Show();
                    if (OK == DialogReturn.OK)
                    {
                        btnlayTram_Clicked(sender, e);
                    }
                }    
               else if (Preferences.Get(Config.ky, 0) != _tram.KY || Preferences.Get(Config.thang, 0) != _tram.THANG || Preferences.Get(Config.nam, 0) != _tram.NAM)
                {
                    MessageYESNO messageYESNO = new MessageYESNO("Thông báo", "Dữ liệu kì GCS đã thay đổi. bạn có muốn nạp lại không?");
                    var OK = await messageYESNO.Show();
                    if (OK == DialogReturn.OK)
                    {
                        btnlayTram_Clicked(sender, e);
                    }    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox message = new MessageBox("Thông Báo", ex.Message);
                await message.Show();
            }
            
        }
    }
    }
