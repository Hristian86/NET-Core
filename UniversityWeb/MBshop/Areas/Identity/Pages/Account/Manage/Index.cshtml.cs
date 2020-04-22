using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MBshop.Service;
using MBshop.Service.interfaces;
using MBshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MBshop.Service.StaticProperyes;

namespace MBshop.Areas.Identity.Pages.Account.Manage
{

    public partial class IndexModel : PageModel
    {
        private readonly IProfileEditService _edit;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private bool resultFromCheckNickName;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IProfileEditService edit
            )
        {
            this._edit = edit;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        //Display properies
        public string ChatName { get; private set; }

        public string Avatar { get; private set; }
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Address { get; private set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Chat nick name template - Aaa12 , aaa12 , aaaa , Aaaa aaa, Aaaa Aaa,")]
            [MaxLength(20, ErrorMessage = "To long chat nick name")]
            [RegularExpression(@"\b([A-Z][a-z]+\d+)|([a-z]+\d+)|([A-Z]+[a-z]+ [A-Z]+[a-z]+)|([A-Z]+ [A-Z]+)|([a-z]+ [a-z]+)|([a-z]+)|([a-z]+ [a-z]+)|([A-Z]{1}[a-z]+\d+)|([a-z]+[0-9]+)|([a-z]+ )|([A-Z]+[a-z]+)|([A-Z]+)", ErrorMessage = "Invalid symbols")]
            public string ChatName { get; set; }

            [Phone]
            [Display(Name = "Phone number example +359-1234-123-123")]
            [MaxLength(20)]
            [RegularExpression(@"\+\d{3}-\d{4}-\d{3}-\d{3}",ErrorMessage = "Invalid phone number!")]
            public string PhoneNumber { get; set; }

            [Display(Name = "First name")]
            [MaxLength(20, ErrorMessage = "Too Long Name")]
            [RegularExpression(@"(^|\s)(?<word>[a-zA-Z][a-zA-Z']*)", ErrorMessage = "First name must contains only letters")]
            public string FirstName { get; set; }

            [Display(Name = "Last name")]
            [MaxLength(20, ErrorMessage = "Too log last name")]
            [RegularExpression(@"(^|\s)(?<word>[a-zA-Z][a-zA-Z']*)", ErrorMessage = "Last name must contains only letters")]
            public string LastName { get; set; }

            [Display(Name = "Address")]
            [MaxLength(50, ErrorMessage = "Too long address description")]
            [RegularExpression(@"[A-Za-z0-9]+",ErrorMessage ="It contains only letters and digits")]
            public string Address { get; set; }

            [Display(Name = "Image Avatar")]
            public string Avatar { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (user.UserName != null)
            {
                //Creating current profile properties for placeholder

                var usr = _edit.GetUserProperties(user.UserName);
                this.Avatar = usr.Avatar;
                this.ChatName = usr.ChatName;
                this.FirstName = usr.FirstName;
                this.LastName = usr.LastName;
                this.Address = usr.Address;
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);

                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);

            //Making Custom Identity properties
            if (user.Id != null)
            {

                this.resultFromCheckNickName = await _edit.SaveUserProperties(Input.Avatar, Input.ChatName, Input.FirstName, Input.LastName, Input.Address, user.Id);
                
            }

            if (this.resultFromCheckNickName)
            {

                GlobalAlertMessages.StatusMessage = "This chat nick name " + $"( {Input.ChatName} )" + " is already taken please choose different name!";
            }
            else
            {
                GlobalAlertMessages.StatusMessage = "Your profile has been updated!";
            }

            return RedirectToPage();
        }
    }
}