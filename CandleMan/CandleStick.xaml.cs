using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CandleMan
{
    public sealed partial class CandleStick : UserControl
    {
        public CandleStick()
        {
            this.InitializeComponent();
            Loaded += CandleStick_Loaded;
        }


        private void CandleStick_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    

        public void Initi()
        {
            double shadowHeight = (CandelModel.HighPrice - CandelModel.LowPrice) * CandelModel.Mass;
            double bodyHeight = (Math.Abs(CandelModel.OpenPrice - CandelModel.ClosePrice)) *CandelModel.Mass;
            double verticalOffset = CandelModel.OpenPrice < CandelModel.ClosePrice ? CandelModel.HighPrice - CandelModel.ClosePrice : CandelModel.HighPrice - CandelModel.OpenPrice;
            FillColor = CandelModel.OpenPrice < CandelModel.ClosePrice ? Color.FromArgb(200, 0, 255, 24) : Color.FromArgb(200, 255, 0, 25);
            ShadowRect.Height = shadowHeight;
            BodyRect.Height = Math.Abs(bodyHeight);
            BodyTrans.TranslateY = verticalOffset * CandelModel.Mass;
            Bindings.Update();
        }

        public CandleModel CandelModel { get; set; }
        public Color FillColor { get; set; }
        
        
    }
}
