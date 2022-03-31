using System.Windows;

namespace RequestDelivery
{
    /// <summary>
    /// Логика взаимодействия для ChangeRequest.xaml
    /// </summary>
    public partial class ChangeRequest : Window
    {
        public ChangeRequest(DeliveryRequest deliveryRequest)
        {
            InitializeComponent();
            DataContext = new ChangeDeliveryRequestViewModel(deliveryRequest);
            Utility.EventAggregator.GetEvent<RequestChangedEvent>().Subscribe(obj => 
            {
                this.Close();
            });
        }
    }
}
