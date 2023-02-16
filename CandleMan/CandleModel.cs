using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CandleMan
{
    public class CandleModel : ObservableObject
    {
        /// <inheritdoc cref="OpenPrice"/>
        private double _openPrice;

        /// <summary>
        /// Цена открытия.
        /// </summary>
        public double OpenPrice
        {
            get => _openPrice;
            set => SetProperty(ref _openPrice, value);
        }

        /// <inheritdoc cref="ClosePrice"/>
        private double _closePrice;

        /// <summary>
        /// Цена закрыти
        /// </summary>
        public double ClosePrice
        {
            get => _closePrice;
            set => SetProperty(ref _closePrice, value);
        }

        /// <inheritdoc cref="HighPrice"/>
        private double _highPrice;

        /// <summary>
        /// Высшая цена.
        /// </summary>
        public double HighPrice
        {
            get => _highPrice;
            set => SetProperty(ref _highPrice, value);
        }

        /// <inheritdoc cref="LowPrice"/>
        private double _lowPrice;

        /// <summary>
        /// Нисшая цена.
        /// </summary>
        public double LowPrice
        {
            get => _lowPrice;
            set => SetProperty(ref _lowPrice, value);
        }

        public CandleModel(double openPrice, double closePrice, double highPrice, double lowPrice)
        {
            OpenPrice = openPrice;

            ClosePrice = closePrice;

            HighPrice = highPrice;

            LowPrice = lowPrice;

        }

        /// <inheritdoc cref="Mass"/>
        private double _mass =100;

        /// <summary>
        /// Масштаб
        /// </summary>
        public double Mass
        {
            get => _mass;
            set => SetProperty(ref _mass, value);
        }   

        public CandleModel()
        {

        }
    }

}
