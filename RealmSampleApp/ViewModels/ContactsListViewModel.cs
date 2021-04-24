using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;

namespace RealmSampleApp
{
    class ContactsListViewModel : BaseViewModel
    {
        readonly WeakEventManager pullToRefreshCompletedEventManager = new();

        IReadOnlyList<ContactModel> _allContactsList = Array.Empty<ContactModel>();

        public ContactsListViewModel()
        {
            RefreshCommand = new AsyncCommand(ExecuteRefreshCommand);
        }

        public event EventHandler PullToRefreshCompleted
        {
            add => pullToRefreshCompletedEventManager.AddEventHandler(value);
            remove => pullToRefreshCompletedEventManager.RemoveEventHandler(value);
        }

        public ICommand RefreshCommand { get; }

        public IReadOnlyList<ContactModel> AllContactsList
        {
            get => _allContactsList;
            set => SetProperty(ref _allContactsList, value);
        }

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
                        splits[i] = char.ToUpper(splits[i][0]) + splits[i][1..];
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

            await minimumRefreshTime.ConfigureAwait(false);

            OnPullToRefreshCompleted();
        }

        void OnPullToRefreshCompleted() => pullToRefreshCompletedEventManager.RaiseEvent(this, EventArgs.Empty, nameof(PullToRefreshCompleted));
    }
}
