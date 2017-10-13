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

namespace My_Band
{
    public class UserProfileFragment : Android.Support.V4.App.Fragment
    {

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment 
            View view = inflater.Inflate(Resource.Layout.UserProfileLayout, container, false);
            Button mBtnEditProfile = view.FindViewById<Button>(Resource.Id.btnAddBand);

            mBtnEditProfile.Click += mBtnEditProfile_Click;

            return view;
        }

        private void mBtnEditProfile_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(Activities.ActivityAddBand));
            this.StartActivity(intent);

        }
    }
}