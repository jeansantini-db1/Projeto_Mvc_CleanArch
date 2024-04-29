﻿using System.Threading.Tasks;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticate _authentication;

    public AccountController(IAuthenticate authentication)
    {
        _authentication = authentication;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var result = await _authentication.RegisterUser(model.Email, model.Password);
        if (result)
            return Redirect("/");

        ModelState.AddModelError(string.Empty, "Registro inválido");
        return View(model);
    }
    
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _authentication.Authenticate(model.Email, model.Password);

        if (result)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
                return RedirectToAction("Index", "Home");
            return Redirect(model.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Login inválido");
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authentication.Logout();
        return Redirect("/Account/Login");
    }
}