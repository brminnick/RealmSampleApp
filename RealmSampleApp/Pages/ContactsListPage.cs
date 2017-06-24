using System;
using Xamarin.Forms;
namespace RealmSampleApp
{
    public class ContactsListPage : BaseContentPage<ContactsListViewModel>
    {
        #region Constant Fields
        readonly ListView _contactsListView;
        readonly ToolbarItem _addContactButton;
        #endregion

        #region Constructors
        public ContactsListPage()
        {
            _addContactButton = new ToolbarItem { Text = "+" };
            ToolbarItems.Add(_addContactButton);

            _contactsListView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemTemplate = new DataTemplate(typeof(ContactsListTextCell)),
                IsPullToRefreshEnabled = true,
                BackgroundColor = Color.Transparent
            };
            _contactsListView.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.AllContactsList));
            _contactsListView.SetBinding(ListView.RefreshCommandProperty, nameof(ViewModel.RefreshCommand));

            Title = "Contacts";

            Content = _contactsListView;
        }
        #endregion

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(_contactsListView.BeginRefresh);
        }

        protected override void SubscribeEventHandlers()
        {
            _contactsListView.ItemSelected += HandleItemSelected;
            _addContactButton.Clicked += HandleAddContactButtonClicked;
            ViewModel.PullToRefreshCompleted += HandlePullToRefreshCompleted;
        }

        protected override void UnsubscribeEventHandlers()
        {
            _contactsListView.ItemSelected -= HandleItemSelected;
            _addContactButton.Clicked -= HandleAddContactButtonClicked;
            ViewModel.PullToRefreshCompleted -= HandlePullToRefreshCompleted;
        }

        void HandleItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedContactModel = e?.SelectedItem as ContactModel;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new ContactDetailPage(selectedContactModel, false));
                listView.SelectedItem = null;
            });
        }

        void HandleAddContactButtonClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
               await Navigation.PushModalAsync(new BaseNavigationPage(new ContactDetailPage(new ContactModel(), true))));
        }

        void HandlePullToRefreshCompleted(object sender, EventArgs e) =>
            Device.BeginInvokeOnMainThread(_contactsListView.EndRefresh);
        #endregion
    }
}
