using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResdisPubSub.PubSub
{
    /// <summary>
    /// 订阅-发布消息接口
    /// </summary>
    public interface ISubscribeService
    {
        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="subChannel">频道：消息的名称</param>
        /// <param name="action"></param>
        void Subscribe(string subChannel, Action<string> action);

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="channel">频道：消息的名称</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        void Publish<T>(string channel, T msg);

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel">频道：消息的名称</param>
        void Unsubscribe(string channel);

        /// <summary>
        /// 取消全部订阅
        /// </summary>
        void UnsubscribeAll();
    }
}
