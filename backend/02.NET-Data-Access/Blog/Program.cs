using Blog.Models;
using Blog.Repositories;
using Microsoft.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string CONNECTION_STRING = @"Server=localhost,1433;Database=blog;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            var connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();

            //ReadUsers(connection);
            ReadUsersWithRoles(connection);
            //CreateUser(connection);
            //ReadRoles(connection);
            //ReadTags(connection);

            connection.Close();
        }

        public static void ReadUsers(SqlConnection connection)
        {
            var repository = new Repository<User>(connection);
            var users = repository.Get();

            foreach (var item in users)
            {
                Console.WriteLine(item.Name);
                foreach (var role in item.Roles)
                {
                    Console.WriteLine($" - {role.Name}");
                }
            }
        }
        public static void ReadUsersWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var items = repository.GetWithRoles();

            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
                foreach (var role in item.Roles)
                {
                    Console.WriteLine($" - {role.Name}");
                }
            }
        }
        public static void CreateUser(SqlConnection connection)
        {
            var user = new User()
            {
                Email = "email@cassiano.com",
                Bio = "bio",
                Image = "image.png",
                Name = "Cassiano",
                PasswordHash = "hash",
                Slug = "cassiano"
            };

            var repository = new Repository<User>(connection);
            repository.Create(user);
        }
        public static void ReadRoles(SqlConnection connection)
        {
            var repository = new Repository<Role>(connection);
            var items = repository.Get();

            foreach (var item in items)
                Console.WriteLine(item.Name);
        }
        public static void ReadTags(SqlConnection connection)
        {
            var repository = new Repository<Tag>(connection);
            var items = repository.Get();

            foreach (var item in items)
                Console.WriteLine(item.Name);
        }
    }
}