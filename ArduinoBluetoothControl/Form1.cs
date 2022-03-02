using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net;
using System.IO;

namespace ArduinoBluetoothControl
{
    public partial class ArduinoController : Form
    {
        string tempValues;
        float temperature;
        bool isSaving = false;
        DateTime now;
        private SerialPort serialPort;
        
        public ArduinoController()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            saveButton.Enabled = false;
            stopButton.Enabled = false;
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox.Items.Add(port);
            }
        }

        private void ArduinoController_FormClosed(object sender, FormClosedEventArgs e)
        {
            isSaving = false;
            Application.Exit();
        }

        /********************************************************************************************
            Button actions
        */

        private void connectButton_Click(object sender, EventArgs e)
        {         
            if (comboBox.Text != "")
            {
                try
                {
                    CreateSerialPortConnection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Virhe");
                }
            }
            else
            {
                MessageBox.Show("Valitse yhdistettävä portti ensin!", "Virhe");
            }
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            if(serialPort != null)
            {
                serialPort.Dispose();
                serialPort.Close();
            }
            
            Application.Exit();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            comboBox.Enabled = false;
            Thread t = new Thread(GetTempFromArduino);
            t.Start();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveButton.Enabled = false;    
            Thread th = new Thread(PostTempValuesToDatabase);
            th.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            saveButton.Enabled = true;
            stopButton.Enabled = false;
            isSaving = false;
        }

        /*******************************************************************************************
           Functions
       */

        public void CreateSerialPortConnection()    //Open serial connections
        {
            serialPort = new SerialPort();
            serialPort.PortName = comboBox.SelectedItem.ToString();
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Open();
            connectButton.Text = "YHDISTETTY";
            connectButton.BackColor = Color.LightGreen;
            connectButton.Enabled = false;
            comboBox.Enabled = false;
            startButton.Enabled = true;
        }

        public void ConvertStringTempearatureToDouble() //  Convert string value to number
        {
            temperature = (float)Convert.ToDouble(tempValues);
        }

        public void CheckIfValidTemperatureValue()
        {
            try
            {          
                int num = 0;
                char invalid = '.';
                for (int i = 0; i < tempValues.Length; i++)
                {
                    if (i == invalid)
                    {
                        num++;
                        if (num == 2)
                        {
                            textBox.Text = "";
             
                        }
                    }
                }
              
                ConvertStringTempearatureToDouble();
            }
            catch(Exception)
            {

            }
            
        }

        public void GetTempFromArduino()    //  Get sensor values from Arduino
        {
            saveButton.Enabled = true;
            while (serialPort.IsOpen)
            {
                try
                {
                    tempValues = serialPort.ReadLine();
                    CheckIfValidTemperatureValue();
                    now = DateTime.Now;
                    if (temperature >= -10 && temperature <= 60)
                    {
                        //buttonSaveTemps.Enabled = true;
                        textBox.Text = "\r\n" + "Aika: " + now.ToString(("dd-MM-yyyy HH:mm:ss")) + "\r\n\r\n" + "Lämpötila: " + temperature.ToString();
                    }
                   
                    Thread.Sleep(5000);
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message, "Virhe");                   
                }
            }
        }

        public void PostTempValuesToDatabase()  //  API connection to database
        {
            saveButton.Enabled = false;
            stopButton.Enabled = true;
            isSaving = true;
            try
            {
                while (isSaving == true && serialPort.IsOpen)
                {
                    string json = "{\"timestamp\":\"" + now.ToString("dd-MM-yyyy HH:mm:ss") + "\",\"value\":\"" + temperature + "\"}";
                    string url = String.Format("http://localhost:5000/api/arduinoData/temperatures/saveTemperature");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.ContentType = "application/json";
                    request.Method = "POST";

                    using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                        Thread.Sleep(5000);
                    }

                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        string result;
                        using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                        {
                            result = rdr.ReadToEnd();
                            Console.WriteLine(result);
                        }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err);
                        //return;
                        isSaving = false;
                        serialPort.Dispose();
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                isSaving = false;
            }
            
        }
    }
}
