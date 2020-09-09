using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlObjects
{
    public interface IControlSystem
    {/// <summary>
    /// 连接控制器
    /// </summary>
        bool Connect();

       // void ConnectAsync();
        /// <summary>
        /// 断开控制器连接
        /// </summary>
        void Disconnect();
        /// <summary>
        /// 查看连接状态
        /// </summary>
        /// <returns></returns>
        bool IsConnect();
        /// <summary>
        /// 读输入信号
        /// </summary>
        /// <returns></returns>
        object ReadInputSignal(string signalName);

        /// <summary>
        /// 读状态信号
        /// </summary>
        /// <returns></returns>
        object ReadStateSignal(string signalName);
        /// <summary>
        /// 写输出信号
        /// </summary>
        void WriteOutputSignal(string signalName,object value);

    }

    public class MessageArgs : EventArgs {
        private string messageInfo;
        public string message {
            get { return messageInfo; }
        }
        public MessageArgs(string message) {
            messageInfo = message;
        }
    }
}
