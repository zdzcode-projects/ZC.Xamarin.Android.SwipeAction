using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ZC.Xamarin.Android.SwipeAction
{
    public class ZCHorizontalScrollView : HorizontalScrollView
    {

        public event EventHandler ScrollStopped;

        private bool isScrolling = false;
        private int scrollCount = 0;
        private bool isScrollingByCode = false;

        private bool freeze = false;

        public ZCHorizontalScrollView(Context context) : base(context)
        {
            LayoutChange += ZCHorizontalScrollView_LayoutChange;
            //ScrollChange += ZCHorizontalScrollView_ScrollChange;
        }

        private void ZCHorizontalScrollView_AnimationEnd(object sender, global::Android.Views.Animations.Animation.AnimationEndEventArgs e)
        {
            //OnAnimationEnd();
        }

        public ZCHorizontalScrollView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            LayoutChange += ZCHorizontalScrollView_LayoutChange;
            //ScrollChange += ZCHorizontalScrollView_ScrollChange;
        }

        public ZCHorizontalScrollView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            LayoutChange += ZCHorizontalScrollView_LayoutChange;
            //ScrollChange += ZCHorizontalScrollView_ScrollChange;
        }

        public ZCHorizontalScrollView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            LayoutChange += ZCHorizontalScrollView_LayoutChange;
            //ScrollChange += ZCHorizontalScrollView_ScrollChange;
        }

        private void ZCHorizontalScrollView_LayoutChange(object sender, LayoutChangeEventArgs e)
        {
            isScrollingByCode = true;
            ScrollTo(((View)Parent).Width, 0);
        }

        private void ZCHorizontalScrollView_ScrollChange(object sender, ScrollChangeEventArgs e)
        {
            if (!isScrolling)
            {
                Task.Run(() => OnScrollingEnd());
            }

            isScrolling = true;
            scrollCount++;
        }

        public void OnScrollingEnd()
        {
            int oldScrollCount = 0;
            while (true)
            {
                Task.Delay(100).Wait();

                if (oldScrollCount == scrollCount)
                {
                    isScrolling = false;
                    scrollCount = 0;

                    if (isScrollingByCode)
                    {
                        isScrollingByCode = false;
                    }
                    else
                    {
                        isScrollingByCode = true;

                        ScrollStopped?.Invoke(this, null);

                        if (!freeze)
                            SmoothScrollTo(((View)Parent).Width, 0);


                    }

                    break;
                }

                oldScrollCount = scrollCount;
            }
        }

        public void FreezeScroll()
        {
            freeze = true;
        }

        public void UnfreezeScroll()
        {
            if (freeze)
            {
                freeze = false;

                isScrolling = false;
                scrollCount = 0;
                isScrollingByCode = true;

                SmoothScrollTo(((View)Parent).Width, 0);
            }
        }

        private void ZCHorizontalScrollView_Touch(object sender, TouchEventArgs e)
        {
            base.OnTouchEvent(e.Event);
        }
    }
}