/******************************************************************************
 *
 * Filename: PicoHRDLConsole.cs
 *
 * Description:
 *     This is a Windows Forms application that demonstrates how to use the
 *     PicoLog High Resolution Data Logger (picohrdl) driver functions 
 *     using .NET to collect data on Channel 1 of an ADC-20/24 device.
 *      
 * Supported PicoLog models:
 *  
 *     ADC-20
 *     ADC-24
 *      
 * Copyright © 2015-2017 Pico Technology Ltd. See LICENSE file for terms.
 *      
 ******************************************************************************/

using Newtonsoft.Json;
using PicoHRDLImports;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PicoHRDLGui
{
    public partial class PicoHRDLCSGuiForm : Form
    {
        short handle;

        HRDLthread driverthread = new HRDLthread();
        ScottPlot.Plottable.ScatterPlotList<double> scatterlist;

        public PicoHRDLCSGuiForm()
        {
            InitializeComponent();
            scatterlist = formsPlot1.Plot.AddScatterList();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            channel1DataTextBox.Clear();
            numSamplesCollectedTextBox.Clear();

            Thread.Sleep(1000);

            if (handle > 0)
            {
                // Set Input channel 1 - enabled, range = 2500mV, single ended
                short analogChannelStatus = Imports.SetAnalogInChannel(handle,
                                                                       (short)Imports.HRDLInputs.HRDL_ANALOG_IN_CHANNEL_1,
                                                                       1,
                                                                       (short)Imports.HRDLRange.HRDL_2500_MV,
                                                                       0);

                // Set Interval time = 80 ms, conversion time = 60 ms
                short returnIntervalStatus = Imports.SetInterval(handle, 80, (short)Imports.HRDLConversionTime.HRDL_60MS);

                // Specify number of values to collect and capture block of data
                int numSamplesPerChannel = 0;

                Int32.TryParse(numSamplesPerChannelTextBox.Text, out numSamplesPerChannel);

                short status = Imports.HRDLRun(handle, numSamplesPerChannel, (short)Imports.BlockMethod.HRDL_BM_BLOCK);

                short ready = Imports.HRDLReady(handle);

                while (ready != 1)
                {
                    ready = Imports.HRDLReady(handle);
                    Thread.Sleep(100);
                }

                short stopStatus = Imports.HRDLStop(handle);

                // Get data values
                short numActiveChannels = 0;

                short numAnalogueChannelsStatus = Imports.GetNumberOfEnabledChannels(handle, out numActiveChannels);

                //int[] data = new int[numActiveChannels * numSamplesPerChannel];
                short overflow = 0;

                //int numSamplesCollectedPerChannel = Imports.GetValues(handle, data, out overflow, numSamplesPerChannel);


                int[] time = new int[numActiveChannels * numSamplesPerChannel];
                int[] data = new int[numActiveChannels * numSamplesPerChannel];

                int numSamplesCollectedPerChannel = Imports.GetTimesAndValues(handle, time, data, out overflow, numSamplesPerChannel);

                // Get Max Min ADC Count values for Channel 1
                int minAdc = 0;
                int maxAdc = 0;

                short returnAdcMaxMin = Imports.GetMinMaxAdcCounts(handle,
                                                                   out minAdc,
                                                                   out maxAdc,
                                                                   (short)Imports.HRDLInputs.HRDL_ANALOG_IN_CHANNEL_1);

                // Display retreived data
                numSamplesCollectedTextBox.Text += numSamplesCollectedPerChannel.ToString();

                float[] scaledData = new float[numSamplesCollectedPerChannel * numActiveChannels];

                for (int n = 0; n < numSamplesCollectedPerChannel; n++)
                {
                    scaledData[n] = adcToMv(data[n], (short)Imports.HRDLRange.HRDL_2500_MV, maxAdc);
                    channel1DataTextBox.Text += "Raw: " + data[n] + "\tScaled: " + scaledData[n] + "\r\n";
                }

                formsPlot1.Plot.AddScatter(time.Select(x => (double)x).ToArray(), scaledData.Select(x => (double)x).ToArray());
                formsPlot1.Refresh();
            }
            else
            {
                MessageBox.Show("No connection to device.");
            }
        }

        /**
         * GetDeviceInfo 
         * 
         * Prints information about the device to the console window.
         * 
         * Inputs:
         *      handle - the handle to the device
         */
        public void GetDeviceInfo(short handle)
        {
            string[] description = {
                           "Driver Version    ",
                           "USB Version       ",
                           "Hardware Version  ",
                           "Variant Info      ",
                           "Serial            ",
                           "Cal Date          ",
                           "Kernel Ver        "
                         };

            System.Text.StringBuilder line = new System.Text.StringBuilder(80);

            if (handle >= 0)
            {
                for (short i = 0; i < 6; i++)
                {

                    Imports.GetUnitInfo(handle, line, 80, i);

                    unitInfoTextBox.Text += description[i] + ": " + line.ToString() + "\r\n";
                }
            }

        }

        /**
         * adcToMv 
         * 
         * 
         */
        public float adcToMv(int value, short range, int maxValue)
        {
            float mvValue = 0.0f;

            float vMax = (float)(Imports.MAX_VOLTAGE_RANGE / Math.Pow(2, range)); // Find voltage scaling factor

            mvValue = ((float)value / maxValue) * vMax;

            return mvValue;
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            // Clear text boxes
            unitInfoTextBox.Clear();

            handle = Imports.HRDLOpenUnit();

            GetDeviceInfo(handle);

            // Set Mains Rejection

            short setMainsStatus = Imports.SetMains(handle, Imports.HRDLMainsRejection.HRDL_FIFTY_HERTZ);   // Set noise rejection for 50Hz  
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            short closeStatus = Imports.HRDLCloseUnit(handle);

            //System.Windows.Forms.Application.Exit();
        }

        private void btnSS_Click(object sender, EventArgs e)
        {
            channel1DataTextBox.Clear();
            numSamplesCollectedTextBox.Clear();

            Thread.Sleep(1000);

            if (handle > 0)
            {
                // Set Input channel 1 - enabled, range = 2500mV, single ended
                short analogChannelStatus = Imports.SetAnalogInChannel(handle,
                                                                       (short)Imports.HRDLInputs.HRDL_ANALOG_IN_CHANNEL_1,
                                                                       1,
                                                                       (short)Imports.HRDLRange.HRDL_2500_MV,
                                                                       0);

                // Set Interval time = 80 ms, conversion time = 60 ms
                short returnIntervalStatus = Imports.SetInterval(handle, 80, (short)Imports.HRDLConversionTime.HRDL_60MS);

                // Specify number of values to collect and capture block of data
                int numSamplesPerChannel = 0;

                Int32.TryParse(numSamplesPerChannelTextBox.Text, out numSamplesPerChannel);

                short status = Imports.HRDLRun(handle, numSamplesPerChannel, (short)Imports.BlockMethod.HRDL_BM_STREAM);

                Thread.Sleep(1000);
                short ready = Imports.HRDLReady(handle);

                while (ready != 1)
                {
                    ready = Imports.HRDLReady(handle);
                    Thread.Sleep(100);
                }


                // Get data values
                short numActiveChannels = 0;

                short numAnalogueChannelsStatus = Imports.GetNumberOfEnabledChannels(handle, out numActiveChannels);

                //int[] data = new int[numActiveChannels * numSamplesPerChannel];
                short overflow = 0;

                //int numSamplesCollectedPerChannel = Imports.GetValues(handle, data, out overflow, numSamplesPerChannel);


                int[] time = new int[numActiveChannels * numSamplesPerChannel];
                int[] data = new int[numActiveChannels * numSamplesPerChannel];

                int numSamplesCollectedPerChannel = Imports.GetTimesAndValues(handle, time, data, out overflow, numSamplesPerChannel);

                short stopStatus = Imports.HRDLStop(handle);

                // Get Max Min ADC Count values for Channel 1
                int minAdc = 0;
                int maxAdc = 0;

                short returnAdcMaxMin = Imports.GetMinMaxAdcCounts(handle,
                                                                   out minAdc,
                                                                   out maxAdc,
                                                                   (short)Imports.HRDLInputs.HRDL_ANALOG_IN_CHANNEL_1);

                // Display retreived data
                numSamplesCollectedTextBox.Text += numSamplesCollectedPerChannel.ToString();

                float[] scaledData = new float[numSamplesCollectedPerChannel * numActiveChannels];
                float[] scaledTime = new float[numSamplesCollectedPerChannel * numActiveChannels];

                for (int n = 0; n < numSamplesCollectedPerChannel; n++)
                {
                    scaledTime[n] = time[n];
                    scaledData[n] = adcToMv(data[n], (short)Imports.HRDLRange.HRDL_2500_MV, maxAdc);
                    channel1DataTextBox.Text += "Time: " + time[n] + "\tRaw: " + data[n] + "\tScaled: " + scaledData[n] + "\r\n";
                }

                channel1DataTextBox.Text += "X: " + scaledTime.Length + "\tY: " + scaledData.Length + "\r\n";
                formsPlot1.Plot.AddScatter(scaledTime.Select(x => (double)x).ToArray(), scaledData.Select(x => (double)x).ToArray());
                formsPlot1.Refresh();
            }
            else
            {
                MessageBox.Show("No connection to device.");
            }
        }

        private static Tuple<float[], float[]> liveResult = new Tuple<float[], float[]>(null, null);
        private rawInfo rawi;
        private void btnLive_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Clear();
            scatterlist.Clear();
            rawi = new rawInfo();
            liveResult = driverthread.startLive(handle, 10);
            scatterlist.AddRange(liveResult.Item1.Select(x => (double)x).ToArray(), liveResult.Item2.Select(x => (double)x).ToArray());
            formsPlot1.Plot.Add(scatterlist);
            formsPlot1.Refresh();
            timerPlot.Enabled = true;
            timerUpdatePlot.Enabled = true;
        }

        private void btnLiveStop_Click(object sender, EventArgs e)
        {
            driverthread.stopLive(handle);
            timerPlot.Enabled = false;
            timerUpdatePlot.Enabled = false;
            rawi.endDateTime = DateTime.UtcNow;
        }

        private void timerPlot_Tick(object sender, EventArgs e)
        {
            var tempresult = driverthread.collectLive(handle, 10);
            liveResult = Tuple.Create(appendArray(liveResult.Item1, tempresult.Item1), appendArray(liveResult.Item2, tempresult.Item2));
            for (int i = 0; i < tempresult.Item1.Length; i++)
            {
                scatterlist.Add(tempresult.Item1[i], tempresult.Item2[i]);
            }
            formsPlot1.Render();
            formsPlot1.Refresh();
            double currentRightEdge = formsPlot1.Plot.GetAxisLimits().XMax;
            formsPlot1.Plot.SetAxisLimits(xMax: liveResult.Item1.Max() * 1.1);
            formsPlot1.Plot.SetAxisLimits(yMin: liveResult.Item2.Min() * 0.95);
            formsPlot1.Plot.SetAxisLimits(yMax: liveResult.Item2.Max() * 1.05);
            channel1DataTextBox.Text += "Incoming " + tempresult.Item1.Length + ",\t totaling " + liveResult.Item1.Length + "\t data points" + "\r\n";
        }

        private float[] appendArray(float[] m, float[] n)
        {
            var r = new float[m.Length + n.Length];
            m.CopyTo(r, 0);
            n.CopyTo(r, m.Length);
            return r;
        }

        private void timerUpdatePlot_Tick(object sender, EventArgs e)
        {
            formsPlot1.Render();
            formsPlot1.Refresh();
            double currentRightEdge = formsPlot1.Plot.GetAxisLimits().XMax;
            formsPlot1.Plot.SetAxisLimits(xMax: liveResult.Item1.Max() * 1.1);
            formsPlot1.Plot.SetAxisLimits(yMin: liveResult.Item2.Min() * 0.95);
            formsPlot1.Plot.SetAxisLimits(yMax: liveResult.Item2.Max() * 1.05);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (liveResult != null)
            {
                ResultFile Resultobj = new ResultFile(liveResult, rawi);

                Stream savestream;
                var sfd = new SaveFileDialog();
                sfd.RestoreDirectory = true;
                sfd.Filter = "LEP data files (*.ldf)|*.ldf|All files (*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if ((savestream = sfd.OpenFile()) != null)
                    {
                        // 使用Settings.Default["FileTextOrBinary"]记录存储模式
                        // text - 文本模式
                        // bin - binary模式
                        // 默认为binary
                        /*if ((string)Properties.Settings.Default["FileTextOrBinary"] == "text")
                        {*/
                        StreamWriter sw = new StreamWriter(savestream);
                            string js = JsonConvert.SerializeObject(Resultobj);
                            JsonSerializer jslz = new JsonSerializer();
                            sw.WriteLine(js);
                            sw.Close();
                            savestream.Close();
                        /*}
                        else
                        {
                            BinaryWriter bw = new BinaryWriter(savestream);
                            string js = JsonConvert.SerializeObject(Resultobj);
                            JsonSerializer jslz = new JsonSerializer();
                            bw.Write(js);
                            bw.Close();
                            savestream.Close();
                        }*/
                    }
                }
            }
            else
            {
                MessageBox.Show("No live result to write.");
            }
        }

        public bool IsBinary(string filePath, int requiredConsecutiveNul = 1)
        {
            const int charsToCheck = 8000;
            const char nulChar = '\0';

            int nulCount = 0;

            using (var streamReader = new StreamReader(filePath))
            {
                for (var i = 0; i < charsToCheck; i++)
                {
                    if (streamReader.EndOfStream)
                        return false;

                    if ((char)streamReader.Read() == nulChar)
                    {
                        nulCount++;

                        if (nulCount >= requiredConsecutiveNul)
                            return true;
                    }
                    else
                    {
                        nulCount = 0;
                    }
                }
            }
            return false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                ofd.Filter = "LEP data files (*.ldf)|*.ldf|All files (*.*)|*.*";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    /*
                    if (IsBinary(ofd.ToString()))
                    {
                        using (var osr = new BinaryReader(ofd.OpenFile()))
                        {
                            var osrout = osr.ReadString();
                            formsPlot1.Plot.Clear();
                            scatterlist.Clear();
                            ResultFile resultopenfile = JsonConvert.DeserializeObject<ResultFile>(osrout);
                            scatterlist.AddRange(resultopenfile.rawTuple.Item1.Select(x => (double)x).ToArray(), resultopenfile.rawTuple.Item2.Select(x => (double)x).ToArray());
                            formsPlot1.Plot.Add(scatterlist);
                            formsPlot1.Refresh();
                        }
                    }
                    else
                    {
                    */
                        using (var osr = new StreamReader(ofd.OpenFile()))
                        {
                            var osrout = osr.ReadToEnd();
                            formsPlot1.Plot.Clear();
                            scatterlist.Clear();
                            ResultFile resultopenfile = JsonConvert.DeserializeObject<ResultFile>(osrout);
                            scatterlist.AddRange(resultopenfile.rawTuple.Item1.Select(x => (double)x).ToArray(), resultopenfile.rawTuple.Item2.Select(x => (double)x).ToArray());
                            formsPlot1.Plot.Add(scatterlist);
                            formsPlot1.Refresh();
                        }
                    //}
                }
            }
        }
    }
}



