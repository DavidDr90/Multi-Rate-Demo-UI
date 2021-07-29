using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Net;
using System.Text.RegularExpressions;
using System.Reflection;

namespace App
{
    public partial class Form1 : Form
    {
        
        #region DLL Imports

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        private static extern void SetWindowText(IntPtr hWnd, string windowName);

        #endregion


        #region Consts

        private const int networkSamplesWindowSize = 3; // number of value (per seconds) to collect and calculate avg on
        private const int networkSamplesTimeWindowSize = 60; // number of record to save in the graph arrays, each number represent 1 second
        private const float bytesSecondsToMbytesSecondsFactor = 0.000008F; // to convert Bytes per seconds to Mega bytes per seconds

        #endregion


        #region Params

        string[] networkInstancesList = null;

        private double[] highBandArray = new double[networkSamplesTimeWindowSize];  // save only last 60 seconds
        private double[] lowBandArray = new double[networkSamplesTimeWindowSize];  // save only last 60 seconds
        private double[] cellularArray = new double[networkSamplesTimeWindowSize];  // save only last 60 seconds
        private double[] sumArray = new double[networkSamplesTimeWindowSize];  // save only last 60 seconds

        // temp arrays for calculation avg of 3 seconds
        private FixedSizedQueue<float> highBandTemp = new FixedSizedQueue<float>(networkSamplesWindowSize);
        private FixedSizedQueue<float> lowBandTemp = new FixedSizedQueue<float>(networkSamplesWindowSize);
        private FixedSizedQueue<float> cellularTemp = new FixedSizedQueue<float>(networkSamplesWindowSize);
        private FixedSizedQueue<float> sumTemp = new FixedSizedQueue<float>(networkSamplesWindowSize);

        string highBandInterfaceName = string.Empty;
        string lowBandInterfaceName = string.Empty;
        string cellularInterfaceName = string.Empty;

        private Thread networkChartThread;
        private Form formPopup = null;

        private List<Process> wifiIperfProcessList = new List<Process>();
        private List<Process> generalProcessList = new List<Process>();

        private bool connectFlag = false;
        //Ira set true instead of false
        private bool profileConnectionFlag = true;
       
        #endregion


        public Form1()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            PnlNav.Height = BtnView1.Height;
            PnlNav.Top = BtnView1.Top;
            PnlNav.Left = BtnView1.Left;
            BtnView1.BackColor = Color.FromArgb(46, 51, 73);
            numbersPanel.Dock = DockStyle.Fill;
            numbersPanel.BringToFront();

            // retrive all network interfaces from OS
            PerformanceCounterCategory pcg = new PerformanceCounterCategory("Network Interface");
            networkInstancesList = pcg.GetInstanceNames();                     
        }


        #region Button Click functions

        #region Exit Buttons        

        private void xSymboleButton_Click(object sender, EventArgs e)
        {
            // kill all program threads
            try
            {
                // if we start the netwrok thread
                if (networkChartThread != null)
                    networkChartThread.Abort();
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("ThreadAbortException was raised when trying to abort thread");
            }

            // kill all WiFI Iperf open process
            foreach (var process in wifiIperfProcessList)
            {
                SafelyKillProcess(process);
            }

            // kill all Cellular Iperf open process
            foreach (var process in generalProcessList)
            {
                killingProcess(process);
            }

            Close();
        }

        private void killingProcess(Process process)
        {
            try
            {
                Console.WriteLine("Killing process...");
                PrintProcessInfo(process);
                process.Kill();
            }
            catch (Win32Exception)
            {
                Console.WriteLine("The associated process could not be terminated." +
                    "-or- The process is terminating." +
                    "-or- The associated process is a Win16 executable.\n");
                PrintProcessInfo(process);

            }
            catch (NotSupportedException)
            {
                Console.WriteLine(" You are attempting to call System.Diagnostics.Process.Kill for a process that is running on a remote computer." +
                    " The method is available only for processes running on the local computer.");
                PrintProcessInfo(process);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("The process has already exited. " +
                    "-or- There is no process associated with this System.Diagnostics.Process object.");
                PrintProcessInfo(process);
            }
        }

        #endregion


        #region View buttons     

        private void BtnView1_Click(object sender, EventArgs e)
        {
            PnlNav.Height = BtnView1.Height;
            PnlNav.Top = BtnView1.Top;
            PnlNav.Left = BtnView1.Left;
            BtnView1.BackColor = Color.FromArgb(46, 51, 73);
            numbersPanel.Visible = true;
            settingsPanel.Visible = false;
            graphPanel.Visible = false;
            numbersPanel.BringToFront();
        }

        private void BtnView2_Click(object sender, EventArgs e)
        {
            PnlNav.Height = BtnView2.Height;
            PnlNav.Top = BtnView2.Top;
            PnlNav.Left = BtnView2.Left;
            BtnView2.BackColor = Color.FromArgb(46, 51, 73);
            numbersPanel.Visible = false;
            settingsPanel.Visible = false;
            graphPanel.Visible = true;
            graphPanel.BringToFront();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            InitializeComboBox();

            PnlNav.Height = BtnSettings.Height;
            PnlNav.Top = BtnSettings.Top;
            PnlNav.Left = BtnSettings.Left;
            BtnSettings.BackColor = Color.FromArgb(46, 51, 73);
            numbersPanel.Visible = false;
            settingsPanel.Visible = true;
            graphPanel.Visible = false;
            settingsPanel.BringToFront();
        }

        private void BtnView1_Leave(object sender, EventArgs e)
        {
            BtnView1.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnView2_Leave(object sender, EventArgs e)
        {
            BtnView2.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnSettings_Leave(object sender, EventArgs e)
        {
            BtnSettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        #endregion


        #region Start Demo Buttons

        private void btnStartDemo_Click(object sender, EventArgs e)
        {
            //SW RF Kill            
            SetPhyRadioState(Wlan.Dot11RadioState.Off);            
            // start cellular Iper
            startCellularIperf();
            //Thread.Sleep(TimeSpan.FromSeconds(2));

            //SW RF Kill - On            
            SetPhyRadioState(Wlan.Dot11RadioState.On);

            // Connect to entered SSID profile
            connectToProfile();
            //Ira added more delay
            Thread.Sleep(TimeSpan.FromSeconds(4));

            startAllWifiIperf();

            StartMonitoringTraffic();
           
        }       

        /// <summary>
        /// connect or disconnect the wifi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wifiButtom_Click(object sender, EventArgs e)
        {
            try
            {
                stopAllWifiIperfProcess();
              
                Wlan.Dot11RadioState softwareRadioState = Wlan.Dot11RadioState.Off;
                string buttonLabal = string.Empty;

                // set the wanted state based on perviuse state
                softwareRadioState = (connectFlag) ? Wlan.Dot11RadioState.On : Wlan.Dot11RadioState.Off;
                buttonLabal = (connectFlag) ? "Disconnect WiFi" : "Connect WiFi";
                wifiButtom.Text = buttonLabal;
                 
                SetPhyRadioState(softwareRadioState);
                //Console.WriteLine($"Successfully set The SW RF state to {softwareRadioState}");

                if (connectFlag)
                {
                   
                    // David - cannot reduce this value!!!
                    Thread.Sleep(TimeSpan.FromSeconds(4)); // delay to let the connection process finish
                    startAllWifiIperf();
                }
                connectFlag = !connectFlag; // change the state for next click

            }
            catch (Win32Exception ex)
            {
                Console.WriteLine($"Got Win32Exception.\nMessage: [{ex.Message}]");
            }
        }

        private void runIperButton_Click(object sender, EventArgs e)
        {       
            startCellularIperf();
            startAllWifiIperf();
        }

        private void startMonitorButton_Click(object sender, EventArgs e)
        {
            StartMonitoringTraffic();
        }

        #endregion

        private void profileButton_Click(object sender, EventArgs e)
        {
            connectToProfile();
        }

        #endregion


        #region Combo Drop Box

        // This method initializes the combo box, adding a large string array
        // but limiting the drop-down size to six rows so the combo box doesn't 
        // cover other controls when it expands.
        private void InitializeComboBox()
        {
            highBandDrop.MaxDropDownItems = 5;
            lowBandDrop.MaxDropDownItems = 5;
            cellularDrop.MaxDropDownItems = 5;

            if (highBandDrop.Items.Count == 0)
                highBandDrop.Items.AddRange(networkInstancesList);
            if (lowBandDrop.Items.Count == 0)
                lowBandDrop.Items.AddRange(networkInstancesList);
            if (cellularDrop.Items.Count == 0)
                cellularDrop.Items.AddRange(networkInstancesList);


            // Associate the event-handling method with the 
            // SelectedIndexChanged event.
            highBandDrop.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
            lowBandDrop.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
            cellularDrop.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        }


        /// <summary>
        /// save each selected interface for future use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (highBandDrop.SelectedItem != null)
                highBandInterfaceName = highBandDrop.SelectedItem.ToString();
            if (lowBandDrop.SelectedItem != null)
                lowBandInterfaceName = lowBandDrop.SelectedItem.ToString();
            if (cellularDrop.SelectedItem != null)
                cellularInterfaceName = cellularDrop.SelectedItem.ToString();
        }


        #endregion


        #region Graph Update

        /// <summary>
        /// Start a new thread to monitor traffic statistics every 1 sec
        /// show file transfer window
        /// </summary>
        private void StartMonitoringTraffic()
        {
            // make sure we only have one thread running
            if (networkChartThread != null && networkChartThread.IsAlive)
            {
                networkChartThread.Abort();
            }
            networkChartThread = new Thread(new ThreadStart(this.getNetworkPerformanceCountersGraph));
            networkChartThread.IsBackground = true;
            networkChartThread.Start();

            ShowFileTransferDialogBox();
        }

        private void UpdateNetworkChart()
        {

            mainChart.Series["HighBand"].Points.Clear();
            mainChart.Series["LowBand"].Points.Clear();
            mainChart.Series["Cellular"].Points.Clear();
            mainChart.Series["Sum"].Points.Clear();

            for (int i = 0; i < highBandArray.Length - 1; ++i)
            {
                mainChart.Series["HighBand"].Points.AddY(highBandArray[i]);
                mainChart.Series["LowBand"].Points.AddY(lowBandArray[i]);
                mainChart.Series["Cellular"].Points.AddY(cellularArray[i]);
                mainChart.Series["Sum"].Points.AddY(sumArray[i]);
            }
        }

        private void getNetworkPerformanceCountersGraph()
        {
            PerformanceCounter highBandPerformanceCounter = null;
            PerformanceCounter lowBandPerformanceCounter = null;
            PerformanceCounter cellularPerformanceCounter = null;
            float newHighBandValue = 0;
            float newLowBandValue = 0;
            float newCellularValue = 0;
            float newSumValue = 0;

            if (!string.IsNullOrEmpty(highBandInterfaceName))
                highBandPerformanceCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", highBandInterfaceName);
            if (!string.IsNullOrEmpty(lowBandInterfaceName))
                lowBandPerformanceCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", lowBandInterfaceName);
            if (!string.IsNullOrEmpty(cellularInterfaceName))
                cellularPerformanceCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", cellularInterfaceName);

            while (true)
            {
                // read next value
                if (highBandPerformanceCounter != null)
                    newHighBandValue = getNewHighBandValue(highBandPerformanceCounter.NextValue());
                if (lowBandPerformanceCounter != null)
                    newLowBandValue = getNewLowBandValue(lowBandPerformanceCounter.NextValue());
                if (cellularPerformanceCounter != null)
                    newCellularValue = getNewCellularValue(cellularPerformanceCounter.NextValue());


                newSumValue = newHighBandValue + newLowBandValue + newCellularValue;

                string underOneString = "Disconnected";

                if (newHighBandValue < 1)
                    ThreadHelperClass.SetText(this, highBandLabel, underOneString);
                else
                    ThreadHelperClass.SetText(this, highBandLabel, FileSizeFormatter.FormatSizeNew(newHighBandValue));

                if (newLowBandValue < 1)
                    ThreadHelperClass.SetText(this, lowBandLabel, underOneString);
                else
                    ThreadHelperClass.SetText(this, lowBandLabel, FileSizeFormatter.FormatSizeNew(newLowBandValue));

                if (newCellularValue < 1)
                    ThreadHelperClass.SetText(this, cellularLabel, underOneString);
                else
                    ThreadHelperClass.SetText(this, cellularLabel, FileSizeFormatter.FormatSizeNew(newCellularValue));

                if (newSumValue < 1)
                    ThreadHelperClass.SetText(this, aggregateLabel, underOneString);
                else
                    ThreadHelperClass.SetText(this, aggregateLabel, FileSizeFormatter.FormatSizeNew(newSumValue));

                // update graph data
                highBandArray[highBandArray.Length - 1] = Math.Round(newHighBandValue, 0);
                lowBandArray[lowBandArray.Length - 1] = Math.Round(newLowBandValue, 0);
                cellularArray[cellularArray.Length - 1] = Math.Round(newCellularValue, 0);
                sumArray[sumArray.Length - 1] = Math.Round(newSumValue, 0);

                Array.Copy(highBandArray, 1, highBandArray, 0, highBandArray.Length - 1);
                Array.Copy(lowBandArray, 1, lowBandArray, 0, lowBandArray.Length - 1);
                Array.Copy(cellularArray, 1, cellularArray, 0, cellularArray.Length - 1);
                Array.Copy(sumArray, 1, sumArray, 0, sumArray.Length - 1);

                if (mainChart.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { UpdateNetworkChart(); });
                }
                else
                {
                    //......
                }

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        private float getNewHighBandValue(float v)
        {
            v *= bytesSecondsToMbytesSecondsFactor;
            highBandTemp.Enqueue(v);
            return highBandTemp.Average();
        }

        private float getNewLowBandValue(float v)
        {
            v *= bytesSecondsToMbytesSecondsFactor;
            lowBandTemp.Enqueue(v);
            return lowBandTemp.Average();
        }

        private float getNewCellularValue(float v)
        {
            v *= bytesSecondsToMbytesSecondsFactor;
            cellularTemp.Enqueue(v);
            return cellularTemp.Average();
        }      
        
        /// <summary>
        /// This function show a popup window with git of file transfer
        /// </summary>
        public void ShowFileTransferDialogBox()
        {
            // create only one popup window
            if (formPopup != null)
                return;
            formPopup = new Form();
            formPopup.Size = new Size(285, 100);  // much the gif image size

            // add the git picture to the popup window
            //formPopup.Controls.Add(picture);
            fileTransferPicture.Dock = DockStyle.Fill;
            fileTransferPicture.Visible = true;
            formPopup.Controls.Add(fileTransferPicture);

            // remove unneeded controls
            formPopup.MaximizeBox = false;
            formPopup.MinimizeBox = false;

            // show the popup
            formPopup.Show(this);
        }


        #endregion


        #region IPerf Running        

        private void startWiFi1Iperf()
        {
            if (!string.IsNullOrEmpty(iperfCmdWiFi1.Text))
            {
                startIPerfProcess(iperfCmdWiFi1.Text);
            }
        }
        
        private void startWiFi2Iperf()
        {
            if (!string.IsNullOrEmpty(iperfCmdWiFi2.Text))
            {
                startIPerfProcess(iperfCmdWiFi2.Text);        
            }
        }

        private void startCellularIperf()
        {
            if (!string.IsNullOrEmpty(iperfCmdCellular.Text))
                startIPerfProcess(iperfCmdCellular.Text, true);
        }

        /// <summary>
        /// Start all WiFi Iperf with small delay between team
        /// </summary>
        private void startAllWifiIperf()
        {
            //Thread.Sleep(TimeSpan.FromMilliseconds(500));
            startWiFi1Iperf();
            //Thread.Sleep(TimeSpan.FromMilliseconds(500));
            startWiFi2Iperf();
        }

        /// <summary>
        /// start process using input text line
        /// </summary>
        /// <param name="text"></param>
        private Process startIPerfProcess(string text, bool isCellularIperf = false)
        {
            var firstSpaceIndex = -1;
            var pathToExe = string.Empty;
            var arguments = string.Empty;
            string directoryName = string.Empty;
            try
            {
                // split file name and args part
                firstSpaceIndex = text.IndexOf(' ');
                pathToExe = text.Substring(0, firstSpaceIndex);
                arguments = text.Substring(firstSpaceIndex);
                directoryName = Path.GetDirectoryName(pathToExe);
            }
            catch (Exception e)
            {
                MessageBox.Show($"We ran into an error while parsing IPerf args, input command: {text}\n" +
                        $"Error Message: {e.Message}", "Error While Parsing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                Process process = new Process
                {
                    StartInfo =
                {
                    FileName = pathToExe,
                    Arguments = arguments,
                    UseShellExecute = true,
                    CreateNoWindow = false,
                    RedirectStandardOutput = false,
                    WorkingDirectory = directoryName,
                    WindowStyle = ProcessWindowStyle.Minimized
                },
                    EnableRaisingEvents = true,
                };

                Console.WriteLine($"Starting to run process = [{pathToExe}]");
                process.Start();
                if (isCellularIperf)
                    generalProcessList.Add(process);
                else
                    wifiIperfProcessList.Add(process);  // add the process for future use
                
                // David - remove this sleep time
                // change the window name only for active process
                //Thread.Sleep(TimeSpan.FromMilliseconds(10));
                if (!process.HasExited)
                {
                    IntPtr handle = process.MainWindowHandle;
                    SetWindowText(handle, $"{pathToExe}: {arguments}");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"We ran into an error while trying to run Iperf using cmd: {pathToExe}: {arguments}\n" +
                    $"Error Message: {e.Message}", "Error While trying to start Iperf", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                        
            return null;
        }   
       
        #endregion


        #region set SW RF Kill Fucntions       

        public void SetPhyRadioState(Wlan.Dot11RadioState softwareRadioState)
        {
            try {

                var resourceName = "App.Resources.SwRfKill.exe";
                string tempFolder = @"C:\MRATTemp";
                string filePath = Path.Combine(tempFolder, "SwRfKill.exe");
                Directory.CreateDirectory(tempFolder);
                // read the exe data from the project
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    byte[] byteData = StreamToBytes(stream);
                    File.WriteAllBytes(filePath, byteData); // save it to a new file in defaul location                   
                }

                string args = string.Empty;
                switch (softwareRadioState)
                {
                    case Wlan.Dot11RadioState.On:
                        args = "on";
                        break;
                    case Wlan.Dot11RadioState.Off:
                        args = "off";
                        break;
                }

                // run the cpp code
                CreateAndRunProcess(filePath, args);

                // old C# implementation
                //var phyRadioState = new Wlan.WlanPhyRadioState
                //{
                //    dwPhyIndex = 0,
                //    dot11SoftwareRadioState = softwareRadioState
                //};
                //SetInterface(Wlan.WlanIntfOpcode.RadioState, phyRadioState);
                // David - try to improve connect/disconnect time
                //Thread.Sleep(TimeSpan.FromSeconds(2));

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while trying to change SW RF state!\nMessage: [{e.Message}]");
                MessageBox.Show($"Error while trying to change SW RF state!\n" +
                        $"Error Message: {e.Message}", "Error While Parsing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SetInterface<T>(Wlan.WlanIntfOpcode opCode, T value) where T : struct
        {
            Type outputType = typeof(T).IsEnum ? Enum.GetUnderlyingType(typeof(T)) : typeof(T);
            int valueSize = Marshal.SizeOf(outputType);
            IntPtr valuePtr = Marshal.AllocHGlobal(valueSize);
            var wlanClient = new WlanClient();
            IntPtr ifaceList;
            Wlan.WlanEnumInterfaces(wlanClient.ClientHandle, IntPtr.Zero, out ifaceList);
            Wlan.WlanInterfaceInfoListHeader header =
                (Wlan.WlanInterfaceInfoListHeader)Marshal.PtrToStructure(ifaceList, typeof(Wlan.WlanInterfaceInfoListHeader));
            long listIterator = ifaceList.ToInt64() + Marshal.SizeOf(header);
            Wlan.WlanInterfaceInfo info =
                (Wlan.WlanInterfaceInfo)Marshal.PtrToStructure(new IntPtr(listIterator), typeof(Wlan.WlanInterfaceInfo));
            try
            {
                Marshal.StructureToPtr(value, valuePtr, true);

                Wlan.ThrowIfError(
                    Wlan.WlanSetInterface(wlanClient.ClientHandle, info.interfaceGuid, opCode, (uint)valueSize, valuePtr, IntPtr.Zero));
            }
            finally
            {
                Marshal.FreeHGlobal(valuePtr);
            }
        }

        /// <summary>
        /// StreamToBytes - Converts a Stream to a byte array. Eg: Get a Stream from a file,url, or open file handle.
        /// </summary>
        /// <param name="input">input is the stream we are to return as a byte array</param>
        /// <returns>byte[] The Array of bytes that represents the contents of the stream</returns>
        static byte[] StreamToBytes(Stream input)
        {

            int capacity = input.CanSeek ? (int)input.Length : 0; //Bitwise operator - If can seek, Capacity becomes Length, else becomes 0.
            using (MemoryStream output = new MemoryStream(capacity)) //Using the MemoryStream output, with the given capacity.
            {
                int readLength;
                byte[] buffer = new byte[capacity/*4096*/];  //An array of bytes
                do
                {
                    readLength = input.Read(buffer, 0, buffer.Length);   //Read the memory data, into the buffer
                    output.Write(buffer, 0, readLength); //Write the buffer to the output MemoryStream incrementally.
                }
                while (readLength != 0); //Do all this while the readLength is not 0
                return output.ToArray();  //When finished, return the finished MemoryStream object as an array.
            }

        }
        #endregion


        #region Make Window Movable

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        Point lastPoint;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        #endregion


        #region General Functions
        

        private void PrintProcessInfo(Process process)
        {
            if (!process.HasExited)
                Console.WriteLine(@"{0} | ID: {1} | Status {2} | Memory (private working set in Bytes) {3}",
                                process.ProcessName, process.Id, process.Responding, process.PrivateMemorySize64);
        }


        /// <summary>
        /// return the index of the input char in a given string
        /// </summary>
        /// <param name="s">original string</param>
        /// <param name="t">char to look for</param>
        /// <param name="n">place number</param>
        /// <returns></returns>
        public int GetNthIndex(string s, char t, int n)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// change the port number in the cmd string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="portNumber"></param>
        /// <returns></returns>
        private string replacePortNumber(string text, int portNumber)
        {
            string start = string.Empty;
            string end = string.Empty;
            string cmdWithNewPort = string.Empty;

            int spaceIndex = 0;
            int portIndex = text.IndexOf("-p");
            if (portIndex >= 0)
            {
                portIndex += 2;
                start = text.Substring(0, portIndex);
                end = text.Substring(portIndex);
                spaceIndex = GetNthIndex(end, ' ', 2) + 1;
                end = end.Substring(spaceIndex);

                cmdWithNewPort = $"{start} {portNumber.ToString()} {end}";
            }
            return cmdWithNewPort;
        }

        /// <summary>
        /// using regex to replace port number
        /// </summary>
        /// <param name="text"></param>
        /// <param name="portNumber"></param>
        /// <returns></returns>        
        private string replacePortNumber1(string text, int portNumber)
        {
            return Regex.Replace(text, @"-p \d+", $"-p {portNumber}");
        }

        #endregion
         

        /// <summary>
        /// connect to input SSID profile name using netsh command
        /// change the connect/disconnect button
        /// </summary>
        private void connectToProfile()
        {
            stopAllWifiIperfProcess();
            
            // if profile name is empty don't do anything
            if (string.IsNullOrEmpty(profileName.Text))
                return;

            string cmd = string.Empty;
            // if we want to run release/renew flow
            //if (releaseCheckBox.Checked)
            //{
                //profileButton.Text = $"{((profileConnectionFlag) ? "Release" : "Renew")}";
                //Ira for release renew
                //cmd = $"ipconfig /{((profileConnectionFlag) ? @"renew ""Wi-Fi"" & ipconfig /renew ""Wi-Fi 2""" : $"release *Fi*")}";
            //}
            //else//for connect disconnect flow
            //{
                profileButton.Text = $"{((profileConnectionFlag) ? "Disconnect" : "Connect")} Profile";
                //Ira - You were right David, the command I sent is incorrect, sorry )))
                cmd = $"netsh wlan {((profileConnectionFlag) ? $"connect name ={profileName.Text} ssid={profileName.Text}" : "disconnect")}";
            //}                                     

            CreateAndRunProcess("cmd.exe", $"/c {cmd}");

            //Ira - Need to start Ipef Again, but the command below doesn't help            
            if (profileConnectionFlag)
            {
                startAllWifiIperf();
            }

            profileConnectionFlag = !profileConnectionFlag;            
        }
      
        private void CreateAndRunProcess(string pathToExe, string arguments)
        {
            string directoryName = Path.GetDirectoryName(pathToExe);
            Process process = new Process
            {
                StartInfo =
                {
                    FileName = pathToExe,
                    Arguments = arguments,
                    UseShellExecute = true,
                    CreateNoWindow = false,
                    WorkingDirectory = directoryName,
                    WindowStyle = ProcessWindowStyle.Minimized
                },
                EnableRaisingEvents = true,
            };        

            Console.WriteLine($"Starting to run process = [{pathToExe}]");
            process.Start();
            generalProcessList.Add(process);  // add the process for future use

            // David - remove this sleep time
            // change the window name only for active process
            //Thread.Sleep(TimeSpan.FromMilliseconds(10));
            if (!process.HasExited)
            {
                IntPtr handle = process.MainWindowHandle;
                SetWindowText(handle, $"{pathToExe}: {arguments}");
            }
        }   

        /// <summary>
        /// change the lable on the button based on the checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void releaseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (releaseCheckBox.Checked)
            {
                profileButton.Text = $"{((profileConnectionFlag) ? "Release" : "Renew")}";
            }
            else//for connect disconnect flow
            {
                profileButton.Text = $"{((profileConnectionFlag) ? "Disconnect" : "Connect")} Profile";
            }
        }


        #region Safely Kill Iperf Process

        internal const int CTRL_C_EVENT = 0;
        [DllImport("kernel32.dll")]
        internal static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(uint dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool FreeConsole();
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
        // Delegate type to be used as the Handler Routine for SCCH
        delegate Boolean ConsoleCtrlDelegate(uint CtrlType);


        /// <summary>
        /// sending ctrl+c to input process
        /// based on this answer: https://stackoverflow.com/questions/283128/how-do-i-send-ctrlc-to-a-process-in-c
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool SafelyKillProcess(Process p)
        {
            if (AttachConsole((uint)p.Id))
            {
                SetConsoleCtrlHandler(null, true);
                try
                {
                    if (!GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0))
                        return false;
                    p.WaitForExit();
                }
                finally
                {
                    SetConsoleCtrlHandler(null, false);
                    FreeConsole();
                }
                return true;
            }
            return false;
        }

        private void stopAllWifiIperfProcess()
        {
            //stop all WiFi process using ctrl+c
            foreach (var process in wifiIperfProcessList)
            {
                SafelyKillProcess(process);
                // wait for stabiliszation
                
            }
            //David -move outside the loop to improve time
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
        }


        #endregion
    }
}

