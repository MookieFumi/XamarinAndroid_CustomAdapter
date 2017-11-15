using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using CustomAdapter.Adapters;
using CustomAdapter.Models;

namespace CustomAdapter.Activities
{
    [Activity(Label = "CustomAdapter", MainLauncher = true, Theme = "@android:style/Theme.Material.Light")]
    public class MainActivity : Activity
    {
        private ListView _listView;
        private UserAdapter _adapter;
        private int _number = 15;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var items = GetItems();

            _adapter = new UserAdapter(this, Resource.Layout.MainRow, items);

            _adapter.NameChanged += (sender, args) =>
            {
                Toast.MakeText(this, $"New name {args.NewName}", ToastLength.Short).Show();
            };

            _listView = FindViewById<ListView>(Resource.Id.lvItems);

            _listView.Adapter = _adapter;

            var addButton = FindViewById<Button>(Resource.Id.addButton);
            addButton.Click += (sender, e) =>
            {
                var text = FindViewById<EditText>(Resource.Id.itemEditText).Text;
                if (string.IsNullOrEmpty(text)) return;

                _adapter.AddItem(new User(_number, text));

                _number++;
            };

            var retrieveButton = FindViewById<Button>(Resource.Id.retrieveButton);
            retrieveButton.Click += (sender, e) =>
            {
                Toast.MakeText(this, $"Items from Activity {_adapter.GetItems().Count()}", ToastLength.Short).Show();
            };
        }

        private List<User> GetItems()
        {
            var retVal = new List<User>();
            for (var i = 0; i < _number; i++)
            {
                retVal.Add(new User(i, "MookieFumi"));
            }
            return retVal;
        }
    }
}

