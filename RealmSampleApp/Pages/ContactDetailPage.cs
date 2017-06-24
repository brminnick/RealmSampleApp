using System;
using Xamarin.Forms;

namespace RealmSampleApp
{
    public class ContactDetailPage : BaseContentPage<ContactDetailViewModel>
    {
        #region Constant Fields
        readonly ContactModel _contactModel;
        ToolbarItem _saveToobarItem, _cancelToolbarItem;
        #endregion

        #region Constructors
        public ContactDetailPage(ContactModel selectedContact, bool canEdit)
        {
            _contactModel = selectedContact;
            BindingContext = _contactModel;

            var firstNameTextLabel = new ContactDetailLabel { Text = "First Name" };

            var firstNameDataEntry = new ContactDetailEntry(canEdit);
            firstNameDataEntry.SetBinding(Entry.TextProperty, nameof(_contactModel.FirstName));

            var lastNameTextLabel = new ContactDetailLabel { Text = "Last Name" };

            var lastNameDataEntry = new ContactDetailEntry(canEdit);
            lastNameDataEntry.SetBinding(Entry.TextProperty, nameof(_contactModel.LastName));

            var phoneNumberTextLabel = new ContactDetailLabel { Text = "Phone Number" };

            var phoneNumberDataEntry = new ContactDetailEntry(canEdit);
            phoneNumberDataEntry.SetBinding(Entry.TextProperty, nameof(_contactModel.PhoneNumber));

            _saveToobarItem = new ToolbarItem { Text = "Save", Priority = 0 };
            _cancelToolbarItem = new ToolbarItem { Text = "Cancel", Priority = 1 };

            switch (canEdit)
            {
                case true:
                    ToolbarItems.Add(_saveToobarItem);
                    ToolbarItems.Add(_cancelToolbarItem);
                    break;
            }

            Title = "Contact Details";

            Padding = new Thickness(20, 0, 20, 0);

            Content = new StackLayout
            {
                Margin = new Thickness(0, 10, 0, 0),
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
        #endregion

        #region Methods
        protected override void SubscribeEventHandlers()
        {
            _cancelToolbarItem.Clicked += HandleCancelToolBarItemClicked;
            _saveToobarItem.Clicked += HandleSaveToolbarItemClicked;
        }

        protected override void UnsubscribeEventHandlers()
        {
            _cancelToolbarItem.Clicked -= HandleCancelToolBarItemClicked;
            _saveToobarItem.Clicked -= HandleSaveToolbarItemClicked;
        }

        async void HandleSaveToolbarItemClicked(object sender, EventArgs e)
        {
            await ContactRealm.SaveContact(_contactModel);
            PopPage();
        }

        void HandleCancelToolBarItemClicked(object sender, EventArgs e) => PopPage();
        void PopPage() => Device.BeginInvokeOnMainThread(async () => await Navigation.PopModalAsync());
        #endregion

        #region Classes
        class ContactDetailEntry : Entry
        {
            public ContactDetailEntry(bool canEdit)
            {
                IsEnabled = canEdit;
				TextColor = Color.FromHex("2B3E50");
            }
        }

        class ContactDetailLabel : Label
        {
            public ContactDetailLabel() => TextColor = Color.FromHex("1B2A38");
        }
        #endregion
    }
}
