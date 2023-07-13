using System.ComponentModel.DataAnnotations;

namespace Practical_20.Utility
{
    public class ValidateEmailDomain : ValidationAttribute
    {
        private readonly string allowDomain;
        public ValidateEmailDomain(string allowDomain)
        {
            this.allowDomain = allowDomain;
        }

        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split("@");
            return strings[1].ToLower() == allowDomain.ToLower();
        }
    }
}
