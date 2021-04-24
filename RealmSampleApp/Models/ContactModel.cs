using System;
using System.ComponentModel;
using Realms;

namespace RealmSampleApp
{
    public class ContactModel : RealmObject
    {
        public ContactModel() => Id = Guid.NewGuid().ToString();

        public string FullName => $"{FirstName} {LastName}";

        [PrimaryKey]
        public string Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
    }
}

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public record IsExternalInit;
}
