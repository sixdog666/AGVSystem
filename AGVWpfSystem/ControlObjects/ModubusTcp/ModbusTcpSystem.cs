using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.ModBus;
using HslCommunication;

namespace ControlObjects {

  partial  class ModbusTcpSystem : DeviceBase {
        ModbusTcpNet modusTcp;
        string controlName;
        public EventHandler ConnectFail_Happened;
        private bool connectFlag;
        public ModbusTcpSystem(string name,string ipAddress, int port = 502, byte station = 1):base(name) {
            modusTcp = new ModbusTcpNet(ipAddress, port, station);
        }
        private async void ConnectAsync() {
            modusTcp?.ConnectClose();
            // modusTcp.ConnectServer();
            OperateResult result = await modusTcp.ConnectServerAsync();
            if (!result.IsSuccess) {
                MessageArgs args = new MessageArgs(base.adressName + "连接失败");
                ConnectFail_Happened?.Invoke(this, args);
            }
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

        public override object ReadStateSignal(string signalName) {
            throw new NotImplementedException();
        }

        protected override ushort ReadUInt16(string adress) {
            ushort value = modusTcp.ReadUInt16(adress).Content;
            return value;
        }

        protected override uint ReadUInt32(string adress) {
            uint value = modusTcp.ReadUInt32(adress).Content;
            return value;
        }

        protected override ulong ReadUInt64(string adress) {
            ulong value = modusTcp.ReadUInt64(adress).Content;
            return value;
        }

        protected override short ReadInt16(string adress) {
            short value = modusTcp.ReadInt16(adress).Content;
            return value;
        }

        protected override int ReadInt32(string adress) {
            int value = modusTcp.ReadInt32(adress).Content;
            return value;
        }

        protected override long ReadInt64(string adress) {
            long value = modusTcp.ReadInt64(adress).Content;
            return value;
        }

        protected override bool ReadBoolean(string adress) {
            bool coil = modusTcp.ReadCoil(adress).Content;
            return coil;
        }

        protected override byte ReadByte(string adress) {
            byte value =Convert.ToByte(modusTcp.ReadUInt16(adress).Content>>8);
            return value;
        }

        protected override sbyte ReadSbyte(string adress) {
            sbyte value = Convert.ToSByte(modusTcp.ReadInt16(adress).Content >> 8);
            return value;
        }

        protected override double ReadDouble(string adress) {
            double value = modusTcp.ReadDouble(adress).Content;
            return value;
        }

        protected override float ReadSingle(string adress) {
            float value = modusTcp.ReadFloat(adress).Content;
            return value;
        }

        protected override string ReadString(string adress,ushort length) {
            string value = modusTcp.ReadString(adress, length).Content;
            return value;
        }

        internal override void Write(string adress, double value) {
            modusTcp.Write(adress,value);
        }

        protected override void Write(string adress, ulong value) {
            modusTcp.Write(adress, value);
        }

        protected override void Write(string adress, bool value) {
            modusTcp.Write(adress, value);
        }

        protected override void Write(string adress, short value) {
            modusTcp.Write(adress, value);
        }

        protected override void Write(string adress, ushort value) {
            modusTcp.Write(adress, value);
        }

        protected override void Write(string adress, long value) {
            modusTcp.Write(adress, value);
        }

        protected override void Write(string adress, byte value) {
            modusTcp.Write(adress, value);
        }

        protected override void Write(string adress, string value) {
            modusTcp.Write(adress, value);
        }
    }
}
