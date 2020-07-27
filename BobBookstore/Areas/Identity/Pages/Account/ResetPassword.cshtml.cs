﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace BobBookstore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly CognitoUserManager<CognitoUser> _userManager;
        private readonly ILogger<LoginWith2faModel> _logger;

        public ResetPasswordModel(UserManager<CognitoUser> userManger, ILogger<LoginWith2faModel> logger)
        {
            _userManager = userManger as CognitoUserManager<CognitoUser>;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string HandlerName { get; set; }
        public string ReturnUrl { get; set; }
        public string inputEmail { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            //[Required]
            [DataType(DataType.Text)]
            [Display(Name = "Reset Token")]
            public string ResetToken { get; set; }

            //[Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostSecondAsync(string returnUrl = null)
        {
            HandlerName = "Second";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to retrieve user.");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.ResetToken, Input.NewPassword);

            if (result.Succeeded)
            {
                _logger.LogInformation("Password reset for user with ID '{UserId}'.", user.UserID);
                return LocalRedirect(returnUrl);
            }
            else
            {
                _logger.LogInformation("Unable to rest password for user with ID '{UserId}'.", user.UserID);
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return Page();
            }
        }
        public async Task<IActionResult> OnPostFirstAsync(string returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);


            if (user == null)
            {
                throw new InvalidOperationException($"Unable to retrieve user.");
            }
            var a = await _userManager.ResetPasswordAsync(user);
            ModelState.AddModelError(string.Empty, "Now please enter your Token and new password");
            return Page();
        }

    }
}