namespace SimpleJWT{
    public class User{
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public User (string userName, string password){
            UserName = userName;
            Password = password;
        }

    }
}