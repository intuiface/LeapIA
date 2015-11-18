using IntuiLab.Leap.Exceptions;
using System;
using System.Collections.Generic;

namespace IntuiLab.Leap
{
    internal class LeapPlugin : IDisposable
    {
        /// <summary>
        /// The LeapListener instance
        /// </summary>
        private LeapListener m_LeapListener;
        public LeapListener LeapListener
        {
            get
            {
                return m_LeapListener;
            }
        }

        /// <summary>
        /// Singleton
        /// </summary>
        private static LeapPlugin m_Instance;
        public static LeapPlugin Instance
        {
            get
            {
                return m_Instance;
            }
        }

        #region Constructor

        public LeapPlugin()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
                m_LeapListener = new LeapListener();
            }
            else
            {
                throw new LeapException("LeapPlugin is already instancied");
            }
        }

        #endregion

        /// <summary>
        /// Clear the resources
        /// </summary>
        public void Dispose()
        {
            if (m_Instance != null)
            {
                m_LeapListener.Dispose();
                m_Instance = null;
            }
        }
    }

    /// <summary>
    /// Main class for the Start and Stop methods called by the player
    /// </summary>
    public class Main
    {
        #region Attributes
        /// <summary>
        /// Counter of Start calls
        /// </summary>
        private static int m_nbInterfaceStart = 0;

        /// <summary>
        /// List of instanciated facades
        /// </summary>
        private static List<IDisposable> m_lstFacades = new List<IDisposable>();
        #endregion

        public static void Start()
        {
            m_nbInterfaceStart++;
        }


        /// <summary>
        /// Register a facade
        /// </summary>
        /// <param name="facade"></param>
        public static void RegisterFacade(IDisposable facade)
        {
            if (m_lstFacades.Contains(facade) == false)
            {
                m_lstFacades.Add(facade);
            }
        }

        public static void Stop()
        {
            m_nbInterfaceStart--;
            if (m_nbInterfaceStart == 0)
            {
                // All IA have been stopped
                foreach (var facade in m_lstFacades)
                {
                    facade.Dispose();
                }

                LeapPlugin.Instance.Dispose();
            }
        }
    }
}
