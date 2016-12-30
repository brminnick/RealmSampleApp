using System;
using Xamarin.Forms;
namespace RealmSampleApp
{
	public class ContactsDetailPage : ContentPage
	{
		public ContactsDetailPage(ContactModel selectedContact)
		{
			BindingContext = new ContactDetailViewModel(selectedContact);

			var firstNameTextLabel = new Label
			{
				Text = "First Name"
			};

			var firstNameDataEntry = new Entry();
			firstNameDataEntry.SetBinding<ContactDetailViewModel>(Entry.TextProperty, vm => vm.Contact.FirstName);

			var lastNameTextLabel = new Label
			{
				Text = "Last Name"
			};

			var lastNameDataEntry = new Entry();
			lastNameDataEntry.SetBinding<ContactDetailViewModel>(Entry.TextProperty, vm => vm.Contact.LastName);

			var phoneNumberTextLabel = new Label
			{
				Text = "Phone Number"
			};

			var phoneNumberDataEntry = new Entry();
			phoneNumberDataEntry.SetBinding<ContactDetailViewModel>(Entry.TextProperty, vm => vm.Contact.PhoneNumber);

			Padding = new Thickness(20, 0, 20, 0);

			Content = new StackLayout
			{
				Margin = new Thickness(0, 5, 0, 0),
				Children = {
					firstNameTextLabel,
					firstNameDataEntry,
					lastNameTextLabel,
					lastNameDataEntry,
					phoneNumberTextLabel,
					phoneNumberDataEntry
				}
			};
		}
	}
}
