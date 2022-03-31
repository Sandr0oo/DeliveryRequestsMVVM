using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RequestDelivery
{
	public class ChangeDeliveryRequestViewModel : INotifyPropertyChanged
	{
		private DelegateCommand _saveRequestCommand;
		private DelegateCommand _cancelCommand;

		private DeliveryRequest _request;
		public DeliveryRequest Request
		{
			get { return _request; }
			set
			{
				_request = value;
				OnPropertyChanged("Request");
			}
		}

		public ChangeDeliveryRequestViewModel(DeliveryRequest requestForChange)
		{
			Request = requestForChange;
		}

		public ChangeDeliveryRequestViewModel()
		{

		}

        #region Commands
        public DelegateCommand SaveCommand => _saveRequestCommand ?? new DelegateCommand(obj =>
		{
		   Utility.EventAggregator.GetEvent<RequestChangedEvent>().Publish(Request);
		});

		public DelegateCommand CacncelCommand => _cancelCommand ?? new DelegateCommand(obj => 
		{
			Utility.EventAggregator.GetEvent<RequestChangedEvent>().Publish(null);
		});
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
