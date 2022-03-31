using EnumsNET;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RequestDelivery
{
    public class DeliveryRequest : INotifyPropertyChanged
    {
        public long Id { get; set; }

        public enum Status
        {
            [Description("Принято")]
            Received,

            [Description("Передано в производство")]
            TransferredToProduction,

            [Description("Выполнено")]
            Completed,

            [Description("Отменена")]
            Canceled
        }

        private string _receivingAddress;

        public string ReceivingAddress
        {
            get { return _receivingAddress; }
            set
            {
                _receivingAddress = value;
                OnPropertyChanged("ReceivingAddress");
            }
        }

        private string _shippingAddress;

        public string ShippingAddress
        {
            get { return _shippingAddress; }
            set
            {
                _shippingAddress = value;
                OnPropertyChanged("ShippingAddress");
            }


        }

        private double _weight;

        public double Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }

        private double _length;

        public double Length
        {
            get { return _length; }
            set
            {
                _length = value;
                OnPropertyChanged("Length");
                OnPropertyChanged("Volume");
            }
        }

        private double _width;

        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
                OnPropertyChanged("Volume");
            }
        }

        private double _height;

        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
                OnPropertyChanged("Volume");
            }
        }

        private double _volume;

        public double Volume
        {
            get { return Length * Width * Height; }
            set
            {
                _volume = value;
                OnPropertyChanged("Volume");
            }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        private string _contactDetails;

        public string ContactDetails
        {
            get { return _contactDetails; }
            set
            {
                _contactDetails = value;
                OnPropertyChanged("ContactDetails");
            }
        }

        private string _shipmentCity;

        public string ShipmentCity
        {
            get { return _shipmentCity; }
            set
            {
                _shipmentCity = value;
                OnPropertyChanged("ShipmentCity");
            }
        }

        private string _cityOfDelivery;

        public string CityOfDelivery
        {
            get { return _cityOfDelivery; }
            set
            {
                _cityOfDelivery = value;
                OnPropertyChanged("CityOfDelivery");
            }
        }

        private Status _deliveryStatus;

        public Status DeliveryStatus
        {
            get
            {
                DeliveryStatusForSearch = _deliveryStatus.AsString(EnumFormat.Description);
                return _deliveryStatus;
            }
            set
            {
                _deliveryStatus = value;
                DeliveryStatusForSearch = value.AsString(EnumFormat.Description);
                OnPropertyChanged("DeliveryStatus");
            }
        }

        private string _deliveryStatusForSearch;

        public string DeliveryStatusForSearch
        {
            get { return _deliveryStatusForSearch; }
            set
            {
                _deliveryStatusForSearch = value;
                OnPropertyChanged("DeliveryStatusForSearch");
            }
        }

        public DeliveryRequest(DeliveryRequest request)
        {
            Id = request.Id;
            ReceivingAddress = request.ReceivingAddress;
            ShippingAddress = request.ShippingAddress;
            Weight = request.Weight;
            Length = request.Length;
            Width = request.Width;
            Height = request.Height;
            Volume = request.Volume;
            Date = request.Date;
            ContactDetails = request.ContactDetails;
            ShipmentCity = request.CityOfDelivery;
            DeliveryStatus = request.DeliveryStatus;
        }

        public DeliveryRequest()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
