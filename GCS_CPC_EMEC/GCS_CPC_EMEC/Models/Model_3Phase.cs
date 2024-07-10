using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GCS_CPC_EMEC.Models
{
   public class Model_3Phase : BindablelModel
    {
		public string TEN_KHANG { get; set; }

		public string DIA_CHI { get; set; }
		double? _csmoi = 0.0;
		public string LOAI_BCS { get; set; }
		public string SERY_CTO { get; set; }
		public Double? HSN { get; set; }

		public double? CS_CU { get; set; }
		Int32? _slttiep = 0;
		public Int32? SL_TTIEP
		{
			get => _slttiep;
			set
			{
				if (_slttiep != value)
				{
					_slttiep = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(SL_TONG));
					OnPropertyChanged(nameof(STATUS));
				}
					
				
			}
		}
		// cho nay check lai ky a nhe, e lam quick
        public string CS_MOI_STR { get => CS_MOI.ToString();

			set { 
				if (!string.IsNullOrEmpty (value))
				{
					_csmoi = double.Parse(value);
					OnPropertyChanged(nameof(SL_TONG));
					OnPropertyChanged(nameof(SL_MOI));
					OnPropertyChanged(nameof(STATUS));
				}
				

			}  }


        public double? CS_MOI { get => _csmoi;
			set { 
				if (_csmoi != value)
				{
					_csmoi = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(SL_TONG));
					OnPropertyChanged(nameof(SL_MOI));
					OnPropertyChanged(nameof(STATUS));
					OnPropertyChanged(nameof(CS_MOI_STR));
				}
					
				
			} 
		}		
		public Int32? SL_MOI { get; set; }

		
		public double? SLUONG_1 { get; set; }
		public double? SLUONG_2 { get; set; }

		public double? SLUONG_3 { get; set; }

		 Int32? slTong= 0;
		public Int32? SL_TONG 
		{
			get {
				double sl1 = Convert.ToDouble (SLUONG_1);
				double sl2 = Convert.ToDouble(SLUONG_2);
				double sl3 = Convert.ToDouble(SLUONG_3);
				double sltb = (sl1 + sl2 + sl3) / 3;
				double socu = Convert.ToDouble(CS_CU);
				double csomoi = Convert.ToDouble(CS_MOI);
				double sltructiep = Convert.ToDouble(SL_TTIEP);
				Int32 sanluong = Convert.ToInt32((csomoi - socu) * Convert.ToDouble(HSN));
				double cantren = Convert.ToDouble(Preferences.Get(Config.canTren, "0"));
				double canduoi = Convert.ToDouble(Preferences.Get(Config.canDuoi, "0"));
				if (csomoi < socu) //quay vòng
				{
					double csquayvong = 0;
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
					SL_MOI = Convert.ToInt32((csomoi + csquayvong - socu) * Convert.ToDouble(HSN));
					slTong = Convert.ToInt32((csomoi + csquayvong - socu) * Convert.ToDouble(HSN) + sltructiep);
					STATUS = 1;

				}
				else if (sanluong  > sltb * cantren || sanluong < sltb * canduoi)
				{
					SL_MOI = sanluong;
					slTong = Convert.ToInt32( sanluong + sltructiep) ;
					STATUS = 2;
				}
				else
				{
					SL_MOI = sanluong;
					slTong = Convert.ToInt32(sanluong + sltructiep);
					STATUS = 0;
				}
				
				return slTong;
			}
			set { value = slTong; }
		}

		public int STATUS { get; set; }
		public int? KY { get; set; }

		public int? THANG { get; set; }

		public int? NAM { get; set; }
	}
}
