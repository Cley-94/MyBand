using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using ImageViews.Rounded;

namespace My_Band
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment 
            View view = inflater.Inflate(Resource.Layout.HomeLayout, container, false);
            /*RatingBar mRbRank1 = view.FindViewById<RatingBar>(Resource.Id.rbRank1);
            RatingBar mRbRank2 = view.FindViewById<RatingBar>(Resource.Id.rbRank2);
            RatingBar mRbRank3 = view.FindViewById<RatingBar>(Resource.Id.rbRank3);
            RatingBar mRbRank4 = view.FindViewById<RatingBar>(Resource.Id.rbRank4);
            RatingBar mRbRank5 = view.FindViewById<RatingBar>(Resource.Id.rbRank5);
            mRbRank1.Rating = 5f;
            mRbRank2.Rating = 4.7f;
            mRbRank3.Rating = 4.5f;
            mRbRank4.Rating = 4.2f;
            mRbRank5.Rating = 1.1f;
            */

            var btnCard = view.FindViewById<RelativeLayout>(Resource.Id.rlCardView);
            btnCard.Click += mBtnExploreWeekBands;

            var btnExplore = view.FindViewById<Button>(Resource.Id.btnExplore);
            btnExplore.Click += mBtnExploreWeekBands;
            
            return view;
        }

        private void mBtnExploreWeekBands(object sender, EventArgs e)
        {
            Intent intent = new Intent(Context, typeof(MainActivity));
            this.StartActivity(intent);
        }
    }
}