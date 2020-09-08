using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Models.UserModels
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage = ModelsResources.UsernameRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ModelsResources.PasswordRequired)]
        public string Password { get; set; }
    }
}