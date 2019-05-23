using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZC.Xamarin.Android.SwipeAction
{
    public class SwipeLayout : ZCHorizontalScrollView
    {
        public event EventHandler SwipedLeft;
        public event EventHandler SwipedRight;

        private bool SwipeLeft = false;
        private bool SwipeRight = false;
        private bool isTouching = false;

        public SwipeLayout(Context context) : base(context)
        {
            ScrollChange += SwipeLayout_ScrollChange;
            ScrollStopped += SwipeRelativeLayout_ScrollStopped;
            Touch += SwipeLayout_Touch;


        }        

        public SwipeLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            ScrollChange += SwipeLayout_ScrollChange;
            ScrollStopped += SwipeRelativeLayout_ScrollStopped;
            Touch += SwipeLayout_Touch;
        }

       

        public SwipeLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            ScrollChange += SwipeLayout_ScrollChange;
            ScrollStopped += SwipeRelativeLayout_ScrollStopped;
            Touch += SwipeLayout_Touch;
        }

        public SwipeLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            ScrollChange += SwipeLayout_ScrollChange;
            ScrollStopped += SwipeRelativeLayout_ScrollStopped;
            Touch += SwipeLayout_Touch;
        }

        private void SwipeLayout_ScrollChange(object sender, ScrollChangeEventArgs e)
        {
            var scroll = (SwipeLayout)sender;

            SwipeRight = false;
            SwipeLeft = false;

            if (scroll.ScrollX < scroll.Width / 2)
            {
                SwipeRight = true;
                //swiperight
                //SwipedRight?.Invoke(sender, e);

            }
            else if (scroll.ScrollX > scroll.Width * 1.5)
            {
                SwipeLeft = true;
                //swipeleft
                //SwipedLeft?.Invoke(sender, e);
            }
            if (!tokenSource.IsCancellationRequested)
            {
                tokenSource.Cancel();
            }

            if (!isTouching)
            {
                tokenSource = new CancellationTokenSource();
                Task.Run(() => InvokeSwipe(tokenSource.Token));
            }
        }

        CancellationTokenSource tokenSource = new CancellationTokenSource();

        async Task InvokeSwipe(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(100);

                if(!cancellationToken.IsCancellationRequested)
                {
                    if (SwipeRight)
                    {
                        SwipedRight?.Invoke(this, null);

                    }
                    else if (SwipeLeft)
                    {
                        SwipedLeft?.Invoke(this, null);
                    }
                    else
                    {
                        SmoothScrollTo(((View)Parent).Width, 0);
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                
            }
            catch (Exception ex)
            {

            }
        }

        private void SwipeLayout_Touch(object sender, TouchEventArgs e)
        {
            OnTouchEvent(e.Event);

            switch(e.Event.ActionMasked)
            {
                case MotionEventActions.Cancel:                    
                case MotionEventActions.Up:
                    isTouching = false;
                    tokenSource = new CancellationTokenSource();
                    Task.Run(() => InvokeSwipe(tokenSource.Token));
                    break;
                default:
                    isTouching = true;
                    if (!tokenSource.IsCancellationRequested)
                    {
                        tokenSource.Cancel();
                    }
                    break;
            }                        
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



        public View InflateSwipeItem(int resource)
        {
            LinearLayout swipe_content = FindViewById<LinearLayout>(Resource.Id.swipe_content);

            var view = LayoutInflater.From(Context)
                .Inflate(resource, swipe_content, false);

            swipe_content.AddView(view, 1);

            return view;
        }

        public void InflateSwipeItem(View view)
        {
            LinearLayout swipe_content  = FindViewById<LinearLayout>(Resource.Id.swipe_content);
            swipe_content.AddView(view, 1);
        }

    }
}