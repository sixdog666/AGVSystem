using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlObjects {
   public abstract class DeviceInfoFactory {
        public abstract void CreateCommDevice(DeviceBase device, string strFilePath);
    }
}
