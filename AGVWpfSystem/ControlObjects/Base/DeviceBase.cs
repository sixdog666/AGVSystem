using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlObjects {
    public struct ValueInfo {
      public Type valueType;
        public string adress;
        public ushort length;
    }
  public abstract class DeviceBase {

        private Hashtable InputSignal;
        private Hashtable OutputSignal;
        private Hashtable StatusSignal;
        protected string adressName;

        public DeviceBase(string strName) {
            adressName = strName;
            InputSignal = new Hashtable();
            OutputSignal = new Hashtable();
            StatusSignal = new Hashtable();
        }
        public Hashtable Inputs {
            get {
                return InputSignal;
            }
        }
        public Hashtable Outputs {
            get {
                return OutputSignal;
            }
        }
        public Hashtable Status {
            get {
                return StatusSignal;
            }
        }

        public void AddInputSignal(string signalName, ValueInfo signalInfo) {
            InputSignal.Add(signalName, signalInfo);
        }
        public void AddOutputSignal(string signalName, ValueInfo signalInfo) {
            OutputSignal.Add(signalName, signalInfo);
        }
        public void AddStatusSignal(string signalName, ValueInfo signalInfo) {
            StatusSignal.Add(signalName, signalInfo);
        }
        /// <summary>
        /// 连接控制器
        /// </summary>
     public abstract  bool Connect();

        // void ConnectAsync();
        /// <summary>
        /// 断开控制器连接
        /// </summary>
        public abstract void Disconnect();
        /// <summary>
        /// 查看连接状态
        /// </summary>
        /// <returns></returns>
        public abstract bool IsConnect();
        /// <summary>
        /// 读输入信号
        /// </summary>
        /// <returns></returns>
        public  object ReadInputSignal(string signalName) {
            if (Inputs.ContainsKey(signalName)) {
                ValueInfo value = (ValueInfo)Inputs[signalName];
                switch (value.valueType.ToString()) {
                    case "System.UInt16":
                        return ReadUInt16(value.adress);
                    case "System.Uint32":
                        return ReadUInt32(value.adress);
                    case "System.Uint64":
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
                    case "System.Sbyte":
                        return ReadSbyte(value.adress);
                    case "System.Double":
                        return ReadDouble(value.adress);
                    case "System.Single":
                        return ReadSingle(value.adress);
                    case "System.String":
                        return ReadString(value.adress,value.length);
                }
            }
            return null;
        }

        /// <summary>
        /// 读状态信号
        /// </summary>
        /// <returns></returns>
        public abstract object ReadStateSignal(string signalName);
        /// <summary>
        /// 写输出信号
        /// </summary>
        public void WriteOutputSignal(string signalName, object value) {
            if (Outputs.ContainsKey(signalName)) {
                ValueInfo output = (ValueInfo)Outputs[signalName];
                switch (output.valueType.ToString()) {
                    case "System.UInt16":
                        Write(output.adress, (ushort)value);
                        break;
                    case "System.Uint32":
                        Write(output.adress, (uint)value);
                        break;
                    case "System.Uint64":
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
                    case "System.Sbyte":
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
internal abstract void Write(string adress, double value);
        protected abstract void Write(string adress, ulong value);
        protected abstract System.UInt16 ReadUInt16(string adress);
        protected abstract System.UInt32 ReadUInt32(string adress);
        protected abstract System.UInt64 ReadUInt64(string adress);
        protected abstract System.Int16 ReadInt16(string adress);
        protected abstract System.Int32 ReadInt32(string adress);
        protected abstract System.Int64 ReadInt64(string adress);
        protected abstract System.Boolean ReadBoolean(string adress);

        protected abstract System.Byte ReadByte(string adress);
        protected abstract System.SByte ReadSbyte(string adress);

        protected abstract System.Double ReadDouble(string adress);
        protected abstract System.Single ReadSingle(string adress);
        protected abstract System.String ReadString(string adress, ushort length);
        protected abstract void Write(string adress, bool Value);
        protected abstract void Write(string adress, short Value);
        protected abstract void Write(string adress, ushort Value);
        protected abstract void Write(string adress, long Value);
        protected abstract void Write(string adress, byte Value);
        protected abstract void Write(string adress, string Value);
    }
}
