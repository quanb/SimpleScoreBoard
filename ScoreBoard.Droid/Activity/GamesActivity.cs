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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.GamesListViewMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionNew:
                    string default_game_name = "Game";
                    AlertDialog.Builder alert1 = new AlertDialog.Builder(this);
                    EditText input = new EditText(this);
                    input.Text = default_game_name;
                    alert1.SetTitle("Game Name");
                    alert1.SetView(input);
                    alert1.SetPositiveButton("OK", delegate { AddGame(input.Text); });
                    alert1.Show();
                    return true;

                case Resource.Id.actionRefresh:
                    GameData.Service.RefreshCache();
                    _adapter.NotifyDataSetChanged();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected void AddGame(string gameName)
        {
            Game game = new Game();
            if(string.IsNullOrWhiteSpace(gameName))
                game.Name = "Game";
            else
                game.Name = gameName;
            GameData.Service.SaveGame(game);
        }
    }

    
}

