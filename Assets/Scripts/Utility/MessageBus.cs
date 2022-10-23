using UniRx;
using System;

namespace Utility
{

    /// <summary>
    /// Base class for custom game events.
    /// Extend this class to implement your own events.
    /// </summary>
    public class GameEvent
    {

    }

    /// <summary>
    /// Proxy for UniRx event system.
    /// </summary>
    public static class MessageBus
    {

        /// <summary>
        /// Publishes a new event of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="evnt"></param>
        public static void Publish<T>(T evnt) where T : GameEvent
        {
            MessageBroker.Default.Publish(evnt);
        }

        /// <summary>
        /// Creates and subscribable to an event of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IObservable<T> OnEvent<T>() where T : GameEvent
        {
            return MessageBroker.Default.Receive<T>();
        }


        /// <summary>
        /// Clears event subscriptions.
        /// Note that this method called when a level is removed.
        /// So it is not possible to have inter-level events at the moment.
        /// </summary>
        public static void ClearSubs()
        {
            MessageBroker.Default.Clear();
            //MessageBroker.Default.ClearSubs();
        }
    }
}