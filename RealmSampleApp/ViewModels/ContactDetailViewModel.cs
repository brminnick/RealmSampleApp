using System;
namespace RealmSampleApp
{
	public class ContactDetailViewModel
	{
		#region Constructors
		public ContactDetailViewModel(ContactModel selectedContact)
		{
			Contact = selectedContact;
		}
		#endregion

		#region Properties
		public ContactModel Contact { get; }
		#endregion
	}
}
