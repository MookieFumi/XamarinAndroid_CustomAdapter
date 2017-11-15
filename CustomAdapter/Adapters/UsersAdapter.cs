using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using CustomAdapter.Models;
using CustomAdapter.ViewHolders;

namespace CustomAdapter.Adapters
{
    public class UsersAdapter : ArrayAdapter<User>
    {
        private readonly Context _context;
        private readonly int _textViewResourceId;
        private readonly List<User> _items;
        private static int _calls;

        public UsersAdapter(Context context, int textViewResourceId, List<User> items) : base(context, textViewResourceId, items)
        {
            _textViewResourceId = textViewResourceId;
            _items = items;
            _context = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            System.Diagnostics.Debug.WriteLine($"call no {_calls++}");

            UserViewHolder viewHolder;

            var item = GetItem(position);

            // Check if an existing view is being reused, otherwise inflate the view
            if (convertView == null)
            {
                System.Diagnostics.Debug.WriteLine($"inflate the view, position {position}");
                // If there's no view to re-use, inflate a brand new view for row
                convertView = LayoutInflater.From(_context).Inflate(_textViewResourceId, null);

                viewHolder = new UserViewHolder(convertView, item.Id);
                viewHolder.UpdateButton.Click += (sender, args) =>
                {
                    var user = this.GetItem(viewHolder.Position);
                    System.Diagnostics.Debug.WriteLine(user);
                    user.Name = "Renamed";
                    // when updating, we have to call to NotifyDataSetChanged
                    this.NotifyDataSetChanged();
                };

                viewHolder.RemoveButton.Click += (sender, args) =>
                {
                    var user = this.GetItem(viewHolder.Position);
                    System.Diagnostics.Debug.WriteLine(user);
                    _items.Remove(user);
                    this.Remove(user);
                };

                convertView.SetTag(Resource.Id.TAG_ID, viewHolder);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"view is being recycled, position {position}");

                // View is being recycled, retrieve the viewHolder object from tag
                viewHolder = (UserViewHolder)convertView.GetTag(Resource.Id.TAG_ID);
            }

            viewHolder.Position = position;

            viewHolder.NameTextView.Text = $"{item} - [position: {position}]";

            return convertView;
        }

        public  void AddUser(User user)
        {
            _items.Add(user);

            this.Clear();
            this.AddAll(_items);
        }
    }
}