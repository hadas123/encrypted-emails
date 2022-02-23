using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intel.Dal;
using System.IO;
using System.Windows;

namespace Mailbox
{
    class JHI_SINGLTONE
    {
        static Jhi jhi;
        static JhiSession session;
        static string appletID;
        static string appletPath;
        
       

        public const int SET_PASSWORD = 1,
                         LOGIN = 2,
                         RESET_PASSWORD = 3,
                         ENCRPT_CONTENT = 4,
                         DENCRPT_CONTENT = 5,
                         GET_PUBLIC_KEY = 6,
                         LOGOUT=7,
                         SING=8,
                         VERIFY = 9;
        public static byte[] sendIntAndReciveBytes( int cmdId)
        {

            byte[] sendBuff = { };
            byte[] recvBuff = new byte[2000];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return recvBuff;
        }

        public static string sendAndRcive(string sendString, int cmdId) {
            byte[] sendBuff = Encoding.ASCII.GetBytes(sendString); 
            byte[] recvBuff = new byte[2000]; 
            int responseCode; 
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return Encoding.ASCII.GetString(recvBuff);
        }
        public static string sendAndRcive(byte[] sendBuff, int cmdId)
        {   int bufferSize = (sendBuff.Length - 260) * 256;
            byte[] recvBuff = new byte[bufferSize];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return Encoding.ASCII.GetString(recvBuff);
           
        }
        public static byte[] sendBytesAndRciveBytesToEncrypt(byte[] sendBuff, int cmdId)
        {
            int bufferSize = (sendBuff.Length - 260) * 256;
            byte[] recvBuff = new byte[bufferSize];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return (recvBuff);

        }
        public static byte[] sendBytesAndRciveBytesToSing(byte[] sendBuff, int cmdId)
        {
            int bufferSize = (sendBuff.Length ) + 256;
            byte[] recvBuff = new byte[bufferSize];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return (recvBuff);

        }

        public static byte[] sendBytesAndRciveBytesToDEncrypt(byte[] sendBuff, int cmdId)
        {
            int bufferSize = (sendBuff.Length);
            byte[] recvBuff = new byte[bufferSize];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return (recvBuff);

        }

        public static string sendAndRcive( int cmdId)
        {
            byte[] sendBuff = { };
            byte[] recvBuff = new byte[2000];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return UTF32Encoding.UTF8.GetString(recvBuff);
        }
        public static bool sendAndRecive(string sendString, int cmdId)
        {
            byte[] sendBuff = UTF32Encoding.UTF8.GetBytes(sendString);
            byte[] recvBuff = new byte[2000];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return recvBuff[0]==1;
        }
        public static bool sendAndRecive(byte[] sendBuff, int cmdId)
        {
           
            byte[] recvBuff = new byte[1];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return recvBuff[0] == 1;
        }

        public static string decrypt(string file,string from)
        {
            byte[] fileContent = File.ReadAllBytes(file);
            if(!verify(fileContent,from))return "untrusted source";




            string responde = "";
            for (int i = 256; i < fileContent.Length;)
            {
                byte[] toSend = new byte[256];
                for (int j = 0; j < 256; j++) { toSend[j] = fileContent[i]; i++; }


                responde += Encoding.ASCII.GetString(

                           JHI_SINGLTONE.sendBytesAndRciveBytesToDEncrypt(


                          toSend, JHI_SINGLTONE.DENCRPT_CONTENT)

                           );
            }
            return responde;
        }
       
        private static bool verify(byte[] fileContent,string from)
        {
            byte[] PublicKey = File.ReadAllBytes(@".\..\..\address\" + from + ".text");
            return sendAndRecive((PublicKey).Concat(fileContent).ToArray(), VERIFY);
        }

        //public static string decrypt(string file)
        //{


        //    return Encoding.ASCII.GetString(

        //                   JHI_SINGLTONE.sendBytesAndRciveBytesToDEncrypt(


        //                  File.ReadAllBytes(file), JHI_SINGLTONE.DENCRPT_CONTENT_AND_VERIFY)

        //                   );

        //}
        public static byte[] ecrypt(string content, string to)
        {

            byte[] PublicKey = File.ReadAllBytes(@".\..\..\address\" + to + ".text");
            byte[] responde = { };
            for (int i = 0; i < content.Length; i++)
            {
                byte[] c = Encoding.ASCII.GetBytes(content[i].ToString());
                responde = responde.Concat(
                            sendBytesAndRciveBytesToEncrypt(
                                (PublicKey).Concat(c).ToArray(),
                                ENCRPT_CONTENT
                                )
                            ).ToArray();
            }
            responde= sendBytesAndRciveBytesToSing(
                               responde,
                               SING
                               ).Concat(responde).ToArray();
            return responde;
        }
        //public static byte[] ecrypt(string content, string to)
        //{

        //    byte[] PublicKey = File.ReadAllBytes(@".\..\..\address\" + to + ".text");
        //     byte[]Contant= Encoding.ASCII.GetBytes(content);
        //    byte[] tosend = (PublicKey).Concat(Contant).ToArray();


        //    return   sendBytesAndRciveBytesToEncrypt(
        //                        tosend,
        //                        ENCRPT_CONTENT_AND_SING
        //                        );



        //}

        public static bool sendAndRecive( int cmdId)
        {
            byte[] sendBuff = { };
            byte[] recvBuff = new byte[2000];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return recvBuff[0] == 1;
        }
        public static int sendAndRcive(int sendInt, int cmdId)
        {   
            byte[] sendBuff = UTF32Encoding.UTF8.GetBytes(sendInt.ToString());
            byte[] recvBuff = new byte[2000];
            int responseCode;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            return int.Parse(UTF32Encoding.UTF8.GetString(recvBuff));
        }

        public static void close()
        {try
            {
                jhi.CloseSession(session);
                jhi.Uninstall(appletID);
            }
            catch { }
        }

        public static void inizilze()
        {
            jhi = Jhi.Instance;
             appletID = "86c5c52d-8e0b-4067-acaa-e5f32cb66efc";
             appletPath = "C:\\Users\\eatba\\Desktop\\securty\\finalProject5\\bin\\finalProject5-debug.dalp";
          jhi.Install(appletID, appletPath);
            byte[] initBuffer = new byte[] { };
            jhi.CreateSession(appletID, JHI_SESSION_FLAGS.None, initBuffer, out session);

            

        }
    }
}
   
