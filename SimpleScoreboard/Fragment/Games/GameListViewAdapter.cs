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
    public class GameListViewAdapter : BaseAdapter<Game>
    {
        private readonly Activity _context;

        public GameListViewAdapter(Activity context)
        {
            _context = context;
        }

        public override int Count
        {
            get { return GameData.Service.Games.Count; }
        }

        public override long GetItemId(int position)
        {
            return GameData.Service.Games[position].Id.Value;
        }

        public override Game this[int position]
        {
            get { return GameData.Service.Games[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.GameListItem, null);

            Game game = GameData.Service.Games[position];
            view.FindViewById<TextView>(Resource.Id.lblGameName).Text = game.Name;

            return view;
        }
    }
}