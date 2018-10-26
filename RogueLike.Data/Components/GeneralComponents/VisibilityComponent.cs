using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Data.Components.GeneralComponents
{
    class VisibilityComponent
    {
 

        public bool isVisible = false;

        public VisibilityComponent(bool isVisible)
        {
            this.isVisible = isVisible;
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
    }
}
