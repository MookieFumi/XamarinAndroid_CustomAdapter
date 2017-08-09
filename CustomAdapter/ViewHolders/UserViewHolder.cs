using Android.Views;
using Android.Widget;

namespace CustomAdapter.ViewHolders
{
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
}