using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using JP.Wasabeef.Recyclerview.Animators;

namespace SwipeActionSimple
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView recyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            
            LinearLayoutManager layoutManager = new LinearLayoutManager(this);

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);

            recyclerView.SetItemAnimator(new LandingAnimator());
            recyclerView.GetItemAnimator().AddDuration = 500;
            recyclerView.GetItemAnimator().RemoveDuration = 500;

            recyclerView.SetAdapter(new SwipeActionAdapter(this));

            recyclerView.SetLayoutManager(layoutManager);

            recyclerView.RefreshDrawableState();

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}