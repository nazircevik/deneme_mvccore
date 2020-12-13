using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using deneme_mvccore.Identity;
using deneme_mvccore.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace deneme_mvccore.Controllers
{
    public class SecurityController : Controller
    {
        private UserManager<AppIdentityUser> _usermanager;
        private SignInManager<AppIdentityUser> _signInManager;
        public SecurityController(UserManager<AppIdentityUser> userManager,
            SignInManager<AppIdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _usermanager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            else
            {
                var user = await _usermanager.FindByNameAsync(loginViewModel.UserName);
                if (user != null)
                {
                    if (!await _usermanager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(String.Empty, ("Email onayla"));
                        return View(loginViewModel);
                    }
                }
                var result =
                    await _signInManager.PasswordSignInAsync(
                       loginViewModel.UserName, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Student");
                }
                ModelState.AddModelError(String.Empty, "Login Failded");
                return View(loginViewModel);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Student");
        }
        public IActionResult AccesDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var user = new AppIdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
                // Age=registerViewModel.Age,
                 
            };
            var result = await _usermanager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                var confirmationCode = _usermanager.GenerateEmailConfirmationTokenAsync(user);
                var callBackUrl = Url.Action("ConfirmEmail", "Security", new { user.Id, code = confirmationCode });
                // Send Email

                return RedirectToAction("Index", "Student");
            }
            return View(registerViewModel);
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == "" || code == "")
            {
                return RedirectToAction("Index", "Student");
            }
            var user = await _usermanager.FindByNameAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("Unable to find the user");
            }
            var result = await _usermanager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {

                return View("ConfirmEmail");
            }
            return RedirectToAction("Index", "Student");


        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                return View();
            }
            var user = await _usermanager.FindByNameAsync(email);
            if (user == null)
            {
                return View();
            }
            var confirmationCode = await _usermanager.GeneratePasswordResetTokenAsync(user);
            var callBackUrl = Url.Action("ConfirmEmail", "Security", new { user.Id, code = confirmationCode });
            return RedirectToAction("ForgotPasswordEmailSent");
        }
        public IActionResult ForgotPasswordEmailSent()
        {
            return View();
        }
        public IActionResult ResetPassword(string UserId, string code)
        {
            if (UserId == null || code == null)
            {
                throw new ApplicationException("asdasdasdsadasdas");

            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }
            var user = await _usermanager.FindByEmailAsync(resetPasswordViewModel.Email);
            if (user == null)
            {
                throw new ApplicationException("asdsadas");

            }
            var result = await _usermanager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);
        if(result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirm");
            }
            return View();
        }
        public IActionResult ResetPasswordConfirm()
        {

            return View();

        }
    }
}
