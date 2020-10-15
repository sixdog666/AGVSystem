using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlObjects.Modbus {
    public enum eModbusAdressTitle {
        No, AdressName, Adress, AdressType, AdressValue, AdressLenth, IsInput, IsOutput
    }
    public enum eModbusRealAdressTitle {
        No, StartAdress, AdressName, Offset, Length, Value, IsInput, IsOutput
    }
    public class ModbusAdress :DeviceAdress{
        private string strAdress;
        private int intStation;
        public string SignalAdress {
            get {
                return strAdress;
            }
            set {
                strAdress = value;
            }
        }
    }
}
