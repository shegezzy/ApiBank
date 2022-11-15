using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ApiBank.Models
{
    public class AuthenticateModel
    {
        [Required] //let's validate the account is 10-digit number using regexp attribute
        [RegularExpression(@"^{0} [1-9]\d{9}$|^{1-9}$")]
        public string AccountNumber { get; set; }
        [Required]
        public string Pin { get; set; }
    }
}
