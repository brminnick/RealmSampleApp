using System;
using Xamarin.Forms;
namespace RealmSampleApp
{
	public class ContactsListPage : ContentPage
	{
		#region Constant Fields
		readonly ListView _contactsListView;
		#endregion

		#region Constructors
		public ContactsListPage()
		{
			var viewModel = new ContactsListViewModel();
			BindingContext = viewModel;

			var addContactButton = new ToolbarItem
			{
				Text = "+"
			};
			addContactButton.Clicked += HandleAddContactButtonClicked;
			ToolbarItems.Add(addContactButton);

			_contactsListView = new ListView(ListViewCachingStrategy.RecycleElement)
			{
				ItemTemplate = new DataTemplate(typeof(ContactsListViewCell))
			};
			_contactsListView.SetBinding<ContactsListViewModel>(ListView.ItemsSourceProperty, vm => vm.AllContactsDataList);

			Title = "Contacts";

			Content = _contactsListView;
		}

	#endregion

		#region Methods
		protected override void OnAppearing()
		{
			base.OnAppearing();

			_contactsListView.ItemSelected += HandleItemSelected;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			_contactsListView.ItemSelected -= HandleItemSelected;
		}

		async void HandleItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var listView = sender as ListView;
			var selectedContactModel = e?.SelectedItem as ContactModel;

			await Navigation.PushAsync(new ContactsDetailPage(selectedContactModel));

			listView.SelectedItem = null;
		}

		async void HandleAddContactButtonClicked(object sender, EventArgs e)
		{
			var newContact = new ContactModel();
			RealmDatabase.AddContact(newContact);

			await Navigation.PushAsync(new ContactsDetailPage(newContact));
		}
		#endregion
	}
}
