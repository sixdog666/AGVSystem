using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;
using System.Threading;
using System.IO;

namespace ControlObjects.S7 {
    public enum PlcCpuType {
        S7200 = 0,
        S7300 = 10,
        S7400 = 20,
        S71200 = 30,
        S71500 = 40
    }
    class S7System : DeviceBase {
        Plc plc;
     
        public S7System(string strName, PlcCpuType cpuType, string PLCIp, short rack, short slot) : base(strName) {
            CpuType cpu = (CpuType)cpuType;
            plc = new Plc(cpu, PLCIp, rack, slot);
        }

        public override bool Connect() {
            try {
                plc.Open();
                return true;
            } catch (Exception ) {

                return false;
            }
        }

        public override void Disconnect() {
            try {

                if (plc.IsConnected)
                    plc.Close();

            }
            catch (Exception) {

            }
     
        }

        public override bool IsConnect() {
            return plc.IsConnected;
        }

        public override object ReadInputSignal(string signalName) {
            if (Inputs.ContainsKey(signalName)) {
                AdressValueInfo value = (AdressValueInfo)Inputs[signalName];
                switch (value.valueType.ToString()) {
                    case "System.UInt16":
                        return ReadUInt16(value.adress);
                    case "System.UInt32":
                        return ReadUInt32(value.adress);
                    case "System.UInt64":
                        return ReadUInt64(value.adress);
                    case "System.Int16":
                        return ReadInt16(value.adress);
                    case "System.Int32":
                        return ReadInt32(value.adress);
                    case "System.Int64":
                        return ReadInt64(value.adress);
                    case "System.Boolean":
                        return ReadBoolean(value.adress);
                    case "System.Byte":
                        return ReadByte(value.adress);
                    case "System.SByte":
                        return ReadSbyte(value.adress);
                    case "System.Double":
                        return ReadDouble(value.adress);
                    case "System.Single":
                        return ReadSingle(value.adress);
                    case "System.String":
                        return ReadString(value.adress, value.length);
                }
            }
            return null;
        }


        public override void WriteOutputSignal(string signalName, object value) {
            if (Outputs.ContainsKey(signalName)) {
                AdressValueInfo output = (AdressValueInfo)Outputs[signalName];
                S7Adress signalAdress = (S7Adress)output.adress;
                plc.Write((DataType)signalAdress.dataType, signalAdress.dbAdress, signalAdress.startAdress, value);
            }
        }
        protected  bool ReadBoolean(DeviceAdress adress) {
            S7Adress signalAdress = (S7Adress)adress;
            bool result = (bool)plc.Read((DataType)signalAdress.dataType, signalAdress.dbAdress, signalAdress.startAdress, VarType.Bit, 1);
            return result;
        }

        protected  byte ReadByte(DeviceAdress adress) {

            S7Adress signalAdress = (S7Adress)adress;
            byte result = (byte)plc.Read((DataType)signalAdress.dataType, signalAdress.dbAdress,signalAdress.startAdress,VarType.Byte,1);
            return result;
        }

        protected  double ReadDouble(DeviceAdress adress) {
            S7Adress signalAdress = (S7Adress)adress;
            byte[] result = (byte[])plc.Read((DataType)signalAdress.dataType, signalAdress.dbAdress, signalAdress.startAdress, VarType.DInt, signalAdress.byteCount);
            return result[0];
        }

        protected  short ReadInt16(DeviceAdress adress) {
            S7Adress signalAdress = (S7Adress)adress;
            short result = (short)plc.Read((DataType)signalAdress.dataType, signalAdress.dbAdress, signalAdress.startAdress, VarType.Int, 1);
            return result;
        }

        protected  int ReadInt32(DeviceAdress adress) {
            S7Adress signalAdress = (S7Adress)adress;
            int result = (int)plc.Read((DataType)signalAdress.dataType, signalAdress.dbAdress, signalAdress.startAdress, VarType.DInt, 1);
            return result;

        }

        protected  long ReadInt64(DeviceAdress adress) {

            S7Adress signalAdress = (S7Adress)adress;
            long result = (long)plc.Read((DataType)signalAdress.dataType, signalAdress.dbAdress, signalAdress.startAdress, VarType.DWord, 1);
            return result;
        }

        protected  sbyte ReadSbyte(DeviceAdress adress) {
            throw new NotImplementedException();
        }

        protected  float ReadSingle(DeviceAdress adress) {
            S7Adress signalAdress = (S7Adress)adress;
            float result = (float)plc.Read((DataType)signalAdress.dataType, signalAdress.dbAdress, signalAdress.startAdress, VarType.Real, 1);
            return result;
        }

        protected  string ReadString(DeviceAdress adress, ushort length) {
            S7Adress signalAdress = (S7Adress)adress;
            string result = (string)plc.Read((DataType)signalAdress.dataType, signalAdress.dbAdress, signalAdress.startAdress, VarType.String, length);
            return result;
        }

        protected  ushort ReadUInt16(DeviceAdress adress) {
            throw new NotImplementedException();
        }

        protected  uint ReadUInt32(DeviceAdress adress) {
            throw new NotImplementedException();
        }

        protected  ulong ReadUInt64(DeviceAdress adress) {
            throw new NotImplementedException();
        }

        protected override void ReadRealTimeSignal() {
            if (inputThreadRun) return;
            inputThreadRun = true;
            S7Adress startAdress = (S7Adress)startInputsAdress;
            while (IsConnect() && inputThreadRun) {
                SynInputs = plc.ReadBytes((DataType)startAdress.dataType,startAdress.dbAdress,startAdress.startAdress,startAdress.byteCount);
                Thread.Sleep(100);

            }
        }

        protected override void WriteRealTimeSignal() {
            if (outputThreadRun) return;
            outputThreadRun = true;
            while (IsConnect() && outputThreadRun) {
                S7Adress signalAdress = (S7Adress)startOutputsAdress;
                plc.WriteBytes((DataType)signalAdress.dataType,signalAdress.dbAdress,signalAdress.startAdress,SynOutputs);
                Thread.Sleep(100);
            }
        }


        private void UpdateRealTimeDB() {
            for (int i = 0; i < realTimeInputSignals.Rows.Count; i++) {
                int adress = Convert.ToInt32(realTimeInputSignals.Rows[i][(int)eS7RealTimeAdressTitle.StartAdress]);
                int offset = Convert.ToInt32(realTimeInputSignals.Rows[i][(int)eS7RealTimeAdressTitle.Offset]);
                int length = Convert.ToInt32(realTimeInputSignals.Rows[i][(int)eS7RealTimeAdressTitle.Length]);
                int startAdress = Convert.ToInt32(((S7Adress)startInputsAdress).startAdress);
                realTimeInputSignals.Rows[i][(int)eS7RealTimeAdressTitle.Value] = ConvertValueToByte( SynInputs[adress - startAdress],offset,length);
            }
        }

        private byte[] GetRealTimeOutputDB() {
            byte[] bytes = new byte[128];
            for (int i = 0; i < realTimeOutputSignals.Rows.Count; i++) {
                int adress = Convert.ToInt32(realTimeInputSignals.Rows[i][(int)eS7RealTimeAdressTitle.StartAdress]);
                int startAdress = Convert.ToInt32(((S7Adress)startInputsAdress).startAdress);
                int offset = Convert.ToInt32(realTimeInputSignals.Rows[i][(int)eS7RealTimeAdressTitle.Offset]);
                int length = Convert.ToInt32(realTimeInputSignals.Rows[i][(int)eS7RealTimeAdressTitle.Length]);
                byte value = Convert.ToByte(realTimeOutputSignals.Rows[i][(int)eS7RealTimeAdressTitle.Value]);
                bytes[adress - startAdress] = Convert.ToByte(SynOutputs[adress - startAdress] & ConvertValueToByte(value, offset, length));
            }
            return bytes;
        }
        private byte ConvertValueToByte(byte value, int offset, int length) {
            byte byteValue = Convert.ToByte(value << offset);
            byte temByte=0;
            for (int i = 0; i < length; i++) {
                temByte =Convert.ToByte( temByte + Math.Pow(2, offset + i));

            }
            temByte = Convert.ToByte(byte.MaxValue - temByte);
            byteValue = Convert.ToByte(byteValue & temByte);

            return byteValue;
        }

        private byte ConvertByteToValue(byte byteValue, int offset, int length) {
            byte value = Convert.ToByte((byteValue >> offset) & Convert.ToByte(Math.Pow(2, length) - 1));
            return value;
            // value = value & Convert.ToByte(Math.Pow(2, lenth) - 1);
        }
        public void ReadAressFile(string filePath) {
            string path = System.Environment.CurrentDirectory;
            path = filePath + "\\S7Adress";
            if (!File.Exists(path)) {
                throw new Exception("配置文件不存在！");
            }
            using (StreamReader sr = new StreamReader(path)) {
                var line = sr.ReadLine();
                while (line != null) {
                    line = sr.ReadLine();
                    string[] strs = line.Split(',');
                    if (strs[0] != "No" && strs[0] != string.Empty) {
                        if (strs[(int)eS7AdresssTitle.IsInput].Trim(' ') == "1") {
                            S7Adress adress = new S7Adress();
                            adress.startAdress=Convert.ToInt32(strs[(int)eS7AdresssTitle.StartAdress]);
                            adress.dbAdress = Convert.ToInt32(strs[(int)eS7AdresssTitle.DBAdress]);
                            adress.dataType = GetAdressDataType(strs[(int)eS7AdresssTitle.DataType]);
                            AdressValueInfo valueInfo = new AdressValueInfo() {
                                adress = adress,
                                deviceValue = new S7Value(),
                                length = Convert.ToUInt16(strs[(int)eS7AdresssTitle.ByteCount]),
                                valueType = GetAdressType(strs[(int)eS7AdresssTitle.AdressType]),
                            };
                            this.AddInputSignal(strs[(int)eS7AdresssTitle.AdressName], valueInfo);
                        }
                        if (strs[(int)eS7AdresssTitle.IsOutput].Trim(' ') == "1") {
                          
                        }
                    }
                }
            }
        }

        public void ReadRealAdressFile(string filePath) {
            string path = System.Environment.CurrentDirectory;
            path = filePath + "\\S7RealTimeAdress";
            if (!File.Exists(path)) {
                throw new Exception("配置文件不存在！");
            }
            using (StreamReader sr = new StreamReader(path)) {
                try {
                    var line = sr.ReadLine();
                    string[] strs = line.Split(',');
                    if (strs[0] == "No") {
                        this.realTimeInputSignals = new System.Data.DataTable();
                        this.realTimeOutputSignals = new System.Data.DataTable();
                        for (int i = 0; i < strs.Length; i++) {
                            this.realTimeInputSignals.Columns.Add(strs[i]);
                            this.realTimeOutputSignals.Columns.Add(strs[i]);
                        }
                    }
                    while (line != null) {
                        line = sr.ReadLine();
                        strs = line.Split(',');
                        if (strs[0] != "No" && strs[0] != string.Empty) {
                            if (strs[(int)eS7RealTimeAdressTitle.IsInput] == "1") {
                                System.Data.DataRow dr = this.RealTimeInputSignals.NewRow();
                                for (int i = 0; i < this.realTimeInputSignals.Columns.Count; i++) {
                                    dr[i] = strs[i];
                                }
                                this.realTimeInputSignals.Rows.Add(dr);
                            }
                            else if (strs[(int)eS7RealTimeAdressTitle.IsOutput] == "1") {
                                System.Data.DataRow dr = this.realTimeOutputSignals.NewRow();
                                for (int i = 0; i < this.realTimeOutputSignals.Columns.Count; i++) {
                                    dr[i] = strs[i];
                                }
                                this.realTimeOutputSignals.Rows.Add(dr);
                            }

                        }
                    }

                }
                catch (Exception ex) {

                }
            }

        }

        private PLCAdressType GetAdressDataType(string strType) {

            switch (strType) {
                case "Counter":
                    return PLCAdressType.Counter;
                case "Timer":
                    return PLCAdressType.Timer;
                case "Input":
                    return PLCAdressType.Input;
                case "Output":
                    return PLCAdressType.Output;
                case "Memory":
                    return PLCAdressType.Memory;
                case "DataBlock":
                    return PLCAdressType.DataBlock;

            }
            return PLCAdressType.DataBlock;
        }

        private Type GetAdressType(string strType) {
            switch (strType) {
                case "System.UInt16":
                    return typeof(UInt16);
                case "System.UInt32":
                    return typeof(UInt32);
                case "System.UInt64":
                    return typeof(UInt64);
                case "System.Int16":
                    return typeof(Int16);
                case "System.Int32":
                    return typeof(Int32);
                case "System.Int64":
                    return typeof(Int64);
                case "System.Boolean":
                    return typeof(Boolean);
                case "System.Byte":
                    return typeof(Byte);
                case "System.SByte":
                    return typeof(SByte);
                case "System.Double":
                    return typeof(Double);
                case "System.Single":
                    return typeof(Single);
                case "System.String":
                    return typeof(String);
                default:
                    return null;
            }

        }


    }
}
