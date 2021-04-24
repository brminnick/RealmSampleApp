using Xamarin.Forms;

namespace RealmSampleApp
{
    public class ContactsListTextCell : TextCell
    {
        public ContactsListTextCell()
        {
            TextColor = Color.FromHex("1B2A38");
            DetailColor = Color.FromHex("2B3E50");
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            Text = string.Empty;
            Detail = string.Empty;

            var item = (ContactModel)BindingContext;

            Text = item.FullName;
            Detail = item.PhoneNumber;
        }
    }
}
