using System.ComponentModel.DataAnnotations;

namespace MoneyManagerUi.Data.User
{
    public class RegisterUserModel
    {
        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.FirstNameRequired))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.LastNameRequired))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.UsernameRequired))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.PasswordRequired))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.PasswordConfirmRequired))]
        [Compare(nameof(Password), 
            ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.PasswordConfirmCompare))]
        public string PasswordConfirm { get; set; }
    }
}