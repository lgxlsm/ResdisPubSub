using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace ResdisPubSub.PubSub
{
    /// <summary>
    /// 通过redis实现的订阅-发布机制
    /// </summary>
    public class RedisSubscribe : ISubscribeService
    {
        //链接
        static ConnectionMultiplexer redis;

        static RedisSubscribe()
        {
            ConfigurationOptions config = new ConfigurationOptions()
            {
                AbortOnConnectFail = false,
                ConnectRetry = 10,
                ConnectTimeout = 5000,
                ResolveDns = true,
                SyncTimeout = 5000,
                EndPoints = { { "127.0.0.1:6379" } },
                Password = "111111",
                AllowAdmin = true,
                KeepAlive = 180
            };
            redis = ConnectionMultiplexer.Connect(config);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="channel">频道：消息的名称</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public void Publish<T>(string channel, T msg)
        {
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    redis.GetSubscriber().Publish(channel, JsonConvert.SerializeObject(msg));
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("redis服务错误，详细信息：" + ex.Message + "，来源：" + ex.Source);
            }
   
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="subChannel">频道：消息的名称</param>
        /// <param name="action">收到消息后的处理</param>
        public void Subscribe(string subChannel, Action<string> action)
        {
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    redis.GetSubscriber().Subscribe(subChannel, (channel, message) => { action(message); });
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("redis服务错误，详细信息：" + ex.Message + "，来源：" + ex.Source);
            }

        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel">频道：消息的名称</param>
        public void Unsubscribe(string channel)
        {
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    redis.GetSubscriber().Unsubscribe(channel);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("redis服务错误，详细信息：" + ex.Message + "，来源：" + ex.Source);
            }

        }

        /// <summary>
        /// 取消全部订阅
        /// </summary>
        public void UnsubscribeAll()
        {
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    redis.GetSubscriber().UnsubscribeAll();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("redis服务错误，详细信息：" + ex.Message + "，来源：" + ex.Source);
            }
   
        }
    }
}
