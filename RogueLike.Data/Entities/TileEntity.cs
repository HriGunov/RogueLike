using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Data.Components;
using RogueLike.Data.Components.GeneralComponents;

namespace RogueLike.Data.Entities
{
    public class TileEntity : Entity
    {
        private PositionComponent position;
        private VisualizationComponent visualization;

        
        public TileEntity(PositionComponent position)
        {
            AddComponent(new IsTileComponent());
            AddComponent(position);
            AddComponent(new VisualizationComponent('!'));
            AddComponent(new IsWalkableComponent());
        }
        public TileEntity(int yCoord =0,int xCoord =0) : this(new PositionComponent(yCoord,xCoord))
        {

        }
 

        public PositionComponent PositionComponent { get
            {
                if (position == null)
                {
                    position =  GetComponent(typeof(PositionComponent)) as PositionComponent;
                }

                return position;
            }
        }
        public VisualizationComponent VisualizationComponent
        {
            get
            {
                if (visualization == null)
                {
                    visualization = GetComponent(typeof(VisualizationComponent)) as VisualizationComponent;
                }

                return visualization;
            }
        }

    }
}
