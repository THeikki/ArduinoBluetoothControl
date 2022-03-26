﻿using System;
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
        string sensorValues;
        string gasValue;
        string message;
        string tempValue;
        string[] dataParts;
        float temperature;
        float gas;
        bool isSaving = false;
        DateTime now;
        private SerialPort serialPort;
        
        public ArduinoController()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                    comboBox.Enabled = false;
                    Thread t = new Thread(GetSensorValuesFromArduino);
                    t.Start();
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
       
        private void saveButton_Click(object sender, EventArgs e)
        {
            saveButton.Enabled = false;    
            Thread th = new Thread(PostArduinoValuesToDatabase);
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
        }

        public void ConvertStringToDouble() //  Convert string temperature value to decimal number
        {
            temperature = (float)Convert.ToDouble(tempValue);
            
        }

        public void ConvertGasValueToMessage()
        {
            gas = (float)Convert.ToDouble(gasValue);

            if(gas == 1)
            {
                message = "Kaikki ok";
            }
            else if(gas == 0)
            {
                message = "Kaasuvuoto!";
            }

        }

        public void CheckIfValidTemperatureValue()
        {
            try
            {          
                int num = 0;
                char invalid = '.';
                for (int i = 0; i < tempValue.Length; i++)
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
              
                ConvertStringToDouble();
            }
            catch(Exception)
            {

            }
            
        }

        public void GetSensorValuesFromArduino()    //  Get sensor values (temperature and gas) from Arduino
        {
            saveButton.Enabled = true;
            while (serialPort.IsOpen)
            {
                try
                {
                    sensorValues = serialPort.ReadLine();
                    dataParts = sensorValues.Split(' ');
                    tempValue = dataParts[0];
                    gasValue = dataParts[1];
                    CheckIfValidTemperatureValue();
                    ConvertGasValueToMessage();
                    now = DateTime.Now;
                    if (temperature >= -10 && temperature <= 60)
                    {
                        textBox.Text = "\r\n" + "Aika: " + now.ToString(("dd-MM-yyyy HH:mm:ss")) + "\r\n\r\n" + "Lämpötila: " + temperature.ToString() + 
                            "\r\n\r\n" + "Kaasuarvo: " + message;
                    }
                   
                    Thread.Sleep(5000);
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message, "Virhe");                   
                }
            }
        }

        public void PostArduinoValuesToDatabase()  //  API connection to database
        {
            saveButton.Enabled = false;
            stopButton.Enabled = true;
            isSaving = true;
            try
            {
                while (isSaving == true && serialPort.IsOpen)
                {
                    string json = "{\"timestamp\":\"" + now.ToString("dd-MM-yyyy HH:mm:ss") + "\",\"temperature\":\"" + 
                                    temperature + "\",\"gas\":\"" + message + "\"}";
                    string url = String.Format("http://localhost:5000/api/arduinoData/data/saveArduinoData");
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
