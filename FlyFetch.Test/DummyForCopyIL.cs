using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FlyFetch
{
    public class SampleViewItem
    {
        public virtual string S1 { get; set; }
        public virtual string S2 { get; set; }
        public virtual string S3 { get; set; }
        public virtual string S4 { get; set; }
        public virtual string S5 { get; set; }
    }

     public class OtherViewItem : INotifyPropertyChanged
     {
         public string S1 { get; set; }


         #region INotifyPropertyChanged Members

         public event PropertyChangedEventHandler PropertyChanged;

         #endregion
     }

    class DummyForCopyIL:SampleViewItem,IPageableElement
    {
        IPageHit hit;
        public DummyForCopyIL(IPageHit hit)
        {
            this.hit = hit;
        }
        #region IPageableElement Members

        public int PageIndex
        {
            get;
            set;
        }

        public bool Loaded
        {
            get;
            set;
        }
        public override string S2
        {
            get
            {
                return base.S2;
            }
            set
            {
                base.S2 = value;
            }
        }
        public override string S1
        {
            get
            {
                if(!Loaded)
                    hit.Hit(PageIndex);
                return base.S1;
            }
            set
            {
                base.S1 = value;
            }
        }

      

        #endregion
    }
}
