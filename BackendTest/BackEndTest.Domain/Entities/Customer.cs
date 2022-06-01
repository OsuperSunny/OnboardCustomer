using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public string StateofResidence { get; set; }

        public string LGA { get; set; }

//        phone Number, email, password, state of
//residence, and LGA.

    }
}
