using RogueLike.Core.Systems.TimeTracking;
using RogueLike.Data.Components.GeneralComponents;
using RogueLike.Core.ComponentExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Core.Systems.MovementSystem.Actions
{
    public class MoveToAction : ActionEvent
    {
        private readonly PositionComponent _positionToChange;
        private readonly PositionComponent _targetPosition;

        /// <summary>
        /// Deep clones target position coordinates
        /// </summary>
        /// <param name="positionToChange"></param>
        /// <param name="targetPosition"></param>
        /// <param name="activationTime"></param>
        public MoveToAction(PositionComponent positionToChange, PositionComponent targetPosition,
            long activationTime) : base(activationTime)
        {
            this._positionToChange = positionToChange;
            this._targetPosition = targetPosition;
        }

        public override void Invoke()
        {
            _positionToChange.MoveTo(_targetPosition);
        }
    }
}
