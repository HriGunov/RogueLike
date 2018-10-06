using RogueLike.Data.Components.Abstract;
using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Data.Entities
{
    public class Entity
    {
        public List<Component> Components { get; set; }

        public bool HasComponent(Type typeOfComponent)
        {
            return HasComponent(typeOfComponent.Name);
        }

        private bool HasComponent(string nameOfComponent)
        {
            return Components.Any(component => component.ComponentName == nameOfComponent);
        }

        public bool HasComponents(params Type[] componentTypes)
        {
            foreach (var component in componentTypes)
            {
                if (!this.HasComponent(component))
                {
                    return false;
                }
            }
            return true;
        }

        public Component GetComponent(Type componentType)
        {
            return Components.FirstOrDefault(component => component.ComponentName == componentType.Name);
        }
    }
}
