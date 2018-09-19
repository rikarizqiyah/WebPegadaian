using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPegadaian.ViewModel
{
    public class ProvRegViewModel
    {
        public string Name { get; set; }
        public string Identity_Type { get; set; }
        public int Identity_Number { get; set; }
        public string Gender { get; set; }
        public string Born_Place { get; set; }
        public DateTime Born_Date { get; set; }
        public string Address { get; set; }
        public string Provinces_Id { get; set; }
        public string Regencies_Id { get; set; }
        public string Districts_Id { get; set; }
        public string Villages_Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role_Id { get; set; }
        public int Number_Npwp { get; set; }
        public string Picture_Npwp { get; set; }
        public string Picture_Identity { get; set; }
    }
}