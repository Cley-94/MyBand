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
    public class TokenModel
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public string Expires_In { get; set; }
        public string Refresh_Token { get; set; }
    }
}