using GeneralComponent;
using HYC.LAN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestDisplayBrightness
{
    //定义一个委托，可以看作是对象的一种新类型
    public delegate void delegateRun(bool ChangeISchangeLevel, int ChangePicID);
    public partial class Form1 : Form
    {
        //声明一个事件，事件是委托的一种特殊形式，带封装
        public event delegateRun eventRun;
        public Form1()
        {
            InitializeComponent();
            eventRun += dism.Onchanged;
        }
        Color cl;
        bool ChangeISchangeLevel = false;
        int ChangePicID = 0;
        Dictionary<int[], int> openWith = new Dictionary<int[], int>();
        private void button1_Click(object sender, EventArgs e)
        {
            bool IsConfig = Configure(System.Environment.CurrentDirectory + "//ImgeSize.xml");
            Bitmap bit = new Bitmap(_Weight, _Height);//实例化一个和窗体一样大的bitmap
            if (IsConfig)
            {
                Thread home = new Thread(
                    () => {
                        while (true)
                        {
                            Graphics g = Graphics.FromImage(bit);
                            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
                            g.CopyFromScreen(iActulaWidthCenter - (_Weight / 2), iActulaHeightCenter - (_Height / 2), DestinationX, DestinationY, new Size(_Weight, _Height));//保存整个窗体为图片  
                            cl = bit.GetPixel(1, 1);
                            int R = cl.R;
                            int G = cl.G;
                            int B = cl.B;
                            int[] rgb = { R,G,B};
                            openWith.Add(rgb , ChangePicID);








                            eventRun(ChangeISchangeLevel,  ChangePicID); 
                            Thread.Sleep(2);
                        }
                    });
                home.Start();
            }
            //dism.ROI(System.Environment.CurrentDirectory + "//ImgeSize.xml");
        }

        Displaymonitor dism = new Displaymonitor();
        H6 h6;
        private void Form1_Load(object sender, EventArgs e)
        {          
            h6= new H6();
            bool Connect=h6.Connect("192.168.168.100");
            if (!Connect)
            {
                MessageBox.Show("Connection failed. Please check!");
            }
          bool PowerOn= h6.PowerOn();
            if (!PowerOn)
            {
                MessageBox.Show("Power on failed, please check!");
            }
        }

        public bool Configure(string fileName)
        {

            try
            {
                XmlHelper xmlHelper = new XmlHelper(fileName);
                SourceX = Convert.ToInt32(xmlHelper.GetValue("/ImageSource/Source/@SourceX"));
                SourceY = Convert.ToInt32(xmlHelper.GetValue("/ImageSource/Source/@SourceY"));
                DestinationX = Convert.ToInt32(xmlHelper.GetValue("/ImageSource/Destination/@DestinationX"));
                DestinationY = Convert.ToInt32(xmlHelper.GetValue("/ImageSource/Destination/@DestinationY"));
                _Weight = Convert.ToInt32(xmlHelper.GetValue("/ImageSource/Size/@Weight"));
                _Height = Convert.ToInt32(xmlHelper.GetValue("/ImageSource/Size/@Height"));
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        int SourceX;
        int SourceY;
        int DestinationX;
        int DestinationY;
        int _Weight;
        int _Height;
        int iActulaWidthCenter = Screen.PrimaryScreen.Bounds.Width / 2;
        int iActulaHeightCenter = Screen.PrimaryScreen.Bounds.Height / 2;
    }
}
