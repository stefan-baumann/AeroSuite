using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AeroSuite.Controls
{
    public class HeaderlessTabPageCollection
        : IList<HeaderlessTabPage>
    {
        protected List<HeaderlessTabPage> TabPages { get; set; }
        protected HeaderlessTabControl Owner { get; set; }

        public HeaderlessTabPageCollection(HeaderlessTabControl tabControl)
        {
            this.Owner = tabControl;
            this.TabPages = new List<HeaderlessTabPage>();
        }

        int IList<HeaderlessTabPage>.IndexOf(HeaderlessTabPage item)
        {
            return this.TabPages.IndexOf(item);
        }

        void IList<HeaderlessTabPage>.Insert(int index, HeaderlessTabPage item)
        {
            throw new NotImplementedException();
        }

        void IList<HeaderlessTabPage>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        HeaderlessTabPage IList<HeaderlessTabPage>.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void ICollection<HeaderlessTabPage>.Add(HeaderlessTabPage item)
        {
            throw new NotImplementedException();
        }

        void ICollection<HeaderlessTabPage>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<HeaderlessTabPage>.Contains(HeaderlessTabPage item)
        {
            throw new NotImplementedException();
        }

        void ICollection<HeaderlessTabPage>.CopyTo(HeaderlessTabPage[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int ICollection<HeaderlessTabPage>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<HeaderlessTabPage>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<HeaderlessTabPage>.Remove(HeaderlessTabPage item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<HeaderlessTabPage> IEnumerable<HeaderlessTabPage>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
