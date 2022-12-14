using System;
using System.Text;
public class SynchronousSocketListener
{
    public static string data = null;

    public static void StartListening()
    {
        byte[] bytes = new Byte[1024];

       
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
        Socket listener = new Socket(ipAddress.AddressFamily);
        string fileName = "file.json";
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.  
            while (true)
            {
                Console.WriteLine("Waiting for a connection..."); 
                Socket handler = listener.Accept();
                data = null;
                bytesRec = handler.Receive(bytes);
                data = Encoding.ASCII.GetString(bytes, 0, bytesRec);  
                
                string[] dataArr = data.Split(/*Add required input for splitting the string*/);
                string name = dataArr[0];
                string intrests = dataArr[1];
                string mail = dataArr[2];
                string jsonData = "{ \"name\": \"" + name + "\", \"intrests\": \"" + intrests + "\", \"mail\": \"" + mail + "\" }";
                Console.WriteLine("Name: {0}", name);
                Console.WriteLine("Intrests: {0}", intrests);
                Console.WriteLine("Email: {0}", mail);
                if (/*Add condition based on the code*/)
                {
                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        sw.WriteLine(jsonData);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        sw.WriteLine(jsonData);
                    }
                }
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }
    public static int Main(String[] args)
    {
        Start();
        return 0;
    }
}