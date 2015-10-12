using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ScoreBoard.Core.Entity;
using System.Collections.Generic;

namespace ScoreBoard.Droid
{
    [Activity(Label = "ScoreBoard")]
    public class PlayersActivity : Activity
    {
        ListView _playersListView;
        PlayersListViewAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "games" layout resource
            SetContentView(Resource.Layout.Players);

            _playersListView = FindViewById<ListView>(Resource.Id.playerListView);
            _adapter = new PlayersListViewAdapter(this);
            _playersListView.Adapter = _adapter;
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
                    string default_game_name = "Player";
                    AlertDialog.Builder alert1 = new AlertDialog.Builder(this);
                    EditText input = new EditText(this);
                    input.Text = default_game_name;
                    alert1.SetTitle("Player Name");
                    alert1.SetView(input);
                    alert1.SetPositiveButton("OK", delegate { AddPlayer(input.Text); });
                    alert1.SetNegativeButton("Cancel", (s, e) => { });
                    alert1.Show();
                    return true;

                case Resource.Id.actionRefresh:
                    GameData.PlayerService.RefreshCache();
                    _adapter.NotifyDataSetChanged();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected void AddPlayer(string playerName)
        {
            Player player = new Player();
            if(string.IsNullOrWhiteSpace(playerName))
                player.Name = "Player";
            else
                player.Name = playerName;
            GameData.PlayerService.SavePlayer(player);
        }
    }

    
}

