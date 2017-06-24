using Xamarin.Forms;

namespace RealmSampleApp
{
    public class ContactsListTextCell : TextCell
    {
        #region Constructors
        public ContactsListTextCell()
        {
            TextColor = Color.FromHex("1B2A38");
            DetailColor = Color.FromHex("2B3E50");
        }
        #endregion

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
