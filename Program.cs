using System;
using System.Dynamic;
using Newtonsoft.Json;
using RedisHelper;

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

            Console.WriteLine($"set?{_redisHandler.RedisSet("key1", "value1")}");

            var getValue = _redisHandler.RedisGet<string>("key1");

            Console.WriteLine($"get key1?{getValue}");

            Console.WriteLine($"remove key1?{_redisHandler.RedisRemove("key1")}");

            getValue = _redisHandler.RedisGet<string>("key1");
            Console.WriteLine($"get key1?{getValue}");

            dynamic dobj = new ExpandoObject();
            dobj.key1 = "value1";
            dobj.key2 = "value2";
            dobj.key3 = 1000;
            var jsonObject = JsonConvert.SerializeObject(dobj);

            _redisHandler.RedisSet<string>("json", jsonObject);
            Console.WriteLine(_redisHandler.RedisGet<string>("json"));
        }
    }
}
