using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyCompanyDomain
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
