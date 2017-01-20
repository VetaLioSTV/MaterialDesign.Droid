using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using SupportToolBar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using Android.Support.Design.Widget;
using System;
using Android.Support.V4.View;
using Android.Support.V4.App;
using System.Collections.Generic;
using Android.Views;
using Java.Lang;
using Adr = Android.Resource;

namespace Xamarin.Android.MatDesign
{
		[Activity(Label = "Xamarin.Android.MatDesign", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
		public class MainActivity : AppCompatActivity
		{
			int count;
			DrawerLayout _DrawerLayout;

			protected override void OnCreate(Bundle savedInstanceState)
			{
				base.OnCreate(savedInstanceState);

				// Set our view from the "main" layout resource
				SetContentView(Resource.Layout.Main);

				// Get our button from the layout resource,
				// and attach an event to it

				SupportToolBar ToolBar = FindViewById<SupportToolBar>(Resource.Id.ToolBar);
				SetSupportActionBar(ToolBar);


				SupportActionBar _ActionBar = SupportActionBar;
				_ActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
				_ActionBar.SetDisplayHomeAsUpEnabled(true);

				_DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

				NavigationView NavigationView = FindViewById<NavigationView>(Resource.Id.NavView);

				if (NavigationView != null)
					SetUpDrawerContent(NavigationView);

				TabLayout Tabs = FindViewById<TabLayout>(Resource.Id.TabLayout);
				ViewPager _ViewPager = FindViewById<ViewPager>(Resource.Id.ViewPager);

				SetUpViewPager(_ViewPager);

				Tabs.SetupWithViewPager(_ViewPager);

				FloatingActionButton FloatButton = FindViewById<FloatingActionButton>(Resource.Id.FloatBtn);

				FloatButton.Click += (sender, e) =>
				{
					View Anchor = sender as View;
					Snackbar.Make(Anchor, "Snack Bar", Snackbar.LengthLong).SetAction("Action", v =>
					{
					//Intent siuda
				}).Show();

				};
			}

			void SetUpDrawerContent(NavigationView navigationView)
			{
				navigationView.NavigationItemSelected += (sender, e) =>
				{
					e.MenuItem.SetChecked(true);
					_DrawerLayout.CloseDrawers();
				};
			}

			void SetUpViewPager(ViewPager _ViewPager)
			{
				TabAdapter Adapter = new TabAdapter(SupportFragmentManager);
				Adapter.AddFragment(new Fragment1(), "Fragment1");
				Adapter.AddFragment(new Fragment2(), "Fragment2");
				Adapter.AddFragment(new Fragment3(), "Fragment3");

				_ViewPager.Adapter = Adapter;
			}

			public override bool OnOptionsItemSelected(IMenuItem item)
			{
				switch (item.ItemId)
				{
				case global::Android.Resource.Id.Home:
					_DrawerLayout.OpenDrawer(global::Android.Support.V4.View.GravityCompat.Start); //(int)GravityFlags.Left);
						return true;
					default:
						return base.OnOptionsItemSelected(item);

				}
			}

			class TabAdapter : FragmentPagerAdapter
			{
				List<SupportFragment> Fragments { get; set; }
				List<string> FragmentsNames { get; set; }


				public TabAdapter(SupportFragmentManager Sfm) : base(Sfm)
				{
					Fragments = new List<SupportFragment>();
					FragmentsNames = new List<string>();
				}

				public void AddFragment(SupportFragment Fragment, string FragmentName)
				{
					Fragments.Add(Fragment);
					FragmentsNames.Add(FragmentName);
				}

				public override int Count
				{
					get
					{
						return Fragments.Count;
					}
				}

				public override SupportFragment GetItem(int position)
				{
					return Fragments[position];
				}

				public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
				{
					return new Java.Lang.String(FragmentsNames[position]);
				}
			}
		}
}

