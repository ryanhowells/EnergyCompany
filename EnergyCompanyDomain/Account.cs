using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyCompanyDomain
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AccountId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }
    }
}
