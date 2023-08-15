using ContactBook.Data.Entities;
using ContactBook.Data.Repository;
using ContactBook.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[Controller]")]
    public class ContactBookController : Controller
    {
        private readonly IContactBookRepository _repo;

        public ContactBookController(IContactBookRepository repo)
        {
            _repo = repo;
        }

        [Authorize(Roles = "admin")]  //must be logged in, must also be an admin
        [Authorize(Policy = "CanAdd")]
        [HttpPost("add")]
        public IActionResult AddNewContact([FromBody] AddContactDto model)
        {
            if (ModelState.IsValid)
            {
                var NewContact = new Contact
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                };

                if (_repo.AddContact(NewContact))
                    return Ok("New contact added successfully");

                return BadRequest("Failed to add new contact");
            }
            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpGet("get-all")]
        public IActionResult GetAllContacts()
        {
            var contacts = _repo.GetAllContacts();
            if (contacts == null || contacts.Count == 0)
                return BadRequest("no contacts found");

            var result = contacts.Select(x => new ReturnContactDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
            });

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("single/{id}")]
        public IActionResult GetContact(int id)
        {
            var contact = _repo.GetContact(id);

            if (contact != null)
            {
                var result = new ReturnContactDto
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                    Address = contact.Address,
                };

                return Ok(result);
            }

            return NotFound("Contact does not exist");
        }


        [HttpPut("Update/{id}")]
        public IActionResult UpdateContact(int id, [FromBody] UpdateContactDto model)
        {
            if (ModelState.IsValid)
            {
                var existingContact = _repo.GetContact(id);


                if (existingContact != null)
                {
                    existingContact.FirstName = model.FirstName;
                    existingContact.LastName = model.LastName;
                    existingContact.Email = model.Email;
                    existingContact.PhoneNumber = model.PhoneNumber;
                    existingContact.Address = model.Address;

                    if (_repo.UpdateContact(existingContact))
                    {
                        return Ok("Contact updated successfully");
                    }

                    return BadRequest("Failed to update contact");
                }

                return NotFound("Contact does not exist");
            }

            return BadRequest(ModelState);
        }




        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contact = _repo.GetContact(id);
            if (contact != null)
            {
                if (_repo.DeleteContact(contact))
                    return Ok("Contact deleted successfully");

                return BadRequest("Failed to deleete");
            }

            return BadRequest("Delete Failed: COntact no found");
        }

    }
}
