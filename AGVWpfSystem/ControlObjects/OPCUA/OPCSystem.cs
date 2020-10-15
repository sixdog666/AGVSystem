using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;
using System.Threading;
using System.IO;

namespace ControlObjects.OPCUA {
    class OPCSystem : DeviceBase {
        OPCServer opcServer;
        List<string> strServerList = new List<string>();
        string strHostIp;
        string strServerName;
        OPCGroups opcGroups;
        OPCGroup opcGroup;
        bool connectFlag=false;
        public OPCSystem(string strOPCName, string hostIp, string serverName) : base(strOPCName) {
            opcServer = new OPCServer();
            object serverList = opcServer.GetOPCServers(hostIp);
            foreach (string turn in (Array)serverList) {
                strServerList.Add(turn);
            }
            strHostIp = hostIp;
            strServerName = serverName;
        }

        public override bool Connect() {
            try {
                opcServer.Connect(strServerName, strHostIp);
                if (opcServer.ServerState == (int)OPCServerState.OPCRunning) {
                    connectFlag = true;
                }
                else {
                    connectFlag = false;
                    return false;
                }
            }
            catch (Exception) {
                connectFlag = false;
                return false;
            }
            return true;
        }

        public override void Disconnect() {
            opcServer.Disconnect();
        }

        public override bool IsConnect() {
            return connectFlag;
        }

        public override object ReadInputSignal(string signalName) {

            OPCItem opcItem = opcGroup.OPCItems.Item(signalName);
            return opcItem.Value;
        }

        public override void WriteOutputSignal(string signalName, object value) {
            int[] temp = new int[2] { 0, opcGroup.OPCItems.Item(signalName).ServerHandle };
            Array serverHandles = (Array)temp;
            Array Errors;
            object[] valueTemp = new object[2] { "", value };
            Array values = (Array)valueTemp;
            opcGroup.SyncWrite(1, ref serverHandles, ref values, out Errors);//同步写
            if (Errors.GetValue(1).ToString() == "0") {
                throw new Exception("写入数据错误");
            }
            else {

            }

        }

        private void SetGroupProperty() {
            opcServer.OPCGroups.DefaultGroupIsActive = true;
            opcServer.OPCGroups.DefaultGroupDeadband = 0;
            opcGroup.UpdateRate = 50;
            opcGroup.IsActive = true;
            opcGroup.IsSubscribed = true;

        }

        private void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps) {
            //int a = 1;
            string[] ChangeName = new string[NumItems];

            

        }
        private bool CreateGroup() {
            try {
                opcGroups =opcServer.OPCGroups;
                opcGroup = opcGroups.Add(base.deviceName);
                SetGroupProperty();
                OPCItems  kepItems = opcGroup.OPCItems;
                opcGroup.DataChange += KepGroup_DataChange;
                OPCBrowser oPCBrowser = opcServer.CreateBrowser();
                //展开分支
                oPCBrowser.ShowBranches();
                //展开叶子
                oPCBrowser.ShowLeafs(true);
                foreach (object turn in oPCBrowser) {
                    ///客户端句柄
                    int itmHandleClient=1;
                    string[] strTem;
                    string str;
                    string Tem;
                    Tem = turn.ToString().Split('.')[turn.ToString().Split('.').Length - 1];
                    strTem = Tem.ToString().Split('_');
                    if (strTem.Length >= 2) {
                        str = strTem[strTem.Length - 2];
                        if (str == deviceName) {
                            OPCItem kepItem = kepItems.AddItem(turn.ToString(), itmHandleClient);
                            itmHandleClient += 1;
                            // OPCItem Item = (OPCItem)turn;
                        }
                    }
                }
                // myEnumBuilder.CreateType();
                opcGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(KepGroup_AsyncWriteComplete);

            }
            catch (Exception err) {
                return false;
            }
            return true;
        }
        void KepGroup_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors) {

            for (int i = 1; i <= NumItems; i++) {
                if (Errors.GetValue(i).ToString() != "0") { }
                  
            }
        }

        protected override void ReadRealTimeSignal() {
            try {
                if (inputThreadRun) return;
                inputThreadRun = true;
                while (connectFlag && inputThreadRun) {
                    OPCAdress adress = (OPCAdress)startInputsAdress;
                    OPCItem opcItem = opcGroup.OPCItems.Item(adress.signalID);
                    SynInputs = opcItem.Value;
                    Thread.Sleep(100);
                }
            }
            catch (Exception) {
                inputThreadRun = false;
            }
        }

        protected override void WriteRealTimeSignal() {
            try {
                if (outputThreadRun) return;
                outputThreadRun = true;
                OPCAdress adress = (OPCAdress)startOutputsAdress;
                while (connectFlag && outputThreadRun) {
                    int[] temp = new int[2] { 0, opcGroup.OPCItems.Item(adress.signalID).ServerHandle };
                    Array serverHandles = (Array)temp;
                    Array Errors;
                    object[] valueTemp = new object[2] { "", SynOutputs };
                    Array values = (Array)valueTemp;
                    opcGroup.SyncWrite(1, ref serverHandles, ref values, out Errors);//同步写
                    Thread.Sleep(100);
                }
            }
            catch (Exception) {
                outputThreadRun = false;
            }
        }

        public void ReadAressFile(string filePath) {
            string path = System.Environment.CurrentDirectory;
            path = filePath + "\\OPCAdress";
            if (!File.Exists(path)) {
                throw new Exception("配置文件不存在！");
            }
            using (StreamReader sr = new StreamReader(path)) {
                var line = sr.ReadLine();
                while (line != null) {
                    line = sr.ReadLine();
                    string[] strs = line.Split(',');
                    if (strs[0] != "No" && strs[0] != string.Empty) {
                        if (strs[(int)eOPCAdressTitle.IsInput].Trim(' ') == "1") {
                            OPCAdress adress = new OPCAdress();
                            adress.signalID = strs[(int)eOPCAdressTitle.AdressID];
                            AdressValueInfo valueInfo = new AdressValueInfo() {
                                adress = adress,
                                deviceValue = new OPCValue(),
                                valueType = GetAdressType(strs[(int)eOPCAdressTitle.AdressType]),
                            };
                            this.AddInputSignal(strs[(int)eOPCAdressTitle.AdressName], valueInfo);
                        }
                        if (strs[(int)eOPCAdressTitle.IsOutput].Trim(' ') == "1") {
                            OPCAdress adress = new OPCAdress();
                            adress.signalID = strs[(int)eOPCAdressTitle.AdressID];
                            AdressValueInfo valueInfo = new AdressValueInfo() {
                                adress = adress,
                                deviceValue = new OPCValue(),
                                valueType = GetAdressType(strs[(int)eOPCAdressTitle.AdressType]),
                            };
                            this.AddOutputSignal(strs[(int)eOPCAdressTitle.AdressName], valueInfo);
                        }
                    }
                }
            }
        }


        private Type GetAdressType(string strType) {
            switch (strType) {
                case "System.UInt16":
                    return typeof(UInt16);
                case "System.UInt32":
                    return typeof(UInt32);
                case "System.UInt64":
                    return typeof(UInt64);
                case "System.Int16":
                    return typeof(Int16);
                case "System.Int32":
                    return typeof(Int32);
                case "System.Int64":
                    return typeof(Int64);
                case "System.Boolean":
                    return typeof(Boolean);
                case "System.Byte":
                    return typeof(Byte);
                case "System.SByte":
                    return typeof(SByte);
                case "System.Double":
                    return typeof(Double);
                case "System.Single":
                    return typeof(Single);
                case "System.String":
                    return typeof(String);
                default:
                    return null;
            }

        }

    }
}
