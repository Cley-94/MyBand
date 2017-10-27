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
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;
using My_Band.DataService;
using My_Band.Models;
using Newtonsoft.Json;

namespace My_Band
{
    [Activity(Label = "My Band Altogether", MainLauncher = true , Icon ="@drawable/mybandicon")]
    public class MainActivity : Activity 
    {
        DataServiceAPI dataService;
        private Button mBtnLogIn;
        private Button mBtnSignUp;
        private string mEtEmail;
        private string mEtPassword;
        private TextView mTvErrorLogin;

        public MainActivity()
        {
            dataService = new DataServiceAPI();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Index);
            mBtnLogIn = FindViewById<Button>(Resource.Id.btnLogIn);
            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignIn);

            mBtnLogIn.Click += mBtnLogIn_Click;
            mBtnSignUp.Click += mBtnSignUp_Click;
        }

        private void mBtnSignUp_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivitySignUp));
            this.StartActivity(intent);
        }

        private async void mBtnLogIn_Click(object sender, EventArgs e)
        {
            mEtEmail = FindViewById<EditText>(Resource.Id.etEmailLogin).Text;
            mEtPassword = FindViewById<EditText>(Resource.Id.etPasswordLogin).Text;
            mTvErrorLogin = FindViewById<TextView>(Resource.Id.tvErrorLogin);

            if (!string.IsNullOrEmpty(mEtEmail) || !string.IsNullOrEmpty(mEtPassword))
            {
                UserLoginModel userLogin = new UserLoginModel() {
                    username = mEtEmail,
                    password = mEtPassword
                };
                
                var token = await dataService.PostLogin(userLogin);
                //bool result = true;
                if (token != null)
                {
                    mTvErrorLogin.Text = "";
                    Intent intent = new Intent(this, typeof(ActivityMainView));
                    intent.PutExtra("token", JsonConvert.SerializeObject(token));
                    var user = await dataService.FindByName(userLogin.username, token);
                    if(user != null)
                    {
                        intent.PutExtra("user", JsonConvert.SerializeObject(user));
                        this.StartActivity(intent);
                        this.Finish();
                    }
                }
                else
                    mTvErrorLogin.Text = "Email ou senha incorretos";
            }
            else
                mTvErrorLogin.Text = "Preencha os dois campos";
        }
    }
}

