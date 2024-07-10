#define ERROR_MSG
#undef NODE_LSB
#undef  NODE_MSB
using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace GCS_CPC_EMEC.Services
{
    public static class clsConvert
    {
        private static readonly string[] ASCCII_SPECIAL_CHARS = {"<NUL>", "<SOH>", "<STX>", "<ETX>", "<EOT>", "<ENQ>", "<ACK>", "<BEL>",
                                                                    "<BS>", "<TAB>", "<LF>", "<VT>", "<FF>", "<CR>", "<SO>", "<SI>",
                                                                    "<DLT>", "<DC1>", "<DC2>","<DC3>", "<DC4>", "<NAK>", "<SYN>", "<ETB>",
                                                                    "<CAN>", "<EM>", "<SUB>", "<ESC>", "<FS>", "<GS>", "<RS>", "<US>" //, <SPACE>
                                                                 };
        public static readonly string LOGASCII = "ASCII";
        public static readonly string LOGHEX = "HEX";


        #region TO_STRING
        public static string ReverseString(string s)
        {
            try
            {
                char[] arr = s.ToCharArray();
                Array.Reverse(arr);
                return new string(arr);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringAscii(byte data)
        {
            try
            {
                return data.ToString("X").PadLeft(2, '0');
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringAscii
        /// Chuyen doi chuoi 1byte sang chuoi ascii.
        /// </summary>
        /// <param name="stringlsb"></param>
        /// <returns></returns>
        public static string ToStringAscii(byte[] bytearray)
        {
            try
            {
                string result = "";
                for (int i = 0; i < bytearray.Length; i++)
                {
                    if (bytearray[i] > '\x7F')
                    {
                        result += string.Format("<{0}>", bytearray[i].ToString("x2"));
                    }
                    else
                    {
                        if (bytearray[i] < '\x20')
                        {
                            result += ASCCII_SPECIAL_CHARS[bytearray[i]];
                        }
                        else
                        {
                            result += (char)bytearray[i];
                        }

                    }
                }
                return result.Trim();
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringAscii
        /// Chuyen doi chuoi 1byte sang chuoi ascii.
        /// </summary>
        /// <param name="stringlsb"></param>
        /// <returns></returns>
        public static string ToStringAscii(string data)
        {
            try
            {

                byte[] bytearray = ToBytesLsb(data);
                return ToStringAscii(bytearray);
                /*
                string result = "";
                for (int i = 0; i < bytearray.Length; i++)
                {
                    if (bytearray[i] > '\x7F')
                    {
                        result += string.Format("<{0}>", bytearray[i].ToString("x2"));
                    }
                    else 
                    {
                        if (bytearray[i] < '\x20') 
                        {
                            result += ASCCII_SPECIAL_CHARS[bytearray[i]];
                        }
                        else
                        {
                            result += (char)bytearray[i];
                        }

                    }
                }
                return result.Trim();
                */
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }


        /// <summary>
        /// ToString
        /// Chuyen doi chuoi hexascii 2byte  sang chuoi 1byte
        /// 4142 -> ab
        /// </summary>
        /// <param name="strhexascii"></param>
        /// <returns></returns>
        public static string ToString(string strhexascii)
        {
            try
            {
                string result = "";
                strhexascii = strhexascii.Trim();
                if (strhexascii.Length % 2 == 1) strhexascii += "0";
                for (int i = 0; i < strhexascii.Length; i += 2)
                {
                    //result += (char)int.Parse(strhexascii.Substring(i,2), NumberStyles.AllowHexSpecifier);
                    result += clsConvert.ToStringLsb(Byte.Parse(strhexascii.Substring(i, 2), NumberStyles.AllowHexSpecifier));
                }
                return result;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 2014-02-12
        /// Chuyen doi so ushort sang chuoi Hex voi byte LSB di truoc
        /// vidu : 0x0001 => "0100" 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringHexLsb(ushort value)
        {
            try
            {
                byte[] buffer = BitConverter.GetBytes(value);
                //Array.Reverse(buffer);
                return ToStringHex(buffer);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 2014-02-12
        /// Chuyen doi so ushort sang chuoi Hex voi byte LSB di truoc
        /// vidu : 0x0001 => "0001"         /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringHexMsb(ushort value)
        {
            try
            {
                byte[] buffer = BitConverter.GetBytes(value);
                Array.Reverse(buffer);
                return ToStringHex(buffer);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }


        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Converts a string to  hexa array string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToStringHexArray(string s)
        {
            s = clsConvert.ToStringHex(s);
            string buffer = "";

            for (int i = 0; i < s.Length; i += 2)
                buffer += s.Substring(i, 2) + " ";
            return buffer;
        }


        //////////////////////////////////////////////////////////////////////////
        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        public static string ToStringHexArray(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }


        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringlsb"></param>
        /// <returns></returns>
        public static string ToStringAsciiDecimal(string stringlsb)
        {
            try
            {
                byte[] bytearray = ToBytesLsb(stringlsb);
                string result = "";
                //string result = BitConverter.ToString(bytearray);
                //result = result.Replace("-", "");
                foreach (byte byteValue in bytearray) result += byteValue.ToString("D");
                return result;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        public static string RTCToString7Byte(DateTime dateTime)
        {
            try
            {
                byte hour = (byte)dateTime.Hour;
                byte min = (byte)dateTime.Minute;
                byte sec = (byte)dateTime.Second;
                byte dayofWe = (byte)dateTime.DayOfWeek;
                byte day = (byte)dateTime.Day;
                byte month = (byte)dateTime.Month;
                byte year = (byte)int.Parse(dateTime.Year.ToString().Substring(2, 2));
                string strDateTime = clsConvert.ToStringLsb(hour) + clsConvert.ToStringLsb(min) + clsConvert.ToStringLsb(sec) + clsConvert.ToStringLsb(dayofWe)
                        + clsConvert.ToStringLsb(day) + clsConvert.ToStringLsb(month) + clsConvert.ToStringLsb(year);
                return strDateTime;
            }
            catch
            {
                return "";
            }
        }

        public static string RTCToString7Byte_forpinRTC(DateTime dateTime)
        {
            try
            {
                byte hour = (byte)dateTime.Hour;
                byte min = (byte)dateTime.Minute;
                byte sec = (byte)dateTime.Second;
                byte dayofWe = (byte)dateTime.DayOfWeek;
                byte day = (byte)dateTime.Day;
                byte month = (byte)dateTime.Month;
                byte year = (byte)int.Parse(dateTime.Year.ToString().Substring(2, 2));
                string strDateTime = ToStringHex(sec) + ToStringHex(min) + ToStringHex(hour) + ToStringHex(day) + ToStringHex(dayofWe) + ToStringHex(month) + ToStringHex(year);
                //string strDateTime = clsConvert.ToStringLsb(sec) + clsConvert.ToStringLsb(min) + clsConvert.ToStringLsb(hour) + clsConvert.ToStringLsb(day)
                //      + clsConvert.ToStringLsb(dayofWe) + clsConvert.ToStringLsb(month) + clsConvert.ToStringLsb(year);
                return strDateTime;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        public static DateTime StringtoRTC_forpinRTC(string strdateTime)
        {
            try
            {
                int sec = Convert.ToInt32(strdateTime.Substring(0, 2), 16);
                int min = Convert.ToInt32(strdateTime.Substring(2, 2), 16);
                int hour = Convert.ToInt32(strdateTime.Substring(4, 2), 16);
                int day = Convert.ToInt32(strdateTime.Substring(6, 2), 16);
                int dayofWe = Convert.ToInt32(strdateTime.Substring(8, 2), 16);
                int month = Convert.ToInt32(strdateTime.Substring(10, 2), 16);
                int year = 2000 + Convert.ToInt32(strdateTime.Substring(12, 2), 16);
                DateTime datetime = new DateTime(year, month, day, hour, min, sec);
                return datetime;
            }
            catch
            {
                return new DateTime();
            }
        }

        #region TO_STRING_LSB

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Chuyen doi Byte (gia tri byte tu 0 -> 255) sang chuoi
        /// </summary>
        /// <param name="bytearray"></param>
        /// <returns></returns>
        public static string ToStringLsb(char charvalue)
        {
            try
            {
                byte[] bytearray = { (byte)charvalue };
                return System.Text.Encoding.GetEncoding("iso-8859-1").GetString(bytearray);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }


        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Chuyen doi Byte (gia tri byte tu 0 -> 255) sang chuoi
        /// </summary>
        /// <param name="bytearray"></param>
        /// <returns></returns>
        public static string ToStringLsb(byte bytevalue)
        {
            try
            {
                byte[] bytearray = { bytevalue };
                return System.Text.Encoding.GetEncoding("iso-8859-1").GetString(bytearray);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Chuyen doi Mang byte (gia tri byte tu 0 -> 255)
        /// </summary>
        /// <param name="bytearray"></param>
        /// <returns></returns>
        public static string ToStringLsb(byte[] bytearray)
        {
            return System.Text.Encoding.GetEncoding("iso-8859-1").GetString(bytearray);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Chuyen doi Mang byte (gia tri byte tu 0 -> 255)
        /// </summary>
        /// <param name="bytearray"></param>
        /// <returns></returns>
        public static string ToStringLsb(byte[] bytearray, int length)
        {
            try
            {
                if (length < 0) return "";
                return System.Text.Encoding.GetEncoding("iso-8859-1").GetString(bytearray, 0, length);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringLsb
        /// </summary>
        /// <param name="bytearray"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToStringLsb(byte[] bytearray, int start, int length)
        {
            try
            {
                if (length < 0) return "";
                byte[] buffer = new byte[length];
                Array.Copy(bytearray, start, buffer, 0, length);
                return System.Text.Encoding.GetEncoding("iso-8859-1").GetString(buffer);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }






        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringLsb
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringLsb(UInt16 data)
        {
            return ToStringLsb(ToBytesLsb(data)).PadRight(2);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringLsb(ushort[] data)
        {
            try
            {
                string result = "";
                for (int i = 0; i < data.Length; i++)
                {
                    result += ToStringLsb(data[i]);
                }
                return result;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }

        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringLsb
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringLsb(int data)
        {
            try
            {
                return ToStringLsb(ToBytesLsb(data)).PadRight(2);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }

        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringLsb UInt32
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringLsb(UInt32 data)
        {
            return ToStringLsb(ToBytesLsb(data)).PadRight(4);
        }

        public static string ToStringLsb(UInt64 data)
        {
            return ToStringLsb(ToBytesLsb(data)).PadRight(8);
        }
        //////////////////////////////////////////////////////////////////////////
        #endregion

        #region TO_STRING_MSB
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringMSB byte
        /// </summary>
        /// <param name="bytevalue"></param>
        /// <returns></returns>
        public static string ToStringMsb(byte bytevalue)
        {
            byte[] bytearray = { bytevalue };
            return System.Text.Encoding.GetEncoding("iso-8859-1").GetString(bytearray);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringMSB byte[]
        /// </summary>
        /// <param name="bytearray"></param>
        /// <returns></returns>
        public static string ToStringMsb(byte[] bytearray)
        {
            Array.Reverse(bytearray);
            return System.Text.Encoding.GetEncoding("iso-8859-1").GetString(bytearray);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringMsb(short data)
        {
            return ToStringMsb(BitConverter.GetBytes(data)).PadLeft(2);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringMsb Unt16
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringMsb(ushort data)
        {
            return ToStringMsb(BitConverter.GetBytes(data)).PadLeft(2);
        }


        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringMsb UInt32
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringMsb(uint data)
        {
            return ToStringMsb(BitConverter.GetBytes(data)).PadLeft(4);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToStringLsb
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringMsb(int data)
        {
            return ToStringLsb(ToBytesMsb(data)).PadRight(2);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringMsb(float data)
        {
            return ToStringMsb(BitConverter.GetBytes(data)).PadLeft(4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringMsb(string data_strhex)
        {
            try
            {
                byte[] buffer = strhex2bytes(data_strhex);
                Array.Reverse(buffer);
                return ToStringLsb(buffer);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        #endregion


        #region "ToStringHex"
        /// <summary>
        ///  
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringHex(byte value)
        {
            try
            {
                return value.ToString("X2");
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringHex(ushort value)
        {
            try
            {
                return value.ToString("X4");
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        // quanhdt new
        /// <summary>
        ///  
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringHex(uint value)
        {
            try
            {
                return value.ToString("X8");
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        /// <summary>
        /// ToStringHex
        /// Chuyen chuoi sang chuoi HexAscii
        /// ab -> 4142
        /// </summary>
        /// <param name="stringlsb"></param>
        /// <returns></returns>
        public static string ToStringHex(byte[] buffer)
        {
            //byte[] bytearray = ToBytesLsb(data);
            try
            {
                string result = "";
                foreach (byte byteValue in buffer)
                {
                    result += string.Format("{0}", byteValue.ToString("X2"));
                }
                return result.Trim();
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        /// <summary>
        /// ToStringHex
        /// Chuyen chuoi sang chuoi HexAscii
        /// ab -> 4142
        /// </summary>
        /// <param name="stringlsb"></param>
        /// <returns></returns>
        public static string ToStringHex(string data)
        {
            try
            {
                byte[] bytearray = ToBytesLsb(data);
                string result = "";
                foreach (byte byteValue in bytearray)
                {
                    result += string.Format("{0}", byteValue.ToString("X2"));
                }
                return result.Trim();
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }
        #endregion

        #region TO_SBYTE
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static sbyte ToSByte(string data)
        {
            try
            {
                if (data.Length < 1) return 0;
                byte[] result = new byte[1] { 0 };
                result = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data.Substring(0, 1));
                return (sbyte)result[0];
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }

        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToByte
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static sbyte ToSByte(string data, int index)
        {
            try
            {
                if ((index < 0) && (data.Length < 1)) return 0;
                if (index >= data.Length) return 0;
                byte[] result = new byte[1] { 0 };
                result = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data.Substring(index, 1));
                return (sbyte)result[0];
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }

        }
        #endregion

        #region TO_BYTE
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte ToByte(string data)
        {
            try
            {
                if (data.Length < 1) return 0;
                byte[] result = new byte[1] { 0 };
                result = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data.Substring(0, 1));
                return result[0];
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }

        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToByte
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte ToByte(string data, int index)
        {
            try
            {
                if ((index < 0) && (data.Length < 1)) return 0;
                if (index >= data.Length) return 0;
                byte[] result = new byte[1] { 0 };
                result = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data.Substring(index, 1));
                return result[0];
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }

        }
        #endregion

        #region TO_BYTES
        #region TO_BYTES_LSB
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToBytesLsb UInt16
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytesLsb(UInt16 data)
        {
            return BitConverter.GetBytes(data);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytesLsb(Int32 data)
        {
            return BitConverter.GetBytes(data);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToBytesLsb UInt32
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytesLsb(UInt32 data)
        {
            return BitConverter.GetBytes(data);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToBytesLsb ulong (64bit)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytesLsb(ulong data)
        {
            try
            {
                return BitConverter.GetBytes(data);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return null;
            }

        }

        //////////////////////////////////////////////////////////////////////////
        public static byte[] ToBytesLsb(float data)
        {
            return BitConverter.GetBytes(data);
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToBytesLsb string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytesLsb(string data)
        {
            byte[] buffer = new byte[data.Length];
            buffer = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data);
            return buffer;
        }

        #endregion
        //////////////////////////////////////////////////////////////////////////
        #region TO_BYTES_MSB
        /// <summary>
        /// ToBytesMsb UInt16 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytesMsb(UInt16 data)
        {
            byte[] arr = BitConverter.GetBytes(data);
            Array.Reverse(arr);
            return arr;
        }


        public static byte[] ToBytesMsb(int data)
        {
            try
            {
                byte[] arr = BitConverter.GetBytes(data);
                Array.Reverse(arr);
                return arr;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return null;
            }

        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary> 
        /// ToBytesMsb UInt32
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytesMsb(UInt32 data)
        {
            byte[] arr = BitConverter.GetBytes(data);
            Array.Reverse(arr);
            return arr;
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary> 
        /// ToBytesMsb ulong
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytesMsb(ulong data)
        {
            try
            {
                byte[] arr = BitConverter.GetBytes(data);
                Array.Reverse(arr);
                return arr;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return null;
            }

        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary> 
        /// ToBytesMsb string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytesMsb(string data)
        {
            byte[] buffer = new byte[data.Length];
            buffer = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data);
            Array.Reverse(buffer);
            return buffer;
        }


        //////////////////////////////////////////////////////////////////////////
        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        public static byte[] ToByteArray(string HexArray)
        {
            HexArray = HexArray.Replace(" ", "");
            byte[] buffer = new byte[HexArray.Length / 2];
            for (int i = 0; i < HexArray.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(HexArray.Substring(i, 2), 16);
            return buffer;
        }

        #endregion
        #endregion

        #region TO_UINT16
        //////////////////////////////////////////////////////////////////////////
        public static UInt16 ToUInt16Lsb(string data)
        {
            try
            {
                if (data.Length < 2) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(0, 2));
                return (BitConverter.ToUInt16(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        /// <summary>
        /// ToUInt16Lsb string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt16 ToUInt16Lsb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(index, 2));
                return (BitConverter.ToUInt16(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToUInt16Msb string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt16 ToUInt16Msb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesMsb(data.Substring(index, 2));
                return (BitConverter.ToUInt16(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }

        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringascii"></param>
        /// <returns></returns>
        public static UInt16 ToUInt16Msb(string stringascii)
        {
            try
            {
                if (stringascii.Length > 4) return 0;
                return UInt16.Parse(stringascii, NumberStyles.AllowHexSpecifier);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt16 ToUInt16(byte[] buffer)
        {
            try
            {
                if (buffer.Length < 2) return 0;
                return (BitConverter.ToUInt16(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt16 ToUInt16(byte[] buffer, int index)
        {
            try
            {
                if (buffer.Length < 2) return 0;
                if (buffer.Length < (index + 2)) return 0;
                return (BitConverter.ToUInt16(buffer, index));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        #endregion

        #region TO_INT16
        public static Int16 eee(string data)
        {
            return 1;
        }
        public static Int16 ggg(string data)
        {
            return 1;
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Int16 ToInt16Lsb(string data)
        {
            try
            {
                if (data.Length < 2) return -1;
                byte[] buffer = ToBytesLsb(data.Substring(0, 2));
                return (BitConverter.ToInt16(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Int16 ToInt16Lsb(string data, int index)
        {
            try
            {
                if (index < 0) return -1;
                byte[] buffer = ToBytesLsb(data.Substring(index, 2));
                return (BitConverter.ToInt16(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Int16 ToInt16Msb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesMsb(data.Substring(index, 2));
                return (BitConverter.ToInt16(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }
        }

        #endregion

        #region TO_UINT32

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToUInt32Lsb string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt32 ToUInt32Lsb(string data)
        {
            try
            {
                if (data.Length < 4) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(0, 4));
                return (BitConverter.ToUInt32(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToUInt32Lsb string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt32 ToUInt32Lsb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                if (data.Length < 4 + index) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(index, 4));
                return (BitConverter.ToUInt32(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToUInt32Msb
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static UInt32 ToUInt32Msb(string data)
        {
            try
            {
                if (data.Length < 4)
                {
                    for (int i = data.Length; i < 4; i++) data = ToStringLsb((byte)0) + data;
                }
                byte[] buffer = ToBytesMsb(data.Substring(0, 4));
                return (BitConverter.ToUInt32(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ToUInt32Msb string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt32 ToUInt32Msb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                data = data.Substring(index);
                if (data.Length < 4)
                {
                    for (int i = data.Length; i < 4; i++) data = ToStringLsb((byte)0) + data;
                }
                byte[] buffer = ToBytesMsb(data.Substring(0, 4));
                return (BitConverter.ToUInt32(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        //////////////////////////////////////////////////////////////////////////

        public static UInt32 ToUInt32(float data)
        {
            try
            {
                byte[] buffer = new byte[4] { 0, 0, 0, 0 };
                for (int i = 0; i < 4; i++)
                {
                    buffer[i] = Convert.ToByte((float)Math.Round(data % 256, 0));
                    data = (float)Math.Round(data / 256, 0);
                }
                return BitConverter.ToUInt32(buffer, 0);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        public static UInt32 ToUInt32(byte[] buffer)
        {
            try
            {
                if (buffer.Length < 2) return 0;
                return (BitConverter.ToUInt32(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        #endregion

        #region TO_INT32
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Int32 ToInt32Lsb(string data)
        {
            try
            {
                if (data.Length < 2) return -1;
                byte[] buffer = ToBytesLsb(data.Substring(0, 4));
                return (BitConverter.ToInt32(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }

        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Int32 ToInt32Lsb(string data, int index)
        {
            try
            {
                if (index < 0) return -1;
                byte[] buffer = ToBytesLsb(data.Substring(index, 4));
                return (BitConverter.ToInt32(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Int32 ToInt32Msb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesMsb(data.Substring(index, 4));
                return (BitConverter.ToInt32(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }
        }

        #endregion

        #region TO_INT64
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Int64 ToInt64Lsb(string data)
        {
            try
            {
                if (data.Length < 2) return -1;
                byte[] buffer = ToBytesLsb(data.Substring(0, 8));
                return (BitConverter.ToInt64(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }

        }



        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Int64 ToInt64Lsb(string data, int index)
        {
            try
            {
                if (index < 0) return -1;
                byte[] buffer = ToBytesLsb(data.Substring(index, 8));
                return (BitConverter.ToInt64(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }
        }

        //public static UInt64 ToUInt64Lsb(string data, int index)
        //{
        //    try
        //    {
        //        if (index < 0) return 0;
        //        byte[] buffer = ToBytesLsb(data.Substring(index, 8));
        //        return (BitConverter.ToUInt64(buffer, 0));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //clsLogFile.CatchSystemException(ex);
        //        return 0;
        //    }
        //}

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Int64 ToInt64Msb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesMsb(data.Substring(index, 8));
                return (BitConverter.ToInt64(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }
        }

        #endregion

        #region TO_UINT64
        //////////////////////////////////////////////////////////////////////////
        public static UInt64 ToUInt64Lsb(string data)
        {
            try
            {
                if (data.Length < 2) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(0, 8));
                return (BitConverter.ToUInt64(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }

        }
        /// <summary>
        /// ToUInt64Lsb string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt64 ToUInt64Lsb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(index, 8));
                return (BitConverter.ToUInt64(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt64 ToUInt64Msb(string data)
        {
            try
            {
                if (data.Length < 8) data = data.PadLeft(8, '\x00');
                byte[] buffer = ToBytesMsb(data);
                return (BitConverter.ToUInt64(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }


        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static UInt64 ToUInt64Msb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                data = data.Substring(index);
                if (data.Length < 8) data = data.PadLeft(8, '\x00');
                byte[] buffer = ToBytesMsb(data.Substring(index, 8));
                return (BitConverter.ToUInt64(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        #endregion

        #region TO_SINGLE
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Single ToSingleLsb(string data)
        {
            try
            {
                if (data.Length < 0) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(0, 4));
                Array.Reverse(buffer);
                return (BitConverter.ToSingle(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Single ToSingleLsb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(index, 4));
                Array.Reverse(buffer);
                return (BitConverter.ToSingle(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Single ToSingleMsb(string data)
        {
            try
            {
                if (data.Length < 4) return 0;
                byte[] buffer = ToBytesMsb(data.Substring(0, 4));
                return (BitConverter.ToSingle(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Single ToSingleMsb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesMsb(data.Substring(index, 4));
                return (BitConverter.ToSingle(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        #endregion

        #region TO_FLOAT

        public static float ToFloatLsb(byte[] data)
        {
            try
            {
                if (data.Length < 4) return 0;
                return (BitConverter.ToSingle(data, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// 
        public static float ToFloatLsb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(index, 4));
                return (BitConverter.ToSingle(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }


        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static float ToFloatMsb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesMsb(data.Substring(index, 4));
                return (BitConverter.ToSingle(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        #endregion

        #region TO_DOUBLE
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Double ToDoubleLsb(string data)
        {
            try
            {
                if (data.Length < 8) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(0, 8));
                Array.Reverse(buffer);
                return (BitConverter.ToDouble(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Double ToDoubleLsb(string data, int index)
        {
            try
            {
                if (index < 8) return 0;
                byte[] buffer = ToBytesLsb(data.Substring(index, 8));
                Array.Reverse(buffer);
                return (BitConverter.ToDouble(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Double ToDoubleMsb(string data)
        {
            try
            {
                if (data.Length < 8) return 0;
                byte[] buffer = ToBytesMsb(data.Substring(0, 8));
                return (BitConverter.ToDouble(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Double ToDoubleMsb(string data, int index)
        {
            try
            {
                if (index < 0) return 0;
                byte[] buffer = ToBytesMsb(data.Substring(index, 8));
                return (BitConverter.ToDouble(buffer, 0));
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }
        #endregion

        #region Reverse Bytes
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt16 ReverseBytes(UInt16 value)
        {
            try
            {
                return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt32 ReverseBytes(UInt32 value)
        {
            try
            {
                return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                        (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt64 ReverseBytes(UInt64 value)
        {
            try
            {
                return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                       (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                       (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                       (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0;
            }

        }
        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] strhex2bytes(string data)
        {
            byte[] buffer = new byte[0];
            try
            {
                data = data.Trim();
                data = data.Replace(" ", "");
                int length = data.Length / 2;
                if ((data.Length % 2) != 0) return buffer;
                Array.Resize(ref buffer, length);
                for (int i = 0; i < length; i++)
                {
                    buffer[i] = byte.Parse(data.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier);//Convert.ToByte(data.Substring(i*2,2),16); 
                }
                return buffer;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
            }
            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToStringLsb(string data_strhex)
        {
            try
            {
                byte[] buffer = strhex2bytes(data_strhex);
                return ToStringLsb(buffer);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="strdec"></param>
        /// <param name="numbyteHex"></param>
        /// <returns></returns>
        public static string strDecToStringHex(string strdec, int numbyteHex)
        {
            try
            {
                if (numbyteHex == 4)
                {
                    uint number = uint.Parse(strdec);
                    return clsConvert.ToStringHex(clsConvert.ToStringLsb(number));
                }
                if (numbyteHex == 2)
                {
                    ushort number = ushort.Parse(strdec);
                    return clsConvert.ToStringHex(clsConvert.ToStringLsb(number));
                }
                return "";

            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return "";
            }
        }

        public static string StringToHexString(string asciiString)
        {
            string hex = "";
            //foreach (char c in asciiString)
            //{
            //    int tmp = c;
            //    hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            //}
            foreach (byte c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }




        // ushort to "all"
        /// <summary>
        ///  Chuyen doi chuoi hex sang kieu signle
        /// </summary>
        /// <param name="hexVal"></param>
        /// <returns></returns>
        public static Single HexStringToSingle(string hexVal)
        {
            try
            {
                int i = 0, j = 0;
                byte[] bArray = new byte[4];
                for (i = 0; i <= hexVal.Length - 1; i += 2)
                {
                    bArray[j] = Byte.Parse(hexVal[i].ToString() + hexVal[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    j += 1;
                }
                Array.Reverse(bArray);
                Single s = BitConverter.ToSingle(bArray, 0);
                return (s);
            }
            catch (Exception ex)
            {
                return -1;
                //throw new FormatException("The supplied hex value is either empty or in an incorrect format.  Use the " +
                //    "following format: 00000000", ex);
            }
        }

        //public static string ConvertSingleToHex(Single SngValue)
        //{

        //    string hexString = string.Empty;

        //    Byte[] tmpBytes = BitConverter.GetBytes(SngValue);

        //    Array.Reverse(tmpBytes);

        //    hexString = HexEncoding.ToString(tmpBytes);

        //    return hexString;

        //}

        public static Single HexToSingle(UInt32 value)
        {
            // 48716D6D
            Byte[] bArray = BitConverter.GetBytes(value);
            Single s = BitConverter.ToSingle(bArray, 0);
            return (s);
        }

        /// <summary>
        /// Chuyen so hex sang dec, vi du 0x12 --> 12
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte hex2dec(byte hex)
        {
            try
            {
                byte dec, tam1, tam2;
                tam1 = (byte)(hex & 0x0F);
                tam2 = (byte)((hex >> 4) & 0x0F);
                dec = (byte)(tam2 * 10 + tam1);
                return dec;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.DisplayCatchSystemException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Chuyen so dec sang hex, vi du 12 --> 0x12
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        public static byte dec2hex(byte dec)
        {
            try
            {
                byte hex, tam1, tam2;
                tam1 = (byte)(dec % 10);
                tam2 = (byte)(dec / 10);
                hex = (byte)(tam2 * 16 + tam1);
                return hex;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.DisplayCatchSystemException(ex);
                return 0;
            }

        }

        #region SEARCHED_FUCNCTION
        public static int find_bytes_left(byte[] src, byte[] find)
        {
            int index = -1;
            int matchIndex = 0;
            // handle the complete source array
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == find[matchIndex])
                {
                    if (matchIndex == (find.Length - 1))
                    {
                        index = i - matchIndex;
                        break;
                    }
                    matchIndex++;
                }
                else
                {
                    matchIndex = 0;
                }

            }
            return index;
        }

        public static int find_byte_left(byte[] src, byte find)
        {
            int index = -1;
            // handle the complete source array
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == find)
                {

                    index = i;
                    break;
                }
            }
            return index;
        }

        public static int find_byte_right(byte[] src, byte find)
        {
            int index = -1;
            // handle the complete source array
            //for (int i = 0; i < src.Length; i++)
            for (int i = src.Length - 1; i >= 0; i--)
            {
                if (src[i] == find)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public static int find_bytes_right(byte[] src, byte[] find)
        {
            int index = -1;
            int matchIndex = 0;
            // handle the complete source array
            //for (int i = 0; i < src.Length; i++)
            for (int i = src.Length - 1; i >= 0; i--)
            {
                if (src[i] == find[matchIndex])
                {
                    if (matchIndex == (find.Length - 1))
                    {
                        index = i - matchIndex;
                        break;
                    }
                    matchIndex++;
                }
                else
                {
                    matchIndex = 0;
                }

            }
            return index;
        }

        public static byte[] replace_bytes_left(byte[] src, byte[] search, byte[] repl)
        {
            byte[] dst = null;
            int index = find_bytes_left(src, search);
            if (index >= 0)
            {
                dst = new byte[src.Length - search.Length + repl.Length];
                // before found array
                Buffer.BlockCopy(src, 0, dst, 0, index);
                // repl copy
                Buffer.BlockCopy(repl, 0, dst, index, repl.Length);
                // rest of src array
                Buffer.BlockCopy(
                    src,
                    index + search.Length,
                    dst,
                    index + repl.Length,
                    src.Length - (index + search.Length));
            }
            return dst;
        }

        /// Tham kham them
        //public static byte[] ReplaceBytes(byte[] src, string replace, string replacewith)
        //{
        //    string hex = BitConverter.ToString(src);
        //    hex = hex.Replace("-", "");
        //    hex = hex.Replace(replace, replacewith);
        //    int NumberChars = hex.Length;
        //    byte[] bytes = new byte[NumberChars / 2];
        //    for (int i = 0; i < NumberChars; i += 2)
        //        bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        //    return bytes;
        //}
        #endregion
        #region DEGRESS
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     
        /// </summary>
        /// <param name="Radians"></param>
        /// <returns></returns>
        public static double ToDegrees(double Radians)
        {
            return Radians * 180 / Math.PI;
        }
        #endregion
        #region "Math"
        public static Double DegreesToRadians(Double degrees)
        {
            return 2 * Math.PI * degrees / 360.0;
        }
        #endregion

        #region "Parse"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sNumber"></param>
        /// <returns></returns>
        public static int Parse(string sNumber)
        {
            try
            {

                return int.Parse(sNumber);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sNumber"></param>
        /// <returns></returns>
        public static int Parse(string sNumber, NumberStyles ns)
        {
            try
            {
                return int.Parse(sNumber, ns);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return -1;
            }
        }
        #endregion

        #region ROUDING
        public static decimal RoundUp(decimal number, int places)
        {
            try
            {
                decimal factor = RoundFactor(places);
                number *= factor;
                number = Math.Ceiling(number);
                number /= factor;
            }
            catch { }
            return number;
        }

        public static decimal RoundDown(decimal number, int places)
        {
            try
            {
                decimal factor = RoundFactor(places);
                number *= factor;
                number = Math.Floor(number);
                number /= factor;
            }
            catch { }
            return number;
        }

        internal static decimal RoundFactor(int places)
        {
            decimal factor = 1m;
            if (places < 0)
            {
                places = -places;
                for (int i = 0; i < places; i++)
                    factor /= 10m;
            }
            else
            {
                for (int i = 0; i < places; i++)
                    factor *= 10m;
            }
            return factor;
        }
        #endregion

        #region TUNGNT COPY FROM GELEX
        public static string ByteToBCD(byte bytebcd)
        {
            string str = "";
            byte num = (byte)((bytebcd & 240) >> 4);
            byte num2 = (byte)(bytebcd & 15);
            if (num >= 10)
            {
                str = ToHex(num.ToString());
            }
            else
            {
                str = num.ToString();
            }
            if (num2 >= 10)
            {
                return (str + ToHex(num2.ToString()));
            }
            return (str + num2.ToString());
        }

        public static byte BCDToByte(string strBCD)
        {
            byte num = Convert.ToByte(strBCD[0].ToString());
            byte num2 = Convert.ToByte(strBCD[1].ToString());
            num = (byte)(num << 4);
            return (byte)(num | num2);
        }

        public static string ToHex(string sovao)
        {
            byte num = Convert.ToByte(sovao);
            if (num < 10)
            {
                return num.ToString();
            }
            switch (num)
            {
                case 10:
                    return "A";
                case 11:
                    return "B";
                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
            }
            return "";
        }
        #endregion
        #region smart meter         
        public static string byte2hex(byte Data)
        {
            try
            {
                return Data.ToString("X2");
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static byte hex2byte(string str2)
        {
            return byte.Parse(str2, NumberStyles.AllowHexSpecifier);
        }
        public static string hex2n_str1n(string hex2n)
        {
            string functionReturnValue = null;
            functionReturnValue = "";
            if (hex2n.Length % 2 != 0) return "";
            for (int i = 0; i <= hex2n.Length - 2; i += 2)
            {
                functionReturnValue += Convert.ToChar(byte.Parse(hex2n.Substring(i, 2), NumberStyles.AllowHexSpecifier));
            }
            return functionReturnValue;
        }
        public static string revertHex(string strhex)
        {
            string str_return = "";
            if (strhex.Length % 2 != 0)
                return "";
            for (int i = 0; i <= strhex.Length - 2; i += 2)
            {
                str_return = strhex.Substring(i, 2) + str_return;
            }
            return str_return;
        }
        #endregion
        #region UTC <=> RTC
        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            //return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
            return (dateTime - new DateTime(2012, 1, 1, 0, 0, 0)).TotalSeconds;//.ToLocalTime()).TotalSeconds;
        }


        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            //unixTimeStamp += 15340 * 86400;
            System.DateTime dtDateTime = new DateTime(2012, 1, 1, 0, 0, 0, 0);//(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);//.ToLocalTime();
            return dtDateTime;
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        #endregion

    }


    public static class clsDecryptFile
    {
        public static void Decrypt(string inputFilePath, string outputfilePath, string EncryptionKey)
        {
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }
        public static void Decrypt(string inputFilePath, ref DataSet ds, string EncryptionKey)
        {
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        ds.ReadXml(cs);
                    }
                }
            }
        }
    }
    public class clsEncryptFile
    {
        public static void Encrypt(string inputFilePath, string outputfilePath, string EncryptionKey)
        {
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }
    }

    public static class clsCRC
    {

        #region CRC8BIT

        public enum CRC8_POLY
        {
            CRC8 = 0xd5,
            CRC8_CCITT = 0x07,
            CRC8_DALLAS_MAXIM = 0x18, //0x31,
            CRC8_SAE_J1850 = 0x1D,
            CRC_8_WCDMA = 0x9b,
        };
        public const byte CRC8BIT_INIT = 0xFF;

        private static byte[] table = new byte[256] {
            0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126, 32, 163, 253, 31, 65,
            157, 195, 33, 127, 252, 162, 64, 30, 95, 1, 227, 189, 62, 96, 130, 220,
            35, 125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3, 128, 222, 60, 98,
            190, 224, 2, 92, 223, 129, 99, 61, 124, 34, 192, 158, 29, 67, 161, 255,
            70, 24, 250, 164, 39, 121, 155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
            219, 133, 103, 57, 186, 228, 6, 88, 25, 71, 165, 251, 120, 38, 196, 154,
            101, 59, 217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198, 152, 122, 36,
            248, 166, 68, 26, 153, 199, 37, 123, 58, 100, 134, 216, 91, 5, 231, 185,
            140, 210, 48, 110, 237, 179, 81, 15, 78, 16, 242, 172, 47, 113, 147, 205,
            17, 79, 173, 243, 112, 46, 204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
            175, 241, 19, 77, 206, 144, 114, 44, 109, 51, 209, 143, 12, 82, 176, 238,
            50, 108, 142, 208, 83, 13, 239, 177, 240, 174, 76, 18, 145, 207, 45, 115,
            202, 148, 118, 40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55, 213, 139,
            87, 9, 235, 181, 54, 104, 138, 212, 149, 203, 41, 119, 244, 170, 72, 22,
            233, 183, 85, 11, 136, 214, 52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
            116, 42, 200, 150, 21, 75, 169, 247, 182, 232, 10, 84, 215, 137, 107, 53};

        //////////////////////////////////////////////////////////////////////////
        public static byte _8bit_update(byte crc, char indata)
        {
            try
            {
                byte c = 0;
                c = table[crc ^ (byte)indata];
                return c;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0xFF;
            }
        }

        /// <summary>
        /// crc8bit_update
        /// </summary>
        /// <param name="crc"></param>
        /// <param name="indata"></param>
        /// <returns></returns>
        public static byte _8bit_update(byte crc, byte indata)
        {
            try
            {
                byte c = 0;
                c = table[crc ^ indata];
                return c;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0xFF;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// crc8bit_buffer
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte _8bit_buffer(byte[] val)
        {
            try
            {
                if (val == null) return 0; // throw new ArgumentNullException("val");
                byte c = CRC8BIT_INIT;
                foreach (byte b in val)
                {
                    c = table[c ^ b];
                }
                return c;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0xFF;
            }
        }

        public static byte _8bit_buffer(byte[] val, int index, int length)
        {
            try
            {
                if (val == null) return 0; // throw new ArgumentNullException("val");
                byte c = CRC8BIT_INIT;
                for (int i = index; i < index + length; i++)
                {
                    c = table[c ^ val[i]];
                }
                return c;
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0xFF;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// crc8bit_buffer
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte _8bit_buffer(string data)
        {
            try
            {
                if (data == null) return 0; // throw new ArgumentNullException("val");
                // chuyen sang buffer
                byte[] val = clsConvert.ToBytesLsb(data);// System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data);
                return _8bit_buffer(val);
            }
            catch (System.Exception ex)
            {
                //clsLogFile.CatchSystemException(ex);
                return 0xFF;
            }
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            //unixTimeStamp += 15340 * 86400;
            System.DateTime dtDateTime = new DateTime(2012, 1, 1, 0, 0, 0, 0);//(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);//.ToLocalTime();
            return dtDateTime;
        }
    }
    #endregion
}
