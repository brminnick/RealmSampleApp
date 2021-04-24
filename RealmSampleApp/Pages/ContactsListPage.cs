using System;
using RealmSampleApp.Constants;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RealmSampleApp
{
    class ContactsListPage : BaseContentPage<ContactsListViewModel>
    {
        public ContactsListPage()
        {
            Title = PageTitles.ContactsListPage;

            Content = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemTemplate = new DataTemplate(typeof(ContactsListTextCell)),
                IsPullToRefreshEnabled = true,
                BackgroundColor = Color.Transparent
            }.Bind(ListView.ItemsSourceProperty, nameof(ViewModel.AllContactsList))
             .Bind(ListView.RefreshCommandProperty, nameof(ViewModel.RefreshCommand))
             .Invoke(list => list.ItemSelected += HandleItemSelected);

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "+",
                AutomationId = AutomationIdConstants.AddContactButon
            }.Invoke(addContactButton => addContactButton.Clicked += HandleAddContactButtonClicked));

            ViewModel.PullToRefreshCompleted += HandlePullToRefreshCompleted;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Content is ListView listView)
                MainThread.BeginInvokeOnMainThread(listView.BeginRefresh);
        }

        async void HandleItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.SelectedItem = null;

            if (e?.SelectedItem is ContactModel selectedContactModel)
                await Navigation.PushAsync(new ContactDetailPage(selectedContactModel, false));
        }

        async void HandleAddContactButtonClicked(object sender, EventArgs e) =>
            await Navigation.PushModalAsync(new BaseNavigationPage(new ContactDetailPage(new ContactModel(), true)));

        async void HandlePullToRefreshCompleted(object sender, EventArgs e)
        {
            if (Content is ListView listView)
                await MainThread.InvokeOnMainThreadAsync(listView.EndRefresh);
        }
    }
}
