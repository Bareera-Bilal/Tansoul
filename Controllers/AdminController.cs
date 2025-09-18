using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1WEBMVC.Interfaces;
using P1WEBMVC.Data;
using P1WEBMVC.Models.DomainModels;
using P1WEBMVC.Types;
using P1WEBMVC.Models;

namespace P1WEBMVC.Controllers
{
    public class AdminController : Controller
    {

        private readonly SqlDbContext sqlDbContext;
        private readonly ITokenService tokenService;
       
        public AdminController(ITokenService tokenService, SqlDbContext sqlDbContext)
        {
            this.tokenService = tokenService;
            this.sqlDbContext = sqlDbContext;
           
        }


    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Doctor()
    {
        return View();
    }

    public IActionResult AdminDashboard()
    {
        return View();
    }

   
    }
}
