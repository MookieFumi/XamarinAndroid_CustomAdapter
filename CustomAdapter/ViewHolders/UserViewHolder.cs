using Android.Views;
using Android.Widget;

namespace CustomAdapter.ViewHolders
{
    public interface IPosition
    {
        int Position { get; set; }

    }
    public class UserViewHolder : Java.Lang.Object, IPosition
    {
        public TextView NameTextView { get; set; }
        public EditText SurnameEditText { get; set; }
        public Button RemoveButton { get; set; }
        public Button UpdateButton { get; set; }
        public int Position { get; set; }

        public UserViewHolder(View view)
        {
            NameTextView = view.FindViewById<TextView>(Resource.Id.tvName);
            SurnameEditText = view.FindViewById<EditText>(Resource.Id.etSurname);
            RemoveButton = view.FindViewById<Button>(Resource.Id.removeButton);
            UpdateButton = view.FindViewById<Button>(Resource.Id.updateButton);
        }
    }
}