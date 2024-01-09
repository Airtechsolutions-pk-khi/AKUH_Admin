using BAL.Repositories;
using AKU_Admin._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin.BLL._Services
{
    public class loginService : baseService
    {
        loginDB _service;
        public loginService()
        {
            _service = new loginDB();
        }

        public LoginBLL GetAuthenticateUser(string username, string password)
        {
            try
            {
                return _service.GetAuthenticateUser( username,  password);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }
}
