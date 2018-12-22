using System;

namespace RogueLike.Core.Systems.TimeTracking
{
    public interface IActionEvent
    {
        long ActivationTime { get; set; }
        void Invoke();
    }
}