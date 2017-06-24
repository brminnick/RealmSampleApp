using Realms;

using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RealmSampleApp
{
    public static class ContactRealm
    {
        #region Properties
        public static int Count =>
            Realm.GetInstance().All<ContactModel>().Count();

        public static List<ContactModel> AllContacts =>
            Realm.GetInstance().All<ContactModel>().ToList();
        #endregion

        #region Methods
        public static async Task SaveContact(ContactModel contact) =>
            await Realm.GetInstance().WriteAsync((tempRealm) => tempRealm.Add(contact));
        #endregion
    }
}
