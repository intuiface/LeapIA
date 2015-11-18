            // ****************************************************************************
            // <copyright file="LeapGestureAPI.cs" company="IntuiLab">
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

namespace IntuiLab.Leap.Recognition
{
    /// <summary>
    /// Class to use the Leap Gesture API
    /// </summary>
    internal class LeapGestureAPI : GestureAPI
    {
        #region Fields

        /// <summary>
        /// The instance of the Leap controller
        /// </summary>
        private Controller m_Controller;

        /// <summary>
        /// For debug
        /// </summary>
        private bool m_VerboseMode;

        /// <summary>
        /// For the circle gesture of the Leap API
        /// </summary>
        private Dictionary<int, float> m_CirclesProgress;

        /// <summary>
        /// To know if swipe gesture is already enabled (Enable swipe is the same for SL and SR in the Leap gesture API)
        /// </summary>
        private bool m_IsSwipeLeftEnabled;
        private bool m_IsSwipeRightEnabled;

        #endregion

        #region Constructor

        public LeapGestureAPI(Controller controller, bool verboseMode) : base()
        {
            this.m_Controller = controller;
            this.m_VerboseMode = verboseMode;
            this.m_CirclesProgress = new Dictionary<int, float>();
            this.m_IsSwipeLeftEnabled = false;
            this.m_IsSwipeRightEnabled = false;
        }

        #endregion

        #region Enable/Disable Gestures

        /// <summary>
        /// Enable the recognition of a specific gesture
        /// </summary>
        /// <param name="gestureType">The type of gesture to enable</param>
        public override void EnableGestureRecognition(GestureType gestureType)
        {
            switch (gestureType)
            {
                case GestureType.SwipeLeft:
                    if (!m_IsSwipeRightEnabled)
                        m_Controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
                    m_IsSwipeLeftEnabled = true;
                    break;
                case GestureType.SwipeRight:
                    if (!m_IsSwipeLeftEnabled)
                        m_Controller.EnableGesture(Gesture.GestureType.TYPESWIPE, true);
                    m_IsSwipeRightEnabled = true;
                    break;
                case GestureType.Tap:
                    m_Controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP, true);
                    break;
                case GestureType.Push:
                    m_Controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP, true);
                    break;
                case GestureType.Circle:
                    m_Controller.EnableGesture(Gesture.GestureType.TYPECIRCLE, true);
                    break;
                case GestureType.None:
                    throw new LeapException("None gesture is not implemented in the Leap Gesture API");
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
                case GestureType.SwipeLeft:
                    if (!m_IsSwipeRightEnabled)
                        m_Controller.EnableGesture(Gesture.GestureType.TYPESWIPE, false);
                    m_IsSwipeLeftEnabled = false;
                    break;
                case GestureType.SwipeRight:
                    if (!m_IsSwipeLeftEnabled)
                        m_Controller.EnableGesture(Gesture.GestureType.TYPESWIPE, false);
                    m_IsSwipeRightEnabled = false;
                    break;
                case GestureType.Tap:
                    m_Controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP, false);
                    break;
                case GestureType.Push:
                    m_Controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP, false);
                    break;
                case GestureType.Circle:
                    m_Controller.EnableGesture(Gesture.GestureType.TYPECIRCLE, false);
                    break;
                case GestureType.None:
                    throw new LeapException("None gesture is not implemented in the Leap Gesture API");
            }
        }

        #endregion

        /// <summary>
        /// Performs the gesture detection
        /// </summary>
        /// <param name="frame">The actual frame</param>
        public override void GestureDetection(Frame frame)
        {
            // when the Leap API is used, we collect the list of gestures
            GestureList gestures = frame.Gestures();

            // and raised the corresponding events
            for (int i = 0; i < gestures.Count; i++)
            {
                Gesture gesture = gestures[i];
                switch (gesture.Type)
                {
                    case Gesture.GestureType.TYPEKEYTAP:
                        KeyTapGesture keyTap = new KeyTapGesture(gesture);
                        RaiseTapGestureEvent(0.0f, keyTap.Direction, frame.Timestamp);
                        break;

                    case Gesture.GestureType.TYPESCREENTAP:
                        ScreenTapGesture screenTap = new ScreenTapGesture(gesture);
                        RaisePushGestureEvent(0.0f, screenTap.Direction, frame.Timestamp);
                        break;

                    // for swipes and circles, there is a little more calculations
                    case Gesture.GestureType.TYPESWIPE:
                        ProcessSwipe(gesture);
                        break;

                    case Gesture.GestureType.TYPECIRCLE:
                        ProcessCircle(gesture);
                        break;
                }
            }
        }

        #region Private Methods for Leap Gesture API

        private void ProcessSwipe(Gesture gesture)
        {
            // we calculate some data to try to improve the detection of swipes with the Leap API but it was inconclusive 
            SwipeGesture swipe = new SwipeGesture(gesture);

            if (gesture.State == Gesture.GestureState.STATESTOP)
            {
                float averageSpeed = 0;

                int cpt = 0;
                int i = 1;
                Frame oldFrame;
                SwipeGesture oldGesture;

                do
                {
                    oldFrame = m_Controller.Frame(i);
                    oldGesture = new SwipeGesture(oldFrame.Gesture(gesture.Id));
                    averageSpeed += oldGesture.Speed;
                    cpt++;
                    i++;
                } while (oldGesture.State != Gesture.GestureState.STATESTART && i < 60);

                averageSpeed /= cpt;

                float dx = Math.Abs(swipe.Position.x - swipe.StartPosition.x);

                // we calculate if it is a swipe right or a swipe left
                if (swipe.Direction.x >= 0 && m_IsSwipeRightEnabled)
                    RaiseSwipeRightGestureEvent(averageSpeed, swipe.Position, swipe.Frame.Timestamp);
                else if (swipe.Direction.x < 0 && m_IsSwipeLeftEnabled)
                    RaiseSwipeLeftGestureEvent(averageSpeed, swipe.Position, swipe.Frame.Timestamp);

                if (m_VerboseMode)
                {
                    Console.WriteLine("id={0}, duration={1} s, average speed={2}, position={3}, direction={4}", gesture.Id, gesture.DurationSeconds, averageSpeed, swipe.Position, swipe.Direction);
                    Console.WriteLine("start position={0}, dx={1}", swipe.StartPosition, dx);
                    Console.WriteLine();
                }
            }
        }

        private void ProcessCircle(Gesture gesture)
        {
            CircleGesture circle = new CircleGesture(gesture);
            bool clockwise;
            float angle;

            // we calculate the sens of the circle (clockwise, counterclockwise)
            if (circle.Pointable.Direction.Dot(circle.Normal) >= 0)
                clockwise = true;
            else
                clockwise = false;

            // we calculate the angle already describe by the circle at this frame
            // since the progress does not start at 1, we substract it its initial value to have a more coherent angle
            if (gesture.State == Gesture.GestureState.STATESTART)
                m_CirclesProgress.Add(gesture.Id, circle.Progress); // if the circle just begins then we store its progress value

            if (m_CirclesProgress.ContainsKey(gesture.Id))
            {
                // else we raise the Circle event with the adjusted progress
                if (clockwise)
                {
                    angle = ((circle.Progress - m_CirclesProgress[gesture.Id]) * 360) % 360;
                    RaiseCircleGestureEvent(gesture.Id, circle.Progress, "clockwise", angle);
                }
                else
                {
                    angle = ((1 - (circle.Progress - m_CirclesProgress[gesture.Id]) % 1) * 360);
                    RaiseCircleGestureEvent(gesture.Id, circle.Progress, "counterclockwise", angle);
                }

                if (gesture.State == Gesture.GestureState.STATESTOP)
                    m_CirclesProgress.Remove(gesture.Id);
            }
        }

        #endregion
    }
}
