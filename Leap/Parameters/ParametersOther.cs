using IntuiLab.Leap.Exceptions;
using System;

namespace IntuiLab.Leap
{
    /// <summary>
    /// This class contains the configurable parameters to permits to change them without recompile the solution
    /// </summary>
    public class ParametersOther
    {
        #region Singleton Pattern

        private static volatile ParametersOther m_Instance = null;
        private static object syncRoot = new Object();

        /// <summary>
        /// Singleton
        /// </summary>
        public static ParametersOther Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new ParametersOther();
                        }
                    }
                }
                return m_Instance;
            }
        }

        public ParametersOther()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
                InitializeParameters();
            }
            else
            {
                throw new LeapException("ParametersOther is already instancied");
            }
        }

        #endregion

        #region Constants

        const float DEFAULT_MAX_ANGLE_FOR_LINEAR_DIRECTION = 0.5f; // 0.5 =~ pi/6 // CAUTION
        const float DEFAULT_MAX_DISPLACEMENT_FOR_IMMOBILITY = 3; // mm
        const float DEFAULT_AREA_BOUNDARY_FOR_IMMOBILITY = 60; // mm
        const float DEFAULT_X_BOUNDARY_FOR_HAND_DETECTION = 200; // mm
        const float DEFAULT_Z_BOUNDARY_FOR_HAND_DETECTION = 250; // mm
        const int DEFAULT_MAX_NB_FRAMES = 10;

        #endregion

        #region LeapMath Parameters
        public readonly float LeapMath_maxAngleForLinearDirection = DEFAULT_MAX_ANGLE_FOR_LINEAR_DIRECTION;
        public readonly float LeapMath_maxDisplacementForImmobility = DEFAULT_MAX_DISPLACEMENT_FOR_IMMOBILITY;
        public readonly float LeapMath_areaBoundaryForImmobility = DEFAULT_AREA_BOUNDARY_FOR_IMMOBILITY;
        public readonly float LeapMath_xBoundaryForHandDetection = DEFAULT_X_BOUNDARY_FOR_HAND_DETECTION;
        public readonly float LeapMath_zBoundaryForHandDetection = DEFAULT_Z_BOUNDARY_FOR_HAND_DETECTION;
        #endregion

        #region LeapListener Parameters
        public readonly int LeapListener_maxNbFrames = DEFAULT_MAX_NB_FRAMES; // to lower the frame rate of the Leap device
        #endregion

        #region CSVWriter Parameters
        public string CSVWriter_path { get; set; }
        #endregion

        #region Global Parameters
        internal bool SwipeLeftEnable { get; set; }
        internal bool SwipeRightEnable { get; set; }
        internal bool TapEnable { get; set; }
        internal bool PushEnable { get; set; }
        internal bool RockEnable { get; set; }
        internal bool ScissorsEnable { get; set; }
        internal bool PaperEnable { get; set; }
        internal bool ZeroEnable { get; set; }
        internal bool OneEnable { get; set; }
        internal bool TwoEnable { get; set; }
        internal bool ThreeEnable { get; set; }
        internal bool FourEnable { get; set; }
        internal bool FiveEnable { get; set; }

        internal bool GestureDetectionEnable
        {
            get
            {
                return (SwipeLeftEnable || SwipeRightEnable || TapEnable || PushEnable);
            }
        }

        internal bool PostureDetectionEnable
        {
            get
            {
                return (RockEnable || ScissorsEnable || PaperEnable || ZeroEnable || OneEnable || TwoEnable || ThreeEnable || FourEnable || FiveEnable);
            }
        }

        internal bool FingerPointingEnable { get; set; }
        #endregion

        #region Initialization

        /// <summary>
        /// Initialize all the parameters of the DLL to the default values
        /// </summary>
        private void InitializeParameters()
        {
            // CSVWriter parameters
            CSVWriter_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CSVdata";

            // Global parameters
            SwipeLeftEnable = false;
            SwipeRightEnable = false;
            TapEnable = false;
            PushEnable = false;
            RockEnable = false;
            ScissorsEnable = false;
            PaperEnable = false;
            ZeroEnable = false;
            OneEnable = false;
            TwoEnable = false;
            ThreeEnable = false;
            FourEnable = false;
            FiveEnable = false;

            FingerPointingEnable = false;
        }

        #endregion
    }
}
