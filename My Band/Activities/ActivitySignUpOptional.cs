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
using Newtonsoft.Json;
using System.IO;
using Android.Views.InputMethods;
using My_Band.DataService;

namespace My_Band.Activities
{
    [Activity(Label = "ActivitySignUpOptional")]
    public class ActivitySignUpOptional : Activity
    {
        private DataServiceAPI dataService;
        private UserModel mUser;
        private AutoCompleteTextView mActvGenres;
        private string mEtState;
        private string mEtCity;
        private string mEtPhone;
        private string mEtAbout;
        private Button mBtnSubmit;
        private LinearLayout mLinearLayout;
        private ImageView mBtnRegisterOptionalBack;
        private TextView mTvSkip;

        public ActivitySignUpOptional()
        {
            dataService = new DataServiceAPI();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CadastroOpcional);

            mTvSkip = FindViewById<TextView>(Resource.Id.tvSkip);
            mTvSkip.Click += mTvSkip_Click;

            mBtnSubmit = FindViewById<Button>(Resource.Id.btnRegisterSubmit);
            mActvGenres = FindViewById<AutoCompleteTextView>(Resource.Id.actvGenres);

            mBtnRegisterOptionalBack = FindViewById<ImageView>(Resource.Id.ivRegisterOptionalBack);
            mBtnRegisterOptionalBack.Click += mBtnRegisterOptionalBack_Click;

            Stream seedDataStream = Assets.Open(@"WordList.txt");
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(seedDataStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            string[] wordlist = lines.ToArray();
            ArrayAdapter dictionaryAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, wordlist);
            mActvGenres.Adapter = dictionaryAdapter;

            mLinearLayout = FindViewById<LinearLayout>(Resource.Id.RegisterLinearLayout);
            mLinearLayout.Click += mLinearLayout_Click;

            mBtnSubmit.Click += mBtnSubmit_Click;

        }

        private async void mTvSkip_Click(object sender, EventArgs e)
        {

            mUser = new UserModel();

            mUser = JsonConvert.DeserializeObject<UserModel>(Intent.GetStringExtra("user"));

            mUser.State = mEtState;
            mUser.City = mEtCity;
            mUser.Phone = mEtPhone;
            mUser.About = mEtAbout;

            Intent intent = new Intent(this, typeof(ActivityMainView));

            intent.PutExtra("mUser", JsonConvert.SerializeObject(mUser));

            var result = await dataService.AddUsersAsync(mUser);

            if (result == true)
            {
                this.StartActivity(intent);
                this.Finish();
            }
            else
            {

            }
        }

        private void mLinearLayout_Click(object sender, EventArgs e)
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }

        private async void mBtnSubmit_Click(object sender, EventArgs e)
        {

            mEtState = FindViewById<EditText>(Resource.Id.etState).Text;
            mEtCity = FindViewById<EditText>(Resource.Id.etCity).Text;
            mEtPhone = FindViewById<EditText>(Resource.Id.etPhone).Text;
            mEtAbout = FindViewById<EditText>(Resource.Id.etAbout).Text;

            mUser = new UserModel();

            mUser = JsonConvert.DeserializeObject<UserModel>(Intent.GetStringExtra("user"));

            mUser.State = mEtState;
            mUser.City = mEtCity;
            mUser.Phone = mEtPhone;
            mUser.About = mEtAbout;



            Intent intent = new Intent(this, typeof(ActivityMainView));

            intent.PutExtra("mUser", JsonConvert.SerializeObject(mUser));

            var result = await dataService.AddUsersAsync(mUser);

            if (result == true)
            {
                UserLoginModel userLogin = new UserLoginModel()
                {
                    username = mUser.Name,
                    password = mUser.Password
                };

                var token = await dataService.PostLogin(userLogin);

                if (token != null)
                {
                    intent.PutExtra("token", JsonConvert.SerializeObject(token));
                    var user = await dataService.FindByName(userLogin.username, token);
                    if (user != null)
                    {
                        intent.PutExtra("user", JsonConvert.SerializeObject(user));
                        this.StartActivity(intent);
                        this.Finish();
                    }
                }
            }
            else
            {

            }

        }
        private void mBtnRegisterOptionalBack_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            /*Intent intent = new Intent(this, typeof(ActivitySignUp));
            this.StartActivity(intent);*/
        }
    }
}