using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlObjects.OPCUA {
    class OPCCommFactory : DeviceInfoFactory {
        public override void CreateCommDevice(DeviceBase device, string strFilePath) {
            ((OPCSystem)device).ReadAressFile(strFilePath);
        }
    }
}
