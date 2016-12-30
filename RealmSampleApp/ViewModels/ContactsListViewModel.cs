using System;
using System.Collections.Generic;
using Realms;
using System.Linq;

namespace RealmSampleApp
{
	public class ContactsListViewModel
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
		#endregion

		#region Constructors
		public ContactsListViewModel()
		{

			if (RealmDatabase.Count < 4)
			{
				for (int i = 0; i < 4; i++)
				{
					RealmDatabase.AddContact(GenerateRandomContactModel());
				}
			}

			AllContactsDataList = RealmDatabase.AllContacts;
		}
		#endregion

		#region Properties
		public List<ContactModel> AllContactsDataList { get; }
		#endregion

		#region Methods
		ContactModel GenerateRandomContactModel()
		{
			var contactModel = new ContactModel
			{
				FirstName = GenerateRandomFirstName(),
				LastName = GenerateRandomLastName(),
				PhoneNumber = GenerateRandomPhoneNumber()
			};

			return contactModel;
		}

		string GenerateRandomFirstName()
		{
			return _firstNameList[_random.Next(0, _firstNameList.Count - 1)];
		}

		string GenerateRandomLastName()
		{
			return _lastNameList[_random.Next(0, _lastNameList.Count - 1)];
		}

		string GenerateRandomPhoneNumber()
		{
			return $"{_random.Next(100, 999)}-{_random.Next(100, 999)}-{_random.Next(1000, 9999)}";
		}
		#endregion
	}
}
