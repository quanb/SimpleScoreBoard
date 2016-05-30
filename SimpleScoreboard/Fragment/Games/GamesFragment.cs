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

namespace SimpleScoreboard
{
    public class GamesFragment : Android.Support.V4.App.Fragment
    {
        ListView _gameListView;
        GameListViewAdapter _adapter;
        private FrameLayout _fragmentContainer;

        public GamesFragment(FrameLayout fragmentContainer)
        {
            _fragmentContainer = fragmentContainer;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Games, container, false);

            _gameListView = view.FindViewById<ListView>(Resource.Id.gameListView);
            _adapter = new GameListViewAdapter(this.Activity);
            _gameListView.Adapter = _adapter;
            _gameListView.ItemClick += GameClicked;

            return view;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionNew:
                    string default_game_name = "Game";
                    AlertDialog.Builder alert1 = new AlertDialog.Builder(this.Activity);
                    EditText input = new EditText(this.Activity);
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

            var fragment = new GameAddAttendeeFragment();
            Bundle bundle = new Bundle();
            bundle.PutString("game_name", gameName);
            bundle.PutString("listType", "players");
            fragment.Arguments = bundle;

            var trans = Activity.SupportFragmentManager.BeginTransaction();
            trans.Add(_fragmentContainer.Id, fragment, "GameAddAttendee");
            trans.Commit();
        }

        protected void GameClicked(object sender, ListView.ItemClickEventArgs e)
        {
            var fragment = new GameFragment();
            Bundle bundle = new Bundle();
            bundle.PutInt("game_id", (int)e.Id);
            fragment.Arguments = bundle;

            var activity = Activity as MainActivity;
            //activity.ShowFragment(fragment);
            var trans = Activity.SupportFragmentManager.BeginTransaction();
            trans.Add(_fragmentContainer.Id, fragment, "GameFragment");
            trans.Hide(this);
            trans.AddToBackStack(null);
            activity.StackFragments.Push(this);
            trans.Commit();
            // setup the intent to pass 
            //Intent gameIntent = new Intent(this, typeof(GameActivity));
            //gameIntent.PutExtra("game_id", (int)e.Id);
            //StartActivity(gameIntent);
        }

    }
}