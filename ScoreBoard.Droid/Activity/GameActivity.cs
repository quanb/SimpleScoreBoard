using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ScoreBoard.Core.Entity;

namespace ScoreBoard.Droid
{
    [Activity(Label = "ScoreBoard")]
    public class GameActivity : Activity
    {
        ListView _gameListView;
        GameViewAdapter _adapter;
        Game _game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "games" layout resource
            SetContentView(Resource.Layout.Game);
            Bundle extras = Intent.Extras;
            if (extras != null)
            {
                int _gameId = extras.GetInt("game_id");
                _game = GameData.Service.GetGame(_gameId);
                // set title
                //android_game_game_title
                // Description
                TextView d = FindViewById<TextView>(Resource.Id.gameTitle);
                d.Text = _game.Name;
                _gameListView = FindViewById<ListView>(Resource.Id.gamePlayerListView);
                _adapter = new GameViewAdapter(this, _game);
                _gameListView.Adapter = _adapter;
                _gameListView.ItemClick += (s, e) => { GamePlayerClicked((int)e.Id); };
            }
        }

        protected void GamePlayerClicked(int playerID)
        {
            // setup the intent to pass 
            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new AddPointDialog();
            dialogFragment.Dismissed += (s, e) => 
            {
                GamePlayer gplayer = _game.GetGamePlayerById(playerID);
                gplayer.Score += e.Score;
                GameData.Service.SaveGame(_game);
                _adapter.NotifyDataSetChanged();
            };
            dialogFragment.Show(transaction, "dialog_fragment");
        }

    }

    
}

