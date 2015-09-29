using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ScoreBoard.Droid
{
    [Activity(Label = "ScoreBoard", MainLauncher = true, Icon = "@drawable/icon")]
    public class GamesActivity : Activity
    {
        ListView _gameListView;
        GameListViewAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "games" layout resource
            SetContentView(Resource.Layout.Games);

            _gameListView = FindViewById<ListView>(Resource.Id.gameListView);
            _adapter = new GameListViewAdapter(this);
            _gameListView.Adapter = _adapter;
        }
    }
}

