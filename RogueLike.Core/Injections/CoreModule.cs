using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RogueLike.Core.Systems;
using RogueLike.Core.Systems.DrawingSystem;
using RogueLike.Core.Systems.MovementSystem;
using RogueLike.Core.Systems.TimeTracking;
using RogueLike.Core.Systems.WorldSystem;
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
            builder.RegisterType<WorldSystem>().AsSelf().SingleInstance();
            builder.RegisterType<MovementSystem>().AsSelf().SingleInstance();
            builder.RegisterType<DrawingSystem>().AsSelf().SingleInstance();
            builder.RegisterType<MathSystem>().AsSelf().SingleInstance();
            builder.RegisterType<VisionSystem>().AsSelf().SingleInstance();
            builder.RegisterType<TimeTrackingSystem>().AsSelf().SingleInstance();

            









            base.Load(builder);

        }
    }
}
