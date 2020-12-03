using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sketch.Shared.Data
{
    public class StylusPointCollection : Collection<StylusPoint>
    {
        public StylusPointCollection()
        {
        }

        public StylusPointCollection(IList<StylusPoint> list)
            : base(list)
        {
        }
    }
}
