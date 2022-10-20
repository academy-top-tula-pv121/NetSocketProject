using System.Net.Sockets;

namespace NetSocketProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket socketUdp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }
    }
}