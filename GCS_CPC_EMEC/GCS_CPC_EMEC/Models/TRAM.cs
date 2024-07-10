using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GCS_CPC_EMEC.Models
{
  public class TRAM :BindablelModel
    {
      
        public string MA_DVIQLY { get; set; }
        
        public string MA_TRAM { get; set; }
        public string TEN_TRAM { get; set; } 
        public Int32 KY { get; set; }
        public Int32 THANG { get; set; }
        public Int32 NAM { get; set; }
        int status;
        public int STATUS
        {
            get => status;
            set
            {
                if (value != null)
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
