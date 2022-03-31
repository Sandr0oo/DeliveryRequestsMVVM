using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RequestDelivery
{
    public class DelivaryRequestsViewModel : INotifyPropertyChanged
    {
        private DelegateCommand _addRequestCommand;
        private DelegateCommand _deleteRequestCommand;
        private DelegateCommand _changeCommand;

        System.Reflection.PropertyInfo[] properties;

        private DeliveryRequest _selectedDeliveryRequest;
        public DeliveryRequest SelectedDeliveryRequest
        {
            get { return _selectedDeliveryRequest; }
            set
            {
                _selectedDeliveryRequest = value;
                OnPropertyChanged("SelectedDeliveryRequest");
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                SearchedRequests = String.IsNullOrWhiteSpace(value) ? RequestsList : new ObservableCollection<DeliveryRequest>(RequestsList.Where(request => properties
                    .Any(prop => ((prop.GetValue(request, null) == null) ? "" : prop.GetValue(request, null).ToString().ToLower()).IndexOf(value.ToLower()) != -1)));
                OnPropertyChanged("SearchText");
            }
        }

        private ObservableCollection<DeliveryRequest> _searchedRequests;

        public ObservableCollection<DeliveryRequest> SearchedRequests
        {
            get { return _searchedRequests; }
            set
            {
                _searchedRequests = value;
                OnPropertyChanged("SearchedRequests");
            }
        }

        public ObservableCollection<DeliveryRequest> RequestsList = new ObservableCollection<DeliveryRequest>();

        public DelivaryRequestsViewModel()
        {
            Utility.EventAggregator.GetEvent<RequestChangedEvent>().Subscribe(RequestChanged);
            properties = typeof(DeliveryRequest).GetProperties();
            using (var db = new ApplicationDbContext())
            {
                RequestsList = new ObservableCollection<DeliveryRequest>(db.DeliveryRequests.Select(r => r));
            }
            SearchedRequests = RequestsList;
        }
        public DelegateCommand AddCommand => _addRequestCommand ?? new DelegateCommand(obj =>
        {
            using (var db = new ApplicationDbContext())
            {
                DeliveryRequest newRequest = new DeliveryRequest();
                db.DeliveryRequests.Add(newRequest);
                db.SaveChanges();
                RequestsList.Add(newRequest);
                SelectedDeliveryRequest = newRequest;
            }
        });

        public DelegateCommand DeleteCommand => _deleteRequestCommand ?? new DelegateCommand(obj =>
        {
            using (var db = new ApplicationDbContext())
            {
                if (RequestsList == null || SelectedDeliveryRequest == null)
                    return;
                db.DeliveryRequests.Remove(SelectedDeliveryRequest);
                RequestsList.Remove(SelectedDeliveryRequest);
                db.SaveChanges();
            }
        },
            obj => RequestsList.Count > 0);

        public DelegateCommand ChangeCommand => _changeCommand ?? new DelegateCommand(obj =>
        {
            if (SelectedDeliveryRequest != null)
            {
                ChangeRequest changeRequestWindow = new ChangeRequest(new DeliveryRequest(SelectedDeliveryRequest));
                changeRequestWindow.ShowDialog();

            }
        });
        
        private void RequestChanged(DeliveryRequest recivedRequest)
        {
            if (recivedRequest != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    var requestForChange = db.DeliveryRequests.SingleOrDefault(r => r.Id == recivedRequest.Id);
                    if (requestForChange != null)
                    {

                        db.Entry<DeliveryRequest>(requestForChange).State = EntityState.Detached;
                        db.Entry<DeliveryRequest>(recivedRequest).State = EntityState.Modified;
                        db.SaveChanges();

                        RequestsList.Remove(RequestsList.SingleOrDefault(r => r.Id == recivedRequest.Id));
                        RequestsList.Add(recivedRequest);
                        RequestsList = new ObservableCollection<DeliveryRequest>(RequestsList.OrderBy(r => r.Id));
                        SearchedRequests = RequestsList;
                    }
                }
            }
            SelectedDeliveryRequest = recivedRequest;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
