using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KELEI.PM.DBService.RedisAccess
{
    /// <summary>
    /// Redis操作方法基础类
    /// </summary>
    public abstract class RedisHelper
    {

        #region 属性字段
        /// <summary>
        /// 网站Redis 链接字符串
        /// </summary>
        protected readonly ConnectionMultiplexer _conn;
        /// <summary>
        /// Redis操作对象
        /// </summary>
        protected readonly IDatabase redis = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化Redis操作方法基础类
        /// </summary>
        /// <param name="dbNum">操作的数据库索引0-64(需要在conf文件中配置)</param>
        protected RedisHelper(int? dbNum = null)
        {
            _conn = RedisManager.Instance;
            if (_conn != null)
            {
                redis = _conn.GetDatabase(dbNum ?? RedisManager.RedisDataBaseIndex);
            }
            else
            {
                //throw new ArgumentNullException("Redis连接初始化失败");
            }
        }
        #endregion 构造函数

        #region 外部调用静态方法
        /// <summary>
        /// 获取Redis的String数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisStringService StringService()
        {
            return new RedisStringService();
        }
        /// <summary>
        /// 获取Redis的Hash数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisHashService HashService()
        {
            return new RedisHashService();
        }

        /// <summary>
        /// 获取Redis的List数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisListService ListService()
        {
            return new RedisListService();
        }
        /// <summary>
        /// 获取Redis的Set无序集合数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisSetService SetService()
        {
            return new RedisSetService();
        }
        /// <summary>
        /// 获取Redis的SortedSet(ZSet)有序集合数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisSortedSetService SortedSetService()
        {
            return new RedisSortedSetService();
        }


        #endregion


        #region 不建议公开这些方法，如果项目中用不到，建议注释或者删除
        /// <summary>
        /// 获取Redis事务对象
        /// </summary>
        /// <returns></returns>
        public ITransaction CreateTransaction() { return redis.CreateTransaction(); }

        /// <summary>
        /// 获取Redis服务和常用操作对象
        /// </summary>
        /// <returns></returns>
        public IDatabase GetDatabase() { return redis; }

        /// <summary>
        /// 获取Redis服务
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <returns></returns>
        public IServer GetServer(string hostAndPort) { return _conn.GetServer(hostAndPort); }

        /// <summary>
        /// 执行Redis事务
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public bool RedisTransaction(Action<ITransaction> act)
        {
            var tran = redis.CreateTransaction();
            act.Invoke(tran);
            bool committed = tran.Execute();
            return committed;
        }
        /// <summary>
        /// Redis锁
        /// </summary>
        /// <param name="act"></param>
        /// <param name="ts">锁住时间</param>
        public void RedisLockTake(Action act, TimeSpan ts)
        {
            RedisValue token = Environment.MachineName;
            string lockKey = "lock_LockTake";
            if (redis.LockTake(lockKey, token, ts))
            {
                try
                {
                    act();
                }
                finally
                {
                    redis.LockRelease(lockKey, token);
                }
            }
        }
        #endregion 

        #region 辅助方法

        /// <summary>
        /// 将对象转换成string字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() :
                JsonConvert.SerializeObject(value, Formatting.None);
            return result;
        }
        /// <summary>
        /// 将值反系列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected T ConvertObj<T>(RedisValue value)
        {
            return value.IsNullOrEmpty ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 将值反系列化成对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        protected List<T> ConvetList<T>(RedisValue[] values)
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = ConvertObj<T>(item);
                result.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 将string类型的Key转换成 <see cref="RedisKey"/> 型的Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        protected RedisKey[] ConvertRedisKeys(List<string> redisKeys) { return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray(); }

        /// <summary>
        /// 将string类型的Key转换成 <see cref="RedisKey"/> 型的Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        protected RedisKey[] ConvertRedisKeys(params string[] redisKeys) { return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray(); }

        /// <summary>
        /// 将string类型的Key转换成 <see cref="RedisKey"/> 型的Key，并添加前缀字符串
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        protected RedisKey[] ConvertRedisKeysAddSysCustomKey(params string[] redisKeys) { return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray(); }
        /// <summary>
        /// 将值集合转换成RedisValue集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisValues"></param>
        /// <returns></returns>
        protected RedisValue[] ConvertRedisValue<T>(params T[] redisValues) { return redisValues.Select(o => (RedisValue)ConvertJson<T>(o)).ToArray(); }
        #endregion 辅助方法

    }
}