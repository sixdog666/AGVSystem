using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlObjects.Modbus.ModbusRtu {
    class ModbusRtuFactory : DeviceInfoFactory {
        public override void CreateCommDevice(DeviceBase modbus, string strFilePath) {//string name, string ipAddress, int port = 502, byte station = 1) {
            ((ModbusRtuSystem)modbus).ReadAressFile(strFilePath);
            ((ModbusRtuSystem)modbus).ReadRealAdressFile(strFilePath);
        }
    }
}
