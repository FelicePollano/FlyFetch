using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyFetch
{
    public interface ICountProvider
    {
        void GetCount();
        event EventHandler<CountEventArgs> CountAvailable;

    }

    public class CountEventArgs : EventArgs
    {
        public CountEventArgs(int count)
        {
            this.Count = count;
        }
        public int Count { get; private set; }
    }
}
