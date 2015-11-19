using IntuiLab.Leap.Exceptions;
using System;

namespace IntuiLab.Leap
{
    /// <summary>
    /// This class contains the configurable parameters for pointing
    /// </summary>
    public class ParametersPointing
    {
        #region Singleton Pattern

        private static volatile ParametersPointing m_Instance = null;
        private static object syncRoot = new Object();

        /// <summary>
        /// Singleton
        /// </summary>
        public static ParametersPointing Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new ParametersPointing();
                        }
                    }
                }
                return m_Instance;
            }
        }

        public ParametersPointing()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
                InitializeParameters();
            }
            else
            {
                throw new LeapException("ParametersPointing is already instancied");
            }
        }

        #endregion

        #region Pointing Parameters

        public float MinXOfVirtualScreen { get; set; }
        public float MaxXOfVirtualScreen { get; set; }
        public float MinYOfVirtualScreen { get; set; }
        public float MaxYOfVirtualScreen { get; set; }
        public float ZLimitForTouch { get; set; }

        #endregion

        #region Initialization

        private void InitializeParameters()
        {
            // Pointing parameters
            MinXOfVirtualScreen = -100; // mm
            MaxXOfVirtualScreen = 100; // mm
            MinYOfVirtualScreen = 100; // mm
            MaxYOfVirtualScreen = 200; // mm
            ZLimitForTouch = -80;
        }

        #endregion
    }
}
