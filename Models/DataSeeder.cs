namespace API3.Models
{
    public class DataSeeder
    {

        private readonly User user;
        public DataSeeder(User user)
        {
            this.user = user;

        }
        public void Seed()
        {
            if (!user.UserName.Any())
            {
                var users = new List<User>()
                {
                    new User()
                    {
                        Voornaam = "Maria",
                        Achternaam = "Jaqueline",
                        Geboortedatum = new DateTime(1992,5,19)

                    },
                     new User()
                    {
                        Voornaam = "Jacob",
                        Achternaam = "Chera",
                        Geboortedatum = new DateTime(2000,7,18)

                    }
                };

                
            }
        }
    }
}
