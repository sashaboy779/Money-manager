using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Models.UserModels
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = ModelsResources.FirstNameRequired)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ModelsResources.LastNameRequired)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ModelsResources.UsernameRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ModelsResources.PasswordRequired)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ModelsResources.PasswordConfirmRequired)]
        [Compare(nameof(Password), ErrorMessage = ModelsResources.PasswordConfirmCompare)]
        public string PasswordConfirm { get; set; }
    }
}