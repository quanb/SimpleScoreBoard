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
using ScoreBoard.Core.Entity;

namespace SimpleScoreboard
{ 
    class GameViewAdapter : BaseAdapter<GamePlayer>
    {
        private readonly Activity _context;

        private Game _game;

        public GameViewAdapter(Activity context, Game g)
        {
            _context = context;
            _game = g;
        }

        public override int Count
        {
            get { return _game.Players.Count; }
        }

        public override long GetItemId(int position)
        {
            return _game.Players[position].PlayerId;
        }

        public override GamePlayer this[int position]
        {
            get { return _game.Players[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.GameItem, null);

            GamePlayer gp = _game.Players[position];
            view.FindViewById<TextView>(Resource.Id.playerName).Text = gp.PlayerAlias;
            view.FindViewById<TextView>(Resource.Id.playerScore).Text = gp.Score.ToString();

            return view;
        }
    }
}