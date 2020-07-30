using Android.App;
using Android.Content;
using Android.Provider;
using Android.Widget;
using ITLab_Mobile.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ITLab_Mobile.Droid.Services.Contact))]
namespace ITLab_Mobile.Droid.Services
{
    public class Contact : IContact
    {
        public void SaveContacts(string firstname, string middlename, string lastname, string email, string number)
        {
            var activity = Forms.Context as Activity;
            var intent = new Intent(Intent.ActionInsert);
            intent.SetType(ContactsContract.Contacts.ContentType);
            intent.PutExtra(ContactsContract.Intents.Insert.Name, $"{firstname} {middlename} {lastname}");
            intent.PutExtra(ContactsContract.Intents.Insert.Email, email);

            intent.PutExtra(ContactsContract.Intents.Insert.Company, "РТУ МИРЭА");
            intent.PutExtra(ContactsContract.Intents.Insert.Phone, number);
            activity.StartActivity(intent);
        }
    }
}