using GCS_CPC_EMEC.Dialog;
using GCS_CPC_EMEC.Models;
using GCS_CPC_EMEC.Services;
using GCS_CPC_EMEC.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GCS_CPC_EMEC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationDetail : ContentPage
    {      
        public Model_3Phase Item { get; set; }
        InfomationReposity reposity = new InfomationReposity(App.gCS_Dbcontext);
        public InformationDetail(Model_3Phase item)
        {
            InitializeComponent();
            Title = "Công tơ " + item.SERY_CTO;
            BindingContext = Item = item;
           
        }       
        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }

        private async void btnCapNhat_Clicked(object sender, EventArgs e)
        {
            try
            {
                double sl1 = Convert.ToDouble(txtSL1.Text);
                double sl2 = Convert.ToDouble(txtSL2.Text);
                double sl3 = Convert.ToDouble(txtSL3.Text);
                double sltb = (sl1 + sl2 + sl3) / 3;
                double socu = Convert.ToDouble(txtCSCu.Text);
                double csomoi = Convert.ToDouble(txtCSMoi.Text);
                Int32 sltructiep = Convert.ToInt32(txtSLTTiep.Text);
                double tongsl = Convert.ToDouble(txtTongSL.Text);
                if (csomoi < socu) //quay vòng
                {
                    MessageYESNO message = new MessageYESNO("Thông báo", "Chỉ số cũ lớn hơn chỉ số mới. đây là công tơ quay vòng. bạn có muốn cập nhật không");
                    var result = await message.Show();
                    if (result == DialogReturn.OK)
                    {
                        Double csquayvong = 0;
                        int chieudaiso = 0;
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            if (socu.ToString().Contains(",") == true)
                            {
                                chieudaiso = socu.ToString().Split(',')[0].Length;
                            }
                            else
                            {
                                chieudaiso = socu.ToString().Length;
                            }
                        }
                        else
                        {
                            if (socu.ToString().Contains(".") == true)
                            {
                                chieudaiso = socu.ToString().Split('.')[0].Length;
                            }
                            else
                            {
                                chieudaiso = socu.ToString().Length;
                            }
                        }

                        csquayvong = Convert.ToDouble(Math.Pow(10, chieudaiso));
                        Information information =await reposity.GetOnlyItemAsync (Item.SERY_CTO);
                        information.CS_MOI = csomoi;
                        information.SL_TTIEP = sltructiep;
                        information.SL_MOI = Convert.ToInt32((csomoi - socu + csquayvong) * Item.HSN);
                        information.SL_TONG = Convert.ToInt32(Item.SL_MOI + Item.SL_TTIEP);
                        information.STATUS = 1;
                          information.NGAY_MOI = DateTime.Now;
                         information.NGAY_GIO = DateTime.Now;
                         await reposity.UpdateItemAsync(information);
                        MessageBox messageBox = new MessageBox("Thông Báo", "Cập nhật thành công");
                        await messageBox.Show();
                        await Navigation.PopAsync();
                    }

                }
                else if (tongsl > sltb * Convert.ToDouble(Preferences.Get(Config.canTren, "0")) || tongsl < sltb * Convert.ToDouble(Preferences.Get(Config.canDuoi, "0")))
                {

                    MessageYESNO message = new MessageYESNO("Thông báo", string.Format("SLTB 3 tháng * {0} : {1} " + Environment.NewLine +
                                                                                        "SLTB 3 tháng * {2} : {3} " + Environment.NewLine +
                                                                                        "Chỉ số mới bất thường. Bạn có muốn lưu không", Convert.ToDouble(Preferences.Get(Config.canDuoi, "0")),
                                                            string.Format("{0:##0}", sltb * Convert.ToDouble(Preferences.Get(Config.canDuoi, "0"))), Convert.ToDouble(Preferences.Get(Config.canTren, "0")), string.Format("{0:##0}", sltb * Convert.ToDouble(Preferences.Get(Config.canTren, "0")))));
                    var result = await message.Show();
                    if (result == DialogReturn.OK)
                    {
                        Information information = await reposity.GetOnlyItemAsync(Item.SERY_CTO);
                        information.CS_MOI = csomoi;
                        information.SL_TTIEP = sltructiep;
                        information.SL_MOI = Convert.ToInt32((csomoi - socu) * Item.HSN);
                        information.SL_TONG = Convert.ToInt32(Item.SL_MOI + Item.SL_TTIEP);
                        information.STATUS = 1;
                        information.NGAY_MOI = DateTime.Now;
                        information.NGAY_GIO = DateTime.Now;
                        await reposity.UpdateItemAsync(information);
                        MessageBox messageBox = new MessageBox("Thông Báo", "Cập nhật thành công");
                        await messageBox.Show();
                        await Navigation.PopAsync();
                    }

                }
                else
                {
                    Information information = await reposity.GetOnlyItemAsync(Item.SERY_CTO);
                    information.CS_MOI = csomoi;
                    information.SL_TTIEP = sltructiep;
                    information.SL_MOI = Convert.ToInt32((csomoi - socu ) * Item.HSN);
                    information.SL_TONG = Convert.ToInt32(Item.SL_MOI + Item.SL_TTIEP);
                    information.STATUS = 1;
                    information.NGAY_MOI = DateTime.Now;
                    information.NGAY_GIO = DateTime.Now;
                    await reposity.UpdateItemAsync(information);
                    MessageBox messageBox = new MessageBox("Thông Báo", "Cập nhật thành công");
                    await messageBox.Show();
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox message = new MessageBox("Thông báo", ex.Message);
                await message.Show();
            }
        }

        

        private async void btnXoa_Clicked(object sender, EventArgs e)
        {
            try
            {
                MessageYESNO message = new MessageYESNO("Thông báo", "Bạn có muốn xoá không?");
                var result = await message.Show();
                if (result == DialogReturn.OK)
                {
                    Information info = await reposity.GetOnlyItemAsync(Item.SERY_CTO);
                    Item.CS_MOI = null;
                    Item.SL_MOI = null;
                    Item.SL_TONG = null;
                    info.CS_MOI = null;
                    info.SL_MOI = null;
                    info.SL_TONG = null;
                    await reposity.UpdateItemAsync(info);
                    MessageBox messageBox = new MessageBox("Thông Báo", "Đã xoá thành công?");
                    await messageBox.Show();
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {

                MessageBox messageBox = new MessageBox("Thông Báo", ex.ToString());
                await messageBox.Show();
            }
            
        }

        private void txtCSMoi_Focused(object sender, FocusEventArgs e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                txtCSMoi.CursorPosition = 0;
                txtCSMoi.SelectionLength = txtCSMoi.Text.Length;
            });
           
        }

        private void txtSLTTiep_Focused(object sender, FocusEventArgs e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                txtSLTTiep.CursorPosition = 0;
                txtSLTTiep.SelectionLength = txtSLTTiep.Text.Length;
            });
        }
    }
}
