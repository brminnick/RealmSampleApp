using System;
using Xamarin.Forms;
namespace RealmSampleApp
{
	public class ContactsListViewCell : TextCell
	{
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var item = BindingContext as ContactModel;

			Device.BeginInvokeOnMainThread(() =>
			{
				Text = "Name";
				Detail = $"{item.FirstName} {item.LastName}";
			});
		}
	}
}
