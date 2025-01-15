using Microsoft.AspNetCore.Mvc;
using TestTask.BLL.Models.Contacts;
using TestTask.BLL.Services.Interfaces;
using TestTask.Web.Models.Contacts;

namespace TestTask.Web.Controllers
{
	public class ContactController : Controller
	{
		private readonly IContactService _contactService;
		private readonly IContractorService _contractorService;

		public ContactController(IContactService contactService, IContractorService contractorService)
		{
			_contactService = contactService;
			_contractorService = contractorService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(CancellationToken token)
		{
			var contacts = await _contactService.GetAll(token);
			var contractors = await _contractorService.GetAll(token);
			var model = new IndexViewModel { Contacts = contacts, Contractors = contractors };

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Index(IndexViewModel model, CancellationToken token)
		{
			model.Contacts = await _contactService.GetContactsByContractorId(model.SelectedContractorId, token); ;
			model.Contractors = await _contractorService.GetAll(token); ;

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(long id, CancellationToken token)
		{
			var contact = await _contactService.GetContact(id, token);
			var editContactDto = new EditContactDto
			{
				ContactId = contact.ContactId,
				FullName = contact.FullName,
				ContractorId = contact.ContractorId,
				Email = contact.Email,
			};
			var contractors = await _contractorService.GetAll(token);
			var model = new EditContactViewModel
			{
				Contact = editContactDto,
				Contractors = contractors
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditContactViewModel editContactViewModel, CancellationToken token)
		{
			editContactViewModel.Contractors = await _contractorService.GetAll(token);
			if (!ModelState.IsValid)
				return View(editContactViewModel);

			var contact = new UpdateContactModel
			{
				ContactId = editContactViewModel.Contact.ContactId,
				ContractorId = editContactViewModel.Contact.ContractorId,
				FullName = editContactViewModel.Contact.FullName,
				Email = editContactViewModel.Contact.Email,
			};

			await _contactService.UpdateContact(contact, token);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Add(CancellationToken token)
		{
			var contractors = await _contractorService.GetAll(token);
			var addContactViewModel = new AddContactViewModel { Contractors = contractors };
			return View(addContactViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddContactViewModel addContactViewModel, CancellationToken token)
		{
			var contractors = await _contractorService.GetAll(token);

            if (!ModelState.IsValid)
                return View(new AddContactViewModel { Contractors = contractors });

            var contact = new CreateContactModel
            {
                ContractorId = addContactViewModel.Contact.ContractorId,
                FullName = addContactViewModel.Contact.FullName,
                Email = addContactViewModel.Contact.Email,
            };
            await _contactService.CreateContact(contact, token);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(long id, CancellationToken token)
		{
			await _contactService.DeleteContact(id, token);

			return RedirectToAction("Index");
		}
	}
}
