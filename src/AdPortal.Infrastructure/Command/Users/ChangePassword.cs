namespace AdPortal.Infrastructure.Command.Users
{
    public class ChangePassword : AuthenticatedBaseCommand, ICommand
    {
        public string NewPassword {get; set;}
        public string OldPassword {get; set;}
        public string NewPasswordConfirmation {get; set;}
    }
}