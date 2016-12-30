using Realms;

using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RealmSampleApp
{
	public static class RealmDatabase
	{
		#region Fields
		static Realm _realm = Realm.GetInstance();
		#endregion

		#region
		public static int Count =>
			_realm.All<ContactModel>().Count();

		public static List<ContactModel> AllContacts =>
			_realm.All<ContactModel>().ToList();
		#endregion

		#region Methods
		public static async Task AddContact(ContactModel contact)
		{
			await _realm.WriteAsync(tempRealm =>
			{
				var contactFromRealm = tempRealm.All<ContactModel>()?.Where(x => x.Id == contact.Id)?.FirstOrDefault();

				if (contactFromRealm == null)
				{
					tempRealm.Add(contact);
					return;
				}

				UpdateContact(contactFromRealm, contact);
			});
		}

		static void UpdateContact(ContactModel currentContact, ContactModel updatedContact)
		{
			currentContact.City = updatedContact.City;
			currentContact.FirstName = updatedContact.FirstName;
			currentContact.IsFavorite = updatedContact.IsFavorite;
			currentContact.LastName = updatedContact.LastName;
			currentContact.PhoneNumber = updatedContact.PhoneNumber;
			currentContact.Street1Address = updatedContact.Street1Address;
			currentContact.Street2Address = updatedContact.Street2Address;
			currentContact.ZipCode = updatedContact.ZipCode;
		}
		#endregion
	}
}
