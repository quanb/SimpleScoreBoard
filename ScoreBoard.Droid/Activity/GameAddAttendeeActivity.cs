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
    public class GameAddAttendeeActivity : Activity
    {
        ListView _playersListView;
        GameAddAttendeeAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "games" layout resource
            SetContentView(Resource.Layout.GameAddAttendee);

            _playersListView = FindViewById<ListView>(Resource.Id.listPlayer);
            _adapter = new GameAddAttendeeAdapter(this);
            _playersListView.Adapter = _adapter;

            Button button = FindViewById<Button>(Resource.Id.game_add_participants_start_game);
            button.Click += delegate { SaveGameAndPlayer(); };
                
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

        protected void SaveGameAndPlayer()
        {
            Bundle extras = Intent.Extras;

            // Create an array to store player into
            List<GamePlayer> attendees = new List<GamePlayer>();
            int listItemCount = _playersListView.ChildCount;
            for (int i = 0; i < listItemCount; i++)
            {
                CheckBox cbox = (_playersListView.GetChildAt(i)).FindViewById<CheckBox>(Resource.Id.playerCheckbox);
                if (cbox.Checked)
                {
                    attendees.Add(new GamePlayer()
                    {
                        PlayerId = (int)GameData.PlayerService.Players[i].Id,
                        PlayerAlias = "",
                        Score = 0
                    });
                }
            }
            Game game = new Game()
            {
                Name = Intent.Extras.GetString("game_name"),
                Players = attendees
            };
            GameData.Service.SaveGame(game);
            StartActivity(typeof(GamesActivity));
        }
    }

    
}

