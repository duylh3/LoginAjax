using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ideal.Models;
using ideal.ViewModels;
using ideal.Helper;

namespace ideal.Repository
{
    public class UserRepository
    {
        private IdealDbEntities _db = new IdealDbEntities();

        public UserView Login(LoginView model)
        {
            try
            {
                var item = (from u in _db.UserLogin(model.Username, model.Password)
                            select new UserView()
                            {
                                UserId = u.Id,
                                Username = u.Username
                            }).FirstOrDefault();
                return item;
            }
            catch (Exception ex)
            {
                var Err = ex.Message;
                return null;
            }
        }

        public string Insert(string username, string password, int createdBy)
        {
            try
            {
                _db.InsertUser(username, ED5Helper.Encrypt(password), createdBy);
                return "Insert success!!";
            }catch(Exception ex)
            {
                var Err = ex.InnerException;
                return "Fail nha!";
            }
        }

    }
}