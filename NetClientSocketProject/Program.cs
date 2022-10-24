using System.Net;
using System.Net.Sockets;
using System.Text;


namespace NetClientSocketProject
{
    internal class Program
    {

        static int port = 10001;

        static string addressServer = "127.0.0.1";

        static void SendMessageToServer()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(addressServer), port);
            Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketClient.Connect(endPoint);

            while (true)
            {
                string str;
                Console.Write($"Input message: ");
                str = Console.ReadLine();

                byte[] buffer = Encoding.Default.GetBytes(str);

                socketClient.Send(buffer);

                buffer = new byte[1024];
                StringBuilder strServer = new();

                do
                {
                    int byteCount = socketClient.Receive(buffer, buffer.Length, SocketFlags.None);
                    strServer.Append(Encoding.Default.GetString(buffer, 0, byteCount));
                } while (socketClient.Available > 0);

                Console.WriteLine($"{DateTime.Now.ToShortDateString()} Server answer: {strServer.ToString()}");

                if (str.IndexOf("quit") != -1)
                    break;
                    
            }
            
            //string str = "Client message to server";

            socketClient.Shutdown(SocketShutdown.Both);
            socketClient.Close();
        }
        static void Main(string[] args)
        {
            SendMessageToServer();

            Console.ReadKey();
        }
    }
}