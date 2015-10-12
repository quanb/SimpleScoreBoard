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

namespace ScoreBoard.Droid
{
	public class PlayersListViewAdapter : BaseAdapter<Player>
	{
		private readonly Activity _context;

		public PlayersListViewAdapter(Activity context)
		{
			_context = context;
		}

		public override int Count
		{
			get { return  GameData.PlayerService.Players.Count; }
		}

		public override long GetItemId(int position)
		{
			return GameData.PlayerService.Players[position].Id.Value;
		}

		public override Player this[int position]
		{
			get { return GameData.PlayerService.Players[position]; }
		}
		
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
				view = _context.LayoutInflater.Inflate(Resource.Layout.PlayerListItem, null);

            Player player = GameData.PlayerService.Players[position];
			view.FindViewById<TextView> (Resource.Id.nameTextView).Text = player.Name;

			return view;
		}
	}
}