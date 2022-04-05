using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebSocketSharp;
using WebSocketSharp.Server;

using ProjectPokemonServer.Models;

namespace ProjectPokemonServer
{
    public class EchoAll : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Received from echoall client: " + e.Data);
            Sessions.Broadcast(e.Data);
        }
    }

    public class Connessione : WebSocketBehavior
    {
        //Lista di client connessi
        private static List<WebSocket> _player = new List<WebSocket>();
        //numero di client connessi
        private static int count;

        protected override void OnOpen()
        {
            WebSocket clientN = Context.WebSocket;
            count = _player.Count;
            Console.WriteLine("Richiesta connessione da client: " + (count + 1).ToString());
            //accetto solo due client
            if (count > 1)
            {
                Console.WriteLine("Chiusa connessione con client: " + (count + 1).ToString());
                Context.WebSocket.Close();
            }
            else
            {
                _player.Add(clientN);
            }
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            //invio ad un client i messaggi dell'altro
            Console.WriteLine("Received message from client: " + e.Data);
            if (Context.WebSocket == _player[0])
            {
                _player[1].Send(e.Data);
            }
            else
            {
                _player[0].Send(e.Data);
            }
        }
    }

    public partial class Program
    {
        int nPlayer = 0;
        static void Main(string[] args)
        {
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7890");

            wssv.AddWebSocketService<EchoAll>("/EchoAll");
            wssv.AddWebSocketService<Connessione>("/Connessione");

            wssv.Start();

            Console.WriteLine("Ws Server started on ws://127.0.0.1:7890/EchoAll");
            Console.WriteLine("Ws Server started on ws://127.0.0.1:7890/Connessione");

            

            Console.ReadKey();
            wssv.Stop();
        }

    }
}
