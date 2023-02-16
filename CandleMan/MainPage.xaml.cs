using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CandleMan
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
            UpdateCommand = new AsyncRelayCommand(UpdateAsync, CanExecuteUpdateAsyncCommand);

        }

        private List<CandleModel> Candlles = new List<CandleModel>();

        public double Val { get; set; } = 100;
        public string Timing { get; set; } = "1h";
        public string ErrorCode { get; set; } = "";
        public List<List<double>> Prices { get; set; } = new List<List<double>>();

        private void UpdateGraph()
        {
            MainCanvas.Children.Clear();
            if (!Prices.Any()) return;

            double maxvalue = Prices.Max(x => x[2]);
            double minvalue = Prices.Min(x => x[3]);
            rectd.Width = 10;
            rectd.Height = (maxvalue * Val - minvalue*Val)* 1.1;
            for (int en = 0; en < Prices.Count; en++)
            {

                CandleModel modell = new CandleModel();
                modell.Mass = Val;
                for (int prVal = 0; prVal < Prices[en].Count; prVal++)
                {
                    if (prVal == 1)
                    {
                        modell.OpenPrice = Prices[en][prVal];
                    }
                    else if (prVal == 2)
                    {
                        modell.HighPrice = Prices[en][prVal];
                    }
                    else if (prVal == 3)
                    {
                        modell.LowPrice = Prices[en][prVal];
                    }
                    else if (prVal == 4)
                    {
                        modell.ClosePrice = Prices[en][prVal];
                    }
                }

                rectd.Width += 32;
                CandleStick stick = new CandleStick();
                stick.CandelModel = modell;
                stick.Initi();
                MainCanvas.Children.Add(stick);
                Canvas.SetLeft(stick, en * 30);
                Canvas.SetTop(stick, (maxvalue - modell.HighPrice) * Val);
            }
            Bindings.Update();
        }

        public void UpdateSticks()
        {
            if (!Prices.Any()) return; 
            double height = Prices.Max(x => x[2]) * 1.25;
            rectd.Height = height * Val;
            foreach (CandleStick stick in MainCanvas.Children)
            {
                stick.CandelModel.Mass = Val;
                stick.Initi();
                Canvas.SetTop(stick, (rectd.Height - stick.CandelModel.HighPrice) * Val);
            }
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            await GetData();


            var candle1 = new CandleStick()
            {
            };

        }

        public double MaxVal { get; set; } = 120;


        public string Pair { get; set; } = "BNBUSDT";

        private async Task GetData()
        {
            try
            {
                RestClient clci = new RestClient();
                RestRequest req =
                    new RestRequest($"https://api4.binance.com/api/v3/klines?symbol={Pair}&interval={Timing}");
                RestResponse RESP = await clci.GetAsync(req);
                if (RESP.IsSuccessful)
                {
                    JsonReader read = new JsonTextReader(new StringReader(RESP.Content));
                    var serial = JsonSerializer.Create();
                    Prices = serial.Deserialize<List<List<double>>>(read);
                    int pos = 0;
                    double maxvalue = Prices.Max(x => x[2]);
                    MaxVal = 50000 / maxvalue;
                    UpdateGraph();
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
                Bindings.Update();
                PopaGrid.Visibility = Visibility.Visible;
                await Task.Delay(2500);
                PopaGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <inheritdoc cref="Update"/>
        public AsyncRelayCommand UpdateCommand { get; }

        /// <summary>
        /// Может ли исполнить команду <see cref="UpdateCommand"/>.
        /// </summary>
        private bool CanExecuteUpdateAsyncCommand() => true;

        /// <summary>
        /// Обнов
        /// </summary>
        private async Task UpdateAsync()
        {
            await GetData();

        }


        private void RangeBase_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
        }

        private void MySlide_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            UpdateGraph();
        }

        private void MySlide_OnPointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            UpdateGraph();
        }
    }
}
