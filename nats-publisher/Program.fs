open NATS.Client
open System
open System.Text

type public NatsWorker(host : string, port : string) =
   let mutable _host = host;
   let mutable _port = port;
   let mutable _msg = "hello world"
   let mutable _subject = "foo"
    
   member this.onPublish = 
        let cf = new ConnectionFactory()
        let ip_port = host + ":" + port
        let c = cf.CreateConnection(ip_port)
        printfn "Published message %s to subject %s" _msg _subject
        c.Publish(_subject, Encoding.UTF8.GetBytes(_msg));

   member this.Host
      with get() = _host
      and set(value) = _host <- value

   member this.Port
      with get() = _port
      and set(value) = _port <- value



[<EntryPoint>]
let main argv = 
    printfn "Start Publisher"

    let publish = NatsWorker("10.0.2.2", "4222")
    publish.onPublish

    let receiveKey = Console.ReadLine()
    0 // return an integer exit codegf


