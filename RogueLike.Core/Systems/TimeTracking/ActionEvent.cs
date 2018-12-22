using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Core.Systems.TimeTracking
{
    public abstract class ActionEvent : IActionEvent
    {
       
        public ActionEvent(long activationTime)
        {
            this.ActivationTime = activationTime;
        }

        public long ActivationTime { get; set; }

        public abstract void Invoke();
        
    }
}
