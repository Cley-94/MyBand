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
using My_Band.Models;
using My_Band.Activities;
using Newtonsoft.Json;
using Android.Views.InputMethods;

namespace My_Band
{
    [Activity(Label = "ActivitySignUp")]
    public class ActivitySignUp : Activity
    {

        private Button mBtnNext;
        private ImageView mBtnRegisterBack;
        private LinearLayout mLinearLayout;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Cadastro);

            
            mBtnNext = FindViewById<Button>(Resource.Id.btnNext);
            mBtnNext.Click += mBtnNext_Click;
            
            mLinearLayout = FindViewById<LinearLayout>(Resource.Id.RegisterLinearLayout);
            mLinearLayout.Click += mLinearLayout_Click;

            mBtnRegisterBack = FindViewById<ImageView>(Resource.Id.ivRegisterBack);
            mBtnRegisterBack.Click += mBtnRegisterBack_Click;

        }

        
        private void mLinearLayout_Click(object sender, EventArgs e)
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }

        private void mBtnNext_Click(object sender, EventArgs e)
        {
            string mEtEmail = FindViewById<EditText>(Resource.Id.etEmail).Text;
            string mEtUserName = FindViewById<EditText>(Resource.Id.etUserName).Text;
            string mEtPassword = FindViewById<EditText>(Resource.Id.etPassword).Text;
            string mEtConfirmPassword = FindViewById<EditText>(Resource.Id.etConfirmPassword).Text;

            TextView mTvRequiredEmail = FindViewById<TextView>(Resource.Id.tvRequiredEmail);
            TextView mTvRequiredUsername = FindViewById<TextView>(Resource.Id.tvRequiredUsername);
            TextView mTvRequiredPassword = FindViewById<TextView>(Resource.Id.tvRequiredPassword);
            TextView mTvRequiredConfirmPassword = FindViewById<TextView>(Resource.Id.tvRequiredConfirmPassword);
            bool v = true;

            if (String.IsNullOrEmpty(mEtEmail))
            {
                v = false;
                mTvRequiredEmail.Text = "Campo Obrigatório";
                mTvRequiredEmail.SetTextColor(Android.Graphics.Color.Red);
            }
            else
            {
                mTvRequiredEmail.Text = "";
            }


            if (String.IsNullOrEmpty(mEtUserName))
            {
                v = false;
                mTvRequiredUsername.Text = "Campo Obrigatório";
                mTvRequiredUsername.SetTextColor(Android.Graphics.Color.Red);
            }
            else
            {
                mTvRequiredUsername.Text = "";
            }


            if (String.IsNullOrEmpty(mEtPassword))
            {
                v = false;
                mTvRequiredPassword.Text = "Campo Obrigatório";
                mTvRequiredPassword.SetTextColor(Android.Graphics.Color.Red);
            }
            else
            {
                mTvRequiredPassword.Text = "";
            }


            if (String.IsNullOrEmpty(mEtConfirmPassword))
            {
                v = false;
                mTvRequiredConfirmPassword.Text = "Campo Obrigatório";
                mTvRequiredConfirmPassword.SetTextColor(Android.Graphics.Color.Red);
            }
            else
            {
                mTvRequiredConfirmPassword.Text = "";
            }


            if (mEtConfirmPassword != mEtPassword)
            {
                v = false;
                mTvRequiredConfirmPassword.Text = "Senhas Diferentes";
                mTvRequiredConfirmPassword.SetTextColor(Android.Graphics.Color.Red);
                return;
            }
            if (v == true)
            {
                UserModel user = new UserModel()
                {
                    Email = mEtEmail,
                    Name = mEtUserName,
                    Password = mEtPassword
                };

                Intent intent = new Intent(this, typeof(ActivitySignUpOptional));


                intent.PutExtra("user", JsonConvert.SerializeObject(user));
                this.StartActivity(intent);
            }
            
        }
        
        private void mBtnRegisterBack_Click(object sender, EventArgs e)
        {

            base.OnBackPressed();
            /*Intent intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);*/
        }
    }
}