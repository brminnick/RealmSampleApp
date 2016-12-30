using System;
using Realms;
using System.Dynamic;
using System.ComponentModel;
using System.Text;
namespace RealmSampleApp
{
	public class ContactModel : RealmObject
	{
		#region Properties
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PhoneNumber { get; set; }

		public string FullName => $"{FirstName} {LastName}";
		#endregion
	}
}
