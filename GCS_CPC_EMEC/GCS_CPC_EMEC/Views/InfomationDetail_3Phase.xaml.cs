using GCS_CPC_EMEC.Dialog;
using GCS_CPC_EMEC.Models;
using GCS_CPC_EMEC.ViewModels;
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
    public partial class InfomationDetail_3Phase : ContentPage
    {
        InformationDetailViewModel viewModel;       
        public InfomationDetail_3Phase(InformationDetailViewModel _viewmodel)
        {
            InitializeComponent();
            
            this.BindingContext = viewModel = _viewmodel;     
        }

        private async void btnCapNhat_Clicked(object sender, EventArgs e)
        {
            MessageYESNO message = new MessageYESNO("Thông Báo", "Bạn có muốn lưu không?");
            var result = await message.Show();
            if (result ==  DialogReturn.OK )
            {
                foreach (Model_3Phase item in viewModel.Items)
                {
                    Information information = await viewModel.Informations.GetInformationAsync(item.SERY_CTO, item.LOAI_BCS);
                    information.CS_MOI = Convert.ToDouble(item.CS_MOI);
                    information.SL_TTIEP = Convert.ToInt32 (item.SL_TTIEP);
                    information.SL_MOI = Convert.ToInt32(item.SL_MOI);
                    information.SL_TONG = Convert.ToInt32 (item.SL_TONG);
                    information.NGAY_MOI = DateTime.Now;
                    information.NGAY_GIO = DateTime.Now;
                        await viewModel.Informations.UpdateItemAsync(information);
                }
                MessageBox messageBox = new MessageBox("Thông báo", "Cập nhật thành công!");
                await messageBox.Show();
                await Navigation.PopAsync();
            }
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void btnXoa_Clicked(object sender, EventArgs e)
        {

        }

       

        private void txtCSMoi_Focused(object sender, FocusEventArgs e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                Entry entry = (Entry)sender;
                entry.CursorPosition = 0;
                entry.SelectionLength = entry.Text.Length;
            });
        }
    }
}