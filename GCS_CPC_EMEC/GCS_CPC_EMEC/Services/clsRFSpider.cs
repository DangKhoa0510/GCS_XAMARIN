using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GCS_CPC_EMEC.Services
{
    public static class clsRFSpider
    {
        public enum METER_TYPE
        {
            DT01PRF,
            DT01P80RF,
            DT03PRF,
            DT03P05RF,
            DT03MRF,
            DT01PRFSPIDER,
            DT01MRF
        };
        public struct MeterInfor
        {
            public uint serial;
            public uint serial_write;
            public double kWh;
            public double kWhExport;
            public double varImport;
            public double varExport;
            public double kWhImportNeural;
            public double kWhExportNeural;
            public double kWhWrap;
            public byte counter_prog;
            public byte counter_pulse;
            public int pulse_const;
            public Int16 security;
            public byte tamper;
            public byte maintain;
            public byte rssi_hu;
            public byte rssi_mesh;
            public byte rssi_meter;
            public string data;
            public byte pulse_count;
            public double apparent;
            public double[] voltage;
            public double volNeutral;
            public double[] current;
            public double curNeutral;
            public double[] active_power;
            public double active_power_neutral;
            public double[] reactive_power;
            public double[] apparent_power;
            public double[] power_factor;
            public double[] angle;
            public string raw_data_recv;
            public string raw_data_send;
            public uint displayID;
            public double[] q;
            public double[] t;
            //Elster
            public DateTime rtc;
            public DateTime rtc_write;
            public bool request;
            //
            public double kWhRemain;
            public byte statusRelay;
            public byte meterType;
            public byte error_code;
            public byte config_power;
            public byte timeDisplayDay; // thời gian hiển thị ngày, tháng, năm trên LCD khi có lệnh RF yêu cầu hiển thị
            public byte timeDisplayHour; // thời gian hiển thị giờ, phút, giây trên LCD khi có lệnh RF yêu cầu hiển thị
            public string key_aes;
            public byte index_tariff;
            public double[] tariff_reg_value;
            public double[] tariff_md_value;
            public DateTime[] tariff_md_time;

            public  void Init() 
            {
                serial = 0;
                kWh = 0;
                kWhWrap = 0;
                kWhExport = 0;
                varImport = 0;
                varExport = 0;
                counter_prog = 0;
                security = 0;
                tamper = 0;
                maintain = 0;
                rssi_hu = 0;
                rssi_mesh = 0;
                rssi_meter = 0;
                data = "";
                apparent = 0;
                voltage = new double[3];
                current = new double[3];
                active_power = new double[3];
                reactive_power = new double[3];
                apparent_power = new double[3];
                power_factor = new double[3];
                angle = new double[3];
                q = new double[4];
                t = new double[6];
                displayID = 0;
                rtc = DateTime.Now;
                rtc_write = new DateTime(2012, 1, 1, 0, 0, 0);
                raw_data_recv = "";
                raw_data_send = "";
                //rtc_write = DateTime.Now;
                request = false;
                kWhRemain = 0;
                statusRelay = 0;
                error_code = 0;
                pulse_count = 0;
                config_power = 0xC0;

                timeDisplayDay = 5;
                timeDisplayHour = 5;
                key_aes = "";
                index_tariff = 0;
                tariff_reg_value = new double[32];
                tariff_md_value = new double[32];
                tariff_md_time = new DateTime[32];
            }
        }
        public static MeterInfor meter_info = new MeterInfor();       
        public static uint huid = 99999999;
        public static byte[] keyEMEC = new byte[16]{ 0x45, 0x6D, 0x45, 0x43, 0x63, 0x50, 0x63, 0x72,
                                                              0x44, 0x45, 0x4D, 0x65, 0x74, 0x65, 0x72, 0x73};// EmECcPcrDEMeters
                                                                                                              //SUB_CMD DT03MRF

        public const byte DLT645_START_BYTE = 0x68;
        public const byte DLT645_STOP_BYTE = 0x16;

        //CMD RF
        public const byte CMD_READ_METER = 0x83;
        public const byte CMD_READ = 0xB9; // = 185  read kwh      // Đối với DT03M: CMD_KWH_IMPORT
        public const byte CMD_READ_ALL = 0xBA; // = 186
        public const byte CMD_READ_TARIFF_INFO = 0xF0;
        public const byte SUBCMD_READ_TARIFF_ALL = 0x21;
        public const byte CMD_READ_VAR_IMPORT = 0xCA;
        public const byte CMD_READ_VAR_EXPORT = 0xCB;

        #region "error code"
        public const int ERROR_CMD_WRONG = 1;
        public const int ERROR_DATASEND_PACKET = 2;
        public const int ERROR_ACCESS_PORT = 3;
        public const int ERROR_CRC_WRONG = 4;
        public const int ERROR_NO_DATA_RECEIVED = 5;
        public const int ERROR_WRONG_DATA_RECEIVED = 6;
        public const int ERROR_WRONG_FREQUENCY = 7;
        public const int ERROR_WRONG_BAUDRATE = 8;
        public const int ERROR_CC1101_CONFIG_FAIL = 9;
        public const int METER_TYPE_WRONG = 10;
        public const int METER_RF_CMD_ERROR = 11;
        public const int METER_SERIAL_ERROR = 12;
        public const int ERROR_PATABLE_VALUE = 13;
        public const int ERROR_WRITE_PORT = 14;
        public const int ERROR_LENGTH_DATA = 15;
        public const int ERROR_RFEXT_PROG = 16;
        public const int ERROR_SYSTEM = -1;
        #endregion


        public static int calcRSSIdBm(byte rssi)
        {
            int crssi = 0 - 75;// CC1101_RSSI_OFFSET;
            try
            {
                if (rssi >= 128)
                {
                    crssi = rssi - 256;
                    crssi /= 2;
                    crssi -= 75;// CC1101_RSSI_OFFSET;
                }
                else
                {
                    crssi = rssi / 2;
                    crssi -= 75;// CC1101_RSSI_OFFSET;
                }
            }
            catch { return -1; }
            return crssi;
        }


        public static int generate_message_rf(METER_TYPE _metertype, uint serial, byte cmd_rf, byte sub_cmd, ref string datasend, byte cmd)
        {
            try
            {
                string data_rf = "";
                switch (_metertype)
                {
                    case METER_TYPE.DT01PRFSPIDER:
                    case METER_TYPE.DT01PRF:
                        data_rf = clsConvert.ToStringLsb(serial) + clsConvert.ToStringLsb(cmd_rf);
                        break;

                    case METER_TYPE.DT01MRF:
                        if (cmd_rf == CMD_READ_TARIFF_INFO)
                        {
                            Aes aes = new Aes(Aes.KeySize.Bits128, keyEMEC);
                            byte[] cipher = new byte[16];
                            string data = "";
                            switch (sub_cmd)
                            {
                                case SUBCMD_READ_TARIFF_ALL:
                                    data = clsConvert.ToStringLsb(sub_cmd) + clsConvert.ToStringLsb(meter_info.index_tariff);
                                    if (data.Length < 16)
                                    {
                                        data = data.PadRight(16, '\0');
                                    }
                                    break;
                            }
                            string data_crc = data;
                            byte crc_aes = clsCRC._8bit_buffer(data_crc);
                            byte[] buffer = clsConvert.ToByteArray(clsConvert.ToStringHex(data));
                            clsGen.resize2AES128(ref buffer);
                            aes.encrypt(buffer, ref cipher);
                            buffer = cipher;
                            data = clsConvert.ToStringLsb(buffer, 16);
                            data_rf = clsConvert.ToStringLsb(serial) + clsConvert.ToStringLsb(cmd_rf) + data + clsConvert.ToStringLsb(crc_aes);
                            data_rf = data_rf + clsConvert.ToStringLsb(clsCRC._8bit_buffer(data_rf));
                        }
                        else
                        {
                            data_rf = clsConvert.ToStringLsb(serial) + clsConvert.ToStringLsb(cmd_rf);
                            byte _crc = clsCRC._8bit_buffer(data_rf);
                            data_rf += clsConvert.ToStringLsb(_crc);
                        }
                        break;

                    case METER_TYPE.DT03MRF:
                        if (cmd_rf == CMD_READ_TARIFF_INFO)
                        {
                            Aes aes = new Aes(Aes.KeySize.Bits128, keyEMEC);
                            byte[] cipher = new byte[16];
                            string data = "";
                            switch (sub_cmd)
                            {
                                case SUBCMD_READ_TARIFF_ALL:
                                    data = clsConvert.ToStringLsb(sub_cmd) + clsConvert.ToStringLsb(meter_info.index_tariff);
                                    if (data.Length < 16)
                                    {
                                        data = data.PadRight(16, '\0');
                                    }
                                    break;
                            }
                            string data_crc = data;
                            byte crc_aes = clsCRC._8bit_buffer(data_crc);
                            byte[] buffer = clsConvert.ToByteArray(clsConvert.ToStringHex(data));
                            clsGen.resize2AES128(ref buffer);
                            aes.encrypt(buffer, ref cipher);
                            buffer = cipher;
                            data = clsConvert.ToStringLsb(buffer, 16);
                            data_rf = clsConvert.ToStringLsb(serial) + clsConvert.ToStringLsb(cmd_rf) + data + clsConvert.ToStringLsb(crc_aes);
                            data_rf = data_rf + clsConvert.ToStringLsb(clsCRC._8bit_buffer(data_rf));
                        }
                        else
                        {
                            data_rf = clsConvert.ToStringLsb(serial) + clsConvert.ToStringLsb(cmd_rf);
                            byte _crc = clsCRC._8bit_buffer(data_rf);
                            data_rf += clsConvert.ToStringLsb(_crc);
                        }
                        break;
                    case METER_TYPE.DT03PRF:
                    case METER_TYPE.DT03P05RF:
                        data_rf = clsConvert.ToStringLsb(serial) + clsConvert.ToStringLsb(cmd_rf);
                        break;
                    case METER_TYPE.DT01P80RF:
                        // phân biệt với công tơ có RTC bằng IDs
                        // read, read all, request read, billing no answer
                        switch (cmd_rf)
                        {
                            default:
                                data_rf = clsConvert.ToStringLsb(serial) + clsConvert.ToStringLsb(cmd_rf);
                                byte _crc = clsCRC._8bit_buffer(data_rf);
                                data_rf += clsConvert.ToStringLsb(_crc);
                                break;
                        }
                        break;
                    default:
                        return METER_TYPE_WRONG;
                }
                byte length = 0;
                byte crc = 0;
                length = (byte)(data_rf.Length + 3); //length + cmd + data_rf + crc
                datasend = clsConvert.ToStringLsb(length) + clsConvert.ToStringLsb(cmd) + data_rf;
                crc = clsCRC._8bit_buffer(datasend);
                datasend += clsConvert.ToStringLsb(crc);
                datasend = clsConvert.ToStringLsb(DLT645_START_BYTE) + datasend + clsConvert.ToStringLsb(DLT645_STOP_BYTE);
                return 0;
            }
            catch { return ERROR_SYSTEM; }
        }

        public static int split_message(string buffer_str, ref string[] recv_data)
        {
            try
            {
                int error = 0;
                // Tach goi tin  
                int Count = 0;
                int count_meg = 0;
                while (Count < buffer_str.Length)
                {
                    // kiem tra byte start
                    if (buffer_str.Substring(Count, 1) != clsConvert.ToStringLsb(DLT645_START_BYTE)) break;
                    // length cua goi tin
                    byte length = clsConvert.ToByte(buffer_str.Substring(Count + 1, 1));
                    // kiem tra byte stop
                    if (buffer_str.Substring(Count + length + 1, 1) != clsConvert.ToStringLsb(DLT645_STOP_BYTE)) break;
                    // lay chuoi tung goi tin
                    recv_data[count_meg] = buffer_str.Substring(Count, length + 2);
                    //rs232data[count_meg] = buffer_str.Substring(Count, length + 1 - Count + 1);
                    Count = length + 2;
                    // kiem tra chieu dai
                    byte buffer_len = (byte)(clsConvert.ToByte(recv_data[count_meg], 1) + 2);            // + 2 : 2 byte start va stop
                    if (buffer_len != recv_data[count_meg].Length) continue;
                    recv_data[count_meg] = recv_data[count_meg].Substring(1, recv_data[count_meg].Length - 2); // bo byte start va stop
                    if (clsCRC._8bit_buffer(recv_data[count_meg]) != 0)
                    {
                        error = ERROR_CRC_WRONG;
                    }
                    recv_data[count_meg] = recv_data[count_meg].Substring(0, recv_data[count_meg].Length - 1); // bo byte crc
                                                                                                               // dem so goi tin
                    count_meg++;
                }
                Array.Resize(ref recv_data, count_meg);
                return error;
            }

            catch (Exception ex)
            {
                return ERROR_SYSTEM;
            }
        }

        public static int analyze_message_rf(METER_TYPE _metertype, uint serial, byte cmd_rf, byte sub_cmd, string datarecv, ref string profileData)
        {
            try
            {
                byte length = clsConvert.ToByte(datarecv.Substring(0, 1));
                byte cmd_rev = clsConvert.ToByte(datarecv.Substring(2, 1));
                string data = "";
                byte crc_encrypt_recv = 0;
                byte crc_encrypt_cal = 0;
                byte crc_decrypt_recv = 0;
                byte crc_decrypt_cal = 0;
                byte cmd_rf_recv = 0;
                data = datarecv.Substring(2); //bo byte length + cmd

                switch (_metertype)//if (aes_flag == true) //&& _metertype == METER_TYPE.DT01P80RF.ToString())
                {
                    case METER_TYPE.DT01P80RF:


                        cmd_rf_recv = clsConvert.ToByte(data);
                        if (cmd_rf != cmd_rf_recv) return ERROR_WRONG_DATA_RECEIVED;
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 1);
                        if (meter_info.serial != serial) return ERROR_WRONG_DATA_RECEIVED;
                        /////////////////////////////tinh crc cua goi tin chua ma hoa////////////////////////////////////////
                        //4 byte ID + 1 byte cmd + 16(hoac lon hon) byte data + 1 byte crc goi tin chua ma hoa + crc sau ma hoa + rssi_hu
                        crc_encrypt_recv = clsConvert.ToByte(data, data.Length - 2);
                        crc_encrypt_cal = clsCRC._8bit_buffer(data.Substring(0, data.Length - 2));
                        if (crc_encrypt_recv != crc_encrypt_cal) return ERROR_CRC_WRONG;

                        crc_decrypt_recv = clsConvert.ToByte(data, data.Length - 3);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////
                        {
                            //giai ma 16 byte duoc ma hoa
                            byte[] buffer = clsConvert.ToByteArray(clsConvert.ToStringHex(data.Substring(5, 16)));
                            byte[] deciphered = new byte[16];
                            Aes aes = new Aes(Aes.KeySize.Bits128, keyEMEC);
                            aes.decrypt(buffer, ref deciphered);
                            buffer = deciphered;
                            ////////////////////////////////////////////////////////////////////////
                            crc_decrypt_cal = clsCRC._8bit_buffer(clsConvert.ToStringLsb(buffer));
                            if (crc_decrypt_recv != crc_decrypt_cal)
                            {
                                return ERROR_CRC_WRONG;
                            }
                            ////////////////////////////////////////////////////////////////////////
                            data = data.Substring(0, 5) + clsConvert.ToStringLsb(buffer) + data.Substring(21, data.Length - 21);
                        }
                        break;
                    case METER_TYPE.DT01MRF:
                    case METER_TYPE.DT03MRF:
                        cmd_rf_recv = clsConvert.ToByte(data);
                        if (cmd_rf != cmd_rf_recv) return ERROR_WRONG_DATA_RECEIVED;
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 1);
                        if (meter_info.serial != serial) return ERROR_WRONG_DATA_RECEIVED;
                        /////////////////////////////tinh crc cua goi tin chua ma hoa////////////////////////////////////////
                        //4 byte ID + 1 byte cmd + 16(hoac lon hon) byte data + 1 byte crc goi tin chua ma hoa + crc sau ma hoa + rssi_hu
                        crc_encrypt_recv = clsConvert.ToByte(data, data.Length - 2);
                        crc_encrypt_cal = clsCRC._8bit_buffer(data.Substring(0, data.Length - 2));
                        if (crc_encrypt_recv != crc_encrypt_cal) return ERROR_CRC_WRONG;

                        crc_decrypt_recv = clsConvert.ToByte(data, data.Length - 3);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////
                        {
                            //giai ma 16 byte duoc ma hoa
                            byte[] buffer = clsConvert.ToByteArray(clsConvert.ToStringHex(data.Substring(5, 16)));
                            byte[] deciphered = new byte[16];
                            Aes aes = new Aes(Aes.KeySize.Bits128, keyEMEC);
                            aes.decrypt(buffer, ref deciphered);
                            buffer = deciphered;
                            ////////////////////////////////////////////////////////////////////////

                            if (data.Length < 24)
                            {
                                crc_decrypt_cal = clsCRC._8bit_buffer(clsConvert.ToStringLsb(buffer));
                            }
                            else crc_decrypt_cal = clsCRC._8bit_buffer(clsConvert.ToStringLsb(buffer) + data.Substring(21, data.Length - 24));
                            if (crc_decrypt_recv != crc_decrypt_cal)
                            {
                                return ERROR_CRC_WRONG;
                            }
                            ////////////////////////////////////////////////////////////////////////
                            data = data.Substring(0, 5) + clsConvert.ToStringLsb(buffer) + data.Substring(21, data.Length - 21);
                        }
                        break;
                    case METER_TYPE.DT03PRF:

                        cmd_rf_recv = clsConvert.ToByte(data);
                        if (cmd_rf != cmd_rf_recv) return ERROR_WRONG_DATA_RECEIVED;
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 1);
                        if (meter_info.serial != serial) return ERROR_WRONG_DATA_RECEIVED;
                        /////////////////////////////tinh crc cua goi tin chua ma hoa////////////////////////////////////////
                        //4 byte ID + 1 byte cmd + 16(hoac lon hon) byte data + 1 byte crc goi tin chua ma hoa + crc sau ma hoa + rssi_hu
                        crc_encrypt_recv = clsConvert.ToByte(data, data.Length - 2);
                        crc_encrypt_cal = clsCRC._8bit_buffer(data.Substring(0, data.Length - 2));
                        if (crc_encrypt_recv != crc_encrypt_cal) return ERROR_CRC_WRONG;

                        crc_decrypt_recv = clsConvert.ToByte(data, data.Length - 3);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////
                        {
                            //giai ma 16 byte duoc ma hoa
                            byte[] buffer = clsConvert.ToByteArray(clsConvert.ToStringHex(data.Substring(5, 16)));
                            byte[] deciphered = new byte[16];
                            Aes aes = new Aes(Aes.KeySize.Bits128, keyEMEC);
                            aes.decrypt(buffer, ref deciphered);
                            buffer = deciphered;
                            ////////////////////////////////////////////////////////////////////////
                            if (data.Length < 24)
                            {
                                crc_decrypt_cal = clsCRC._8bit_buffer(clsConvert.ToStringLsb(buffer));
                            }
                            else crc_decrypt_cal = clsCRC._8bit_buffer(clsConvert.ToStringLsb(buffer) + data.Substring(21, data.Length - 24));
                            if (crc_decrypt_recv != crc_decrypt_cal)
                            {
                                return ERROR_CRC_WRONG;
                            }
                            ////////////////////////////////////////////////////////////////////////
                            data = data.Substring(0, 5) + clsConvert.ToStringLsb(buffer) + data.Substring(21, data.Length - 21);
                        }
                        break;
                    case METER_TYPE.DT03P05RF:

                        cmd_rf_recv = clsConvert.ToByte(data);
                        if (cmd_rf != cmd_rf_recv) return ERROR_WRONG_DATA_RECEIVED;
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 1);
                        if (meter_info.serial != serial) return ERROR_WRONG_DATA_RECEIVED;
                        /////////////////////////////tinh crc cua goi tin chua ma hoa////////////////////////////////////////
                        //4 byte ID + 1 byte cmd + 16(hoac lon hon) byte data + 1 byte crc goi tin chua ma hoa + crc sau ma hoa + rssi_hu
                        crc_encrypt_recv = clsConvert.ToByte(data, data.Length - 2);
                        crc_encrypt_cal = clsCRC._8bit_buffer(data.Substring(0, data.Length - 2));
                        if (crc_encrypt_recv != crc_encrypt_cal) return ERROR_CRC_WRONG;

                        crc_decrypt_recv = clsConvert.ToByte(data, data.Length - 3);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////
                        {
                            //giai ma 16 byte duoc ma hoa
                            byte[] buffer = clsConvert.ToByteArray(clsConvert.ToStringHex(data.Substring(5, 16)));
                            byte[] deciphered = new byte[16];
                            Aes aes = new Aes(Aes.KeySize.Bits128, keyEMEC);
                            aes.decrypt(buffer, ref deciphered);
                            buffer = deciphered;
                            ////////////////////////////////////////////////////////////////////////
                            if (data.Length < 24)
                            {
                                crc_decrypt_cal = clsCRC._8bit_buffer(clsConvert.ToStringLsb(buffer));
                            }
                            else crc_decrypt_cal = clsCRC._8bit_buffer(clsConvert.ToStringLsb(buffer) + data.Substring(21, data.Length - 24));
                            if (crc_decrypt_recv != crc_decrypt_cal)
                            {
                                return ERROR_CRC_WRONG;
                            }
                            ////////////////////////////////////////////////////////////////////////
                            data = data.Substring(0, 5) + clsConvert.ToStringLsb(buffer) + data.Substring(21, data.Length - 21);
                        }
                        break;

                    default:
                        cmd_rf_recv = clsConvert.ToByte(data);
                        if (cmd_rf != cmd_rf_recv) return ERROR_WRONG_DATA_RECEIVED;
                        break;
                }
                switch (_metertype)
                {
                    default:
                        data = data.Substring(1);
                        break;
                }

                meter_info.data = clsConvert.ToStringHex(datarecv);
                if (length == 9) { return ERROR_LENGTH_DATA; }
                //if (datarecv.Length < length) return ERROR_LENGTH_DATA;
                int error = -1;
                switch (_metertype)
                {
                    case METER_TYPE.DT01PRF:
                        error = analyze_message_dt01prf(serial, cmd_rf, data, ref profileData);
                        break;
                    case METER_TYPE.DT01PRFSPIDER:
                        error = analyze_message_dt01prfspider(serial, cmd_rf, data, ref profileData);
                        break;
                    case METER_TYPE.DT01P80RF:
                        error = analyze_message_dt01p80rf(serial, cmd_rf, sub_cmd, data, ref profileData);
                        break;
                    case METER_TYPE.DT03PRF:
                        error = analyze_message_dt03prf(serial, cmd_rf, data, ref profileData);
                        break;
                    case METER_TYPE.DT03P05RF:
                        error = analyze_message_dt03p05rf(serial, cmd_rf, data, ref profileData);
                        break;
                    case METER_TYPE.DT01MRF:
                    case METER_TYPE.DT03MRF:
                        if (cmd_rev != cmd_rf) return -1;
                        error = analyze_message_dt03mrf(serial, cmd_rf, sub_cmd, data, ref profileData);
                        break;
                    default: return METER_TYPE_WRONG;
                }
                if (serial != meter_info.serial)
                {
                    return METER_SERIAL_ERROR;
                }
                return error;
            }
            catch (Exception ex)
            {
                return ERROR_SYSTEM;
            }
        }

        private static int analyze_message_dt01prf(uint serial, byte cmd_rf, string data, ref string profileData)
        {
            try
            {
                switch (cmd_rf)
                {
                    case CMD_READ:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                        if (meter_info.serial >= 17000001 || (meter_info.serial >= 14218661 && meter_info.serial <= 14218688)
                            || meter_info.serial.ToString().Substring(0, 4) == "1460" || meter_info.serial.ToString().Substring(0, 4) == "1560"
                            || meter_info.serial.ToString().Substring(0, 4) == "1665")
                        {
                            meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data.Substring(4, 4))) / 100;
                        }
                        else
                        {
                            meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data.Substring(4, 3).PadRight(4, '\0'), 0)) / 10;
                        }
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;
                    case CMD_READ_ALL:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                        meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data.Substring(4, 3).PadRight(4, '\0'), 0)) / 10;
                        meter_info.kWhWrap = (double)(clsConvert.ToUInt32Lsb(data.Substring(7, 3).PadRight(4, '\0'), 0)) / 10;
                        meter_info.counter_prog = clsConvert.ToByte(data, 10);
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;
                    default:
                        return METER_RF_CMD_ERROR;
                }

                if (serial != meter_info.serial) return ERROR_WRONG_DATA_RECEIVED;
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private static int analyze_message_dt01prfspider(uint serial, byte cmd_rf, string data, ref string profileData)
        {
            try
            {
                switch (cmd_rf)
                {
                    case CMD_READ:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                        meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data, 4)) / 100;
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;
                    case CMD_READ_ALL:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                        meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data, 4)) / 100;
                        // TUNGNT: các thông số khác, chưa bik, bổ sung sau
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;
                    default:
                        return METER_RF_CMD_ERROR;
                }
                if (serial != meter_info.serial) return ERROR_WRONG_DATA_RECEIVED;
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private static int analyze_message_dt01p80rf(uint serial, byte cmd_rf, byte sub_cmd, string data, ref string profileData)
        {
            bool aes = true;
            try
            {
                byte sub_cmd_recv = 0;
                switch (cmd_rf)
                {
                    case CMD_READ:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                        if (data.Length == 9) //RF-EXT
                        {
                            meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data, 4)) / 100;
                            meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                            profileData = meter_info.kWh.ToString();
                        }
                        else
                        {
                            if (data.Length == 14)
                            {
                                meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data, 4)) / 100;
                                meter_info.kWhWrap = (double)(clsConvert.ToUInt32Lsb(data, 8)) / 100;
                                meter_info.counter_prog = clsConvert.ToByte(data, 12);
                            }
                            else
                            {
                                if (data.Length >= 15) //&& data.Length < 20)
                                {
                                    meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data, 4)) / 100;
                                    meter_info.kWhWrap = (double)(clsConvert.ToUInt32Lsb(data, 8)) / 100;
                                    meter_info.counter_prog = clsConvert.ToByte(data, 12);
                                    meter_info.security = clsConvert.ToByte(data, 13);
                                    if (data.Length >= 16)
                                    {
                                        meter_info.tamper = clsConvert.ToByte(data, 14);

                                        meter_info.rssi_meter = clsConvert.ToByte(data, 15);

                                    }
                                }
                                else return -1;
                            }
                        }
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;

                    case CMD_READ_ALL:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                        if (data.Length >= 13)
                        {
                            meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data, 4)) / 100;
                            meter_info.kWhExport = (double)(clsConvert.ToUInt32Lsb(data, 8)) / 100;
                            if (data.Length == 14)
                                meter_info.security = clsConvert.ToByte(data, 12);
                            meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                            profileData = meter_info.kWh.ToString();
                            return 0;

                        }
                        if (data.Length >= 15)// && data.Length < 20)
                        {
                            meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data, 4)) / 100;
                            meter_info.kWhExport = (double)(clsConvert.ToUInt32Lsb(data, 8)) / 100;

                            meter_info.counter_prog = clsConvert.ToByte(data, 12); // mới bổ sung thêm ngày 03/07/2015
                            if (data.Length >= 15)
                            {
                                meter_info.security = clsConvert.ToByte(data, 13);
                                meter_info.tamper = clsConvert.ToByte(data, 14);
                            }
                            if (aes == true)
                            {
                                meter_info.rssi_meter = clsConvert.ToByte(data, 15);
                            }

                        }
                        else return -1;
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;

                    default:
                        //1 BYTE CMD, 4 BYTE ID, 4 BYTE DATA
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                        meter_info.kWh = (double)(clsConvert.ToUInt32Lsb(data, 4)) / 100;
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;
                }
                if ((meter_info.security & 0x10) == 0x10)
                {
                    return -1;
                }
                if (serial != meter_info.serial) return ERROR_WRONG_DATA_RECEIVED;
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private static int analyze_message_dt03prf(uint serial, byte cmd_rf, string data, ref string profileData)
        {
            try
            {

                switch (cmd_rf)
                {
                    case CMD_READ:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);

                        meter_info.kWh = (double)(clsConvert.ToUInt64Lsb(data, 4)) / 10000.0;
                        meter_info.counter_prog = clsConvert.ToByte(data, 12);
                        meter_info.security = clsConvert.ToInt16Msb(data, 13);
                        meter_info.rssi_meter = clsConvert.ToByte(data, 15);

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;
                    case CMD_READ_ALL:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);

                        meter_info.kWh = (double)(clsConvert.ToUInt64Lsb(data, 4)) / 10000;
                        meter_info.kWhExport = (double)(clsConvert.ToUInt64Lsb(data, 12)) / 10000;
                        meter_info.counter_prog = clsConvert.ToByte(data, 20);
                        meter_info.security = clsConvert.ToInt16Msb(data, 21);
                        meter_info.rssi_meter = clsConvert.ToByte(data, 23);

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;

                    case CMD_READ_VAR_IMPORT:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                        meter_info.varImport = (double)(clsConvert.ToUInt64Lsb(data, 4)) / 10000.0;
                        meter_info.security = clsConvert.ToInt16Msb(data, 12);
                        meter_info.rssi_meter = clsConvert.ToByte(data, 14);
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.varImport.ToString();
                        break;
                    case CMD_READ_VAR_EXPORT:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                        meter_info.varExport = (double)(clsConvert.ToUInt64Lsb(data, 4)) / 10000.0;
                        meter_info.security = clsConvert.ToInt16Msb(data, 12);
                        meter_info.rssi_meter = clsConvert.ToByte(data, 14);
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.varExport.ToString();
                        break;

                }
                if (serial != meter_info.serial) return ERROR_WRONG_DATA_RECEIVED;
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private static int analyze_message_dt03p05rf(uint serial, byte cmd_rf, string data, ref string profileData)
        {
            try
            {

                switch (cmd_rf)
                {
                    case CMD_READ:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);

                        meter_info.kWh = (double)(clsConvert.ToUInt64Lsb(data, 4)) / 10000;
                        meter_info.counter_prog = clsConvert.ToByte(data, 12);

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;
                    case CMD_READ_ALL:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);

                        meter_info.kWh = (double)(clsConvert.ToUInt64Lsb(data, 4)) / 10000;
                        meter_info.kWhExport = (double)(clsConvert.ToUInt64Lsb(data, 12)) / 10000;
                        meter_info.counter_prog = clsConvert.ToByte(data, 20);

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;

                    case CMD_READ_VAR_IMPORT:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);

                        meter_info.varImport = (double)(clsConvert.ToUInt64Lsb(data, 4)) / 10000.0;

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        if (data.Length == 10)
                        {
                            meter_info.security = clsConvert.ToByte(data, 8);
                        }
                        profileData = meter_info.varImport.ToString();
                        break;
                    case CMD_READ_VAR_EXPORT:
                        meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);

                        meter_info.varExport = (double)(clsConvert.ToUInt64Lsb(data, 4)) / 10000.0;

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        if (data.Length == 10)
                        {
                            meter_info.security = clsConvert.ToByte(data, 8);
                        }
                        profileData = meter_info.varExport.ToString();
                        break;

                    default:
                        return ERROR_WRONG_DATA_RECEIVED;
                }
                if (serial != meter_info.serial) return ERROR_WRONG_DATA_RECEIVED;
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public static int analyze_message_dt03mrf(uint serial, byte cmd_rf, byte sub_cmd, string data, ref string profileData)
        {
            try
            {
                meter_info.serial = clsConvert.ToUInt32Lsb(data, 0);
                data = data.Substring(4);
                switch (cmd_rf)
                {
                    case CMD_READ:

                        meter_info.kWh = clsConvert.ToUInt64Lsb(data, 0) / 10000.0;
                        meter_info.counter_prog = clsConvert.ToByte(data, 8);
                        meter_info.security = clsConvert.ToInt16Lsb(data, 9);
                        meter_info.rssi_meter = clsConvert.ToByte(data, 11);

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;
                    case CMD_READ_ALL:

                        meter_info.kWh = clsConvert.ToUInt64Lsb(data, 0) / 10000.0;
                        meter_info.kWhExport = ((double)clsConvert.ToUInt64Lsb(data, 8)) / 10000.0;
                        meter_info.counter_prog = clsConvert.ToByte(data, 16);
                        meter_info.security = clsConvert.ToInt16Lsb(data, 17);
                        meter_info.rssi_meter = clsConvert.ToByte(data, 19);

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;

                    case CMD_READ_VAR_IMPORT:

                        meter_info.varImport = ((double)clsConvert.ToUInt64Lsb(data, 0)) / 10000.0;
                        meter_info.security = clsConvert.ToInt16Lsb(data, 8);
                        meter_info.rssi_meter = clsConvert.ToByte(data, 10);

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.varImport.ToString();
                        break;
                    case CMD_READ_VAR_EXPORT:

                        meter_info.varExport = ((double)clsConvert.ToUInt64Lsb(data, 0)) / 10000.0;
                        meter_info.security = clsConvert.ToInt16Lsb(data, 8);
                        meter_info.rssi_meter = clsConvert.ToByte(data, 10);

                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.varExport.ToString();
                        break;

                    case CMD_READ_TARIFF_INFO:
                        byte sub_cmd_recv = clsConvert.ToByte(data, 0);
                        meter_info.error_code = 0;
                        if (sub_cmd_recv == 0x15)
                        {
                            meter_info.error_code = clsConvert.ToByte(data, 1);
                            meter_info.security = clsConvert.ToInt16Lsb(data, 2);
                            meter_info.rssi_meter = clsConvert.ToByte(data, 4);
                            return 0;
                        }
                        byte index = clsConvert.ToByte(data, 1);
                        if ((sub_cmd_recv != sub_cmd) || (index != meter_info.index_tariff))
                            return -1;
                        data = data.Substring(2);
                        double unixTimeStamp = 0;
                        switch (sub_cmd)
                        {
                            case SUBCMD_READ_TARIFF_ALL:

                                meter_info.tariff_reg_value[index] = ((double)clsConvert.ToUInt64Lsb(data, 0)) / 10000.0;
                                meter_info.tariff_md_value[index] = ((double)clsConvert.ToInt64Lsb(data, 8)) / 100.0;
                                unixTimeStamp = clsConvert.ToUInt32Lsb(data, 16);
                                meter_info.tariff_md_time[index] = clsConvert.UnixTimeStampToDateTime(unixTimeStamp);
                                meter_info.security = clsConvert.ToInt16Lsb(data, 20);
                                meter_info.rssi_meter = clsConvert.ToByte(data, 22);

                                break;
                        }
                        meter_info.rssi_hu = clsConvert.ToByte(data, data.Length - 1);
                        profileData = meter_info.kWh.ToString();
                        break;

                    default:
                        return ERROR_WRONG_DATA_RECEIVED;
                }
                if (serial != meter_info.serial) return ERROR_WRONG_DATA_RECEIVED;
                return 0;
            }
            catch (Exception ex)
            {
                return ERROR_SYSTEM;
            }
        }
    }
}
