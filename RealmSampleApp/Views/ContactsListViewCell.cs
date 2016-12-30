using System;
using Xamarin.Forms;
namespace RealmSampleApp
{
	public class ContactsListViewCell : ViewCell
	{
		Label _nameValueLabel;

		public ContactsListViewCell()
		{
			var nameTextLabel = new Label
			{
				FontAttributes = FontAttributes.Italic,
				Margin = new Thickness(5, 0, 0, 0),
				FontSize = 15,
				Text = "Name"
			};

			_nameValueLabel = new Label
			{
				FontAttributes = FontAttributes.Bold,
				Margin = new Thickness(5, 0, 0, 0)
			};

			var contactGrid = new Grid
			{
				RowDefinitions = {
					new RowDefinition{ Height = new GridLength (1, GridUnitType.Star) },
					new RowDefinition{ Height = new GridLength (1, GridUnitType.Star) }
				},
				ColumnDefinitions = {
					new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) }
				}
			};
			contactGrid.Children.Add(nameTextLabel, 0, 0);
			contactGrid.Children.Add(_nameValueLabel, 0, 1);

			View = contactGrid;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var item = BindingContext as ContactModel;

			_nameValueLabel.Text = item.FullName;
		}
	}
}
