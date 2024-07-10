using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GCS_CPC_EMEC.Models
{
   public class DVI_QUANLY
    {
        [Key]
        public string MA_DVIQLY { get; set; }
        public string NAME { get; set; } 
    }
}
