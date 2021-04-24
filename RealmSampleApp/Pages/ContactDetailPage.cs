using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using RealmSampleApp.Constants;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace RealmSampleApp
{
    class ContactDetailPage : ContentPage
    {
        readonly ContactModel _contactModel;

        public ContactDetailPage(ContactModel selectedContact, bool canEdit)
        {
            _contactModel = selectedContact;
            BindingContext = _contactModel;

            Title = PageTitles.ContactDetailsPage;

            Padding = new Thickness(20, 0, 20, 0);

            Content = new StackLayout
            {
                Children =
                {
                    new ContactDetailLabel { Text = "First Name" },

                    new ContactDetailEntry(canEdit)
                    {
                        ReturnType = ReturnType.Next,
                        AutomationId = AutomationIdConstants.FirstNameEntry
                    }.Bind(Entry.TextProperty, nameof(_contactModel.FirstName)),

                    new ContactDetailLabel { Text = "Last Name" },

                    new ContactDetailEntry(canEdit)
                    {
                        ReturnType = ReturnType.Next,
                        AutomationId = AutomationIdConstants.LastNameEntry
                    }.Bind(Entry.TextProperty, nameof(_contactModel.LastName)),

                    new ContactDetailLabel { Text = "Phone Number" },

                    new ContactDetailEntry(canEdit)
                    {
                        ReturnType = ReturnType.Go,
                        ReturnCommand = new AsyncCommand(() => SaveContactAndPopPage()),
                        AutomationId = AutomationIdConstants.PhoneNumberEntry
                    }.Bind(Entry.TextProperty, nameof(_contactModel.PhoneNumber))
                }
            }.Margins(0, 10, 0, 0);

            if (canEdit)
            {
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Save",
                    Priority = 0,
                    AutomationId = AutomationIdConstants.SaveContactButton
                }.Invoke(saveButtom => saveButtom.Clicked += HandleSaveToolbarItemClicked));

                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Cancel",
                    Priority = 1,
                    AutomationId = AutomationIdConstants.CancelContactButton
                }.Invoke(cancelButton => cancelButton.Clicked += HandleCancelToolBarItemClicked));
            }
        }

        async Task SaveContactAndPopPage()
        {
            await ContactRealm.SaveContact(_contactModel);
            PopPage();
        }

        async void HandleSaveToolbarItemClicked(object sender, EventArgs e) =>
            await SaveContactAndPopPage();

        void HandleCancelToolBarItemClicked(object sender, EventArgs e) => PopPage();
        void PopPage() => Device.BeginInvokeOnMainThread(async () => await Navigation.PopModalAsync());

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
    }
}
