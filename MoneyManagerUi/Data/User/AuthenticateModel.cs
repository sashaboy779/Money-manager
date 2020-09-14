using System.ComponentModel.DataAnnotations;

namespace MoneyManagerUi.Data.User
{
    public class AuthenticateModel
    {
        [Required(ErrorMessageResourceType = typeof(ModelsResources), ErrorMessageResourceName = nameof(ModelsResources.UsernameRequired))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelsResources), ErrorMessageResourceName = nameof(ModelsResources.PasswordRequired))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}