using System;
using System.Collections.Generic;
using System.Text;

namespace GCS_CPC_EMEC.Interface 
{
   public interface IProcessLoader
    {
        void Hide();
        void Show(string title = "Loading");
    }
}
