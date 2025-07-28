﻿using AutoMapper;
using FashionShop.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FashionShop.Helpers;

namespace FashionShop.Controllers
{
    public class UserController(FashionShopContext context, IMapper mapper) : Controller
    {

        private readonly FashionShopContext _context = context;
        private readonly IMapper _mapper = mapper;

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(ViewModels.Register model)
        {
            if (ModelState.IsValid)
            {
                var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.DienThoai == model.DienThoai);

                if (khachHang != null)
                {
                    ModelState.AddModelError(string.Empty, "PhoneNumber already exists!");
                }
                else
                {
                    try
                    {
                        khachHang = _mapper.Map<KhachHang>(model);

                        khachHang.HieuLuc = true;

                        _context.Add(khachHang);
                        _context.SaveChanges();

                        return RedirectToAction("Success");
                    }
                    catch (Exception)
                    {
                        return View();
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ViewModels.Login model)
        {
            if (ModelState.IsValid)
            {
                var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.DienThoai == model.Phone);

                if (khachHang != null)
                {
                    if (khachHang.HieuLuc == false)
                    {
                        ModelState.AddModelError(string.Empty, "Account has been locked!");
                    }
                    else
                    {
                        if (khachHang.MatKhau != model.Password)
                        {
                            ModelState.AddModelError(string.Empty, "Login failed!");
                        }
                        else
                        {
                            var claims = new List<Claim> {
                                new(ClaimTypes.Name, khachHang.HoTen),
                                new(ClaimTypes.Role, khachHang.VaiTro.ToString()),
                                new("ID", khachHang.MaKh.ToString())
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(claimsPrincipal);

                            if (khachHang.VaiTro == 0)
                            {
                                return Redirect("/Admin/");
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed!");
                }
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }
    }
}
