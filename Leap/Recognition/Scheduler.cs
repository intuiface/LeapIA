            // ****************************************************************************
            // <copyright file="Scheduler.cs" company="IntuiLab">
            // INTUILAB CONFIDENTIAL
			//_____________________
			// [2002] - [2015] IntuiLab SA
			// All Rights Reserved.
			// NOTICE: All information contained herein is, and remains
			// the property of IntuiLab SA. The intellectual and technical
			// concepts contained herein are proprietary to IntuiLab SA
			// and may be covered by U.S. and other country Patents, patents
			// in process, and are protected by trade secret or copyright law.
			// Dissemination of this information or reproduction of this
			// material is strictly forbidden unless prior written permission
			// is obtained from IntuiLab SA.
            // </copyright>
            // ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntuiLab.Leap.Recognition.Gestures;
using IntuiLab.Leap.Recognition.Postures;
using IntuiLab.Leap.DataStructures;
using Leap;
using System.Timers;
using System.Diagnostics;

namespace IntuiLab.Leap.Recognition
{
    /// <summary>
    /// Schedules the data and sends them to the different recognizers.
    /// The class uses IFrame to be more generic and to accept different data sources.
    /// </summary>
    internal class Scheduler
    {
        #region Fields

        /// <summary>
        /// The gesture recognizer
        /// </summary>
        private GestureRecognizer m_GestureRecognizer;

        /// <summary>
        /// The posture recognizer
        /// </summary>
        private PostureRecognizer m_PostureRecognizer;

        /// <summary>
        /// The list of frames sent to the gesture recognizer
        /// </summary>
        private List<IFrame> m_FramesForGestureRecognizer;
        /// <summary>
        /// The list of frames sent to the posture recognizer
        /// </summary>
        private List<IFrame> m_FramesForPostureRecognizer;

        private bool m_VerboseMode;
        //private bool m_EmptyPeriod;
        //private long m_FirstFingerTimestamp;

        /****** For the gestures buffer ******/
        /// <summary>
        /// The buffer of events
        /// </summary>
        private List<GestureDetectedEventArgs> m_Buffer;
        /// <summary>
        /// The flag to indicate if a gesture detection is in progress
        /// </summary>
        private Dictionary<GestureType, bool> m_FlagGesture;
        /// <summary>
        /// Timer started when a gesture is detected and which looks for the end of the gesture
        /// </summary>
        private Timer m_TimesUpTimer;
        /// <summary>
        /// Timer which will be started if the gesture is not finished when the timesUpTimer elapsed, and which will force the gesture to end if it takes too long
        /// </summary>
        private Timer m_DelayTimer;
        /// <summary>
        /// Count the times the delayTimer elapsed
        /// </summary>
        private int m_DelayTimerCounter;
        /************************************/


        /******** For the postures **********/
        /// <summary>
        /// The flag to indicate if a posture DETECTION is in progress (it only indicates if the detection is in progress, not if the posture is held after the raise of the succeed event)
        /// </summary>
        private Dictionary<PostureType, bool> m_FlagPosture;
        /// <summary>
        /// Timer started when a posture detection is in progress and which will verify if the posture detection is lost
        /// </summary>
        private Timer m_PostureLostTimer;
        /// <summary>
        /// Counters for events "postureInProgress", to validate the detection of a posture
        /// </summary>
        private int m_RockPostureCounter;
        private int m_PaperPostureCounter;
        private int m_ScissorsPostureCounter;
        private int m_ZeroPostureCounter;
        private int m_OnePostureCounter;
        private int m_TwoPostureCounter;
        private int m_ThreePostureCounter;
        private int m_FourPostureCounter;
        private int m_FivePostureCounter;
        /*************************************/

        #endregion

        #region Events

        /*********************** Events raised when a gesture is detected **********************************************/               
        public event GestureDetectedEventHandler NoGestureDetected;

        public event GestureDetectedEventHandler SwipeLeftGestureDetected;
        public event GestureDetectedEventHandler SwipeRightGestureDetected;

        public event GestureDetectedEventHandler TapGestureDetected;

        public event GestureDetectedEventHandler PushGestureDetected;

        public void RaiseNoGestureDetectedEvent(GestureDetectedEventArgs e)
        {
            if (NoGestureDetected != null)
                NoGestureDetected(this, e);
        }

        public void RaiseSwipeLeftGestureEvent(GestureDetectedEventArgs e)
        {
            if (SwipeLeftGestureDetected != null)
                SwipeLeftGestureDetected(this, e);
        }

        public void RaiseSwipeRightGestureEvent(GestureDetectedEventArgs e)
        {
            if (SwipeRightGestureDetected != null)
                SwipeRightGestureDetected(this, e);
        }

        private void RaiseTapGestureEvent(GestureDetectedEventArgs e)
        {
            if (TapGestureDetected != null)
                TapGestureDetected(this, e);
        }

        private void RaisePushGestureEvent(GestureDetectedEventArgs e)
        {
            if (PushGestureDetected != null)
                PushGestureDetected(this, e);
        }
        /***************************************************************************************************************/

        /*********************** Events raised when a posture succeed **************************************************/
        public event PostureDetectedEventHandler RockPostureSucceed;
        public event PostureDetectedEventHandler PaperPostureSucceed;
        public event PostureDetectedEventHandler ScissorsPostureSucceed;

        public event PostureDetectedEventHandler ZeroFingerPostureSucceed;
        public event PostureDetectedEventHandler OneFingerPostureSucceed;
        public event PostureDetectedEventHandler TwoFingersPostureSucceed;
        public event PostureDetectedEventHandler ThreeFingersPostureSucceed;
        public event PostureDetectedEventHandler FourFingersPostureSucceed;
        public event PostureDetectedEventHandler FiveFingersPostureSucceed;

        private void RaiseRockPostureSucceed(PostureDetectedEventArgs e)
        {
            if (RockPostureSucceed != null)
                RockPostureSucceed(this, e);
        }

        private void RaisePaperPostureSucceed(PostureDetectedEventArgs e)
        {
            if (PaperPostureSucceed != null)
                PaperPostureSucceed(this, e);
        }

        private void RaiseScissorsPostureSucceed(PostureDetectedEventArgs e)
        {
            if (ScissorsPostureSucceed != null)
                ScissorsPostureSucceed(this, e);
        }

        private void RaiseZeroFingerPostureSucceed(PostureDetectedEventArgs e)
        {
            if (ZeroFingerPostureSucceed != null)
                ZeroFingerPostureSucceed(this, e);
        }

        private void RaiseOneFingerPostureSucceed(PostureDetectedEventArgs e)
        {
            if (OneFingerPostureSucceed != null)
                OneFingerPostureSucceed(this, e);
        }

        private void RaiseTwoFingersPostureSucceed(PostureDetectedEventArgs e)
        {
            if (TwoFingersPostureSucceed != null)
                TwoFingersPostureSucceed(this, e);
        }

        private void RaiseThreeFingersPostureSucceed(PostureDetectedEventArgs e)
        {
            if (ThreeFingersPostureSucceed != null)
                ThreeFingersPostureSucceed(this, e);
        }

        private void RaiseFourFingersPostureSucceed(PostureDetectedEventArgs e)
        {
            if (FourFingersPostureSucceed != null)
                FourFingersPostureSucceed(this, e);
        }

        private void RaiseFiveFingersPostureSucceed(PostureDetectedEventArgs e)
        {
            if (FiveFingersPostureSucceed != null)
                FiveFingersPostureSucceed(this, e);
        }
        /***************************************************************************************************************/

        /********************* Events raised when a posture detection is in progress ***********************************/
        public event PostureDetectedEventHandler RockPostureDetectionInProgress;
        public event PostureDetectedEventHandler PaperPostureDetectionInProgress;
        public event PostureDetectedEventHandler ScissorsPostureDetectionInProgress;

        public event PostureDetectedEventHandler ZeroFingerPostureDetectionInProgress;
        public event PostureDetectedEventHandler OneFingerPostureDetectionInProgress;
        public event PostureDetectedEventHandler TwoFingersPostureDetectionInProgress;
        public event PostureDetectedEventHandler ThreeFingersPostureDetectionInProgress;
        public event PostureDetectedEventHandler FourFingersPostureDetectionInProgress;
        public event PostureDetectedEventHandler FiveFingersPostureDetectionInProgress;

        private void RaiseRockPostureDetectionInProgress(PostureDetectedEventArgs e, int percentage)
        {
            if (RockPostureDetectionInProgress != null)
                RockPostureDetectionInProgress(this, new PostureDetectedEventArgs(PostureType.Rock, percentage, e.Timestamp));
        }

        private void RaisePaperPostureDetectionInProgress(PostureDetectedEventArgs e, int percentage)
        {
            if (PaperPostureDetectionInProgress != null)
                PaperPostureDetectionInProgress(this, new PostureDetectedEventArgs(PostureType.Paper, percentage, e.Timestamp));
        }

        private void RaiseScissorsPostureDetectionInProgress(PostureDetectedEventArgs e, int percentage)
        {
            if (ScissorsPostureDetectionInProgress != null)
                ScissorsPostureDetectionInProgress(this, new PostureDetectedEventArgs(PostureType.Scissors, percentage, e.Timestamp));
        }

        private void RaiseZeroFingerPostureDetectionInProgress(PostureDetectedEventArgs e, int percentage)
        {
            if (ZeroFingerPostureDetectionInProgress != null)
                ZeroFingerPostureDetectionInProgress(this, new PostureDetectedEventArgs(PostureType.Zero, percentage, e.Timestamp));
        }

        private void RaiseOneFingerPostureDetectionInProgress(PostureDetectedEventArgs e, int percentage)
        {
            if (OneFingerPostureDetectionInProgress != null)
                OneFingerPostureDetectionInProgress(this, new PostureDetectedEventArgs(PostureType.One, percentage, e.Timestamp));
        }

        private void RaiseTwoFingersPostureDetectionInProgress(PostureDetectedEventArgs e, int percentage)
        {
            if (TwoFingersPostureDetectionInProgress != null)
                TwoFingersPostureDetectionInProgress(this, new PostureDetectedEventArgs(PostureType.Two, percentage, e.Timestamp));
        }

        private void RaiseThreeFingersPostureDetectionInProgress(PostureDetectedEventArgs e, int percentage)
        {
            if (ThreeFingersPostureDetectionInProgress != null)
                ThreeFingersPostureDetectionInProgress(this, new PostureDetectedEventArgs(PostureType.Three, percentage, e.Timestamp));
        }

        private void RaiseFourFingersPostureDetectionInProgress(PostureDetectedEventArgs e, int percentage)
        {
            if (FourFingersPostureDetectionInProgress != null)
                FourFingersPostureDetectionInProgress(this, new PostureDetectedEventArgs(PostureType.Four, percentage, e.Timestamp));
        }

        private void RaiseFiveFingersPostureDetectionInProgress(PostureDetectedEventArgs e, int percentage)
        {
            if (FiveFingersPostureDetectionInProgress != null)
                FiveFingersPostureDetectionInProgress(this, new PostureDetectedEventArgs(PostureType.Five, percentage, e.Timestamp));
        }
        /*********************************************************************************************************************/

        /***************************** Events raised when a posture detection is lost ****************************************/
        public event EventHandler RockPostureDetectionLost;
        public event EventHandler PaperPostureDetectionLost;
        public event EventHandler ScissorsPostureDetectionLost;

        public event EventHandler ZeroFingerPostureDetectionLost;
        public event EventHandler OneFingerPostureDetectionLost;
        public event EventHandler TwoFingersPostureDetectionLost;
        public event EventHandler ThreeFingersPostureDetectionLost;
        public event EventHandler FourFingersPostureDetectionLost;
        public event EventHandler FiveFingersPostureDetectionLost;

        private void RaiseRockPostureDetectionLost()
        {
            if (RockPostureDetectionLost != null)
                RockPostureDetectionLost(this, new EventArgs());
        }

        private void RaisePaperPostureDetectionLost()
        {
            if (PaperPostureDetectionLost != null)
                PaperPostureDetectionLost(this, new EventArgs());
        }

        private void RaiseScissorsPostureDetectionLost()
        {
            if (ScissorsPostureDetectionLost != null)
                ScissorsPostureDetectionLost(this, new EventArgs());
        }

        private void RaiseZeroFingerPostureDetectionLost()
        {
            if (ZeroFingerPostureDetectionLost != null)
                ZeroFingerPostureDetectionLost(this, new EventArgs());
        }

        private void RaiseOneFingerPostureDetectionLost()
        {
            if (OneFingerPostureDetectionLost != null)
                OneFingerPostureDetectionLost(this, new EventArgs());
        }

        private void RaiseTwoFingersPostureDetectionLost()
        {
            if (TwoFingersPostureDetectionLost != null)
                TwoFingersPostureDetectionLost(this, new EventArgs());
        }

        private void RaiseThreeFingersPostureDetectionLost()
        {
            if (ThreeFingersPostureDetectionLost != null)
                ThreeFingersPostureDetectionLost(this, new EventArgs());
        }

        private void RaiseFourFingersPostureDetectionLost()
        {
            if (FourFingersPostureDetectionLost != null)
                FourFingersPostureDetectionLost(this, new EventArgs());
        }

        private void RaiseFiveFingersPostureDetectionLost()
        {
            if (FiveFingersPostureDetectionLost != null)
                FiveFingersPostureDetectionLost(this, new EventArgs());
        }
        /*************************************************************************************************************************/

        #endregion

        #region Constructor

        public Scheduler(bool verboseMode)
        {
            // initialisation of all the fields
            this.m_GestureRecognizer = new GestureRecognizer(verboseMode);
            this.m_PostureRecognizer = new PostureRecognizer(verboseMode);
            this.m_FramesForGestureRecognizer = new List<IFrame>(ParametersGesture.Instance.NbMinFrames);
            this.m_FramesForPostureRecognizer = new List<IFrame>(ParametersPosture.Instance.NbMinFrames);
            this.m_VerboseMode = verboseMode;
            //this.m_EmptyPeriod = false;
            //this.m_FirstFingerTimestamp = 0;

            this.m_Buffer = new List<GestureDetectedEventArgs>();
            this.m_FlagGesture = new Dictionary<GestureType, bool>(4)
            {
                {GestureType.SwipeLeft, false},
                {GestureType.SwipeRight, false},
                {GestureType.Tap, false},
                {GestureType.Push, false}
            };

            this.m_TimesUpTimer = new Timer(ParametersScheduler.Instance.TimesUpTimerInterval);
            this.m_DelayTimer = new Timer(ParametersScheduler.Instance.DelayTimerInterval);
            this.m_TimesUpTimer.Elapsed += OnTimesUpTimerElapsed;
            this.m_DelayTimer.Elapsed += OnDelayTimerElapsed;
            this.m_DelayTimerCounter = 0;

            this.m_FlagPosture = new Dictionary<PostureType, bool>(8)
            {
                {PostureType.Rock, false},
                {PostureType.Scissors, false},
                {PostureType.Paper, false},
                {PostureType.Zero, false},
                {PostureType.One, false},
                {PostureType.Two, false},
                {PostureType.Three, false},
                {PostureType.Four, false},
                {PostureType.Five, false}
            };

            this.m_PostureLostTimer = new Timer(ParametersScheduler.Instance.PostureLostTimerInterval);
            this.m_PostureLostTimer.Elapsed += OnPostureLostTimerElapsed;
        }

        #endregion

        #region Enable/Disable Gestures

        /// <summary>
        /// Enable the recognition of a specific gesture
        /// </summary>
        /// <param name="gestureType">The type of gesture to enable</param>
        internal void EnableGestureRecognition(GestureType gestureType)
        {
            switch (gestureType)
            {
                // we subscribe to the corresponding events from the gesture recognizer
                case GestureType.SwipeLeft:
                    m_GestureRecognizer.SwipeLeftGestureBegin += OnSwipeLeftGestureBegin;
                    m_GestureRecognizer.SwipeLeftGestureDetected_GestureRecognizer += OnSwipeLeftDetected;
                    m_GestureRecognizer.SwipeLeftGestureEnd += OnSwipeLeftGestureEnd;
                    break;
                case GestureType.SwipeRight:
                    m_GestureRecognizer.SwipeRightGestureBegin += OnSwipeRightGestureBegin;
                    m_GestureRecognizer.SwipeRightGestureDetected_GestureRecognizer += OnSwipeRightDetected;
                    m_GestureRecognizer.SwipeRightGestureEnd += OnSwipeRightGestureEnd;
                    break;
                case GestureType.Tap:
                    m_GestureRecognizer.TapGestureBegin += OnTapGestureBegin;
                    m_GestureRecognizer.TapGestureDetected_GestureRecognizer += OnTapGestureDetected;
                    m_GestureRecognizer.TapGestureEnd += OnTapGestureEnd;
                    break;
                case GestureType.Push:
                    m_GestureRecognizer.PushGestureBegin += OnPushGestureBegin;
                    m_GestureRecognizer.PushGestureDetected_GestureRecognizer += OnPushGestureDetected;
                    m_GestureRecognizer.PushGestureEnd += OnPushGestureEnd;
                    break;
                case GestureType.None:
                    m_GestureRecognizer.NoGestureDetected_GestureRecognizer += OnNoGestureDetected;
                    break;
            }
        }

        /// <summary>
        /// Disable the recognition of a specific gesture
        /// </summary>
        /// <param name="gestureType">The type of gesture to disable</param>
        internal void DisableGestureRecognition(GestureType gestureType)
        {
            switch (gestureType)
            {
                // we unsubscribe to the corresponding events from the gesture recognizer
                case GestureType.SwipeLeft:
                    m_GestureRecognizer.SwipeLeftGestureBegin -= OnSwipeLeftGestureBegin;
                    m_GestureRecognizer.SwipeLeftGestureDetected_GestureRecognizer -= OnSwipeLeftDetected;
                    m_GestureRecognizer.SwipeLeftGestureEnd -= OnSwipeLeftGestureEnd;
                    break;
                case GestureType.SwipeRight:
                    m_GestureRecognizer.SwipeRightGestureBegin -= OnSwipeRightGestureBegin;
                    m_GestureRecognizer.SwipeRightGestureDetected_GestureRecognizer -= OnSwipeRightDetected;
                    m_GestureRecognizer.SwipeRightGestureEnd -= OnSwipeRightGestureEnd;
                    break;
                case GestureType.Tap:
                    m_GestureRecognizer.TapGestureBegin -= OnTapGestureBegin;
                    m_GestureRecognizer.TapGestureDetected_GestureRecognizer -= OnTapGestureDetected;
                    m_GestureRecognizer.TapGestureEnd -= OnTapGestureEnd;
                    break;
                case GestureType.Push:
                    m_GestureRecognizer.PushGestureBegin -= OnPushGestureBegin;
                    m_GestureRecognizer.PushGestureDetected_GestureRecognizer -= OnPushGestureDetected;
                    m_GestureRecognizer.PushGestureEnd -= OnPushGestureEnd;
                    break;
                case GestureType.None:
                    m_GestureRecognizer.NoGestureDetected_GestureRecognizer -= OnNoGestureDetected;
                    break;
            }
        }

        #endregion

        #region Enable/Disable Postures

        /// <summary>
        /// Enable the recognition of a specific posture
        /// </summary>
        /// <param name="postureType">The type of posture to enable</param>
        internal void EnablePostureRecognition(PostureType postureType)
        {
            switch (postureType)
            {
                // we subscribe to the corresponding events from the posture recognizer
                case PostureType.Rock:
                    m_PostureRecognizer.PostureRockBegin += OnPostureRockBegin;
                    m_PostureRecognizer.PostureRockHeld += OnPostureRockHeld;
                    break;
                case PostureType.Scissors:
                    m_PostureRecognizer.PostureScissorsBegin += OnPostureScissorsBegin;
                    m_PostureRecognizer.PostureScissorsHeld += OnPostureScissorsHeld;
                    break;
                case PostureType.Paper:
                    m_PostureRecognizer.PosturePaperBegin += OnPosturePaperBegin;
                    m_PostureRecognizer.PosturePaperHeld += OnPosturePaperHeld;
                    break;
                case PostureType.Zero:
                    m_PostureRecognizer.PostureZeroBegin += OnPostureZeroBegin;
                    m_PostureRecognizer.PostureZeroHeld += OnPostureZeroHeld;
                    break;
                case PostureType.One:
                    m_PostureRecognizer.PostureOneBegin += OnPostureOneBegin;
                    m_PostureRecognizer.PostureOneHeld += OnPostureOneHeld;
                    break;
                case PostureType.Two:
                    m_PostureRecognizer.PostureTwoBegin += OnPostureTwoBegin;
                    m_PostureRecognizer.PostureTwoHeld += OnPostureTwoHeld;
                    break;
                case PostureType.Three:
                    m_PostureRecognizer.PostureThreeBegin += OnPostureThreeBegin;
                    m_PostureRecognizer.PostureThreeHeld += OnPostureThreeHeld;
                    break;
                case PostureType.Four:
                    m_PostureRecognizer.PostureFourBegin += OnPostureFourBegin;
                    m_PostureRecognizer.PostureFourHeld += OnPostureFourHeld;
                    break;
                case PostureType.Five:
                    m_PostureRecognizer.PostureFiveBegin += OnPostureFiveBegin;
                    m_PostureRecognizer.PostureFiveHeld += OnPostureFiveHeld;
                    break;
            }
        }

        /// <summary>
        /// Disable the recognition of a specific posture
        /// </summary>
        /// <param name="postureType">The type of posture to disable</param>
        internal void DisablePostureRecognition(PostureType postureType)
        {
            switch (postureType)
            {
                // we unsubscribe to the corresponding events from the posture recognizer
                case PostureType.Rock:
                    m_PostureRecognizer.PostureRockBegin -= OnPostureRockBegin;
                    m_PostureRecognizer.PostureRockHeld -= OnPostureRockHeld;
                    break;
                case PostureType.Scissors:
                    m_PostureRecognizer.PostureScissorsBegin -= OnPostureScissorsBegin;
                    m_PostureRecognizer.PostureScissorsHeld -= OnPostureScissorsHeld;
                    break;
                case PostureType.Paper:
                    m_PostureRecognizer.PosturePaperBegin -= OnPosturePaperBegin;
                    m_PostureRecognizer.PosturePaperHeld -= OnPosturePaperHeld;
                    break;
                case PostureType.Zero:
                    m_PostureRecognizer.PostureZeroBegin -= OnPostureZeroBegin;
                    m_PostureRecognizer.PostureZeroHeld -= OnPostureZeroHeld;
                    break;
                case PostureType.One:
                    m_PostureRecognizer.PostureOneBegin -= OnPostureOneBegin;
                    m_PostureRecognizer.PostureOneHeld -= OnPostureOneHeld;
                    break;
                case PostureType.Two:
                    m_PostureRecognizer.PostureTwoBegin -= OnPostureTwoBegin;
                    m_PostureRecognizer.PostureTwoHeld -= OnPostureTwoHeld;
                    break;
                case PostureType.Three:
                    m_PostureRecognizer.PostureThreeBegin -= OnPostureThreeBegin;
                    m_PostureRecognizer.PostureThreeHeld -= OnPostureThreeHeld;
                    break;
                case PostureType.Four:
                    m_PostureRecognizer.PostureFourBegin -= OnPostureFourBegin;
                    m_PostureRecognizer.PostureFourHeld -= OnPostureFourHeld;
                    break;
                case PostureType.Five:
                    m_PostureRecognizer.PostureFiveBegin -= OnPostureFiveBegin;
                    m_PostureRecognizer.PostureFiveHeld -= OnPostureFiveHeld;
                    break;
            }
        }

        #endregion

        #region Callbacks

        #region Callbacks for Gestures

        /************************* Callbacks for gestures in progress *******************************/

        // All those callbacks do the same thing for the different gestures.
        // First, we check if a minimum time since the last posture recognition is elapsed, to avoid gestures just after the detection of a posture.
        // If it is ok, we add the event to the gesture buffer and restart the timesUpTimer (which will be restart each time a gesture event is intercept).
        // This timer will check if gestures are finished. If not, it will start the delayTimer.
        // This last timer will wait for or force the end of gestures, and then sort the buffer.

        private void OnSwipeLeftDetected(object sender, GestureDetectedEventArgs e)
        {
            if (Math.Abs(e.Timestamp - this.m_PostureRecognizer.LastPostureTimestamp.Item2) > ParametersScheduler.Instance.MinTimeAfterPostureDetections)
            {
                m_Buffer.Add(e);
                RestartTimer_Scheduler(m_TimesUpTimer);
                m_DelayTimer.Stop();

                if (m_VerboseMode)
                    Console.WriteLine("SWIPE LEFT Timestamp=" + e.Timestamp);
            }
        }

        private void OnSwipeRightDetected(object sender, GestureDetectedEventArgs e)
        {
            if (Math.Abs(e.Timestamp - this.m_PostureRecognizer.LastPostureTimestamp.Item2) > ParametersScheduler.Instance.MinTimeAfterPostureDetections)
            {
                m_Buffer.Add(e);
                RestartTimer_Scheduler(m_TimesUpTimer);
                m_DelayTimer.Stop();

                if (m_VerboseMode)
                    Console.WriteLine("SWIPE RIGHT Timestamp=" + e.Timestamp);
            }
        }

        private void OnTapGestureDetected(object sender, GestureDetectedEventArgs e)
        {
            if (Math.Abs(e.Timestamp - this.m_PostureRecognizer.LastPostureTimestamp.Item2) > ParametersScheduler.Instance.MinTimeAfterPostureDetections)
            {
                m_Buffer.Add(e);
                RestartTimer_Scheduler(m_TimesUpTimer);
                m_DelayTimer.Stop();

                if (m_VerboseMode)
                    Console.WriteLine("TAP Timestamp=" + e.Timestamp);
            }
        }

        private void OnPushGestureDetected(object sender, GestureDetectedEventArgs e)
        {
            if (Math.Abs(e.Timestamp - this.m_PostureRecognizer.LastPostureTimestamp.Item2) > ParametersScheduler.Instance.MinTimeAfterPostureDetections)
            {
                m_Buffer.Add(e);
                RestartTimer_Scheduler(m_TimesUpTimer);
                m_DelayTimer.Stop();

                if (m_VerboseMode)
                    Console.WriteLine("PUSH Timestamp=" + e.Timestamp);
            }
        }

        private void OnNoGestureDetected(object sender, GestureDetectedEventArgs e)
        {
            RaiseNoGestureDetectedEvent(e);
        }
        /**********************************************************************************************/

        /********************** Callbacks for gesture's start ****************************************/

        // Those callbacks just set the flag of the corresponding gesture to true.

        private void OnSwipeLeftGestureBegin(object sender, GestureDetectedEventArgs e)
        {
            m_FlagGesture[GestureType.SwipeLeft] = true;

            if (m_VerboseMode)
                Console.WriteLine("SWIPE LEFT BEGINS Timestamp=" + e.Timestamp);
        }

        private void OnSwipeRightGestureBegin(object sender, GestureDetectedEventArgs e)
        {
            m_FlagGesture[GestureType.SwipeRight] = true;

            if (m_VerboseMode)
                Console.WriteLine("SWIPE RIGHT BEGINS Timestamp=" + e.Timestamp);
        }

        private void OnTapGestureBegin(object sender, GestureDetectedEventArgs e)
        {
            m_FlagGesture[GestureType.Tap] = true;

            if (m_VerboseMode)
                Console.WriteLine("TAP BEGINS Timestamp=" + e.Timestamp);
        }

        private void OnPushGestureBegin(object sender, GestureDetectedEventArgs e)
        {
            m_FlagGesture[GestureType.Push] = true;

            if (m_VerboseMode)
                Console.WriteLine("PUSH BEGINS Timestamp=" + e.Timestamp);
        }
        /**********************************************************************************************/
        
        /********************** Callbacks for gesture's end *******************************************/

        // Those callbacks just set the flag of the corresponding gesture to false.

        private void OnSwipeLeftGestureEnd(object sender, GestureDetectedEventArgs e)
        {
            m_FlagGesture[GestureType.SwipeLeft] = false;

            if (m_VerboseMode)
                Console.WriteLine("SWIPE LEFT ENDS Timestamp=" + e.Timestamp);
        }

        private void OnSwipeRightGestureEnd(object sender, GestureDetectedEventArgs e)
        {
            m_FlagGesture[GestureType.SwipeRight] = false;

            if (m_VerboseMode)
                Console.WriteLine("SWIPE RIGHT ENDS Timestamp=" + e.Timestamp);
        }

        private void OnTapGestureEnd(object sender, GestureDetectedEventArgs e)
        {
            m_FlagGesture[GestureType.Tap] = false;

            if (m_VerboseMode)
                Console.WriteLine("TAP ENDS Timestamp=" + e.Timestamp);
        }

        private void OnPushGestureEnd(object sender, GestureDetectedEventArgs e)
        {
            m_FlagGesture[GestureType.Push] = false;

            if (m_VerboseMode)
                Console.WriteLine("PUSH ENDS Timestamp=" + e.Timestamp);
        }
        /**********************************************************************************************/

        #endregion
        
        #region Callbacks for Postures

        /****************** Callbacks for postures begin***************************************************/

        // Those callbacks first set the flag of the corresponding posture detection to true.
        // Then, they start de postureLostTimer which will consider the posture detection as lost if it elapses.
        // To finish they propagate the event "postureInProgress" with a percentage of 0.

        private void OnPostureRockBegin(object sender, PostureDetectedEventArgs e)
        {
            m_FlagPosture[PostureType.Rock] = true;
            m_PostureLostTimer.Start();
            m_RockPostureCounter = 0;
            RaiseRockPostureDetectionInProgress(e, 0);
        }

        private void OnPostureScissorsBegin(object sender, PostureDetectedEventArgs e)
        {
            m_FlagPosture[PostureType.Scissors] = true;
            m_PostureLostTimer.Start();
            m_ScissorsPostureCounter = 0;
            RaiseScissorsPostureDetectionInProgress(e, 0);
        }

        private void OnPosturePaperBegin(object sender, PostureDetectedEventArgs e)
        {
            m_FlagPosture[PostureType.Paper] = true;
            m_PostureLostTimer.Start();
            m_PaperPostureCounter = 0;
            RaisePaperPostureDetectionInProgress(e, 0);
        }

        private void OnPostureZeroBegin(object sender, PostureDetectedEventArgs e)
        {
            m_FlagPosture[PostureType.Zero] = true;
            m_PostureLostTimer.Start();
            m_ZeroPostureCounter = 0;
            RaiseZeroFingerPostureDetectionInProgress(e, 0);
        }

        private void OnPostureOneBegin(object sender, PostureDetectedEventArgs e)
        {
            m_FlagPosture[PostureType.One] = true;
            m_PostureLostTimer.Start();
            m_OnePostureCounter = 0;
            RaiseOneFingerPostureDetectionInProgress(e, 0);
        }

        private void OnPostureTwoBegin(object sender, PostureDetectedEventArgs e)
        {
            m_FlagPosture[PostureType.Two] = true;
            m_PostureLostTimer.Start();
            m_TwoPostureCounter = 0;
            RaiseTwoFingersPostureDetectionInProgress(e, 0);
        }

        private void OnPostureThreeBegin(object sender, PostureDetectedEventArgs e)
        {
            m_FlagPosture[PostureType.Three] = true;
            m_PostureLostTimer.Start();
            m_ThreePostureCounter = 0;
            RaiseThreeFingersPostureDetectionInProgress(e, 0);
        }

        private void OnPostureFourBegin(object sender, PostureDetectedEventArgs e)
        {
            m_FlagPosture[PostureType.Four] = true;
            m_PostureLostTimer.Start();
            m_FourPostureCounter = 0;
            RaiseFourFingersPostureDetectionInProgress(e, 0);
        }

        private void OnPostureFiveBegin(object sender, PostureDetectedEventArgs e)
        {
            m_FlagPosture[PostureType.Five] = true;
            m_PostureLostTimer.Start();
            m_FivePostureCounter = 0;
            RaiseFiveFingersPostureDetectionInProgress(e, 0);
        }
        /*********************************************************************************************/

        /***************** Callbacks for postures held ***********************************************/

        // Those callbacks begin by incerment the counter of event corresponding to the posture.
        // If this counter reachs the "ParametersScheduler.Instance.NbEventsForPostureToBeDetected" value, the "postureSucceed" event is raised and the postureLostTimer is stoped.
        // Otherwise, the event "postureInProgress" with the good percentage is raised, and the postureLostTimer restarted.

        private void OnPostureRockHeld(object sender, PostureDetectedEventArgs e)
        {
            m_RockPostureCounter++;
            if (m_RockPostureCounter < ParametersScheduler.Instance.NbEventsForRockPostureToBeDetected)
            {
                RestartTimer_Scheduler(m_PostureLostTimer);
                RaiseRockPostureDetectionInProgress(e, (m_RockPostureCounter * 100) / ParametersScheduler.Instance.NbEventsForRockPostureToBeDetected);
            }
            else if (m_RockPostureCounter == ParametersScheduler.Instance.NbEventsForRockPostureToBeDetected)
            {
                m_PostureLostTimer.Stop();
                m_FlagPosture[PostureType.Rock] = false;
                RaiseRockPostureDetectionInProgress(e, 100);
                RaiseRockPostureSucceed(e);
            }
        }

        private void OnPosturePaperHeld(object sender, PostureDetectedEventArgs e)
        {
            m_PaperPostureCounter++;
            if (m_PaperPostureCounter < ParametersScheduler.Instance.NbEventsForPaperPostureToBeDetected)
            {
                RestartTimer_Scheduler(m_PostureLostTimer);
                RaisePaperPostureDetectionInProgress(e, (m_PaperPostureCounter * 100) / ParametersScheduler.Instance.NbEventsForPaperPostureToBeDetected);
            }
            else if (m_PaperPostureCounter == ParametersScheduler.Instance.NbEventsForPaperPostureToBeDetected)
            {
                m_PostureLostTimer.Stop();
                m_FlagPosture[PostureType.Paper] = false;
                RaisePaperPostureDetectionInProgress(e, 100);
                RaisePaperPostureSucceed(e);
            }
        }

        private void OnPostureScissorsHeld(object sender, PostureDetectedEventArgs e)
        {
            m_ScissorsPostureCounter++;
            if (m_ScissorsPostureCounter < ParametersScheduler.Instance.NbEventsForScissorsPostureToBeDetected)
            {
                RestartTimer_Scheduler(m_PostureLostTimer);
                RaiseScissorsPostureDetectionInProgress(e, (m_ScissorsPostureCounter * 100) / ParametersScheduler.Instance.NbEventsForScissorsPostureToBeDetected);
            }
            else if (m_ScissorsPostureCounter == ParametersScheduler.Instance.NbEventsForScissorsPostureToBeDetected)
            {
                m_PostureLostTimer.Stop();
                m_FlagPosture[PostureType.Scissors] = false;
                RaiseScissorsPostureDetectionInProgress(e, 100);
                RaiseScissorsPostureSucceed(e);
            }
        }

        private void OnPostureZeroHeld(object sender, PostureDetectedEventArgs e)
        {
            m_ZeroPostureCounter++;
            if (m_ZeroPostureCounter < ParametersScheduler.Instance.NbEventsForZeroPostureToBeDetected)
            {
                RestartTimer_Scheduler(m_PostureLostTimer);
                RaiseZeroFingerPostureDetectionInProgress(e, (m_ZeroPostureCounter * 100) / ParametersScheduler.Instance.NbEventsForZeroPostureToBeDetected);
            }
            else if (m_ZeroPostureCounter == ParametersScheduler.Instance.NbEventsForZeroPostureToBeDetected)
            {
                m_PostureLostTimer.Stop();
                m_FlagPosture[PostureType.Zero] = false;
                RaiseZeroFingerPostureDetectionInProgress(e, 100);
                RaiseZeroFingerPostureSucceed(e);
            }
        }

        private void OnPostureOneHeld(object sender, PostureDetectedEventArgs e)
        {
            m_OnePostureCounter++;
            if (m_OnePostureCounter < ParametersScheduler.Instance.NbEventsForOnePostureToBeDetected)
            {
                RestartTimer_Scheduler(m_PostureLostTimer);
                RaiseOneFingerPostureDetectionInProgress(e, (m_OnePostureCounter * 100) / ParametersScheduler.Instance.NbEventsForOnePostureToBeDetected);
            }
            else if (m_OnePostureCounter == ParametersScheduler.Instance.NbEventsForOnePostureToBeDetected)
            {
                m_PostureLostTimer.Stop();
                m_FlagPosture[PostureType.One] = false;
                RaiseOneFingerPostureDetectionInProgress(e, 100);
                RaiseOneFingerPostureSucceed(e);
            }
        }

        private void OnPostureTwoHeld(object sender, PostureDetectedEventArgs e)
        {
            m_TwoPostureCounter++;
            if (m_TwoPostureCounter < ParametersScheduler.Instance.NbEventsForTwoPostureToBeDetected)
            {
                RestartTimer_Scheduler(m_PostureLostTimer);
                RaiseTwoFingersPostureDetectionInProgress(e, (m_TwoPostureCounter * 100) / ParametersScheduler.Instance.NbEventsForTwoPostureToBeDetected);
            }
            else if (m_TwoPostureCounter == ParametersScheduler.Instance.NbEventsForTwoPostureToBeDetected)
            {
                m_PostureLostTimer.Stop();
                m_FlagPosture[PostureType.Two] = false;
                RaiseTwoFingersPostureDetectionInProgress(e, 100);
                RaiseTwoFingersPostureSucceed(e);
            }
        }

        private void OnPostureThreeHeld(object sender, PostureDetectedEventArgs e)
        {
            m_ThreePostureCounter++;
            if (m_ThreePostureCounter < ParametersScheduler.Instance.NbEventsForThreePostureToBeDetected)
            {
                RestartTimer_Scheduler(m_PostureLostTimer);
                RaiseThreeFingersPostureDetectionInProgress(e, (m_ThreePostureCounter * 100) / ParametersScheduler.Instance.NbEventsForThreePostureToBeDetected);
            }
            else if (m_ThreePostureCounter == ParametersScheduler.Instance.NbEventsForThreePostureToBeDetected)
            {
                m_PostureLostTimer.Stop();
                m_FlagPosture[PostureType.Three] = false;
                RaiseThreeFingersPostureDetectionInProgress(e, 100);
                RaiseThreeFingersPostureSucceed(e);
            }
        }

        private void OnPostureFourHeld(object sender, PostureDetectedEventArgs e)
        {
            m_FourPostureCounter++;
            if (m_FourPostureCounter < ParametersScheduler.Instance.NbEventsForFourPostureToBeDetected)
            {
                RestartTimer_Scheduler(m_PostureLostTimer);
                RaiseFourFingersPostureDetectionInProgress(e, (m_FourPostureCounter * 100) / ParametersScheduler.Instance.NbEventsForFourPostureToBeDetected);
            }
            else if (m_FourPostureCounter == ParametersScheduler.Instance.NbEventsForFourPostureToBeDetected)
            {
                m_PostureLostTimer.Stop();
                m_FlagPosture[PostureType.Four] = false;
                RaiseFourFingersPostureDetectionInProgress(e, 100);
                RaiseFourFingersPostureSucceed(e);
            }
        }

        private void OnPostureFiveHeld(object sender, PostureDetectedEventArgs e)
        {
            m_FivePostureCounter++;
            if (m_FivePostureCounter < ParametersScheduler.Instance.NbEventsForFivePostureToBeDetected)
            {
                RestartTimer_Scheduler(m_PostureLostTimer);
                RaiseFiveFingersPostureDetectionInProgress(e, (m_FivePostureCounter * 100) / ParametersScheduler.Instance.NbEventsForFivePostureToBeDetected);
            }
            else if (m_FivePostureCounter == ParametersScheduler.Instance.NbEventsForFivePostureToBeDetected)
            {
                m_PostureLostTimer.Stop();
                m_FlagPosture[PostureType.Five] = false;
                RaiseFiveFingersPostureDetectionInProgress(e, 100);
                RaiseFiveFingersPostureSucceed(e);
            }
        }
        /**************************************************************************************************************/

        /// <summary>
        /// If this timer elapses, it means that a posture detection is lost
        /// </summary>
        private void OnPostureLostTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // this function looks if a posture detection is in progress, if so it set the flag to false and raised the "postureDetectionLost" event
            m_PostureLostTimer.Stop();

            if (m_FlagPosture[PostureType.Rock] == true)
            {
                m_FlagPosture[PostureType.Rock] = false;
                RaiseRockPostureDetectionLost();
            }
            if (m_FlagPosture[PostureType.Scissors] == true)
            {
                m_FlagPosture[PostureType.Scissors] = false;
                RaiseScissorsPostureDetectionLost();
            }
            if (m_FlagPosture[PostureType.Paper] == true)
            {
                m_FlagPosture[PostureType.Paper] = false;
                RaisePaperPostureDetectionLost();
            }
            if (m_FlagPosture[PostureType.Zero] == true)
            {
                m_FlagPosture[PostureType.Zero] = false;
                RaiseZeroFingerPostureDetectionLost();
            }
            if (m_FlagPosture[PostureType.One] == true)
            {
                m_FlagPosture[PostureType.One] = false;
                RaiseOneFingerPostureDetectionLost();
            }
            if (m_FlagPosture[PostureType.Two] == true)
            {
                m_FlagPosture[PostureType.Two] = false;
                RaiseTwoFingersPostureDetectionLost();
            }
            if (m_FlagPosture[PostureType.Three] == true)
            {
                m_FlagPosture[PostureType.Three] = false;
                RaiseThreeFingersPostureDetectionLost();
            }
            if (m_FlagPosture[PostureType.Four] == true)
            {
                m_FlagPosture[PostureType.Four] = false;
                RaiseFourFingersPostureDetectionLost();
            }
            if (m_FlagPosture[PostureType.Five] == true)
            {
                m_FlagPosture[PostureType.Five] = false;
                RaiseFiveFingersPostureDetectionLost();
            }
        }

        #endregion
        
        #endregion

        /// <summary>
        /// This function prepare data and send it to the gesture recognizer
        /// </summary>
        /// <param name="frame">The actual frame</param>
        public void AddFrameGesture(IFrame frame)
        {
            #region Skip first frames (not used)
            // Skip some frames if there were no fingers in older frames
            //if (framesForGestureRecognizer.Count == PropertiesLeapPlugin.Instance.GestureRecognizer_nbMinFrames && !emptyPeriod)
            //{
            //    int nbOfEmptyFrames = 0;
            //    foreach (IFrame f in framesForGestureRecognizer)

            //    {
            //        if (f.Fingers.Count == 0)
            //        {
            //            nbOfEmptyFrames++;
            //        }
            //    }
            //    if (nbOfEmptyFrames == PropertiesLeapPlugin.Instance.GestureRecognizer_nbMinFrames)
            //    {
            //        emptyPeriod = true;
            //    }
            //}

            //if (emptyPeriod && frame.Fingers.Count > 0)
            //{
            //    firstFingerTimestamp = frame.Timestamp;
            //    emptyPeriod = false;
            //}
            #endregion

            // Addition of the frame to the list
            if (frame.IsValid)
            {
                if (m_FramesForGestureRecognizer.Count >= ParametersGesture.Instance.NbMinFrames)
                {
                    m_FramesForGestureRecognizer.RemoveAt(0);
                }
                m_FramesForGestureRecognizer.Add(frame);
            }


            // Prepare data for the gesture recognizer and process it

            if (m_FramesForGestureRecognizer.Count == ParametersGesture.Instance.NbMinFrames)
            {
                // we identify the frontmost finger
                FingerData finger = frame.FrontmostFinger;

                // we check if a movement is starting or in progress
                if (finger.IsValid)
                {
                    // we need a minimum length for the gesture (= nb minimum of frames where the finger appears)
                    bool fingerPresentInEachFrame = true;
                    foreach (IFrame f in m_FramesForGestureRecognizer)
                    {
                        if (!f.Finger(finger.Id).IsValid)
                        {
                            fingerPresentInEachFrame = false;
                        }
                    }

                    // if the finger appears at least in the nbMinFrames previous frames, we go on
                    if (fingerPresentInEachFrame)
                    {
                        // we send the list of frames to the gesture recognizer
                        List<IFrame> framesForGestureRecognizerReverse = new List<IFrame>(m_FramesForGestureRecognizer);
                        framesForGestureRecognizerReverse.Reverse();
                        m_GestureRecognizer.Process(framesForGestureRecognizerReverse, finger.Id);
                    }
                }

                // to avoid a blocking if the frame's timestamp goes back to 0
                long timeSinceLastGestureDetection = (frame.Timestamp > m_GestureRecognizer.LastGestureTimestamp.Item2) ? (frame.Timestamp - m_GestureRecognizer.LastGestureTimestamp.Item2) : 0;

                // we check if a movement is stopping
                IFrame previousFrame = m_FramesForGestureRecognizer[ParametersGesture.Instance.NbMinFrames - 2];
                if (timeSinceLastGestureDetection < ParametersScheduler.Instance.TimeBetweenTwoFrames) // if an event was raised at the previous frame
                {
                    // we send the 2 last frames to the gesture recognizer to check if the movement is stopping
                    m_GestureRecognizer.CheckGestureEnd(new List<IFrame>(2) { frame, previousFrame }, finger.Id);
                }
            }
        }



        /// <summary>
        /// This function prepare data and send it to the posture recognizer
        /// </summary>
        /// <param name="frame">The actual frame</param>
        public void AddFramePosture(IFrame frame)
        {
            // Addition of the frame to the list
            if (frame.IsValid)
            {
                if (m_FramesForPostureRecognizer.Count >= ParametersPosture.Instance.NbMinFrames)
                {
                    m_FramesForPostureRecognizer.RemoveAt(0);
                }
                m_FramesForPostureRecognizer.Add(frame);
            }

            // Prepare data for the posture recognizer and process it

            if (m_FramesForPostureRecognizer.Count == ParametersPosture.Instance.NbMinFrames)
            {
                // we identify the frontmost hand
                HandData hand = frame.FrontmostHand;
                if (hand.IsValid)
                {
                    // if the hand is present in the nbMinFrames previous frames, we go on
                    bool handPresentInEachFrame = true;
                    foreach (IFrame f in m_FramesForPostureRecognizer)
                    {
                        if (!f.Hand(hand.Id).IsValid)
                        {
                            handPresentInEachFrame = false;
                        }
                    }

                    if (handPresentInEachFrame)
                    {
                        // we send the list of frames to the posture recognizer
                        List<IFrame> framesForPostureRecognizerReverse = new List<IFrame>(m_FramesForPostureRecognizer);
                        framesForPostureRecognizerReverse.Reverse();
                        m_PostureRecognizer.Process(framesForPostureRecognizerReverse, hand.Id);
                    }
                }
            }
        }
        

        #region Buffering Methods

            /// <summary>
        /// Retstart a timer
        /// </summary>
        /// <param name="timer">The timer to restart</param>
        private void RestartTimer_Scheduler(Timer timer)
        {
            timer.Stop();
            timer.Start();
        }

        /// <summary>
        /// Once a gesture is detected, this timer is started -> it looks for the end of the gesture.
        /// If the gesture is finished, it sorts the buffer.
        /// If not, it starts an other timer which will wait for the end of the gesture.
        /// </summary>
        private void OnTimesUpTimerElapsed(object sender, ElapsedEventArgs e)
        {
            m_TimesUpTimer.Stop();
            if (m_FlagGesture.ContainsValue(true))
            {
                m_DelayTimer.Start();
            }
            else
            {
                BufferSort();
            }
        }

        /// <summary>
        /// This timer looks for a gesture in progress and wait until it finishes.
        /// If it has been elapsed "ParametersLeapPlugin.Instance.Scheduler_maxDelayTimerCounter" times, it forces the end of the gesture.
        /// Then, when the gesture is finished, it sorts the buffer.
        /// </summary>
        private void OnDelayTimerElapsed(object sender, ElapsedEventArgs e)
        {
            m_DelayTimerCounter++;
            if (m_DelayTimerCounter == ParametersScheduler.Instance.MaxDelayTimerCounter && m_FlagGesture.ContainsValue(true))
            {
                m_FlagGesture = new Dictionary<GestureType, bool>(4)
                {
                    {GestureType.SwipeLeft, false},
                    {GestureType.SwipeRight, false},
                    {GestureType.Tap, false},
                    {GestureType.Push, false}
                };
            }

            if (!m_FlagGesture.ContainsValue(true))
            {
                m_DelayTimer.Stop();
                m_DelayTimerCounter = 0;
                BufferSort();
            }
        }

        /// <summary>
        /// Calculate the priority event in the gesture buffer and raise it.
        /// </summary>
        private void BufferSort()
        {
            // to calculate the occurrence of each type of events
            int swipeLeftCounter = 0;
            int swipeRightCounter = 0;
            int tapCounter = 0;
            int pushCounter = 0;

            // to store the last event of a type
            GestureDetectedEventArgs lastSwipeLeftEvent = null;
            GestureDetectedEventArgs lastSwipeRightEvent = null;
            GestureDetectedEventArgs lastTapEvent = null;
            GestureDetectedEventArgs lastPushEvent = null;

            #region Calculate the occurrence of each event

            foreach (var e in m_Buffer)
            {
                // calculate the occurrence of each event
                switch (e.Type)
                {
                    case GestureType.SwipeLeft:
                        swipeLeftCounter++;
                        lastSwipeLeftEvent = e;
                        break;
                    case GestureType.SwipeRight:
                        swipeRightCounter++;
                        lastSwipeRightEvent = e;
                        break;
                    case GestureType.Tap:
                        tapCounter++;
                        lastTapEvent = e;
                        break;
                    case GestureType.Push:
                        pushCounter++;
                        lastPushEvent = e;
                        break;
                    default:
                        break;
                }
            }

            #endregion

            #region Raise the priority event

            // the priority is given by the order of the if then else loop --> 1. tap, 2. SL, SW and push (max occurrence)
            if (tapCounter > 0)
            {
                RaiseTapGestureEvent(lastTapEvent);
            }
            else
            {
                // between SL, SW and push, the priority is given by the biggest occurrence
                int maxOccurences = Math.Max(swipeLeftCounter, Math.Max(swipeRightCounter, pushCounter));

                if (maxOccurences != 0)
                {
                    // the priority of a gesture (if 2 events have the same occurrence) is given by the order of the if then else loop
                    if (swipeLeftCounter == maxOccurences)
                    {
                        RaiseSwipeLeftGestureEvent(lastSwipeLeftEvent);
                    }
                    else if (swipeRightCounter == maxOccurences)
                    {
                        RaiseSwipeRightGestureEvent(lastSwipeRightEvent);
                    }
                    else if (pushCounter == maxOccurences)
                    {
                        RaisePushGestureEvent(lastPushEvent);
                    }
                }
            }

            #endregion

            m_Buffer.Clear();
        }

        #endregion
    }
}
