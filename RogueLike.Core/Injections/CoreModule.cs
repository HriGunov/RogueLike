using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RogueLike.Core.Systems.MapSystem;
using RogueLike.Data;
using RogueLike.Data.Abstract;

namespace RogueLike.Core.Injections
{
    class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().AsSelf().SingleInstance();
            builder.RegisterType<EntityManager>().As<IEntityManager>().SingleInstance();
            builder.RegisterType<MapSystem>().AsSelf().SingleInstance();

            




            base.Load(builder);

        }
    }
}
