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
using ScoreBoard.Core.Entity;

namespace SimpleScoreboard
{
    public class PlayersFragment : Android.Support.V4.App.Fragment
    {
        ListView _playersListView;
        PlayersListViewAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Players, container, false);

            _playersListView = view.FindViewById<ListView>(Resource.Id.playerListView);
            _adapter = new PlayersListViewAdapter(this.Activity);
            _playersListView.Adapter = _adapter;

            return view;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionNew:
                    string default_game_name = "Player";
                    AlertDialog.Builder alert1 = new AlertDialog.Builder(this.Activity);
                    EditText input = new EditText(this.Activity);
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
            if (string.IsNullOrWhiteSpace(playerName))
                player.Name = "Player";
            else
                player.Name = playerName;
            GameData.PlayerService.SavePlayer(player);
        }
    }
}