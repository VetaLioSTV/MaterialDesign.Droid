using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Xamarin.Android.MatDesign
{
	class SimpleStringRecycleAdapter : RecyclerView.Adapter
	{
		List<string> List;
		Context _Context;
		Resources Res;
		private Dictionary<int, int> mCalculatedSizes;
		private readonly TypedValue mTypedValue = new TypedValue();
		private int mBackground;

		public SimpleStringRecycleAdapter(Context _Context,List<string> List,Resources Resources)
		{
			this.List = List;
			this._Context = _Context;
			this.Res = Resources;
			mCalculatedSizes = new Dictionary<int, int>();
			_Context.Theme.ResolveAttribute(Resource.Attribute.selectableItemBackground, mTypedValue, true);
			mBackground = mTypedValue.ResourceId;
		}

		public override int ItemCount
		{
			get
			{
				return List.Count;
			}
		}

		public override async void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var simpleHolder = holder as Fragment1.SimpleViewHolder;

			simpleHolder.mBoundString = List[position];
			simpleHolder.mTxtView.Text = List[position];

			int drawableID = Fragment1.Cheeses.RandomCheeseDrawable;
			BitmapFactory.Options options = new BitmapFactory.Options();

			if (mCalculatedSizes.ContainsKey(drawableID))
			{
				options.InSampleSize = mCalculatedSizes[drawableID];
			}

			else
			{
				options.InJustDecodeBounds = true;

				BitmapFactory.DecodeResource(Res, drawableID, options);

				options.InSampleSize = Fragment1.Cheeses.CalculateInSampleSize(options, 100, 100);
				options.InJustDecodeBounds = false;

				mCalculatedSizes.Add(drawableID, options.InSampleSize);
			}
			var bitMap = await BitmapFactory.DecodeResourceAsync(Res, drawableID, options);

			simpleHolder.mImageView.SetImageBitmap(bitMap);


		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.List_Item, parent, false);
			view.SetBackgroundResource(mBackground);

			return new Fragment1.SimpleViewHolder(view);
		}
	}

	public static class ExtensionMethods
	{
		public static void SetItemClickListener(this RecyclerView rv, Action<RecyclerView, int, View> action)
		{
			rv.AddOnChildAttachStateChangeListener(new AttachStateChangeListener(rv, action));
		}
	}

	public class AttachStateChangeListener : Java.Lang.Object, RecyclerView.IOnChildAttachStateChangeListener
	{
		private RecyclerView mRecyclerview;
		private Action<RecyclerView, int, View> mAction;

		public AttachStateChangeListener(RecyclerView rv, Action<RecyclerView, int, View> action) : base()
		{
			mRecyclerview = rv;
			mAction = action;
		}

		public void OnChildViewAttachedToWindow(View view)
		{
			view.Click += View_Click;
		}

		public void OnChildViewDetachedFromWindow(View view)
		{
			view.Click -= View_Click;
		}

		private void View_Click(object sender, EventArgs e)
		{
			RecyclerView.ViewHolder holder = mRecyclerview.GetChildViewHolder(((View)sender));
			mAction.Invoke(mRecyclerview, holder.AdapterPosition, ((View)sender));
		}
	}
}