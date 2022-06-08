using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorizationApi.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int ObjectId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
