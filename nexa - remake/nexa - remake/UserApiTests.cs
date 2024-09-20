using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace nexa___remake
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserController
    {
        public User GetUser(int id)
        {
            return new User { Id = id, Name = "Test User" };
        }
    }
    public class UserApiTests
    {
        [Fact]
        public void Test_GetUser_ReturnsUser()
        {
            var userId = 1;
            var expectedUser = new User { Id = userId, Name = "Test User" };
            var controller = new UserController();

            var result = controller.GetUser(userId);

            Assert.Equal(expectedUser.Name, result.Name);
        }
    }
}
