using System;

using Realms;

namespace RealmSampleApp
{
    public class ContactModel : RealmObject
    {
        #region Constructors
        public ContactModel() => Id = Guid.NewGuid().ToString();
        #endregion

        #region Properties
        public string FullName => $"{FirstName} {LastName}";

        [PrimaryKey]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        #endregion
    }
}
