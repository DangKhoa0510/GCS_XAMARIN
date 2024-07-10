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
    public partial class MenuGCS : PopupPage
    {
        TaskCompletionSource<MenuDocRF> _tsk = null;
        public MenuGCS()
        {
            InitializeComponent();
        }      
        public async Task<MenuDocRF> Show()
        {
            _tsk = new TaskCompletionSource<MenuDocRF>();
            await Navigation.PushPopupAsync(this);
            return await _tsk.Task;
        }        

       
        private async void btnDocRF_OnTouchesBegan(object sender, IEnumerable<NGraphics.Point> e)
        {
            await Navigation.PopAllPopupAsync();
            _tsk.SetResult(MenuDocRF.DaDoc);
        }

        private async void btnChuaDoc_OnTouchesBegan(object sender, IEnumerable<NGraphics.Point> e)
        {
            await Navigation.PopAllPopupAsync();
            _tsk.SetResult(MenuDocRF.ChuaDoc);
        }

        private async void btnDocChuaXong_OnTouchesBegan(object sender, IEnumerable<NGraphics.Point> e)
        {
            await Navigation.PopAllPopupAsync();
            _tsk.SetResult(MenuDocRF.DocChuaXong);
        }

        private async void btntatca_OnTouchesBegan(object sender, IEnumerable<NGraphics.Point> e)
        {
            await Navigation.PopAllPopupAsync();
            _tsk.SetResult(MenuDocRF.TatCa);
        }
    }
    public enum MenuDocRF
    {
        DaDoc,
        ChuaDoc,
        DocChuaXong,
        TatCa
    }
}