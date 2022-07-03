using MauiAppDemo.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppDemo.Models
{
    [Table(nameof(User))]
    public class User : ValidationPropertyModel
    {
        private int _No;
        private string _Name;
        private string _Password;

        [Display(Order = 1, Name = nameof(Messages.User_No), ResourceType = typeof(Messages))]
        [Key]
        [Required(ErrorMessageResourceName = nameof(Messages.Error_Required), ErrorMessageResourceType = typeof(Messages))]
        [Range(1, 9999, ErrorMessageResourceName = nameof(Messages.Error_Range), ErrorMessageResourceType = typeof(Messages))]
        public int No
        {
            get => _No;
            set => SetProperty(ref _No, value);
        }

        [Column(nameof(Name))]
        [Display(Order = 2, Name = nameof(Messages.User_Name), ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = nameof(Messages.Error_Required), ErrorMessageResourceType = typeof(Messages))]
        [MinLength(1, ErrorMessageResourceName = nameof(Messages.Error_Required), ErrorMessageResourceType = typeof(Messages))]
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        [Column(nameof(Password))]
        [Display(Order = 3, Name = nameof(Messages.User_Password), ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = nameof(Messages.Error_Required), ErrorMessageResourceType = typeof(Messages))]
        [MinLength(1, ErrorMessageResourceName = nameof(Messages.Error_Required), ErrorMessageResourceType = typeof(Messages))]
        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }
    }
}
