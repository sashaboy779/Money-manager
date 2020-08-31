namespace BusinessLogicLayer.Dto
{
    public class CreatePasswordDto
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
