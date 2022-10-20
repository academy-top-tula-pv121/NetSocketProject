using System.Net;
using System.Net.Sockets;
using System.Text;


namespace NetClientSocketProject
{
    internal class Program
    {
        static int port = 10001;

        static string addressServer = "127.0.0.1";
        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(addressServer), port);
            Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketClient.Connect(endPoint);
            string str = "Client message to server";
            byte[] buffer = Encoding.Default.GetBytes(str);

            socketClient.Send(buffer);

            buffer = new byte[1024];
            StringBuilder strServer = new();

            do
            {
                int byteCount = socketClient.Receive(buffer, buffer.Length, SocketFlags.None);
                strServer.Append(Encoding.Default.GetString(buffer, 0, byteCount));
            } while (socketClient.Available > 0);

            Console.WriteLine($"Server answer: {strServer.ToString()}");

            socketClient.Shutdown(SocketShutdown.Both);
            socketClient.Close();

            Console.ReadKey();
        }
    }
}