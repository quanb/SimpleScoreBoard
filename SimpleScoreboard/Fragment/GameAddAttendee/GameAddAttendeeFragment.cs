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
    public class GameAddAttendeeFragment : Android.Support.V4.App.Fragment
    {
        ListView _playersListView;
        GameAddAttendeeAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Games, container, false);
            _playersListView = view.FindViewById<ListView>(Resource.Id.listPlayer);
            _adapter = new GameAddAttendeeAdapter(this.Activity);
            _playersListView.Adapter = _adapter;

            Button button = view.FindViewById<Button>(Resource.Id.game_add_participants_start_game);
            button.Click += delegate { SaveGameAndPlayer(); };
            return view;
        }

        protected void SaveGameAndPlayer()
        {
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
                        PlayerAlias = GameData.PlayerService.Players[i].Name,
                        Score = 0
                    });
                }
            }
            Game game = new Game()
            {
                Name = Arguments.GetString("game_name"),
                Players = attendees
            };
            GameData.Service.SaveGame(game);
            //StartActivity(typeof(GamesActivity));

            var fragment = new GameFragment();
            Bundle bundle = new Bundle();
            bundle.PutInt("game_id", (int)game.Id);
            fragment.Arguments = bundle;

            var trans = Activity.SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, fragment, "GameFragment");
            trans.Commit();
        }
    }
}