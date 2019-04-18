using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HYC.LAN;
using System.Windows.Forms;
using GeneralComponent;

namespace TestDisplayBrightness
{
  public   class Displaymonitor
    {

        public bool Lastcolor()
        {
           //ChangePicID = 0;
        }
        H6 h6 = new H6();
        //int ChangePicID = 0;
        
       public void Onchanged(bool ChangeISchangeLevel, int ChangePicID)
        {
            if (ChangeISchangeLevel)
            {
                h6.LoadPattern(ChangePicID);
               
            }
        }
          Color cl;
        public bool ROI(string filepath)
        {
          
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
