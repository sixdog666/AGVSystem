
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlObjects.Modbus.ModbusTcp {

    public class ModbusTCPFactory : DeviceInfoFactory {
        public override void CreateCommDevice(DeviceBase modbus,string strFilePath) {//string name, string ipAddress, int port = 502, byte station = 1) {
            ((ModbusTcpSystem)modbus).ReadAressFile(strFilePath);
            ((ModbusTcpSystem)modbus).ReadRealAdressFile(strFilePath);
        }

    }
}
