﻿using Discord.Commands;
using Discord.WebSocket;
using SlimBot.Discord;
using SlimBot.Storage;
using SlimBot.Storage.Implementations;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace SlimBot
{
    public static class Unity
    {
        private static UnityContainer _container;

        public static UnityContainer Container
        {
            get
            {
                if(_container == null) 
                {
                    RegisterTypes();
                }
                return _container;
            }
        }

        public static void RegisterTypes()
        {
            _container = new UnityContainer();
            _container.RegisterSingleton<IDataStorage, JsonStorage>();
            _container.RegisterSingleton<ILogger, Logger>();
            _container.RegisterType<DiscordSocketConfig>(new InjectionFactory(i => SocketConfig.GetDefault()));
            _container.RegisterSingleton<DiscordSocketClient>(new InjectionConstructor(typeof(DiscordSocketConfig)));         
            _container.RegisterSingleton<CommandService>();
            _container.RegisterSingleton<DiscordCommandHandler>();
            _container.RegisterSingleton<Connection>();
        }

        public static T Resolve<T>()
        {
            return (T) Container.Resolve(typeof(T), string.Empty, new CompositeResolverOverride());
        }
    }
}
