using Core.EntitiesBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
   public class BigData: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string PersonInCharge { get; set; }
        public string KindOf { get; set; }

        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Lütfen geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Lütfen geçerli bir e-posta adresi girin.")]
        public string Email1 { get; set; }
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Lütfen geçerli bir e-posta adresi girin.")]
        public string Email2 { get; set; }


        [RegularExpression(@"^\(?([1-9]{1})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{4})$", ErrorMessage = "Lütfen geçerli bir telefon numarası  girin.")]
        public string Phone { get; set; }
        [RegularExpression(@"^\(?([1-9]{1})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{4})$", ErrorMessage = "Lütfen geçerli bir telefon numarası  girin.")]
        public string Phone1 { get; set; }
        [RegularExpression(@"^\(?([1-9]{1})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{4})$", ErrorMessage = "Lütfen geçerli bir telefon numarası  girin.")]
        public string Phone2 { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public string City { get; set; }


        public string Town { get; set; }

        public string Address { get; set; }
        public bool SmsPermission { get; set; }
        public bool EmailPermission { get; set; }
        public bool CallPermission { get; set; }
        public string Region { get; set; }
        [MaxLength(10, ErrorMessage = "10 Karakterden fazla girilemez")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Format hatası")]
        public string TaxAdministration { get; set; }
        public string TaxNumber { get; set; }
        [MaxLength(11, ErrorMessage = "11 Karakterden fazla girilemez")]
        [RegularExpression("^([0-9]{11})$", ErrorMessage = "Format hatası")]
        public string TcIdentityNumber { get; set; }
        public string CustomerClass { get; set; }
        public int Representative { get; set; }
        public int ConfirmationType { get; set; }
        public string PostCode { get; set; }
        public string TaxInfo { get; set; }
        public int LogoId { get; set; }
        public string CariKodu { get; set; }
        public string Slsman { get; set; }
        public string Not { get; set; }

        public DateTime CreatedDate { get; set; }


        public string Facebook { get; set; }
        public string Youtube { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
    }
}
