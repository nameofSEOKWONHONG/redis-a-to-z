using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;

namespace RedisHelper
{
    public class RedisHandler
    {
        readonly string _conn = string.Empty;

        public bool IsConnected
        {
            get
            {
                return IsConnect();
            }
        }

        public RedisHandler(string conn)
        {
            if (string.IsNullOrEmpty(conn)) throw new KeyNotFoundException("connection string is empty.");

            _conn = conn;
        }

        public RedisManagerPool GetRedisManagerPool()
        {
            if (IsConnected)
            {
                return new RedisManagerPool(_conn);
            }

            return null;
        }

        public bool Set<T>(string key, T value)
        {
            using (var manager = new RedisManagerPool(_conn))
            {
                using (var client = manager.GetClient())
                {
                    return client.Set(key, value);
                }
            }
        }

        public void Sets<T>(IDictionary<string, T> keyvalues)
        {
            using (var manager = new RedisManagerPool(_conn))
            {
                using (var client = manager.GetClient())
                {
                    client.SetAll(keyvalues);
                }
            }
        }

        public T Get<T>(string key)
        {
            using (var manager = new RedisManagerPool(_conn))
            {
                using (var client = manager.GetClient())
                {
                    return client.Get<T>(key);
                }
            }
        }

        public IDictionary<string, T> Gets<T>(IEnumerable<string> keys)
        {
            using (var manager = new RedisManagerPool(_conn))
            {
                using (var client = manager.GetClient())
                {
                    return client.GetAll<T>(keys);
                }
            }
        }

        public List<string> GetKeyAll()
        {
            using (var manager = new RedisManagerPool(_conn))
            {
                using (var client = manager.GetClient())
                {
                    return client.GetAllKeys();
                }
            }
        }

        public bool Remove(string key)
        {
            using (var manager = new RedisManagerPool(_conn))
            {
                using (var client = manager.GetClient())
                {
                    return client.Remove(key);
                }
            }
        }


        private bool IsConnect()
        {
            using (var manager = new RedisManagerPool(_conn))
            {
                using (var client = manager.GetClient())
                {
                    var response = client.Echo("ping");

                    if ("ping".Equals(response.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}