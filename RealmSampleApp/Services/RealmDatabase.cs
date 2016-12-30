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

		#region Properties
		public static int Count =>
			_realm.All<ContactModel>().Count();

		public static List<ContactModel> AllContacts =>
			_realm.All<ContactModel>().ToList();
		#endregion

		#region Methods
		public static void AddContact(ContactModel contact)
		{
			_realm.Write(() =>
			{
				_realm.Add(contact);
			});
		}
		#endregion
	}
}
