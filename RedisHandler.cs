using System.Collections;
using System.Collections.Generic;
using ServiceStack.Redis;

namespace RedisHelper
{
    public class RedisHandler {
        readonly string _conn = string.Empty;

        public bool IsConnected {
            get {
                return IsConnect();
            }
        }

        public RedisHandler(string conn) {
            _conn = conn;
        }

        public bool RedisSet<T>(string key, T value) {
           using(var manager = new RedisManagerPool(_conn)) {
                using(var client = manager.GetClient()) {
                    return client.Set(key, value);
                }
            }
        }

        public void RedisSets<T>(IDictionary<string, T> keyvalues) {
           using(var manager = new RedisManagerPool(_conn)) {
                using(var client = manager.GetClient()) {
                    client.SetAll(keyvalues);
                }
            }
        }

        public T RedisGet<T>(string key) {
           using(var manager = new RedisManagerPool(_conn)) {
                using(var client = manager.GetClient()) {
                    return client.Get<T>(key);
                }
            }  
        }

        public IDictionary<string, T> RedisGets<T>(IEnumerable<string> keys) {
           using(var manager = new RedisManagerPool(_conn)) {
                using(var client = manager.GetClient()) {
                    return client.GetAll<T>(keys);
                }
            }  
        }

        public bool RedisRemove(string key) {
           using(var manager = new RedisManagerPool(_conn)) {
                using(var client = manager.GetClient()) {
                    return client.Remove(key);
                }
            }     
        }   


        private bool IsConnect(){ 
            using(var manager = new RedisManagerPool(_conn)) {
                using(var client = manager.GetClient()) {
                    var response = client.Echo("ping");

                    if("ping".Equals(response.ToLower())) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}