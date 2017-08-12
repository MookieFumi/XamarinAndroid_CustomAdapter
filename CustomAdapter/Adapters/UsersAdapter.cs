using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
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

        public event EventHandler<int> UpdateUser;
        public event EventHandler<int> RemoveUser;

        public UsersAdapter(Context context, int textViewResourceId, List<User> items) : base(context, textViewResourceId, items)
        {
            _items = items;
            _textViewResourceId = textViewResourceId;
            _context = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            UserViewHolder viewHolder;
            var user = GetItem(position);

            if (convertView == null)
            {
                convertView = LayoutInflater.From(_context).Inflate(_textViewResourceId, null);
                viewHolder = new UserViewHolder(convertView, user.Id);

                viewHolder.UpdateButton.Click += (sender, e) =>
                {
                    var pos = (int)((View)sender).GetTag(Resource.Id.TAG_ID);
                    UpdateUser?.Invoke(this, pos);
                };

                viewHolder.RemoveButton.Click += (sender, e) =>
                {
                    var pos = (int)((View)sender).GetTag(Resource.Id.TAG_ID);
                    Animation animation = AnimationUtils.LoadAnimation(_context, Resource.Animation.fadeout);
                    convertView.StartAnimation(animation);
                    Handler handler = new Handler();
                    handler.PostDelayed(() =>
                    {
                        RemoveUser?.Invoke(this, pos);
                    }, 1000);
                };
            }
            else
            {
                viewHolder = (UserViewHolder)convertView.GetTag(Resource.Id.TAG_ID);
            }

            convertView.SetTag(Resource.Id.TAG_ID, viewHolder);
            viewHolder.RemoveButton.SetTag(Resource.Id.TAG_ID, position);
            viewHolder.UpdateButton.SetTag(Resource.Id.TAG_ID, position);

            viewHolder.NameTextView.Text = $"{user.Name} - [Position: {position}]";

            return convertView;
        }
    }
}