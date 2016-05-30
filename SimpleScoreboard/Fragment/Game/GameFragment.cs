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
using SimpleScoreboard.Dialog;

namespace SimpleScoreboard
{
    public class GameFragment : Android.Support.V4.App.Fragment
    {
        ListView _gameListView;
        GameViewAdapter _adapter;
        Game _game;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Game, container, false);
            if (Arguments != null)
            {
                int _gameId = Arguments.GetInt("game_id");
                _game = GameData.Service.GetGame(_gameId);
                // set title
                //android_game_game_title
                // Description
                TextView d = view.FindViewById<TextView>(Resource.Id.gameTitle);
                d.Text = _game.Name;
                _gameListView = view.FindViewById<ListView>(Resource.Id.gamePlayerListView);
                _adapter = new GameViewAdapter(Activity, _game);
                _gameListView.Adapter = _adapter;
                _gameListView.ItemClick += (s, e) => { GamePlayerClicked((int)e.Id); };
            }

            return view;
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