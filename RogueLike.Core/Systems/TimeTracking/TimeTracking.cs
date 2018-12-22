using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Core.Systems.TimeTracking
{
    public class TimeTrackingSystem
    {
        private long currentTime;
        Queue<IActionEvent> queuedActions;
        public TimeTrackingSystem()
        {
            queuedActions = new Queue<IActionEvent>();
        }
        public long CurrentTime => currentTime;
        public void AddAction(IActionEvent actionEvent)
        {
            actionEvent.ActivationTime += currentTime;
            queuedActions.Enqueue(actionEvent);

        }
        /// <summary>
        /// Executes all Actions between current time and current time + time increment.
        /// </summary> 
        public void AdvanceTime(long timeIncrement)
        {
            currentTime += timeIncrement;

            if (queuedActions.Any())
            {
                while (currentTime >= queuedActions.Peek().ActivationTime)
                {
                    queuedActions.Dequeue().Invoke();

                    if (!queuedActions.Any())
                    {
                        break;
                    }
                }

            }
        }
    }
}
