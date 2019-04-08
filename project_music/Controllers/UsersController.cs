using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using project_music.Models;

namespace WebApplication1.Controllers
{

    public class UsersController : ApiController
    {
        //----------הכנסת המשתמש לרשימת המשתמשים בדאטה בייס----------------//
        [HttpPost]
        [Route("api/Users")]
        public void Post([FromBody]Users user)
        {
            user.Insert();
        }

        //----------הבאת המשתמש לפי הסיסמה והמייל שלו----------------//

        [HttpGet]
        [Route("api/Users")]
        public Users Get(string Email, string Password)
        {
            
            Users user = new Users();
            user = user.Login(Email, Password);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        //----------עדכון פרטי המשתמש----------------//

        [HttpPut]
        [Route("api/Users")]
        public void PUT([FromBody]Users user)
        {

            Users U = new Users();

            U.SaveChange(user);

        }





    }
}
