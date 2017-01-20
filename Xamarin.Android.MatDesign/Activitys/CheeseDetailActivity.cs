
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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using SupportToolBar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.App;

namespace Xamarin.Android.MatDesign
{
	[Activity(Label = "CheeseDetailActivity",Theme="@style/Theme.DesignDemo")]
	public class CheeseDetailActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.CheeseDetail);

			SupportToolBar _ToolBar = FindViewById<SupportToolBar>(Resource.Id.DetailToolBar);
			SetSupportActionBar(_ToolBar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);

			var CheeseName = Intent.GetStringExtra("Sir");

			CollapsingToolbarLayout Collaps = FindViewById<CollapsingToolbarLayout>(Resource.Id.CollapsBarLayout);
			Collaps.Title = CheeseName;


			LoadBackDrop();

			// Create your application here
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case global::Android.Resource.Id.Home:
					Finish();
					return true;
				default:
					return base.OnOptionsItemSelected(item);
					
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.ActionMenu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		void LoadBackDrop()
		{
			ImageView img = FindViewById<ImageView>(Resource.Id.Img);
			img.SetImageResource(Fragment1.Cheeses.RandomCheeseDrawable);
		}
}
}
