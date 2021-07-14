using System.Windows.Forms;

namespace App
{
    public static class ThreadHelperClass
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);
        delegate void SetChartCallback(Form form, System.Windows.Forms.DataVisualization.Charting.Chart chart, double[] dataList);

        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }
        
        public static void addDataPoint(Form form, System.Windows.Forms.DataVisualization.Charting.Chart chart, double[] dataList)
        {
            if (chart.InvokeRequired)
            {// this prevents the invoke loop

                SetChartCallback d = new SetChartCallback(addDataPoint);
                form.Invoke(d, new object[] { form, chart, dataList });// invoke call for _THIS_ function to execute on the UI thread
            }
            else
            {
                //function logic to actually add the datapoint goes here
                //chart.Series[0].Points.Clear();

                //foreach(var item in dataList)                
                //{
                //    chart.Series[0].Points.AddY(item);
                //}

                chart.Series["Series1"].Points.Clear();

                for (int i = 0; i < dataList.Length - 1; ++i)
                {
                    chart.Series["Series1"].Points.AddY(dataList[i]);
                }
            }
        }

    }
}