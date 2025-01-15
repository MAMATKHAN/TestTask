using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Models.Contacts
{
    public class EditContactDto
    {
        public long ContactId { get; set; }
        [Required(ErrorMessage = "Не выбран контрагент")]
        public long ContractorId { get; set; }
        [Required(ErrorMessage = "Не указано ФИО")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Не указан email")]
        public string Email { get; set; }
    }
}
