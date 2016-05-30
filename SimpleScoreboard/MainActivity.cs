using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;
using System.Collections.Generic;

namespace SimpleScoreboard
{
    [Activity(Label = "SimpleScoreboard", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity : ActionBarActivity
    {
        private SupportToolbar _toolbar;
        private SupportToolbar mStandAloneToolbar;
        //private Button mBtnToolbar;
        private SupportFragment _currentFragment;
        private FrameLayout _fragmentContainer;
        private GamesFragment _gamesFragment1;
        private PlayersFragment _playersFragment2;
        private Stack<SupportFragment> _stackFragments;
        private LinearLayout _btnGames, _btnPlayers;

        public Stack<SupportFragment> StackFragments
        {
            get
            {
                return _stackFragments;
            }
            set
            {
                _stackFragments = value;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mStandAloneToolbar = FindViewById<SupportToolbar>(Resource.Id.standalone_toolbar);
            _fragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragmentContainer);
            //mBtnToolbar = FindViewById<Button>(Resource.Id.btnToolbar);
            _btnGames = FindViewById<LinearLayout>(Resource.Id.btnGames);
            _btnPlayers = FindViewById<LinearLayout>(Resource.Id.btnPlayers);

            _stackFragments = new Stack<SupportFragment>();

            SetSupportActionBar(_toolbar);
            SupportActionBar.Title = "Yay Yay Yay!";

            //mStandAloneToolbar.InflateMenu(Resource.Menu.toolbox_menu);
            mStandAloneToolbar.MenuItemClick += mStandAloneToolbar_MenuItemClick;

            //mBtnToolbar.Click += mBtnToolbar_Click;

            _gamesFragment1 = new GamesFragment(_fragmentContainer);
            _playersFragment2 = new PlayersFragment();


            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, _playersFragment2, "Fragment3");
            trans.Hide(_playersFragment2);

            trans.Add(Resource.Id.fragmentContainer, _gamesFragment1, "Fragment1");
            trans.Commit();
            _currentFragment = _gamesFragment1;

            _btnPlayers.FocusChange += (s, e) =>
            {
                if (e.HasFocus)
                {
                    ShowFragment(_playersFragment2);
                }
            };

            _btnGames.FocusChange += (s, e) =>
            {
                if (e.HasFocus)
                {
                    ShowFragment(_gamesFragment1);
                }
            };
        }

        void mBtnToolbar_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Awesome");
        }

        void mStandAloneToolbar_MenuItemClick(object sender, SupportToolbar.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.action_edit:

                    Console.WriteLine("Edit");
                    break;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    switch (item.ItemId)
        //    {

        //    }
        //    return base.OnOptionsItemSelected(item);
        //}

        public override void OnBackPressed()
        {

            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                SupportFragmentManager.PopBackStack();
                _currentFragment = _stackFragments.Pop();
            }

            else
            {
                base.OnBackPressed();
            }
        }

        public void ShowFragment(SupportFragment fragment)
        {
            if (fragment.IsVisible)
            {
                return;
            }

            var trans = SupportFragmentManager.BeginTransaction();

            trans.SetCustomAnimations(Resource.Animation.slide_in, Resource.Animation.slide_out, Resource.Animation.slide_in, Resource.Animation.slide_out);

            fragment.View.BringToFront();
            _currentFragment.View.BringToFront();

            trans.Hide(_currentFragment);
            trans.Show(fragment);

            trans.AddToBackStack(null);
            _stackFragments.Push(_currentFragment);
            trans.Commit();

            _currentFragment = fragment;
        }

    }
}

