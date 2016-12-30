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
		[PrimaryKey]
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PhoneNumber { get; set; }

		public string Street1Address { get; set; }

		public string Street2Address { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string ZipCode { get; set; }

		public bool IsFavorite { get; set; }

		public string Address => GetAddress();
		#endregion

		string GetAddress()
		{
			var addressString = new StringBuilder();

			addressString.AppendLine(Street1Address);

			if (!(string.IsNullOrEmpty(Street2Address)))
				addressString.AppendLine(Street2Address);

			addressString.AppendLine($"{City}, {State} {ZipCode}");

			return addressString.ToString();
		}
	}
}
