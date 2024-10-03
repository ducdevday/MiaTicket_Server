using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiaTicket.DataCache
{
    public interface IRedisCacheService
    {
        T? GetData<T>(string key);
        void SetData<T>(string key, T data, TimeSpan expiredIn);
        void RemoveData(string key);
        void RemoveDataBaseOnPattern(string pattern);
    }

    public class RedisCacherService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacherService(IDistributedCache cache, IConnectionMultiplexer connectionMultiplexer)
        {
            _cache = cache;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public T? GetData<T>(string key)
        {
            var data = _cache.GetString(key);
            if (data is null) {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(data);
        }

        public void SetData<T>(string key, T data, TimeSpan expiredIn)
        {
            var options = new DistributedCacheEntryOptions() {
                AbsoluteExpirationRelativeToNow = expiredIn
            };
            _cache.SetString(key, JsonSerializer.Serialize(data), options);
        }

        public void RemoveData(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveDataBaseOnPattern(string pattern)
        {
            var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());

            var keys = server.Keys(pattern: pattern);
            foreach (var key in keys)
            {
                RemoveData(key);
            }
        }
    }
}
