//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPegadaian.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Type_Laptop
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public Nullable<int> Harddisk_Capacity { get; set; }
        public Nullable<int> Years_Of_Buy { get; set; }
        public Nullable<decimal> Selling_Price { get; set; }
        public string Condition { get; set; }
        public string Picture { get; set; }
        public int Transaction_Id { get; set; }
    
        public virtual Transaction Transaction { get; set; }
    }
}
