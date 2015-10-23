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
            _gameListView.ItemClick += GameClicked;
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
                    alert1.SetNegativeButton("Cancel", (s, e) => { });
                    alert1.Show();
                    _adapter.NotifyDataSetChanged();
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
            //Game game = new Game();
            //if(string.IsNullOrWhiteSpace(gameName))
            //    game.Name = "Game";
            //else
            //    game.Name = gameName;
            //GameData.Service.SaveGame(game);
            if (string.IsNullOrWhiteSpace(gameName))
                gameName = "Game";

            Intent participants = new Intent(this, typeof(GameAddAttendeeActivity));
            participants.PutExtra("game_name", gameName);
            participants.PutExtra("listType", "players");
            StartActivity(participants);
        }

        protected void GameClicked(object sender, ListView.ItemClickEventArgs e)
        {
            // setup the intent to pass 
            Intent gameIntent = new Intent(this, typeof(GameActivity));
            gameIntent.PutExtra("game_id", (int)e.Id);
            StartActivity(gameIntent);
        }
    }
}

