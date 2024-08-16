using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer2
{
    [Serializable()]
    class ViewImageControlException : System.Exception
    {
        public ViewImageControlException() : base() { }
        public ViewImageControlException(string message) : base(message) { }
        public ViewImageControlException(string message, System.Exception inner) : base(message, inner) { }

        protected ViewImageControlException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
        {
        }
    }
}
