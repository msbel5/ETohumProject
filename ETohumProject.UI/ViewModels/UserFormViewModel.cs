using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETohumProject.DAL.Models;

namespace ETohumProject.UI.ViewModels
{
    public class UserFormViewModel
    {
        public User User { get; set; }

        public string Title
        {
            get
            {
                if (User != null && User.Id != 0)
                    return "Edit User";

                return "New User";

            }
        }
    }
}