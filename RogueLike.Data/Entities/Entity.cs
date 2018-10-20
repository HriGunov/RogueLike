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
        public Dictionary<string,Component> Components { get; set; } = new Dictionary<string, Component>();

        public void AddComponent(Component component)
        {
            Components.Add(component.GetType().Name, component);
        }
        public bool HasComponent(Type typeOfComponent)
        {
            return HasComponent(typeOfComponent.Name);
        }

        private bool HasComponent(string nameOfComponent)
        {
            return Components.ContainsKey(nameOfComponent);
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
           return GetComponent(componentType.Name);
        }
        public Component GetComponent(string componentName)
        {
            return Components[componentName];
        }
    }
}
