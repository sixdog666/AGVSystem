using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlObjects {
    public struct AdressValueInfo {
        public Type valueType;
        public DeviceAdress adress;
        public DeviceValue deviceValue;
        public ushort length;
    }
    public abstract class DeviceBase {
        protected byte[] SynInputs;
        protected byte[] SynOutputs;
        protected DataTable realTimeInputSignals;
        protected DataTable realTimeOutputSignals;
        protected DeviceAdress startInputsAdress;
        protected DeviceAdress startOutputsAdress;
        private Hashtable InputSignals;
        private Hashtable OutputSignals;
        protected bool inputThreadRun = false;
        protected bool outputThreadRun = false;
        protected string deviceName;

        public DeviceBase(string strName) {
            deviceName = strName;
            SynInputs = new byte[128];
            SynOutputs = new byte[128];
            InputSignals = new Hashtable();
            OutputSignals = new Hashtable();

        }
        public DeviceAdress StartInputsAdress {
            set { startInputsAdress = value; }
            get { return startInputsAdress; }
        }
        public DeviceAdress StartOutputsAdress {
            set { startOutputsAdress = value; }
            get { return startOutputsAdress; }
        }

        public Hashtable Inputs {
            get {
                return InputSignals;
            }
        }
        public Hashtable Outputs {
            get {
                return OutputSignals;
            }
        }

        public DataTable RealTimeInputSignals {
            get {
                return realTimeInputSignals;
            }

        }
        public DataTable RealTimeOutputSignals {
            get {
                return realTimeOutputSignals;
            }

        }

        public void AddInputSignal(string signalName, AdressValueInfo signalInfo) {
            InputSignals.Add(signalName, signalInfo);
        }
        public void AddOutputSignal(string signalName, AdressValueInfo signalInfo) {
            OutputSignals.Add(signalName, signalInfo);
        }
        public void RealTimeSignalThreadStart(string startInputAdr, string endInputAdr) {
            Thread threadRead = new Thread(ReadRealTimeSignal);
            if (threadRead.ThreadState != ThreadState.Running) {
                threadRead.Start();
            }
            Thread threadWrite = new Thread(WriteRealTimeSignal);
            if (threadWrite.ThreadState != ThreadState.Running) {
                threadWrite.Start();
            }

        }
        public void ReadTimeSignalThreadStop() {
            inputThreadRun = false;
            outputThreadRun = false;
        }
        /// <summary>
        /// 连接控制器
        /// </summary>
        public abstract bool Connect();

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
        public abstract object ReadInputSignal(string signalName);

        /// 写输出信号
        /// </summary>
        /// 
        public abstract void WriteOutputSignal(string signalName, object value);

        protected abstract void ReadRealTimeSignal();

        protected abstract void WriteRealTimeSignal();

    }
}
