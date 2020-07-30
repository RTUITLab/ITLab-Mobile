namespace ITLab_Mobile.Services
{
    public interface IContact
    {
        void SaveContacts(string firstname, string middlename, string lastname, string email, string number);
    }
}
