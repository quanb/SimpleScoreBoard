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

namespace ScoreBoard.Droid
{
    public class GamesFragment : Fragment
    {
        ListView _gameListView;
        GameListViewAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Games, container, false);

            _gameListView = view.FindViewById<ListView>(Resource.Id.gameListView);
            _adapter = new GameListViewAdapter(this.Activity);
            _gameListView.Adapter = _adapter;
            _gameListView.ItemClick += GameClicked;

            return view;
        }

        protected void GameClicked(object sender, ListView.ItemClickEventArgs e)
        {
            // setup the intent to pass 
            //Intent gameIntent = new Intent(this, typeof(GameActivity));
            //gameIntent.PutExtra("game_id", (int)e.Id);
            //StartActivity(gameIntent);


        }
    }
}