using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class DialogDocRFNotOK : PopupPage
    {
        TaskCompletionSource<DialogReturn> _tsk = null;
        public DialogDocRFNotOK(string thongbao, string noidung)
        {
            InitializeComponent();
            lblThongBao.Text = thongbao;
            lblMessage.Text = noidung;
        }

        private async void btnOK_Clicked(object sender, EventArgs e)
        {
            
          await  Navigation.PopAllPopupAsync();
            _tsk.SetResult(DialogReturn.Stop);
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PopAllPopupAsync();
            _tsk.SetResult(DialogReturn.Cancel);
        }

        private async void btnDoclai_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PopAllPopupAsync();
            _tsk.SetResult(DialogReturn.Repeat);
        }

       
        public async Task<DialogReturn> Show()
        {
            _tsk = new TaskCompletionSource<DialogReturn>();
            await Navigation.PushPopupAsync(this);          
            return await _tsk.Task;
        }
        
    }
}