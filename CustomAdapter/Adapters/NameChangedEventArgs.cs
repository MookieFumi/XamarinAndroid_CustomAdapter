using System;

namespace CustomAdapter.Adapters
{
    public class NameChangedEventArgs : EventArgs
    {
        public string OldName { get; }
        public string NewName { get; }

        public NameChangedEventArgs(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }
    }
}