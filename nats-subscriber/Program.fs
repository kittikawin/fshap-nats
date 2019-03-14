open NATS.Client
open System
open System.Text

type public NatsWorker(host : string, port : string) =
   let mutable _host = host;
   let mutable _port = port;
   let mutable _subject = "foo"
   
   member this._onConnect = 
        let cf = new ConnectionFactory()
        let ip_port = host + ":" + port
        let c = cf.CreateConnection(ip_port)
        let sAsync = c.SubscribeAsync(_subject)
        sAsync.MessageHandler.Add( fun (a) -> 
            let msg = Encoding.UTF8.GetString(a.Message.Data)
            printfn "-- Subject -- : %s, -- Message -- %s" a.Message.Subject msg)
        sAsync.Start()
        
   member this.Host
      with get() = _host
      and set(value) = _host <- value

   member this.Port
      with get() = _port
      and set(value) = _port <- value


[<EntryPoint>]
let main argv = 
    printfn "Start subscriber `foo`"
    let nats = new NatsWorker("10.0.2.2", "4222")
    nats._onConnect
    
    let receiveKey = Console.ReadLine() // for break exit
    0 // return an integer exit code
