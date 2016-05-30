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

namespace SimpleScoreboard.Dialog
{

        public class AddPointDialogEventArgs : EventArgs
        {
            public int Score { get; set; }
        }

        public delegate void DialogEventHandler(object sender, AddPointDialogEventArgs args);

        public class AddPointDialog : Android.Support.V4.App.DialogFragment
        {
            public event DialogEventHandler Dismissed;

            Button _btnPlus;
            Button _btnMinus;
            Button _btnOK;
            Button _btnCancel;
            TextView _txtScore;

            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                this.Cancelable = false;
            }

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                // Inflate the custom dialog view
                View view = inflater.Inflate(Resource.Layout.AddPoint, container, false);
                this.Dialog.SetTitle("Score in turn");
                // Remove the title bar from the dialog
                //this.Dialog.RequestWindowFeature((int)DialogFragmentStyle.NoTitle);
                // remove the fading background
                //this.Dialog.Window.ClearFlags(WindowManagerFlags.DimBehind);

                _txtScore = view.FindViewById<TextView>(Resource.Id.textScore);

                _btnPlus = view.FindViewById<Button>(Resource.Id.plusButton);
                _btnPlus.Click += delegate { PlusPoint(); };

                _btnMinus = view.FindViewById<Button>(Resource.Id.minusButton);
                _btnMinus.Click += delegate { MinusPoint(); };

                _btnOK = view.FindViewById<Button>(Resource.Id.okButton);
                _btnOK.Click += BtnOkClick;

                _btnCancel = view.FindViewById<Button>(Resource.Id.cancelButton);
                _btnCancel.Click += (sender, args) => Dismiss();

                return view;
            }

            protected void PlusPoint()
            {
                int point = 0;
                string current_score = _txtScore.Text;
                point = int.Parse(current_score) + 1;
                _txtScore.Text = point.ToString();
            }

            protected void MinusPoint()
            {
                int point = 0;
                string current_score = _txtScore.Text;
                point = int.Parse(current_score) - 1;
                _txtScore.Text = point.ToString();
            }

            void BtnOkClick(object sender, EventArgs e)
            {
                if (null != Dismissed)
                    Dismissed(this, new AddPointDialogEventArgs { Score = int.Parse(_txtScore.Text) });
                this.Dismiss();
            }

        }

}