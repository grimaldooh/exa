using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exa.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
       
        public String Password { get; set; }

       

    }
}
