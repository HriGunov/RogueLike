using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Core.Systems.TimeTracking
{
    class ITimeTrackable
    {
        private readonly IActionEvent actionEvent;

        // TODO linked-list

        public ITimeTrackable(IActionEvent actionEvent)
        {
            this.actionEvent = actionEvent;
        }
        public long ActivationTime { get; set; }
    }
}
