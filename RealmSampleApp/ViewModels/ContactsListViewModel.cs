using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
namespace RealmSampleApp
{
	public class ContactsListViewModel : BaseViewModel
	{
		#region Constant Fields
		readonly Random _random = new Random();

		readonly List<string> _firstNameList = new List<string>
		{
			{"Brandon"},
			{"Kim"},
			{"Allen"},
			{"Bruce"}
		};

		readonly List<string> _lastNameList = new List<string>
		{
			{"Smith"},
			{"Jones"},
			{"Watson"},
			{"Minnick"}
		};

		readonly List<string> _streetList = new List<string>
		{
			{"Folsom St."},
			{"Clementina St."},
			{"Sansome St."},
			{"Pacific Ave."}
		};

		readonly List<string> _cityList = new List<string>
		{
			{"San Francisco"},
			{"Melbourne"},
			{"Seattle"},
			{"Portland"}
		};

		readonly List<string> _stateList = new List<string>
		{
			{"CA"},
			{"FL"},
			{"WA"},
			{"OR"}
		};
		#endregion

		#region Fields
		List<ContactModel> _allContactsDataList, _viewableContactsDataList;
		#endregion

		public ContactsListViewModel()
		{
			Task.Run(async () =>
			{
				if (RealmDatabase.Count == 0)
				{
					for (int i = 0; i < 4; i++)
						await RealmDatabase.AddContact(GenerateRandomContactModel());
				}

				AllContactsDataList = RealmDatabase.AllContacts;
				ViewableContactsDataList = GetViewableContactsListData();
			});
		}

		#region Properties
		public List<ContactModel> AllContactsDataList
		{
			get { return _allContactsDataList; }
			set { SetProperty(ref _allContactsDataList, value); }
		}

		public List<ContactModel> ViewableContactsDataList
		{
			get { return _viewableContactsDataList; }
			set { SetProperty(ref _viewableContactsDataList, value); }
		}
		#endregion

		#region Methods
		ContactModel GenerateRandomContactModel()
		{
			var contactModel = new ContactModel
			{
				FirstName = GenerateRandomFirstName(),
				City = GenerateRandomCity(),
				LastName = GenerateRandomLastName(),
				PhoneNumber = GenerateRandomPhoneNumber(),
				State = GenerateRandomState(),
				Street1Address = GenerateRandomStreetAddress(),
				ZipCode = GenerateRandomZipCode()
			};

			return contactModel;
		}

		List<ContactModel> GetViewableContactsListData()
		{
			return AllContactsDataList;
		}

		string GenerateRandomFirstName()
		{
			return _firstNameList[_random.Next(0, _firstNameList.Count - 1)];
		}

		string GenerateRandomCity()
		{
			return _cityList[_random.Next(0, _cityList.Count - 1)];
		}

		string GenerateRandomLastName()
		{
			return _lastNameList[_random.Next(0, _lastNameList.Count - 1)];
		}

		string GenerateRandomPhoneNumber()
		{
			return $"{_random.Next(100, 999)}-{_random.Next(100, 999)}-{_random.Next(1000, 9999)}";
		}

		string GenerateRandomState()
		{
			return _stateList[_random.Next(0, _stateList.Count - 1)];
		}

		string GenerateRandomStreetAddress()
		{
			return _streetList[_random.Next(0, _streetList.Count - 1)];
		}

		string GenerateRandomZipCode()
		{
			return _random.Next(11111, 99999).ToString();
		}
		#endregion
	}
}
