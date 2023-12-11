using EkstremHava.Data.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EkstremHava.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
    }
}
