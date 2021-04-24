using System;
using RealmSampleApp.Constants;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace RealmSampleApp.UITests
{
    class ContactDetailsPage : BasePage
    {
        readonly Query _firstNameEntry, _lastNameEntry, _phoneNumberEntry,
            _saveButton, _cancelButton;

        public ContactDetailsPage(IApp app) : base(app, PageTitles.ContactDetailsPage)
        {
            _firstNameEntry = x => x.Marked(AutomationIdConstants.FirstNameEntry);
            _lastNameEntry = x => x.Marked(AutomationIdConstants.LastNameEntry);
            _phoneNumberEntry = x => x.Marked(AutomationIdConstants.PhoneNumberEntry);
            _saveButton = x => x.Marked(AutomationIdConstants.SaveContactButton);
            _cancelButton = x => x.Marked(AutomationIdConstants.CancelContactButton);
        }

        public void EnterFirstNameText(string text, bool shouldUseReturnKey) =>
            EnterText(_firstNameEntry, text, shouldUseReturnKey);

        public void EnterLastNameText(string text, bool shouldUseReturnKey) =>
            EnterText(_lastNameEntry, text, shouldUseReturnKey);

        public void EnterPhoneNumberText(string text, bool shouldUseReturnKey) =>
            EnterText(_phoneNumberEntry, text, shouldUseReturnKey);

        public void PopulateAllTextFields(string firstName, string lastName, string phoneNumber, bool shouldUseReturnKey)
        {
            EnterFirstNameText(firstName, shouldUseReturnKey);
            EnterLastNameText(lastName, shouldUseReturnKey);
            EnterPhoneNumberText(phoneNumber, shouldUseReturnKey);
        }

        public void TapSaveButton()
        {
            if (App is iOSApp)
                App.Tap(_saveButton);
            else if (App is AndroidApp)
                App.Tap("Save");
            else
                throw new NotSupportedException();

            App.Screenshot("Save Button Tapped");
        }

        public void TapCancelButton()
        {
            if (App is iOSApp)
                App.Tap(_cancelButton);

            else if (App is AndroidApp)
                App.Tap("Cancel");
            else
                throw new NotSupportedException();

            App.Screenshot("Cancel Button Tapped");
        }

        void EnterText(Query query, string text, bool shouldUseReturnKey)
        {
            if (shouldUseReturnKey)
                EnterTextThenTapEnter(query, text);
            else
                EnterTextThenDismissKeyboard(query, text);
        }

        void EnterTextThenDismissKeyboard(Query query, string text)
        {
            App.EnterText(query, text);
            App.DismissKeyboard();
            App.Screenshot($"Entered Text: {text}");
        }

        void EnterTextThenTapEnter(Query query, string text)
        {
            App.Tap(query);

            App.ClearText();
            App.EnterText(text);

            App.Screenshot($"Entered Text: {text}");

            App.PressEnter();
        }
    }
}
