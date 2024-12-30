using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagerAPI.Domain.ValueObjects.Output
{
    public class UserResultVO
    {
        public UserResultVO(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; }

        public string Name { get; }

        public string Email { get; }
    }
}
