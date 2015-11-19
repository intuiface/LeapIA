using IntuiLab.Leap.Exceptions;
using System;

namespace IntuiLab.Leap
{
    /// <summary>
    /// This class contains the configurable parameters for Postures
    /// </summary>
    public class ParametersPosture
    {
        #region Singleton Pattern

        private static volatile ParametersPosture m_Instance = null;
        private static object syncRoot = new Object();

        /// <summary>
        /// Singleton
        /// </summary>
        public static ParametersPosture Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new ParametersPosture();
                        }
                    }
                }
                return m_Instance;
            }
        }

        public ParametersPosture()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
                InitializeParameters();
            }
            else
            {
                throw new LeapException("ParametersPosture is already instancied");
            }
        }

        #endregion

        #region Constants

        const int DEFAULT_POSTURE_END_TIMER_INTERVAL = 100;

        #endregion

        #region Parameters

        public int NbMinFrames { get; set; }
        public readonly int PostureEndTimerInterval = DEFAULT_POSTURE_END_TIMER_INTERVAL;

        #endregion

        #region Initialization

        private void InitializeParameters()
        {
            // l parameters
            NbMinFrames = 10; // represents the number of frames the hand must be in position for postures to begin to be recognized
        }

        #endregion
    }
}
