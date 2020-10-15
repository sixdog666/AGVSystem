using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlObjects.S7 {
    public enum eS7AdresssTitle {
        No, AdressName, AdressType, DataType, DBAdress, StartAdress, ByteCount, AdressValue, IsInput, IsOutput, IsReal
    }

    public enum eS7RealTimeAdressTitle {
        No, StartAdress, AdressID, Offset, Length, Value, IsInput, IsOutput
    }
    public enum PLCAdressType {
        Counter = 28,
        Timer = 29,
        Input = 129,
        Output = 130,
        Memory = 131,
        DataBlock = 132
    }
    class S7Adress :DeviceAdress{

        public PLCAdressType dataType;
        public int dbAdress;
        public int startAdress;
        public int byteCount;
    }
}
