using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GCS_CPC_EMEC.Dialog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogDocRF_Bat_Thuong : PopupPage
    {
        TaskCompletionSource<DialogReturn> _tcs = null;       
        public DialogDocRF_Bat_Thuong(string thongbao,string sl1, string chisomoi)
        {
            InitializeComponent();
            lblThongBao.Text = thongbao;
            lblSL.Text = sl1;           
            lblChiSoMoi.Text = chisomoi;          
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {          

            await  Navigation.PopAllPopupAsync();           
            _tcs.SetResult(DialogReturn.Cancel);
        }

        private async void btnOK_Clicked(object sender, EventArgs e)
        {
           
             await  Navigation.PopAllPopupAsync();           
            _tcs.SetResult(DialogReturn.OK);
        }
        public async Task<DialogReturn> Show()
        {
            _tcs = new TaskCompletionSource<DialogReturn>();
            await Navigation.PushPopupAsync(this);
            return await _tcs.Task;
        }

        private async void btnStop_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PopAllPopupAsync();
            _tcs.SetResult(DialogReturn.Stop);
        }
    }
}