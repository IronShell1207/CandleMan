using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CandleMan
{
    internal class vm : ObservableObject
    {
        public Canvas Canvas { get; set; }

        public vm()
        {
        }
    }
}
