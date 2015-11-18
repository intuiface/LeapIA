            // ****************************************************************************
            // <copyright file="IntuiLabGestureAPI.cs" company="IntuiLab">
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
using Leap;
using IntuiLab.Leap.Recognition.Gestures;
using IntuiLab.Leap.Exceptions;
using IntuiLab.Leap.Events;
using IntuiLab.Leap.DataStructures;

namespace IntuiLab.Leap.Recognition
{
    /// <summary>
    /// Class to use the IntuiLab Gesture detection API
    /// </summary>
    internal class IntuiLabGestureAPI : GestureAPI
    {
        #region Field

        /// <summary>
        /// The scheduler for gesture recognition
        /// </summary>
        private Scheduler m_Scheduler;

        #endregion

        #region Constructor

        public IntuiLabGestureAPI(Scheduler scheduler) : base()
        {
            this.m_Scheduler = scheduler;
        }

        #endregion

        #region Enable/Disable Gesture

        /// <summary>
        /// Enable the recognition of a specific gesture
        /// </summary>
        /// <param name="gestureType">The type of gesture to enable</param>
        public override void EnableGestureRecognition(GestureType gestureType)
        {
            switch (gestureType)
            {
                // we subscribe to the corresponding events from the scheduler (the one which prepare data and send them to the detection algorithm)
                case GestureType.SwipeLeft:
                    m_Scheduler.EnableGestureRecognition(GestureType.SwipeLeft);
                    m_Scheduler.SwipeLeftGestureDetected += OnSwipeLeftDetected;
                    break;
                case GestureType.SwipeRight:
                    m_Scheduler.EnableGestureRecognition(GestureType.SwipeRight);
                    m_Scheduler.SwipeRightGestureDetected += OnSwipeRightDetected;
                    break;
                case GestureType.Tap:
                    m_Scheduler.EnableGestureRecognition(GestureType.Tap);
                    m_Scheduler.TapGestureDetected += OnTapGestureDetected;
                    break;
                case GestureType.Push:
                    m_Scheduler.EnableGestureRecognition(GestureType.Push);
                    m_Scheduler.PushGestureDetected += OnPushGestureDetected;
                    break;
                case GestureType.None:
                    m_Scheduler.EnableGestureRecognition(GestureType.None);
                    m_Scheduler.NoGestureDetected += OnNoGestureDetected;
                    break;
                case GestureType.Circle:
                    throw new LeapException("The Circle gesture detection is not available in the IntuiLab gesture API");
            }
        }

        /// <summary>
        /// Disable the recognition of a specific gesture
        /// </summary>
        /// <param name="gestureType">The type of gesture to disable</param>
        public override void DisableGestureRecognition(GestureType gestureType)
        {
            switch (gestureType)
            {
                // we unsubscribe to the corresponding events
                case GestureType.SwipeLeft:
                    m_Scheduler.DisableGestureRecognition(GestureType.SwipeLeft);
                    m_Scheduler.SwipeLeftGestureDetected -= OnSwipeLeftDetected;
                    break;
                case GestureType.SwipeRight:
                    m_Scheduler.DisableGestureRecognition(GestureType.SwipeRight);
                    m_Scheduler.SwipeRightGestureDetected -= OnSwipeRightDetected;
                    break;
                case GestureType.Tap:
                    m_Scheduler.DisableGestureRecognition(GestureType.Tap);
                    m_Scheduler.TapGestureDetected -= OnTapGestureDetected;
                    break;
                case GestureType.Push:
                    m_Scheduler.DisableGestureRecognition(GestureType.Push);
                    m_Scheduler.PushGestureDetected -= OnPushGestureDetected;
                    break;
                case GestureType.None:
                    m_Scheduler.DisableGestureRecognition(GestureType.None);
                    m_Scheduler.NoGestureDetected -= OnNoGestureDetected;
                    break;
            }
        }

        #endregion

        /// <summary>
        /// Performs the gesture detection
        /// </summary>
        /// <param name="frame">The actual frame</param>
        public override void GestureDetection(Frame frame)
        {
            // if we used our API, the frame is just sent to the scheduler which will prepare it and send it to the recognizers
            m_Scheduler.AddFrameGesture(new LeapFrame(frame));
        }

        #region Callbacks
        // Those callbacks just propagate the events

        /********** Callbacks for gestures recognition ***********************************/

        private void OnSwipeLeftDetected(object sender, GestureDetectedEventArgs e)
        {
            RaiseSwipeLeftGestureEvent(e.Speed, e.Direction, e.Timestamp);
        }

        private void OnSwipeRightDetected(object sender, GestureDetectedEventArgs e)
        {
            RaiseSwipeRightGestureEvent(e.Speed, e.Direction, e.Timestamp);
        }

        private void OnTapGestureDetected(object sender, GestureDetectedEventArgs e)
        {
            RaiseTapGestureEvent(e.Speed, e.Direction, e.Timestamp);
        }

        private void OnPushGestureDetected(object sender, GestureDetectedEventArgs e)
        {
            RaisePushGestureEvent(e.Speed, e.Direction, e.Timestamp);
        }

        private void OnNoGestureDetected(object sender, GestureDetectedEventArgs e)
        {
            RaiseNoGestureEvent(e.Speed, e.Direction, e.Timestamp);
        }

        #endregion
    }
}
