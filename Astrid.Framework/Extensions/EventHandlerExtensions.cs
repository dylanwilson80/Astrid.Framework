using System;

namespace Astrid.Framework.Extensions
{
    public static class EventHandlerExtensions
    {
        public static void Raise(this EventHandler eventHandler, object sender, EventArgs eventArgs)
        {
            var handler = eventHandler;

            if (handler != null)
                handler(sender, eventArgs);
        }

        public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T eventArgs)
            where T : EventArgs
        {
            var handler = eventHandler;

            if (handler != null)
                handler(sender, eventArgs);
        }
    }
}