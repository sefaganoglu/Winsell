using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Winsell.Hopi.API.Models;

namespace Winsell.Hopi.API.Controllers
{
    [Route("api/[controller]")]
    public class HopiController : Controller
    {

        [HttpGet("masagetir")]
        public List<Masa> GetTableList()
        {

            return new List<Masa>() { new Masa() { masaNo = 1 }, new Masa() { masaNo = 2 }, new Masa() { masaNo = 3 } };
        }
        
        [HttpGet("masagetir2")]
        public List<Masa> GetTableList2()
        {

            return new List<Masa>() { new Masa() { masaNo = 4 }, new Masa() { masaNo = 5 }, new Masa() { masaNo = 6 } };
        }
    }
}
