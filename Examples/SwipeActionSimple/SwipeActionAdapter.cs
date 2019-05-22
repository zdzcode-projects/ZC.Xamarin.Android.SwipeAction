using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                "Linha 3"
            });

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position, IList<Object> payloads)
        {
            base.OnBindViewHolder(holder, position, payloads);
        }

        public override void OnViewAttachedToWindow(Object holder)
        {
            SwipeActionViewHolder viewHolder = (SwipeActionViewHolder)holder;

            viewHolder.swipeRelativeLayout.ScrollTo(viewHolder.swipeRelativeLayout.Width / 2, 0);

            base.OnViewAttachedToWindow(holder);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SwipeActionViewHolder viewHolder = (SwipeActionViewHolder)holder;

            var item = _list[position];

            viewHolder.swipeRelativeLayout.SwipedLeft += SwipeRelativeLayout_SwipedLeft;
            viewHolder.swipeRelativeLayout.SwipedRight += SwipeRelativeLayout_SwipedRight; ;
        }

        private void SwipeRelativeLayout_SwipedRight(object sender, System.EventArgs e)
        {
            var x  = (SwipeLayout)sender;

            x.FreezeScroll();
        }

        private void SwipeRelativeLayout_SwipedLeft(object sender, System.EventArgs e)
        {
            var x = (SwipeLayout)sender;

            x.UnfreezeScroll();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //var view  = _swipeActionManager.InflateSwipeAction(parent, Resource.Layout.item_view);

            var view = (SwipeLayout)LayoutInflater.From(_context)
                .Inflate(ZC.Xamarin.Android.SwipeAction.Resource.Layout.swipe_layout, parent, false);

            view.InflateSwipeItem(Resource.Layout.item_view);
            
            var viewHolder = new SwipeActionViewHolder(view);

            int wt = GetScreenWidth(_context);

            for (var i = 0; i < viewHolder.swipe_content.ChildCount; i++)
            {
                viewHolder.swipe_content.GetChildAt(i).SetMinimumWidth(wt);
            }
            //viewHolder.textValue1.SetWidth(wt);
            //viewHolder.textValue2.SetMinimumWidth(wt);
            //viewHolder.textValue3.SetWidth(wt);

          //  viewHolder.horizontalScrollView.SetMinimumWidth(wt);
         //  viewHolder.horizontalScrollView.ScrollTo(viewHolder.horizontalScrollView.MinimumWidth / 2, 0);

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
        public TextView textValue1;
        public LinearLayout swipe_content;
        public TextView textValue3;

        public SwipeLayout swipeRelativeLayout;

        public SwipeActionViewHolder(View itemView) : base(itemView)
        {



            //textValue1 = itemView.FindViewById<TextView>(ZC.Xamarin.Android.SwipeAction.Resource.Id.textValue1);
            //textValue2 = itemView.FindViewById<RelativeLayout>(ZC.Xamarin.Android.SwipeAction.Resource.Id.SwipeRelativeLayoutContent);
            //textValue3 = itemView.FindViewById<TextView>(ZC.Xamarin.Android.SwipeAction.Resource.Id.textValue3);

            swipe_content = itemView.FindViewById<LinearLayout>(ZC.Xamarin.Android.SwipeAction.Resource.Id.swipe_content);
        }
    }
}