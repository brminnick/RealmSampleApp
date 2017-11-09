using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RealmSampleApp
{
    public class ContactsListViewModel : BaseViewModel
    {
        #region Fields
        ICommand _refreshCommand;
        IList<ContactModel> _allContactsList;
        #endregion

        #region Events
        public event EventHandler PullToRefreshCompleted;
        #endregion

        #region Properties
        public ICommand RefreshCommand => _refreshCommand ??
            (_refreshCommand = new Command(async () => await ExecuteRefreshCommand()));

        public IList<ContactModel> AllContactsList
        {
            get => _allContactsList;
            set => SetProperty(ref _allContactsList, value);
        }
        #endregion

        #region Methods
        ContactModel GenerateRandomContactModel()
        {
            var random = new Random((int)DateTime.Now.Ticks);

            var contactModel = new ContactModel
            {
                FirstName = GenerateRandomFirstName(),
                LastName = GenerateRandomLastName(),
                PhoneNumber = GenerateRandomPhoneNumber()
            };

            return contactModel;

            string GenerateRandomFirstName() =>
                ToPascalCase(NameGenerator.GenerateFirstName((Gender)random.Next(0, Enum.GetNames(typeof(Gender)).Length - 1)));

            string GenerateRandomLastName() =>
                ToPascalCase(NameGenerator.GenerateLastName());

            string GenerateRandomPhoneNumber() =>
                $"{random.Next(100, 999)}-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
        }

        string ToPascalCase(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string lowerCaseText = text.ToLower();

            string[] splits = lowerCaseText.Split(' ');

            for (int i = 0; i < splits.Length; i++)
            {
                switch (splits[i].Length)
                {
                    case 1:
                        break;

                    default:
                        splits[i] = char.ToUpper(splits[i][0]) + splits[i].Substring(1);
                        break;
                }
            }

            return string.Join(" ", splits);

        }

        async Task ExecuteRefreshCommand()
        {
            var minimumRefreshTime = Task.Delay(1000);

            var contactCount = ContactRealm.Count;

            for (int i = 0; i < 4 - contactCount; i++)
                await ContactRealm.SaveContact(GenerateRandomContactModel());

            AllContactsList = ContactRealm.AllContacts;

            await minimumRefreshTime;

            OnPullToRefreshCompleted();
        }

        void OnPullToRefreshCompleted() =>
            PullToRefreshCompleted?.Invoke(this, EventArgs.Empty);
        #endregion
    }
}
