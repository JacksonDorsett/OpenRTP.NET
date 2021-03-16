# OpenRTP.NET
OpenRTP.NET is an open source implementation of the RTP protocol adhering to the RFC 3550. OpenRTP.Net will be an easy to integrate and easy to extend for whatever streaming purposes the user needs.


## Usage

```C#
Session client = new Session(ip, port);
if(!client.IsConnected){
    Console.WriteLine("failed to connect session.")
}

client.Send(H264Payload);
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.



## License
[MIT](https://choosealicense.com/licenses/mit/)