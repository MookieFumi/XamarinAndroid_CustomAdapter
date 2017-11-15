using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using CustomAdapter.ViewHolders;

namespace CustomAdapter.Adapters
{
    public abstract class MyAdapterBase<TItem, TViewHolder> : ArrayAdapter<TItem> where TViewHolder : class, IPosition
    {
        private readonly Context _context;
        private readonly int _textViewResourceId;
        private readonly List<TItem> _items;
        private static int _calls;
        public event EventHandler<TItem> ItemRemoved;
        public event EventHandler<TItem> ItemAdded; 

        protected virtual void OnItemRemoved(TItem item)
        {
            ItemRemoved?.Invoke(this, item);
        }

        protected virtual void OnItemAdded(TItem item)
        {
            ItemAdded?.Invoke(this, item);
        }

        protected MyAdapterBase(Context context, int textViewResourceId, List<TItem> items) : base(context, textViewResourceId, items)
        {
            _textViewResourceId = textViewResourceId;
            _items = items;
            _context = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            System.Diagnostics.Debug.WriteLine($"call no {_calls++}");

            TViewHolder viewHolder;

            var item = GetItem(position);

            var isBeingReused = convertView != null;
            if (isBeingReused)
            {
                System.Diagnostics.Debug.WriteLine($"view is being recycled, position {position}");

                viewHolder = convertView.GetTag(Resource.Id.TAG_ID) as TViewHolder;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"inflate the view, position {position}");

                convertView = LayoutInflater.From(_context).Inflate(_textViewResourceId, null);

                viewHolder = (TViewHolder)Activator.CreateInstance(typeof(TViewHolder), args: convertView);

                SetUpEvents(viewHolder);

                convertView.SetTag(Resource.Id.TAG_ID, viewHolder as Java.Lang.Object);
            }

            viewHolder.Position = position;

            Inflate(item, viewHolder);

            return convertView;
        }

        protected abstract void SetUpEvents(TViewHolder viewHolder);

        protected abstract void Inflate(TItem item, TViewHolder viewHolder);

        public IEnumerable<TItem> GetItems()
        {
            return _items;
        }

        public void AddItem(TItem item)
        {
            _items.Add(item);
            this.Clear();
            this.AddAll(_items);
            OnItemAdded(item);
        }

        public void RemoveItem(TItem item)
        {
            _items.Remove(item);
            this.Remove(item);
            OnItemRemoved(item);
        }

        public void RemoveItem(int position)
        {
            var item = this.GetItem(position);
            RemoveItem(item);
        }
    }
}