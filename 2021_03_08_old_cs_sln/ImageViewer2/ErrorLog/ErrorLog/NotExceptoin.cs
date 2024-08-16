using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLog
{
    [Serializable()]
    class NotExceptoin : System.Exception
    {
        public NotExceptoin() : base() { }
        public NotExceptoin(string message) : base(message) { }
        public NotExceptoin(string message, System.Exception inner) : base(message, inner) { }

        protected NotExceptoin(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
        {
        }
    }
}
