using ModelValidationsExample.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.Models
{
    public class Person
    {
        [Required(ErrorMessage = "{0} 不能为空")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} 字符长度在 {2} 和 {1} 之间")]
        [RegularExpression("^[A-Za-z .]$", ErrorMessage = "{0} 包含字符, 空格和点(.)")]
        public string? PersonName { get; set; }
        [EmailAddress(ErrorMessage = "{0} 为无效的邮箱格式")]
        public string? Email { get; set; }
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "{0} 手机号格式错误")]
        [StringLength(11, ErrorMessage = "手机号长度错误")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "{0} 不能为空")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "{0} 不能为空")]
        [Compare("Password", ErrorMessage = "{0} 和 {1} 不匹配")]
        public string? ConfirmPassword { get; set; }
        [Range(0, 999.99, ErrorMessage = "{0} 必须在 {1} 和 {2} 之间")]
        public double? Price { get; set; }

        [MinimumYearValidator]
        public DateTime? DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"Person object - Person name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, ConfirmPassword: {ConfirmPassword}, Price: {Price}";
        }
    }
}
