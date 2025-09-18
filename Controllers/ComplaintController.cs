using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace P1WEBMVC.Controllers;
// public class ComplaintController : Controller
// {
    // private static List<Complaint> _complaints = new List<Complaint>();

    // Display form to submit complaints
    public class ComplaintController : Controller
{
    public IActionResult Submit()
    {
        return View();
    }


}

