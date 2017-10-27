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
using My_Band.Activities;
using Newtonsoft.Json;
using My_Band.Models;

namespace My_Band
{
    [Activity(Label = "ActivityMainView")]
    public class ActivityMainView : AppCompatActivity //Activity
    {
        DrawerLayout drawerLayout;
        TabLayout tabLayout;
        //Button mBtnAddBand;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var token = JsonConvert.DeserializeObject<TokenModel>(Intent.GetStringExtra("token"));
            var user = JsonConvert.DeserializeObject<UserModel>(Intent.GetStringExtra("user"));

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Initialize toolbar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            tabLayout = FindViewById<TabLayout>(Resource.Id.sliding_tabsIcon);


            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            FnInitTabLayout(token, user);
            //Load default screen
            /*var ft = SupportFragmentManager.BeginTransaction();
            ft.AddToBackStack(null);
            ft.Add(Resource.Id.FrameLayout, new IconTextCallFragment());
            ft.Commit();*/

            //Trata os eventos dos cliques editar perfil
            /*mBtnAddBand = FindViewById<Button>(Resource.Id.btnAddBand);
            mBtnAddBand.Click += mBtnEditProfile_Click;*/

        }
        //click do botão inserir banda
        private void mBtnEditProfile_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityAddBand));
            this.StartActivity(intent);
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    // React on 'nav_home' selection
                    break;
                case (Resource.Id.nav_messages):
                    //
                    break;
                case (Resource.Id.nav_friends):
                    // React on 'Friends' selection
                    break;
            }
            // Close drawer
            drawerLayout.CloseDrawers();
        }

        void FnInitTabLayout(TokenModel token, UserModel user)
        {
            tabLayout.SetTabTextColors(Android.Graphics.Color.Aqua, Android.Graphics.Color.AntiqueWhite);
            //Fragment array
            var fragments = new Android.Support.V4.App.Fragment[]
            {
                new HomeFragment(),
                new NotificationsFragment(),
                new UserProfileFragment(),
            };
            Bundle args = new Bundle();
            args.PutString("Token", JsonConvert.SerializeObject(token));
            //args.PutString("Token_Type", token.Token_Type);
            //args.PutString("Refresh_Token", token.Refresh_Token);
            //args.PutString("Access_Token", token.Access_Token);
            //args.PutString("Expires_In", token.Expires_In);
            args.PutString("user", JsonConvert.SerializeObject(user));
            //passing token to all the fragments
            int c = fragments.Count();
            for(int i = 0; i < c; i++)
            {
                fragments[i].Arguments = args;
            }
            
            FragmentManager.BeginTransaction()
                .AddToBackStack(null)
                .Commit();
            //Tab title array
            var titles = CharSequence.ArrayFromStringArray(new[] {
                GetString(Resource.String.strCall),
                GetString(Resource.String.strMessage),
                GetString(Resource.String.strData),
            });
            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpagerIcon);
            //viewpager holding fragment array and tab title text
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            // Give the TabLayout the ViewPager 
            tabLayout.SetupWithViewPager(viewPager);
            
            //tabLayout.SetTabTextColors(
            FnSetIcons();
            //FnSetupTabIconsWithText ();

        }
        void FnSetIcons()
        {
            tabLayout.GetTabAt(0).SetIcon(Resource.Drawable.ic_home);
            tabLayout.GetTabAt(1).SetIcon(Resource.Drawable.ic_notificationt);
            tabLayout.GetTabAt(2).SetIcon(Resource.Drawable.ic_usert);
        }
    }
}