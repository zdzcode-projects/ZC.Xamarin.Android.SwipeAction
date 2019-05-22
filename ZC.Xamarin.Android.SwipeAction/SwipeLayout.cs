using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

namespace ZC.Xamarin.Android.SwipeAction
{
    public class SwipeLayout : ZCHorizontalScrollView
    {
        public event EventHandler SwipedLeft;
        public event EventHandler SwipedRight;

        public SwipeLayout(Context context) : base(context)
        {
            ScrollStopped += SwipeRelativeLayout_ScrollStopped;
        }

        public SwipeLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            ScrollStopped += SwipeRelativeLayout_ScrollStopped;
        }

        public SwipeLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            ScrollStopped += SwipeRelativeLayout_ScrollStopped;
        }

        public SwipeLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            ScrollStopped += SwipeRelativeLayout_ScrollStopped;
        }

        private void SwipeRelativeLayout_ScrollStopped(object sender, EventArgs e)
        {
            var scroll = (SwipeLayout)sender;

            if (scroll.ScrollX < scroll.Width / 2)
            {
                //swiperight
                SwipedRight?.Invoke(sender, e);

            }
            else if (scroll.ScrollX > scroll.Width * 1.5)
            {
                //swipeleft
                SwipedLeft?.Invoke(sender, e);
            }
        }

        public void InflateSwipeItem(int resource)
        {
            LinearLayout swipe_content = FindViewById<LinearLayout>(Resource.Id.swipe_content);

            var view = LayoutInflater.From(Context)
                .Inflate(resource, swipe_content, false);

            swipe_content.AddView(view, 1);
        }

        public void InflateSwipeItem(View view)
        {
            LinearLayout swipe_content  = FindViewById<LinearLayout>(Resource.Id.swipe_content);
            swipe_content.AddView(view, 1);
        }

    }
}