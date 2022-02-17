using System.Diagnostics;
using System.IO.Ports;

namespace COMCommunication
{
    public class COM
    {
        public void Start()
        {
            using (SerialPort serialPort = new SerialPort())
            {
                serialPort.PortName = "COM1";
                serialPort.BaudRate = 9600;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                //serialPort.DtrEnable = true;

                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(ErrorReceivedHandler);

                serialPort.Open();
            }
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Debug.Print("Data Received:");
            Debug.Print(indata);
        }

        private static void ErrorReceivedHandler(object sender, SerialErrorReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Debug.Print("Data Received:");
            Debug.Print(indata);
        }
    }
}
