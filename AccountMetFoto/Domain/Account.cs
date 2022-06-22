using System.ComponentModel;

namespace AccountMetFoto.Domain
{
    public enum Gender
    {
        [Description("Man")]
        M,
        [Description("Vrouw")]
        F,
        [Description("kweethetnietgoed")]
        X
    }

    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DOB { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
    }
}
