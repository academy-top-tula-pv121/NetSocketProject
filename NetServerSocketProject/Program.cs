using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetServerSocketProject
{
    internal class Program
    {
        static int port = 10001;
        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, port);

            Socket socketListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketListen.Bind(endPoint);
            socketListen.Listen();

            Console.WriteLine($"Server is Listener...");

            while(true)
            {
                Socket socketHandler = socketListen.Accept();
                StringBuilder str = new StringBuilder();

                byte[] buffer = new byte[1024];
                do
                {
                    int byteCount = socketHandler.Receive(buffer);
                    str.Append(Encoding.Default.GetString(buffer, 0, byteCount));
                } while (socketHandler.Available > 0);

                Console.WriteLine($"{DateTime.Now} : {str.ToString()}");

                buffer = Encoding.Default.GetBytes("Your message is read");
                socketHandler.Send(buffer);

                socketHandler.Shutdown(SocketShutdown.Both);
                socketHandler.Close();
            }
        }
    }
}