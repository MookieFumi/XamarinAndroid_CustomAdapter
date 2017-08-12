using System.Collections.Generic;
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
        private List<User> _items;
        private ListView _listView;
        private UsersAdapter _adapter;
        private int _number = 40;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            _items = GenerateData();
            _adapter = new UsersAdapter(this, Resource.Layout.MainRow, _items);
            _adapter.UpdateUser += (sender, e) =>
            {
                _items[e].Name = "Renamed";
                _adapter.NotifyDataSetChanged();
                Toast.MakeText(this, $"Update button clicked - [Position: {e}]", ToastLength.Short).Show();
            };
            _adapter.RemoveUser += (sender, e) =>
            {
                _items.RemoveAt(e);
                UpdateData();
                Toast.MakeText(this, $"Remove button clicked - [Position: {e}]", ToastLength.Short).Show();
            };

            _listView = FindViewById<ListView>(Resource.Id.lvItems);
            _listView.Adapter = _adapter;

            var addButton = FindViewById<Button>(Resource.Id.addButton);
            addButton.Click += (sender, e) =>
            {
                var text = FindViewById<EditText>(Resource.Id.itemEditText).Text;
                if (string.IsNullOrEmpty(text)) return;

                _items.Add(new User(_number, $"{text} - {_number * 99}"));
                UpdateData();
                _number++;
            };
        }

        private void UpdateData()
        {
            _adapter.Clear();
            _adapter.AddAll(_items);
            _adapter.NotifyDataSetChanged();
        }

        private List<User> GenerateData()
        {
            var retVal = new List<User>();
            for (var i = 1; i <= _number; i++)
            {
                retVal.Add(new User(i, $"MookieFumi - {i * 99}"));
            }
            return retVal;
        }
    }
}

