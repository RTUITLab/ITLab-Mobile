using Contacts;
using Foundation;
using ITLab_Mobile.Services;

namespace ITLab_Mobile.iOS.Services
{
    public class Contact : IContact
    {
        public void SaveContacts(string firstname, string middlename, string lastname, string email, string number)
        {
            var store = new CNContactStore();
            var contact = new CNMutableContact();
            var cellPhone = new CNLabeledValue<CNPhoneNumber>(CNLabelPhoneNumberKey.Mobile, new CNPhoneNumber(number));
            var phoneNumber = new[] { cellPhone };
            contact.PhoneNumbers = phoneNumber;
            contact.GivenName = $"{firstname} {middlename} {lastname}";

            var workEmail = new CNLabeledValue<NSString>(new NSString("work"), new NSString(email));
            var emailAdd = new[] { workEmail };
            contact.EmailAddresses = emailAdd;

            contact.OrganizationName = "РТУ МИРЭА";

            var saveRequest = new CNSaveRequest();
            saveRequest.AddContact(contact, store.DefaultContainerIdentifier);
        }
    }
}