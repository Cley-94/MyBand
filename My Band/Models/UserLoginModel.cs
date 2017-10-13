using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace My_Band.Models
{
    public class UserLoginModel
    {
        public String EmailLogin { get; set; }
        public String PasswordLogin { get; set; }
    }
}