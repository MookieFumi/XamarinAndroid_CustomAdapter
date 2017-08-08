using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Linq;

namespace CustomAdapter
{
    [Activity(Label = "CustomAdapter", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //https://github.com/codepath/android_guides/wiki/Using-an-ArrayAdapter-with-ListView

        private List<User> _items = new List<User>();
        private UsersAdapter _adapter;
        private ListView _listView;
        private int number = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            _items = GenerateData();
            _adapter = new UsersAdapter(this, Resource.Layout.MainRow, _items);
            //_adapter.Update += (object sender, int e) =>
            //{
            //    Console.WriteLine("Position: " + e);
            //    _adapter.NotifyDataSetChanged();
            //};

            _listView = FindViewById<ListView>(Resource.Id.lvItems);
            _listView.Adapter = _adapter;

            var addButton = FindViewById<Button>(Resource.Id.addButton);
            addButton.Click += (sender, e) =>
            {
                var text = FindViewById<EditText>(Resource.Id.itemEditText).Text;
                if (!string.IsNullOrEmpty(text))
                {
                    _adapter.Add(new User(number, $"{text} - {number}"));
                    _adapter.NotifyDataSetChanged();
                    number++;
                }
            };
        }

        private List<User> GenerateData()
        {
            var retVal = new List<User>();
            for (int i = 0; i < number; i++)
            {
                retVal.Add(new User(i, $"MookieFumi - {i}"));
            }
            return retVal;
        }
    }

    public class UsersAdapter : ArrayAdapter<User>
    {
        private readonly Context _context;
        private readonly int _textViewResourceId;
        private readonly List<User> _items;
        //public event EventHandler<int> Update;

        public UsersAdapter(Context context, int textViewResourceId, List<User> items) : base(context, textViewResourceId, items)
        {
            _items = items;
            _textViewResourceId = textViewResourceId;
            _context = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            UserViewHolder viewHolder;
            User user = GetItem(position);

            if (convertView == null)
            {
                convertView = LayoutInflater.From(_context).Inflate(_textViewResourceId, null);
                viewHolder = new UserViewHolder(convertView, user.Id);
                convertView.SetTag(Resource.Id.TAG_ID, viewHolder);

                viewHolder.UpdateButton.SetTag(Resource.Id.TAG_ID, viewHolder.Id);
                viewHolder.UpdateButton.Click += (object sender, EventArgs e) =>
                {
                    var id = (int)((View)sender).GetTag(Resource.Id.TAG_ID);
                    var item = _items.Single(p => p.Id == id);
                    item.Name = String.Empty;
                    this.NotifyDataSetChanged();
                    //Update?.Invoke(this, (int)((View)sender).GetTag(Resource.Id.TAG_ID));
                };

                viewHolder.RemoveButton.Click += (sender, e) =>
                {
                    Toast.MakeText(_context, "Remove button clicked", ToastLength.Short).Show();
                };
            }
            else
            {
                viewHolder = (UserViewHolder)convertView.GetTag(Resource.Id.TAG_ID);
            }


            viewHolder.NameTextView.Text = user.Name;

            return convertView;
        }
    }

    public class UserViewHolder : Java.Lang.Object
    {
        public int Id { get; set; }
        public TextView NameTextView { get; set; }
        public Button RemoveButton { get; set; }
        public Button UpdateButton { get; set; }

        public UserViewHolder(View view, int id)
        {
            Id = id;
            NameTextView = view.FindViewById<TextView>(Resource.Id.tvName);
            RemoveButton = view.FindViewById<Button>(Resource.Id.removeButton);
            UpdateButton = view.FindViewById<Button>(Resource.Id.updateButton);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

