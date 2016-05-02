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
    [Activity(Label = "GamesListActivity")]
    public class GamesListActivity : Activity
    {
        private FrameLayout _FragmentContainer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView (Resource.Layout.NewGames);
            _FragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragmentContainer);
            var trans = FragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, new GamesFragment(), "GamesFragment");
            trans.Commit();
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
                    //_adapter.NotifyDataSetChanged();
                    return true;

                case Resource.Id.actionRefresh:
                    GameData.Service.RefreshCache();
                    //_adapter.NotifyDataSetChanged();
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
    }
}