//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace B403Blog.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Ticket
    {
        public int TicketID { get; set; }
        [Required(ErrorMessage = "Konu alan� zorunludur!")]
        public string TicketKonu { get; set; }
        [Required(ErrorMessage = "A��klama alan� zorunludur!")]
        public string TicketAciklama { get; set; }
        public int GonderenId { get; set; }
        public string GonderenAdi { get; set; }
    }
}
