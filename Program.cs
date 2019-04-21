using System;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;
using RedisHelper;
using JWLibrary;

namespace redis_a_to_z
{
    class Program
    {
        static RedisHandler _redisHandler = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Redis!");

            _redisHandler = new RedisHandler("127.0.0.1:6379");

            Console.WriteLine($"connected?{_redisHandler.IsConnected}");

            Console.WriteLine($"set?{_redisHandler.Set("key1", "value1")}");

            var getValue = _redisHandler.Get<string>("key1");

            Console.WriteLine($"get key1?{getValue}");

            Console.WriteLine($"remove key1?{_redisHandler.Remove("key1")}");

            getValue = _redisHandler.Get<string>("key1");
            Console.WriteLine($"get key1?{getValue}");

            dynamic dobj = new ExpandoObject();
            dobj.key1 = "value1";
            dobj.key2 = "value2";
            dobj.key3 = 1000;
            var jsonObject = JsonConvert.SerializeObject(dobj);

            _redisHandler.Set<string>("json", jsonObject);
            Console.WriteLine(_redisHandler.Get<string>("json"));

            Console.WriteLine($"keys:{_redisHandler.GetKeyAll().ToString<String>()}");

            using (var manager = _redisHandler.GetRedisManagerPool())
            {
                using (var client = manager.GetClient())
                {
                    Console.WriteLine($"ping:{client.Ping()}");
                }
            }
        }
    }
}
