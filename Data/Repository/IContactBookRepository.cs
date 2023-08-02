using ContactBook.Data.Entities;

namespace ContactBook.Data.Repository
{
    public interface IContactBookRepository
    {
        public bool AddContact(Contact contact);
        public bool UpdateContact(Contact contact);
        public bool DeleteContact(Contact contact);
        public Contact GetContact(int id);
        public List<Contact> GetAllContacts();

    }
}
