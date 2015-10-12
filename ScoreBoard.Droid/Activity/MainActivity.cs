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

namespace ScoreBoard.Droid
{
    [Activity(Label = "MainActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            // Load ListView and listen to clicks
            String[] options = new String[] { "Players", "Games"};
            ListView lv = (ListView)FindViewById(Resource.Id.MainListView);
            ArrayAdapter<String> myListsAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, options);
            lv.Adapter = myListsAdapter;
            lv.ItemClick += lv_ItemClick;
        }

        protected void lv_ItemClick(object sender, ListView.ItemClickEventArgs e)
        {
            //cast the view
            TextView wordView = (TextView)e.View;
            //retrieve the chosen word
            String selection = wordView.Text;

            switch (selection)
            {
                case "Players":
                    StartActivity(typeof(PlayersActivity));
                    break;
                case "Games":
                    StartActivity(typeof(GamesActivity));
                    break;
                default:
                    Toast.MakeText(this, "Unrecognized Command", ToastLength.Short).Show();
                    break;
            }
                

        }
    }
}