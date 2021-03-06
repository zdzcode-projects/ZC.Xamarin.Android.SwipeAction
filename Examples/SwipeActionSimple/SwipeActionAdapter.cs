﻿using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using ZC.Xamarin.Android.SwipeAction;

namespace SwipeActionSimple
{
    public class SwipeActionAdapter : RecyclerView.Adapter
    {
        private Context _context;
        public List<string> _list;

        public override int ItemCount => _list.Count;

        public SwipeActionAdapter(Context context)
        {
            _context = context;

            _list = new List<string>(new string[] {
                "Linha 1",
                "Linha 2",
                "Linha 3",
                "Linha 4",
                "Linha 5",
                "Linha 6",
                "Linha 7",
                "Linha 8",
                "Linha 9",
                "Linha 10",
                "Linha 11",
                "Linha 12",
                "Linha 13",
                "Linha 14",
                "Linha 15",
            });

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SwipeActionViewHolder viewHolder = (SwipeActionViewHolder)holder;

            var item = _list[position];

            viewHolder.position.Text = item;

            viewHolder.swipeLayout.SwipedLeft += SwipeRelativeLayout_SwipedLeft;
            viewHolder.swipeLayout.SwipedRight += SwipeRelativeLayout_SwipedRight;
        }

        private void SwipeRelativeLayout_SwipedRight(object sender, System.EventArgs e)
        {
            var swipeLayout  = (SwipeLayout)sender;

            //x.FreezeScroll();
        }

        private void SwipeRelativeLayout_SwipedLeft(object sender, System.EventArgs e)
        {
            var swipeLayout = (SwipeLayout)sender;

            TextView position = swipeLayout.FindViewById<TextView>(ZC.Xamarin.Android.SwipeAction.Resource.Id.swipePosition);

            //swipeLayout.FreezeScroll();
            var x = _list.IndexOf(position.Text);

            if (x >= 0)
            {
                _list.RemoveAt(x);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    NotifyItemRemoved(x);
                    //swipeLayout.UnfreezeScroll();
                });


            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = (SwipeLayout)LayoutInflater.From(_context)
                .Inflate(ZC.Xamarin.Android.SwipeAction.Resource.Layout.swipe_layout, parent, false);

            int wt = GetScreenWidth(_context);

            view.SetMinimumWidth(wt);

            var itemView = view.InflateSwipeItem(Resource.Layout.item_view);

            itemView.SetMinimumWidth(wt);

            var viewHolder = new SwipeActionViewHolder(view);

            for (var i = 0; i < viewHolder.swipe_content.ChildCount; i++)
            {
                viewHolder.swipe_content.GetChildAt(i).SetMinimumWidth(wt);                
            }

            return viewHolder;
        }

        public static int GetScreenWidth(Context context)
        {
            IWindowManager windowManager = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            DisplayMetrics dm = new DisplayMetrics();
            windowManager.DefaultDisplay.GetMetrics(dm);
            return dm.WidthPixels;
        }
    }

    public class ScrollProperty
    {
        public int ScrollCount { get; set; } = 0;
        public bool IsScrolling { get; set; } = false;
        public bool IsScrollingByCode { get; set; } = false;

        public HorizontalScrollView HorizontalScrollView { get; set; }
    }

    public class SwipeActionViewHolder : RecyclerView.ViewHolder
    {
        public TextView position;

        public TextView textValue1;
        public LinearLayout swipe_content;
        public TextView textValue3;

        public SwipeLayout swipeLayout;

        public SwipeActionViewHolder(SwipeLayout itemView) : base(itemView)
        {

            position = itemView.FindViewById<TextView>(ZC.Xamarin.Android.SwipeAction.Resource.Id.swipePosition);

            //textValue1 = itemView.FindViewById<TextView>(ZC.Xamarin.Android.SwipeAction.Resource.Id.textValue1);
            //textValue2 = itemView.FindViewById<RelativeLayout>(ZC.Xamarin.Android.SwipeAction.Resource.Id.SwipeRelativeLayoutContent);
            //textValue3 = itemView.FindViewById<TextView>(ZC.Xamarin.Android.SwipeAction.Resource.Id.textValue3);

            swipeLayout = itemView;

            swipe_content = itemView.FindViewById<LinearLayout>(ZC.Xamarin.Android.SwipeAction.Resource.Id.swipe_content);
        }
    }
}