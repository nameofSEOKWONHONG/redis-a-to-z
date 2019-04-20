* redis-server tutorial & example
    - test env : windows 10 wsdl(ubuntu 18.74 - bash)
    1. server install & run
        1) sudo apt-get update(or upgrade)
        2) sudo apt-get install redis-server    
        3) sudo systemctl enable redis-server.service (native ubuntu) or sudo service redis-server start (wsdl)
        4) sudo service redis-server stop (wsdl)
    2. client install & run
        1) sudo apt-get install redis-tools
        2) redis-cli or redis-cli -h [ip] ping (connection test)
        3) redis-cli -h [ip]
    3. data structure
        1) strings
        (1) save image file 
            - inserted binary
            - cmd : cat test.png | redis-cli -h [ip] -x set myimage
            - cmd : redis-cli -h [ip] get myimage > ok.png
        (2) atomic increament
            - set counter 1000
            - INCR counter
                1001
            - INCR counter
                1002
            - INCRBY counter 50
                1052
            - DECR counter
                1051
            - DECRBY counter 50
                1001
        (3) MSET/MGET (multi-sets, gets)
            - MSET key1 "hello" key2 "World"
            - get key1
                "hello"
            - get key2 
                "world"
            - mget key1 key2
        2) list
        (1) lpush/rpush/pop(left, right push)
            - rpush mylist A(append)
            - rpush mylist B(append)
            - lrange mylist 0 -1 (start, stop)
            - lpush mylist first(insert!!!)
            - rpop mylist

.... and .... lose time.... so, replace.... url : https://www.joinc.co.kr/w/man/12/REDIS/IntroDataType
