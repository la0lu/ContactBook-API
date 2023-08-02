using ContactBook.Data.Entities;

namespace ContactBook.Data.Repository
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly ContactBookContext _context;

        public ContactBookRepository(ContactBookContext context)
        {
            _context = context;
        }

        public bool AddContact(Contact contact)
        {
            _context.Add(contact);
            _context.SaveChanges();
            return true;
            
        }

        public bool DeleteContact(Contact contact)
        {
            _context.Remove(contact);
            _context.SaveChanges();
            return true;
        }
        
        public bool UpdateContact(Contact contact)
        {
           _context.Update(contact);
            _context.SaveChanges();
            return true;
        }

        public List<Contact> GetAllContacts()
        {
           return _context.Contacts.ToList();
        }

        public Contact? GetContact(int id)
        {
            var result = _context.Contacts.FirstOrDefault(x => x.Id == id);

            if (result != null)
                return result;
            return null;
             
        }
    }
}
