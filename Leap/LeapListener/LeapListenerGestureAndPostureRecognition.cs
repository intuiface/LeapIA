            // ****************************************************************************
            // <copyright file="LeapListenerGestureAndPostureRecognition.cs" company="IntuiLab">
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
using IntuiLab.Leap.Events;
using IntuiLab.Leap.Recognition;
using IntuiLab.Leap.Recognition.Gestures;
using IntuiLab.Leap.Recognition.Postures;
using IntuiLab.Leap.Exceptions;
using IntuiLab.Leap.DataStructures;
using Leap;

namespace IntuiLab.Leap
{
    public partial class LeapListener
    {
        #region Fields

        /// <summary>
        /// The data scheduler for the recognition
        /// </summary>
        private Scheduler m_Scheduler;

        /// <summary>
        /// To chose between IntuiLab and Leap gesture API
        /// </summary>
        private GestureAPI m_GestureAPI;

        #endregion

        #region Properties

        /// <summary>
        /// The angle corresponding to the progress of the Circle motion
        /// --> from 0 to 360° if clockwise
        /// --> from 360 to 0° if counterclockwise
        /// </summary>
        private float m_Angle;
        public float Angle
        {
            get
            {
                return m_Angle;
            }
            private set
            {
                m_Angle = value;
                NotifyPropertyChanged("Angle");
            }
        }

        #endregion

        #region Events

        /************ Events raised when gestures are detected **************************************/

        /// <summary>
        /// Event raised when a Tap is detected
        /// </summary>
        public event LinearGestureEventHandler TapGestureDetected;

        /// <summary>
        /// Event raised when a Push is detected
        /// </summary>
        public event LinearGestureEventHandler PushGestureDetected;

        /// <summary>
        /// Events raised when Swipes are detected
        /// </summary>
        public event LinearGestureEventHandler SwipeRightGestureDetected;
        public event LinearGestureEventHandler SwipeLeftGestureDetected;

        /// <summary>
        /// Event raised when a Circle is detected
        /// </summary>
        public event CircleGestureEventHandler CircleGestureDetected;

        /// <summary>
        /// Event raised when no gesture is detected (for debug)
        /// </summary>
        public event LinearGestureEventHandler NoGestureDetected;


        private void RaiseTapGestureEvent(LinearGestureEventArgs e)
        {
            if (TapGestureDetected != null)
                TapGestureDetected(this, e);
        }

        private void RaisePushGestureEvent(LinearGestureEventArgs e)
        {
            if (PushGestureDetected != null)
                PushGestureDetected(this, e);
        }

        private void RaiseSwipeRightGestureEvent(LinearGestureEventArgs e)
        {
            if (SwipeRightGestureDetected != null)
                SwipeRightGestureDetected(this, e);
        }

        private void RaiseSwipeLeftGestureEvent(LinearGestureEventArgs e)
        {
            if (SwipeLeftGestureDetected != null)
                SwipeLeftGestureDetected(this, e);
        }

        private void RaiseCircleGestureEvent(CircleGestureEventArgs e)
        {
            if (CircleGestureDetected != null)
            {
                CircleGestureDetected(this, e);
                Angle = e.Angle;
            }
        }

        private void RaiseNoGestureEvent(LinearGestureEventArgs e)
        {
            if (NoGestureDetected != null)
                NoGestureDetected(this, e);
        }

        /********************************************************************************************/

        /************ Events raised when postures are detected **************************************/

        /// <summary>
        /// Events raised when "Rock" posture is detected
        /// </summary>
        public event PostureEventHandler RockPostureSucceed;
        public event PostureEventHandler RockPostureDetectionInProgress;
        public event PostureEventHandler RockPostureDetectionLost;

        /// <summary>
        /// Events raised when "Paper" posture is detected
        /// </summary>
        public event PostureEventHandler PaperPostureSucceed;
        public event PostureEventHandler PaperPostureDetectionInProgress;
        public event PostureEventHandler PaperPostureDetectionLost;

        /// <summary>
        /// Events raised when "Scissors" posture is detected
        /// </summary>
        public event PostureEventHandler ScissorsPostureSucceed;
        public event PostureEventHandler ScissorsPostureDetectionInProgress;
        public event PostureEventHandler ScissorsPostureDetectionLost;

        /// <summary>
        /// Events raised when fingers count postures are detected
        /// </summary>
        public event PostureEventHandler ZeroFingerPostureSucceed;
        public event PostureEventHandler OneFingerPostureSucceed;
        public event PostureEventHandler TwoFingersPostureSucceed;
        public event PostureEventHandler ThreeFingersPostureSucceed;
        public event PostureEventHandler FourFingersPostureSucceed;
        public event PostureEventHandler FiveFingersPostureSucceed;

        public event PostureEventHandler ZeroFingerPostureDetectionInProgress;
        public event PostureEventHandler OneFingerPostureDetectionInProgress;
        public event PostureEventHandler TwoFingersPostureDetectionInProgress;
        public event PostureEventHandler ThreeFingersPostureDetectionInProgress;
        public event PostureEventHandler FourFingersPostureDetectionInProgress;
        public event PostureEventHandler FiveFingersPostureDetectionInProgress;

        public event PostureEventHandler ZeroFingerPostureDetectionLost;
        public event PostureEventHandler OneFingerPostureDetectionLost;
        public event PostureEventHandler TwoFingersPostureDetectionLost;
        public event PostureEventHandler ThreeFingersPostureDetectionLost;
        public event PostureEventHandler FourFingersPostureDetectionLost;
        public event PostureEventHandler FiveFingersPostureDetectionLost;

        private void RaiseRockPostureSucceed()
        {
            if (RockPostureSucceed != null)
                RockPostureSucceed(this, new PostureEventArgs(100));
        }
        private void RaiseRockPostureDetectionInProgress(int percentage)
        {
            if (RockPostureDetectionInProgress != null)
                RockPostureDetectionInProgress(this, new PostureEventArgs(percentage));
        }
        private void RaiseRockPostureDetectionLost()
        {
            if (RockPostureDetectionLost != null)
                RockPostureDetectionLost(this, new PostureEventArgs());
        }


        private void RaisePaperPostureSucceed()
        {
            if (PaperPostureSucceed != null)
                PaperPostureSucceed(this, new PostureEventArgs(100));
        }
        private void RaisePaperPostureDetectionInProgress(int percentage)
        {
            if (PaperPostureDetectionInProgress != null)
                PaperPostureDetectionInProgress(this, new PostureEventArgs(percentage));
        }
        private void RaisePaperPostureDetectionLost()
        {
            if (PaperPostureDetectionLost != null)
                PaperPostureDetectionLost(this, new PostureEventArgs());
        }


        private void RaiseScissorsPostureSucceed()
        {
            if (ScissorsPostureSucceed != null)
                ScissorsPostureSucceed(this, new PostureEventArgs(100));
        }
        private void RaiseScissorsPostureDetectionInProgress(int percentage)
        {
            if (ScissorsPostureDetectionInProgress != null)
                ScissorsPostureDetectionInProgress(this, new PostureEventArgs(percentage));
        }
        private void RaiseScissorsPostureDetectionLost()
        {
            if (ScissorsPostureDetectionLost != null)
                ScissorsPostureDetectionLost(this, new PostureEventArgs());
        }


        private void RaiseZeroPostureSucceed()
        {
            if (ZeroFingerPostureSucceed != null)
                ZeroFingerPostureSucceed(this, new PostureEventArgs(100));
        }
        private void RaiseZeroPostureDetectionInProgress(int percentage)
        {
            if (ZeroFingerPostureDetectionInProgress != null)
                ZeroFingerPostureDetectionInProgress(this, new PostureEventArgs(percentage));
        }
        private void RaiseZeroPostureDetectionLost()
        {
            if (ZeroFingerPostureDetectionLost != null)
                ZeroFingerPostureDetectionLost(this, new PostureEventArgs());
        }


        private void RaiseOnePostureSucceed()
        {
            if (OneFingerPostureSucceed != null)
                OneFingerPostureSucceed(this, new PostureEventArgs(100));
        }
        private void RaiseOnePostureDetectionInProgress(int percentage)
        {
            if (OneFingerPostureDetectionInProgress != null)
                OneFingerPostureDetectionInProgress(this, new PostureEventArgs(percentage));
        }
        private void RaiseOnePostureDetectionLost()
        {
            if (OneFingerPostureDetectionLost != null)
                OneFingerPostureDetectionLost(this, new PostureEventArgs());
        }


        private void RaiseTwoPostureSucceed()
        {
            if (TwoFingersPostureSucceed != null)
                TwoFingersPostureSucceed(this, new PostureEventArgs(100));
        }
        private void RaiseTwoPostureDetectionInProgress(int percentage)
        {
            if (TwoFingersPostureDetectionInProgress != null)
                TwoFingersPostureDetectionInProgress(this, new PostureEventArgs(percentage));
        }
        private void RaiseTwoPostureDetectionLost()
        {
            if (TwoFingersPostureDetectionLost != null)
                TwoFingersPostureDetectionLost(this, new PostureEventArgs());
        }


        private void RaiseThreePostureSucceed()
        {
            if (ThreeFingersPostureSucceed != null)
                ThreeFingersPostureSucceed(this, new PostureEventArgs(100));
        }
        private void RaiseThreePostureDetectionInProgress(int percentage)
        {
            if (ThreeFingersPostureDetectionInProgress != null)
                ThreeFingersPostureDetectionInProgress(this, new PostureEventArgs(percentage));
        }
        private void RaiseThreePostureDetectionLost()
        {
            if (ThreeFingersPostureDetectionLost != null)
                ThreeFingersPostureDetectionLost(this, new PostureEventArgs());
        }


        private void RaiseFourPostureSucceed()
        {
            if (FourFingersPostureSucceed != null)
                FourFingersPostureSucceed(this, new PostureEventArgs(100));
        }
        private void RaiseFourPostureDetectionInProgress(int percentage)
        {
            if (FourFingersPostureDetectionInProgress != null)
                FourFingersPostureDetectionInProgress(this, new PostureEventArgs(percentage));
        }
        private void RaiseFourPostureDetectionLost()
        {
            if (FourFingersPostureDetectionLost != null)
                FourFingersPostureDetectionLost(this, new PostureEventArgs());
        }


        private void RaiseFivePostureSucceed()
        {
            if (FiveFingersPostureSucceed != null)
                FiveFingersPostureSucceed(this, new PostureEventArgs(100));
        }
        private void RaiseFivePostureDetectionInProgress(int percentage)
        {
            if (FiveFingersPostureDetectionInProgress != null)
                FiveFingersPostureDetectionInProgress(this, new PostureEventArgs(percentage));
        }
        private void RaiseFivePostureDetectionLost()
        {
            if (FiveFingersPostureDetectionLost != null)
                FiveFingersPostureDetectionLost(this, new PostureEventArgs());
        }

        /********************************************************************************************/

        #endregion

        #region Enable/Disable Gestures

        /// <summary>
        /// Enable the recognition of a specific gesture
        /// </summary>
        /// <param name="gestureType">The type of gesture to enable</param>
        public void EnableGestureRecognition(GestureType gestureType)
        {
            switch (gestureType)
            {
                // we subscribe to the corresponding events from the scheduler (the one which prepare data and send them to the detection algorithm)
                case GestureType.SwipeLeft:
                    ParametersOther.Instance.SwipeLeftEnable = true;
                    m_GestureAPI.EnableGestureRecognition(GestureType.SwipeLeft);
                    m_GestureAPI.SwipeLeftGestureDetected += OnSwipeLeftDetected;
                    break;
                case GestureType.SwipeRight:
                    ParametersOther.Instance.SwipeRightEnable = true;
                    m_GestureAPI.EnableGestureRecognition(GestureType.SwipeRight);
                    m_GestureAPI.SwipeRightGestureDetected += OnSwipeRightDetected;
                    break;
                case GestureType.Tap:
                    ParametersOther.Instance.TapEnable = true;
                    m_GestureAPI.EnableGestureRecognition(GestureType.Tap);
                    m_GestureAPI.TapGestureDetected += OnTapGestureDetected;
                    break;
                case GestureType.Push:
                    ParametersOther.Instance.PushEnable = true;
                    m_GestureAPI.EnableGestureRecognition(GestureType.Push);
                    m_GestureAPI.PushGestureDetected += OnPushGestureDetected;
                    break;
                case GestureType.None:
                    m_GestureAPI.EnableGestureRecognition(GestureType.None);
                    m_GestureAPI.NoGestureDetected += OnNoGestureDetected;
                    break;

            }
        }

        /// <summary>
        /// Disable the recognition of a specific gesture
        /// </summary>
        /// <param name="gestureType">The type of gesture to disable</param>
        public void DisableGestureRecognition(GestureType gestureType)
        {
            switch (gestureType)
            {
                // we unsubscribe to the corresponding events
                case GestureType.SwipeLeft:
                    ParametersOther.Instance.SwipeLeftEnable = false;
                    m_GestureAPI.DisableGestureRecognition(GestureType.SwipeLeft);
                    m_GestureAPI.SwipeLeftGestureDetected -= OnSwipeLeftDetected;
                    break;
                case GestureType.SwipeRight:
                    ParametersOther.Instance.SwipeRightEnable = false;
                    m_GestureAPI.DisableGestureRecognition(GestureType.SwipeRight);
                    m_GestureAPI.SwipeRightGestureDetected -= OnSwipeRightDetected;
                    break;
                case GestureType.Tap:
                    ParametersOther.Instance.TapEnable = false;
                    m_GestureAPI.DisableGestureRecognition(GestureType.Tap);
                    m_GestureAPI.TapGestureDetected -= OnTapGestureDetected;
                    break;
                case GestureType.Push:
                    ParametersOther.Instance.PushEnable = false;
                    m_GestureAPI.DisableGestureRecognition(GestureType.Push);
                    m_GestureAPI.PushGestureDetected -= OnPushGestureDetected;
                    break;
                case GestureType.None:
                    m_GestureAPI.DisableGestureRecognition(GestureType.None);
                    m_GestureAPI.NoGestureDetected -= OnNoGestureDetected;
                    break;
            }
        }

        #endregion

        #region Enable/Disable Postures

        /// <summary>
        /// Enable the recognition of a specific posture
        /// </summary>
        /// <param name="postureType">The type of posture to enable</param>
        public void EnablePostureRecognition(PostureType postureType)
        {
            switch (postureType)
            {
                // we subscribe to the corresponding events from the scheduler
                case PostureType.Rock:
                    ParametersOther.Instance.RockEnable = true;
                    m_Scheduler.EnablePostureRecognition(PostureType.Rock);
                    m_Scheduler.RockPostureSucceed += OnRockPostureSucceed;
                    m_Scheduler.RockPostureDetectionInProgress += OnRockPostureDetectionInProgress;
                    m_Scheduler.RockPostureDetectionLost += OnRockPostureDetectionLost;
                    break;
                case PostureType.Paper:
                    ParametersOther.Instance.PaperEnable = true;
                    m_Scheduler.EnablePostureRecognition(PostureType.Paper);
                    m_Scheduler.PaperPostureSucceed += OnPaperPostureSucceed;
                    m_Scheduler.PaperPostureDetectionInProgress += OnPaperPostureDetectionInProgress;
                    m_Scheduler.PaperPostureDetectionLost += OnPaperPostureDetectionLost;
                    break;
                case PostureType.Scissors:
                    ParametersOther.Instance.ScissorsEnable = true;
                    m_Scheduler.EnablePostureRecognition(PostureType.Scissors);
                    m_Scheduler.ScissorsPostureSucceed += OnScissorsPostureSucceed;
                    m_Scheduler.ScissorsPostureDetectionInProgress += OnScissorsPostureDetectionInProgress;
                    m_Scheduler.ScissorsPostureDetectionLost += OnScissorsPostureDetectionLost;
                    break;
                case PostureType.Zero:
                    ParametersOther.Instance.ZeroEnable = true;
                    m_Scheduler.EnablePostureRecognition(PostureType.Zero);
                    m_Scheduler.ZeroFingerPostureSucceed += OnZeroFingerPostureSucceed;
                    m_Scheduler.ZeroFingerPostureDetectionInProgress += OnZeroFingerPostureDetectionInProgress;
                    m_Scheduler.ZeroFingerPostureDetectionLost += OnZeroFingerPostureDetectionLost;
                    break;
                case PostureType.One:
                    ParametersOther.Instance.OneEnable = true;
                    m_Scheduler.EnablePostureRecognition(PostureType.One);
                    m_Scheduler.OneFingerPostureSucceed += OnOneFingerPostureSucceed;
                    m_Scheduler.OneFingerPostureDetectionInProgress += OnOneFingerPostureDetectionInProgress;
                    m_Scheduler.OneFingerPostureDetectionLost += OnOneFingerPostureDetectionLost;
                    break;
                case PostureType.Two:
                    ParametersOther.Instance.TwoEnable = true;
                    m_Scheduler.EnablePostureRecognition(PostureType.Two);
                    m_Scheduler.TwoFingersPostureSucceed += OnTwoFingersPostureSucceed;
                    m_Scheduler.TwoFingersPostureDetectionInProgress += OnTwoFingersPostureDetectionInProgress;
                    m_Scheduler.TwoFingersPostureDetectionLost += OnTwoFingersPostureDetectionLost;
                    break;
                case PostureType.Three:
                    ParametersOther.Instance.ThreeEnable = true;
                    m_Scheduler.EnablePostureRecognition(PostureType.Three);
                    m_Scheduler.ThreeFingersPostureSucceed += OnThreeFingersPostureSucceed;
                    m_Scheduler.ThreeFingersPostureDetectionInProgress += OnThreeFingersPostureDetectionInProgress;
                    m_Scheduler.ThreeFingersPostureDetectionLost += OnThreeFingersPostureDetectionLost;
                    break;
                case PostureType.Four:
                    ParametersOther.Instance.FourEnable = true;
                    m_Scheduler.EnablePostureRecognition(PostureType.Four);
                    m_Scheduler.FourFingersPostureSucceed += OnFourFingersPostureSucceed;
                    m_Scheduler.FourFingersPostureDetectionInProgress += OnFourFingersPostureDetectionInProgress;
                    m_Scheduler.FourFingersPostureDetectionLost += OnFourFingersPostureDetectionLost;
                    break;
                case PostureType.Five:
                    ParametersOther.Instance.FiveEnable = true;
                    m_Scheduler.EnablePostureRecognition(PostureType.Five);
                    m_Scheduler.FiveFingersPostureSucceed += OnFiveFingersPostureSucceed;
                    m_Scheduler.FiveFingersPostureDetectionInProgress += OnFiveFingersPostureDetectionInProgress;
                    m_Scheduler.FiveFingersPostureDetectionLost += OnFiveFingersPostureDetectionLost;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Disable the recognition of a specific posture
        /// </summary>
        /// <param name="postureType">The type of posture to disable</param>
        public void DisablePostureRecognition(PostureType postureType)
        {
            switch (postureType)
            {
                // we unsubscribe to the corresponding events from the scheduler
                case PostureType.Rock:
                    ParametersOther.Instance.RockEnable = false;
                    m_Scheduler.DisablePostureRecognition(PostureType.Rock);
                    m_Scheduler.RockPostureSucceed -= OnRockPostureSucceed;
                    m_Scheduler.RockPostureDetectionInProgress -= OnRockPostureDetectionInProgress;
                    m_Scheduler.RockPostureDetectionLost -= OnRockPostureDetectionLost;
                    break;
                case PostureType.Paper:
                    ParametersOther.Instance.PaperEnable = false;
                    m_Scheduler.DisablePostureRecognition(PostureType.Paper);
                    m_Scheduler.PaperPostureSucceed -= OnPaperPostureSucceed;
                    m_Scheduler.PaperPostureDetectionInProgress -= OnPaperPostureDetectionInProgress;
                    m_Scheduler.PaperPostureDetectionLost -= OnPaperPostureDetectionLost;
                    break;
                case PostureType.Scissors:
                    ParametersOther.Instance.ScissorsEnable = false;
                    m_Scheduler.DisablePostureRecognition(PostureType.Scissors);
                    m_Scheduler.ScissorsPostureSucceed -= OnScissorsPostureSucceed;
                    m_Scheduler.ScissorsPostureDetectionInProgress -= OnScissorsPostureDetectionInProgress;
                    m_Scheduler.ScissorsPostureDetectionLost -= OnScissorsPostureDetectionLost;
                    break;
                case PostureType.Zero:
                    ParametersOther.Instance.ZeroEnable = false;
                    m_Scheduler.DisablePostureRecognition(PostureType.Zero);
                    m_Scheduler.ZeroFingerPostureSucceed -= OnZeroFingerPostureSucceed;
                    m_Scheduler.ZeroFingerPostureDetectionInProgress -= OnZeroFingerPostureDetectionInProgress;
                    m_Scheduler.ZeroFingerPostureDetectionLost -= OnZeroFingerPostureDetectionLost;
                    break;
                case PostureType.One:
                    ParametersOther.Instance.OneEnable = false;
                    m_Scheduler.DisablePostureRecognition(PostureType.One);
                    m_Scheduler.OneFingerPostureSucceed -= OnOneFingerPostureSucceed;
                    m_Scheduler.OneFingerPostureDetectionInProgress -= OnOneFingerPostureDetectionInProgress;
                    m_Scheduler.OneFingerPostureDetectionLost -= OnOneFingerPostureDetectionLost;
                    break;
                case PostureType.Two:
                    ParametersOther.Instance.TwoEnable = false;
                    m_Scheduler.DisablePostureRecognition(PostureType.Two);
                    m_Scheduler.TwoFingersPostureSucceed -= OnTwoFingersPostureSucceed;
                    m_Scheduler.TwoFingersPostureDetectionInProgress -= OnTwoFingersPostureDetectionInProgress;
                    m_Scheduler.TwoFingersPostureDetectionLost -= OnTwoFingersPostureDetectionLost;
                    break;
                case PostureType.Three:
                    ParametersOther.Instance.ThreeEnable = false;
                    m_Scheduler.DisablePostureRecognition(PostureType.Three);
                    m_Scheduler.ThreeFingersPostureSucceed -= OnThreeFingersPostureSucceed;
                    m_Scheduler.ThreeFingersPostureDetectionInProgress -= OnThreeFingersPostureDetectionInProgress;
                    m_Scheduler.ThreeFingersPostureDetectionLost -= OnThreeFingersPostureDetectionLost;
                    break;
                case PostureType.Four:
                    ParametersOther.Instance.FourEnable = false;
                    m_Scheduler.DisablePostureRecognition(PostureType.Four);
                    m_Scheduler.FourFingersPostureSucceed -= OnFourFingersPostureSucceed;
                    m_Scheduler.FourFingersPostureDetectionInProgress -= OnFourFingersPostureDetectionInProgress;
                    m_Scheduler.FourFingersPostureDetectionLost -= OnFourFingersPostureDetectionLost;
                    break;
                case PostureType.Five:
                    ParametersOther.Instance.FiveEnable = false;
                    m_Scheduler.DisablePostureRecognition(PostureType.Five);
                    m_Scheduler.FiveFingersPostureSucceed -= OnFiveFingersPostureSucceed;
                    m_Scheduler.FiveFingersPostureDetectionInProgress -= OnFiveFingersPostureDetectionInProgress;
                    m_Scheduler.FiveFingersPostureDetectionLost -= OnFiveFingersPostureDetectionLost;
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void GestureAndPostureDetection(Frame frame)
        {
            /********************** GESTURES AND POSTURES DETECTION *********************************/
            // there are two different processes, one that uses the Leap Gesture API, one that uses our Gestures API

            if (ParametersOther.Instance.GestureDetectionEnable)
            {
                m_GestureAPI.GestureDetection(frame); // gesture detection
            }

            if (ParametersOther.Instance.PostureDetectionEnable)
            {
                m_Scheduler.AddFramePosture(new LeapFrame(frame)); // posture detection
            }

            /***************************************************************************/
        }

        #region Callbacks

        /********** Callbacks for gestures recognition ***********************************/

        private void OnSwipeLeftDetected(object sender, LinearGestureEventArgs e)
        {
            RaiseSwipeLeftGestureEvent(e);
        }

        private void OnSwipeRightDetected(object sender, LinearGestureEventArgs e)
        {
            RaiseSwipeRightGestureEvent(e);
        }

        private void OnTapGestureDetected(object sender, LinearGestureEventArgs e)
        {
            RaiseTapGestureEvent(e);
        }

        private void OnPushGestureDetected(object sender, LinearGestureEventArgs e)
        {
            RaisePushGestureEvent(e);
        }

        private void OnCircleGestureDetected(object sender, CircleGestureEventArgs e)
        {
            RaiseCircleGestureEvent(e);
        }

        private void OnNoGestureDetected(object sender, LinearGestureEventArgs e)
        {
            RaiseNoGestureEvent(e);
        }

        /*********************************************************************************/

        /********** Callbacks for postures recognition ***********************************/

        private void OnRockPostureSucceed(object sender, PostureDetectedEventArgs e)
        {
            RaiseRockPostureSucceed();
        }
        private void OnRockPostureDetectionInProgress(object sender, PostureDetectedEventArgs e)
        {
            RaiseRockPostureDetectionInProgress(e.Percentage);
        }
        private void OnRockPostureDetectionLost(object sender, EventArgs e)
        {
            RaiseRockPostureDetectionLost();
        }


        private void OnPaperPostureSucceed(object sender, PostureDetectedEventArgs e)
        {
            RaisePaperPostureSucceed();
        }
        private void OnPaperPostureDetectionInProgress(object sender, PostureDetectedEventArgs e)
        {
            RaisePaperPostureDetectionInProgress(e.Percentage);
        }
        private void OnPaperPostureDetectionLost(object sender, EventArgs e)
        {
            RaisePaperPostureDetectionLost();
        }


        private void OnScissorsPostureSucceed(object sender, PostureDetectedEventArgs e)
        {
            RaiseScissorsPostureSucceed();
        }
        private void OnScissorsPostureDetectionInProgress(object sender, PostureDetectedEventArgs e)
        {
            RaiseScissorsPostureDetectionInProgress(e.Percentage);
        }
        private void OnScissorsPostureDetectionLost(object sender, EventArgs e)
        {
            RaiseScissorsPostureDetectionLost();
        }


        private void OnZeroFingerPostureSucceed(object sender, PostureDetectedEventArgs e)
        {
            RaiseZeroPostureSucceed();
        }
        private void OnZeroFingerPostureDetectionInProgress(object sender, PostureDetectedEventArgs e)
        {
            RaiseZeroPostureDetectionInProgress(e.Percentage);
        }
        private void OnZeroFingerPostureDetectionLost(object sender, EventArgs e)
        {
            RaiseZeroPostureDetectionLost();
        }


        private void OnOneFingerPostureSucceed(object sender, PostureDetectedEventArgs e)
        {
            RaiseOnePostureSucceed();
        }
        private void OnOneFingerPostureDetectionInProgress(object sender, PostureDetectedEventArgs e)
        {
            RaiseOnePostureDetectionInProgress(e.Percentage);
        }
        private void OnOneFingerPostureDetectionLost(object sender, EventArgs e)
        {
            RaiseOnePostureDetectionLost();
        }


        private void OnTwoFingersPostureSucceed(object sender, PostureDetectedEventArgs e)
        {
            RaiseTwoPostureSucceed();
        }
        private void OnTwoFingersPostureDetectionInProgress(object sender, PostureDetectedEventArgs e)
        {
            RaiseTwoPostureDetectionInProgress(e.Percentage);
        }
        private void OnTwoFingersPostureDetectionLost(object sender, EventArgs e)
        {
            RaiseTwoPostureDetectionLost();
        }


        private void OnThreeFingersPostureSucceed(object sender, PostureDetectedEventArgs e)
        {
            RaiseThreePostureSucceed();
        }
        private void OnThreeFingersPostureDetectionInProgress(object sender, PostureDetectedEventArgs e)
        {
            RaiseThreePostureDetectionInProgress(e.Percentage);
        }
        private void OnThreeFingersPostureDetectionLost(object sender, EventArgs e)
        {
            RaiseThreePostureDetectionLost();
        }


        private void OnFourFingersPostureSucceed(object sender, PostureDetectedEventArgs e)
        {
            RaiseFourPostureSucceed();
        }
        private void OnFourFingersPostureDetectionInProgress(object sender, PostureDetectedEventArgs e)
        {
            RaiseFourPostureDetectionInProgress(e.Percentage);
        }
        private void OnFourFingersPostureDetectionLost(object sender, EventArgs e)
        {
            RaiseFourPostureDetectionLost();
        }


        private void OnFiveFingersPostureSucceed(object sender, PostureDetectedEventArgs e)
        {
            RaiseFivePostureSucceed();
        }
        private void OnFiveFingersPostureDetectionInProgress(object sender, PostureDetectedEventArgs e)
        {
            RaiseFivePostureDetectionInProgress(e.Percentage);
        }
        private void OnFiveFingersPostureDetectionLost(object sender, EventArgs e)
        {
            RaiseFivePostureDetectionLost();
        }

        #endregion
    }
}
