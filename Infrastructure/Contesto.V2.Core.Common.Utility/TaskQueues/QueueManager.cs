//-------------------------------------------------------------------------------------------
//** Copyright © 2018, Fulcrum Digital                                  **
//** All rights reserved.                                                                  **
//**                                                                                       **
//** Redistribution, re-engineering or use of this code - in source                        **
//** or binary forms with or without modifications, are not                                **
//** permitted without prior written consent from appropriate person                       **
//** in Fulcrum Digital                                                 **
//**                                                                                       **
//**                                                                                       **
//** Author    : Fulcrum World Wide                                                        **
//** Created   : 27-Jun-18                                                                 **
//** Purpose   : Queue Manager                                                             **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using System.Collections.Concurrent;

namespace Contesto.V2.Core.Common.Utility.TaskQueues
{
    /// <summary>
    /// Queue Manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueueManager<T> where T : class
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static volatile QueueManager<T> _instance;

        /// <summary>
        /// The synchronize root
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// The task queue
        /// </summary>
        private ConcurrentQueue<T> _taskQueue = new ConcurrentQueue<T>();

        /// <summary>
        /// Prevents a default instance of the <see cref="QueueManager{T}"/> class from being created.
        /// </summary>
        private QueueManager() { }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static QueueManager<T> Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    if (_instance == null)
                        _instance = new QueueManager<T>();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Enqueues the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Enqueue(T model)
        {
            _taskQueue.Enqueue(model);
        }

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            T value;
            while (_taskQueue.TryDequeue(out value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            T value;
            while (_taskQueue.TryPeek(out value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _taskQueue.Count;
        }

        /// <summary>
        /// Items this instance.
        /// </summary>
        /// <returns></returns>
        public ConcurrentQueue<T> Items()
        {
            return _taskQueue;
        }
    }
}