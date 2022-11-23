using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.EntitiesBase;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string PersonInCharge { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string PersonInChargeSurName { get; set; }
        public string KindOf { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Lütfen geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Lütfen geçerli bir e-posta adresi girin.")]
        public string Email1 { get; set; }
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Lütfen geçerli bir e-posta adresi girin.")]
        public string Email2 { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [RegularExpression(@"^\(?([1-9]{1})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{4})$", ErrorMessage = "Lütfen geçerli bir telefon numarası  girin.")]
        public string Phone { get; set; }
        [RegularExpression(@"^\(?([1-9]{1})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{4})$", ErrorMessage = "Lütfen geçerli bir telefon numarası  girin.")]
        public string Phone1 { get; set; }
        [RegularExpression(@"^\(?([1-9]{1})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{4})$", ErrorMessage = "Lütfen geçerli bir telefon numarası  girin.")]
        public string Phone2 { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        public string Town { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string Address { get; set; }
        public bool SmsPermission { get; set; }
        public bool EmailPermission { get; set; }
        public bool CallPermission { get; set; }
        public string Region { get; set; }
        //[MaxLength(11, ErrorMessage = "11 Karakterden fazla girilemez")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Format hatası")]
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string TaxAdministration { get; set; }
        public string TaxNumber { get; set; }
        //[MaxLength(11, ErrorMessage = "11 Karakterden fazla girilemez")]
        [RegularExpression("^([0-9]{11})$", ErrorMessage = "Format hatası")]
        public string TcIdentityNumber { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        //[MaxLength(5, ErrorMessage = "5 Karakterden fazla girilemez")]
        [RegularExpression("^([0-9]{5})$", ErrorMessage = "Format hatası")]
        public string PostCode { get; set; }
        public string CustomerClass { get; set; }
        public int Representative { get; set; }
        public int ConfirmationType { get; set; }

        public int LogoId { get; set; }   
        public string CariKodu { get; set; }
        public string Slsman { get; set; }

        public DateTime CreatedDate { get; set; }

        public string TaxInfo { get; set; }
        public string Facebook { get; set; }
        public string Youtube { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }

       public Customer()
        {
            this.PostCode = "00000";
        }
    }
}