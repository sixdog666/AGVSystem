using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.ModBus;
using HslCommunication;
using ControlObjects.Modbus;
using System.Threading;
using System.IO;

namespace ControlObjects.Modbus.ModbusTcp {

    public class ModbusTcpSystem : DeviceBase {
        ModbusTcpNet modusTcp;
        public EventHandler ConnectFail_Happened;
        private bool connectFlag;
        public ModbusTcpSystem(string name, string ipAddress, int port = 502, byte station = 1) : base(name) {
            modusTcp = new ModbusTcpNet(ipAddress, port, station);
            startInputsAdress = new ModbusAdress() { SignalAdress = "100" };
            startInputsAdress = new ModbusAdress() { SignalAdress = "100" };
        }

        public override bool Connect() {
            modusTcp?.ConnectClose();
            OperateResult result = modusTcp.ConnectServer();
            connectFlag = result.IsSuccess;
            return result.IsSuccess;
        }

        public override void Disconnect() {
            modusTcp?.ConnectClose();
            connectFlag = false;
        }

        public override bool IsConnect() {
            return connectFlag;
        }
        public override object ReadInputSignal(string signalName) {
            if (Inputs.ContainsKey(signalName)) {
                AdressValueInfo value = (AdressValueInfo)Inputs[signalName];
                value.deviceValue = new ModbusValue();
                switch (value.valueType.Name) {
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
                switch (output.valueType.ToString()) {
                    case "System.UInt16":
                        Write(output.adress, (ushort)value);
                        break;
                    case "System.UInt32":
                        Write(output.adress, (uint)value);
                        break;
                    case "System.UInt64":
                        Write(output.adress, (ulong)value);
                        break;
                    case "System.Int16":
                        Write(output.adress, (short)value);
                        break;
                    case "System.Int32":
                        Write(output.adress, (int)value);
                        break;
                    case "System.Int64":
                        Write(output.adress, (long)value);
                        break;
                    case "System.Boolean":
                        Write(output.adress, (bool)value);
                        break;
                    case "System.Byte":
                        Write(output.adress, (byte)value);
                        break;
                    case "System.SByte":
                        Write(output.adress, (sbyte)value);
                        break;
                    case "System.Double":
                        Write(output.adress, (double)value);
                        break;
                    case "System.Single":
                        Write(output.adress, (float)value);
                        break;
                    case "System.String":
                        Write(output.adress, (string)value);
                        break;
                }
            }
        }

        protected ushort ReadUInt16(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            ushort value = modusTcp.ReadUInt16(modbusAdrss.SignalAdress).Content;
            return value;
        }

        protected uint ReadUInt32(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            uint value = modusTcp.ReadUInt32(modbusAdrss.SignalAdress).Content;
            return value;
        }

        protected ulong ReadUInt64(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            ulong value = modusTcp.ReadUInt64(modbusAdrss.SignalAdress).Content;
            return value;
        }

        protected short ReadInt16(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            short value = modusTcp.ReadInt16(modbusAdrss.SignalAdress).Content;
            return value;
        }

        protected int ReadInt32(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            int value = modusTcp.ReadInt32(modbusAdrss.SignalAdress).Content;
            return value;
        }

        protected long ReadInt64(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            long value = modusTcp.ReadInt64(modbusAdrss.SignalAdress).Content;
            return value;
        }

        protected bool ReadBoolean(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            bool coil = modusTcp.ReadCoil(modbusAdrss.SignalAdress).Content;
            return coil;
        }

        protected byte ReadByte(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            byte value = Convert.ToByte(modusTcp.ReadUInt16(modbusAdrss.SignalAdress).Content >> 8);
            return value;
        }

        protected sbyte ReadSbyte(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            sbyte value = Convert.ToSByte(modusTcp.ReadInt16(modbusAdrss.SignalAdress).Content >> 8);
            return value;
        }

        protected double ReadDouble(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            double value = modusTcp.ReadDouble(modbusAdrss.SignalAdress).Content;
            return value;
        }

        protected float ReadSingle(DeviceAdress adress) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            float value = modusTcp.ReadFloat(modbusAdrss.SignalAdress).Content;
            return value;
        }

        protected string ReadString(DeviceAdress adress, ushort length) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            string value = modusTcp.ReadString(modbusAdrss.SignalAdress, length).Content;
            return value;
        }

        internal void Write(DeviceAdress adress, double value) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            modusTcp.Write(modbusAdrss.SignalAdress, value);
        }

        protected void Write(DeviceAdress adress, ulong value) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            modusTcp.Write(modbusAdrss.SignalAdress, value);
        }

        protected void Write(DeviceAdress adress, bool value) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            modusTcp.Write(modbusAdrss.SignalAdress, value);
        }

        protected void Write(DeviceAdress adress, short value) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            modusTcp.Write(modbusAdrss.SignalAdress, value);
        }

        protected void Write(DeviceAdress adress, ushort value) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            modusTcp.Write(modbusAdrss.SignalAdress, value);
        }

        protected void Write(DeviceAdress adress, long value) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            modusTcp.Write(modbusAdrss.SignalAdress, value);
        }

        protected void Write(DeviceAdress adress, byte value) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            modusTcp.Write(modbusAdrss.SignalAdress, value);
        }

        protected void Write(DeviceAdress adress, string value) {
            ModbusAdress modbusAdrss = (ModbusAdress)adress;
            modusTcp.Write(modbusAdrss.SignalAdress, value);
        }

        protected override void ReadRealTimeSignal() {
            if (inputThreadRun) return;
            inputThreadRun = true;
            while (connectFlag && inputThreadRun) {

                OperateResult<byte[]> results = modusTcp.Read(((ModbusAdress)startInputsAdress).SignalAdress, (ushort)SynInputs.Length);
                if (results.IsSuccess) {
                    results.Content.CopyTo(SynInputs, 0);
                    Thread.Sleep(100);
                }
            }


        }

        protected override void WriteRealTimeSignal() {
            if (outputThreadRun) return;
            outputThreadRun = true;
            while (connectFlag && outputThreadRun) {
                modusTcp.Write(((ModbusAdress)startOutputsAdress).SignalAdress, GetRealTimeOutputDB());
                Thread.Sleep(100);
            }
        }

        private void UpdateRealTimeDB() {
            for (int i = 0; i < realTimeInputSignals.Rows.Count; i++) {
                int adress = Convert.ToInt32(realTimeInputSignals.Rows[i][(int)eModbusRealAdressTitle.StartAdress]);
                int offset = Convert.ToInt32(realTimeInputSignals.Rows[i][(int)eModbusRealAdressTitle.Offset]);
                int length = Convert.ToInt32(realTimeInputSignals.Rows[i][(int)eModbusRealAdressTitle.Length]);
                int startAdress = Convert.ToInt32(((ModbusAdress)startInputsAdress).SignalAdress);
                realTimeInputSignals.Rows[i][(int)eModbusRealAdressTitle.Value] = ConvertByteToValue(SynInputs[adress - startAdress], offset, length);
            }
        }

        private byte[] GetRealTimeOutputDB() {
            byte[] bytes = new byte[128];
            for (int i = 0; i < realTimeOutputSignals.Rows.Count; i++) {
                int adress = Convert.ToInt32(realTimeOutputSignals.Rows[i][(int)eModbusRealAdressTitle.StartAdress]);
                int offset = Convert.ToInt32(realTimeOutputSignals.Rows[i][(int)eModbusRealAdressTitle.Offset]);
                int length = Convert.ToInt32(realTimeOutputSignals.Rows[i][(int)eModbusRealAdressTitle.Length]);
                int startAdress = Convert.ToInt32(((ModbusAdress)startInputsAdress).SignalAdress);
                byte value = Convert.ToByte(realTimeOutputSignals.Rows[i][(int)eModbusRealAdressTitle.Value]);
                bytes[adress - startAdress] = Convert.ToByte(SynOutputs[adress - startAdress] + ConvertValueToByte(value, offset, length));
            }
            return bytes;
        }
        private byte ConvertValueToByte(byte value, int offset, int length) {
            byte byteValue = Convert.ToByte(value << offset);
            byte temByte = 0;
            for (int i = 0; i < length; i++) {
                temByte = Convert.ToByte(temByte + Math.Pow(2, offset + i));

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
            path = filePath + "\\ModbusAdress";
            if (!File.Exists(path)) {
                throw new Exception("配置文件不存在！");
            }
            using (StreamReader sr = new StreamReader(path)) {
                var line = sr.ReadLine();
                while (line != null) {
                    line = sr.ReadLine();
                    string[] strs = line.Split(',');
                    if (strs[0] != "No" && strs[0] != string.Empty) {
                        if (strs[(int)eModbusAdressTitle.IsInput].Trim(' ') == "1") {
                            ModbusAdress adress = new ModbusAdress();
                            adress.SignalAdress = strs[(int)eModbusAdressTitle.Adress];
                            AdressValueInfo valueInfo = new AdressValueInfo() {
                                adress = adress,
                                deviceValue = new ModbusValue(),
                                length = Convert.ToUInt16(strs[(int)eModbusAdressTitle.AdressLenth]),
                                valueType = GetAdressType(strs[(int)eModbusAdressTitle.AdressType]),
                            };
                            this.AddInputSignal(strs[(int)eModbusAdressTitle.AdressName], valueInfo);
                        }
                        if (strs[(int)eModbusAdressTitle.IsOutput].Trim(' ') == "1") {
                            ModbusAdress adress = new ModbusAdress();
                            adress.SignalAdress = strs[(int)eModbusAdressTitle.Adress];
                            AdressValueInfo valueInfo = new AdressValueInfo() {
                                adress = adress,
                                deviceValue = new ModbusValue(),
                                length = Convert.ToUInt16(strs[(int)eModbusAdressTitle.AdressLenth]),
                                valueType = GetAdressType(strs[(int)eModbusAdressTitle.AdressType]),
                            };
                            this.AddOutputSignal(strs[(int)eModbusAdressTitle.AdressName], valueInfo);
                        }
                    }
                }
            }
        }

        public void ReadRealAdressFile(string filePath) {
            string path = System.Environment.CurrentDirectory;
            path = filePath + "\\ModbusRealAdress";
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
                            if (strs[(int)eModbusRealAdressTitle.IsInput] == "1") {
                                System.Data.DataRow dr = this.RealTimeInputSignals.NewRow();
                                for (int i = 0; i < this.realTimeInputSignals.Columns.Count; i++) {
                                    dr[i] = strs[i];
                                }
                                this.realTimeInputSignals.Rows.Add(dr);
                            }
                            else if (strs[(int)eModbusRealAdressTitle.IsOutput] == "1") {
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
