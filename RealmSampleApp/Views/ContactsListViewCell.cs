using Xamarin.Forms;

namespace RealmSampleApp
{
    public class ContactsListViewCell : TextCell
    {
        #region Methods
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            Text = string.Empty;
            Detail = string.Empty;

            var item = BindingContext as ContactModel;

            Text = item.FullName;
            Detail = item.PhoneNumber;
        }
        #endregion
    }
}
