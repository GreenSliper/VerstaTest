using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VerstaTest.Models
{
    public partial class Order : IValidatableObject
    {
        public int Id { get; set; }

        [Display(Name = "Sender city")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sender city must be assigned")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "City must be between {2} and {1} characters long")]
        public string SenderCity { get; set; }

        [Display(Name = "Sender address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sender address must be assigned")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Address must be between {2} and {1} characters long")]
        public string SenderAddress { get; set; }

        [Display(Name = "Receiver city")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Receiver city must be assigned")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "City must be between {2} and {1} characters long")]
        public string ReceiverCity { get; set; }

        [Display(Name = "Receiver address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Receiver address must be assigned")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Address must be between {2} and {1} characters long")]
        public string ReceiverAddress { get; set; }

        [Display(Name = "Package weight")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Package weight must be assigned")]
        [RegularExpression(@"^[0-9]{1,4}(.[0-9]{1,2})?$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        [Range((double)0.1, 1000, ErrorMessage = "Package weight must greater than 0.01 kg and less than 1000kg")]
        public decimal? PackageWeight { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Receive date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Receive date must be assigned")]
        public DateTime? ReceiveDate { get; set; }
        public DateTime? CreationTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (ReceiveDate < DateTime.Now.Date)
                errors.Add(new ValidationResult("Receive date cannot be earlier than current date", new List<string>() { nameof(ReceiveDate) }));
            return errors;
        }
    }
}
