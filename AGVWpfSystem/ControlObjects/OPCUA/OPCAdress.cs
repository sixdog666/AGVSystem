using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ControlObjects.OPCUA {
    public enum eOPCAdressTitle {
        No, AdressName, AdressID, AdressType, AdressValue, IsInput, IsOutput, IsReal
    }
    class OPCAdress :DeviceAdress{
        string ID;
        public string signalID {
            get { return ID; }
            set { ID = value; }
        }
    }
}
