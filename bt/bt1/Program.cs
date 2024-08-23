using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace bt1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1.thiet lap endpoint va socket
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("22.12.44.2"), 6969);//khai baos 1 dia chi ip va cong
            Socket sever = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

            //2. ket noi socket voi end point
            sever.Bind(endPoint);

            //3. lang nghe tu client
            sever.Listen(10); //lắng nghe từ 10 client
            Console.WriteLine("watting to conect...");

            //4.chấp nhận kết nối từ client;
            Socket client = sever.Accept();
            Console.WriteLine("chấp nhận kết nối với {0}",client.RemoteEndPoint.ToString());

            //5. gui nhan du lieu 
            string s = "Chao Mung ddien voi may chu";

            //tạo mảng dữ liệu byte để gửi
            byte[] datasend =new byte[1024];
            datasend = Encoding.ASCII.GetBytes(s);
            client.Send(datasend,datasend.Length,SocketFlags.None);

            //tao vong lap de gui du lieu
            while (true) 
            
            {
                byte[] datarecv=new byte[1024]; 
                int recv = client.Receive(datarecv);
                s= Encoding.ASCII.GetString(datarecv,0,recv);
                Console.WriteLine("Client: {0}", s);

                //neu nhan duoc du lieu thi thoat
                if (s.ToUpper().Equals("EXIT")) break;

                //lam moi mang byte gui du lieu
                s=s.ToUpper();
                datasend = Encoding .ASCII.GetBytes(s);
                client.Send(datasend,datasend.Length,SocketFlags.None);




            }
            client.Close();
            sever.Close();



        }
    }
}
