using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Data.Components.Abstract
{
    public abstract class Component
    {
        public Component()
        {
            ComponentName = this.GetType().Name;
        }
        public string ComponentName { get; set; }
    }
}
