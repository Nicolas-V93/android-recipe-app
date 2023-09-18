namespace Imi.Project.Mobile.Dto
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string DateOfBirth { get; set; }
        public bool HasApprovedTerms { get; set; }
    }
}
