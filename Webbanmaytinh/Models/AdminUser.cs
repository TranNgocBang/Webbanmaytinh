
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Webbanmaytinh.Models
{

using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AdminUser
{

        [Display(Name = "M� User")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Name not empty...")]
        [Display(Name = "T�n User")]
        public string NameUser { get; set; }
        [DisplayName("V? Tr�")]
        public string RoleUser { get; set; }
        [DisplayName("Nh?p M?t kh?u")]
        [Required(ErrorMessage = "Pass not empty...")]
        [DataType(DataType.Password)]
        public string PasswordUser { get; set; }
        [NotMapped]
        [Compare("PasswordUser")]
        [DisplayName("Nh?p L?i M?t Kh?u")]
        public string ConfirmPass { get; set; }
        [NotMapped]
        public string ErrorLogin { get; set; }


    }

}
