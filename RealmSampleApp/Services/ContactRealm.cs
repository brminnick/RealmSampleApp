using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;

namespace RealmSampleApp
{
    public static class ContactRealm
    {
        public static int Count =>
            Realm.GetInstance().All<ContactModel>().Count();

        public static IReadOnlyList<ContactModel> AllContacts =>
            Realm.GetInstance().All<ContactModel>().ToList();

        public static Task SaveContact(ContactModel contact) =>
            Realm.GetInstance().WriteAsync(realm => realm.Add(contact));
    }
}
