
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Xamarin.Android.MatDesign
{
	public class Fragment2 : SupportFragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			View _View = inflater.Inflate(Resource.Layout.Fragment2, container, false);
			TextInputLayout InputLayoutPassword = _View.FindViewById<TextInputLayout>(Resource.Id.InputLayoutPassword);
			Button Btn = _View.FindViewById<Button>(Resource.Id.BtnLogin);
			string textInput = InputLayoutPassword.EditText.Text;

			Btn.Click += (sender, e) =>
			{
				if (InputLayoutPassword.EditText.Text != "1234")
				{
					InputLayoutPassword.Error = "Wrong Pass";
				}
			};

			return _View;
		}
	}
}
