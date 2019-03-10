using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBookCRUD.Models
{
    [MetadataType(typeof(PersonMetadata))]
    public partial class Person
    {

    }

    public class PersonMetadata
    {
        [Required (AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите имя")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите фамилию")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите отчество")]
        public string SecondName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите дату рождения")]
        public string BirthDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите описание")]
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите email")]
        public string Email { get; set; }
    }
}