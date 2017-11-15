using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using CustomAdapter.Models;
using CustomAdapter.ViewHolders;

namespace CustomAdapter.Adapters
{
    public class UserAdapter : MyAdapterBase<User, UserViewHolder>
    {
        public event EventHandler<NameChangedEventArgs> NameChanged;

        protected virtual void OnNameChanged(string oldName, string newName)
        {
            NameChanged?.Invoke(this, new NameChangedEventArgs(oldName, newName));
        }

        public UserAdapter(Context context, int textViewResourceId, List<User> items) : base(context, textViewResourceId, items)
        {
        }

        protected override void SetUpEvents(UserViewHolder viewHolder)
        {
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
                RemoveItem(viewHolder.Position);
            };

            viewHolder.SurnameEditText.FocusChange += (sender, args) =>
            {
                if (!args.HasFocus)
                {
                    var user = this.GetItem(viewHolder.Position);
                    System.Diagnostics.Debug.WriteLine($"loosing focus {user}");

                    var oldText = user.Surname ?? string.Empty;
                    var newText = (((EditText)sender).Text??string.Empty).Trim();
                    if (oldText != newText)
                    {
                        user.Surname = newText;
                        OnNameChanged(oldText, newText);
                    }
                }
            };
        }

        protected override void Inflate(User item, UserViewHolder viewHolder)
        {
            viewHolder.NameTextView.Text = $"{item}";
            viewHolder.SurnameEditText.Text = $"{item.Surname}";
        }
    }
}