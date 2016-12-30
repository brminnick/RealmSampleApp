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

			_contactsListView = new ListView
			{
				ItemTemplate = new DataTemplate(typeof(ContactsListViewCell))
			};
			_contactsListView.SetBinding<ContactsListViewModel>(ListView.ItemsSourceProperty, vm => vm.ViewableContactsDataList);

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
			var item = e?.SelectedItem as ContactModel;

			await Navigation.PushAsync(new ContactsDetailPage(item));
		}
		#endregion
	}
}
