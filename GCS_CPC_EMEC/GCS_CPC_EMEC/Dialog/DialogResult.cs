using System;
using System.Collections.Generic;
using System.Text;

namespace GCS_CPC_EMEC.Dialog
{
    public class DialogResult
    {
        public DialogReturn Success { get; set; }
    }
    public enum DialogReturn
    {
        OK = 0,
        Cancel = 1,
        Repeat = 2,
        Stop = 3
    }
}
