            // ****************************************************************************
            // <copyright file="GestureRecognizer.cs" company="IntuiLab">
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
using IntuiLab.Leap.Utils;
using IntuiLab.Leap.DataStructures;

namespace IntuiLab.Leap.Recognition.Gestures
{
    /// <summary>
    /// This class contains the algoritms of gestures recognition
    /// </summary>
    internal class GestureRecognizer
    {
        // The recognition of a gesture is here based on the comparison between the direction vectors of the movement from on frame to another.
        // Those vectors are calculate from the points of the discretized movement.
        // We compare the angle between the direction vector of a frame and the one of the previous frame. For exemple, if the angle is close to 0,
        // the movement is linear. We also consider the movement speed, the minimum number of frames it appears, etc.


        #region Fields

        private bool m_VerboseMode;

        #endregion

        #region Properties

        /// <summary>
        /// Contains the timestamp of the last event raised for a gesture
        /// </summary>
        private Tuple<GestureType, long> m_lastGestureTimestamp;
        public Tuple<GestureType, long> LastGestureTimestamp
        {
            get
            {
                return m_lastGestureTimestamp;
            }
        }

        #endregion

        #region Events

        /*********************************** Events for gestures in progress*****************************************************/
        public event GestureDetectedEventHandler NoGestureDetected_GestureRecognizer;

        public event GestureDetectedEventHandler SwipeLeftGestureDetected_GestureRecognizer;
        public event GestureDetectedEventHandler SwipeRightGestureDetected_GestureRecognizer;

        public event GestureDetectedEventHandler TapGestureDetected_GestureRecognizer;

        public event GestureDetectedEventHandler PushGestureDetected_GestureRecognizer;

        private void RaiseNoGestureDetectedEvent(float speed, Vector direction, long timestamp)
        {
            if (NoGestureDetected_GestureRecognizer != null)
                NoGestureDetected_GestureRecognizer(this, new GestureDetectedEventArgs(GestureType.None, speed, direction, timestamp));
        }

        private void RaiseSwipeLeftGestureEvent(float speed, Vector direction, long timestamp)
        {
            if (SwipeLeftGestureDetected_GestureRecognizer != null)
            {
                SwipeLeftGestureDetected_GestureRecognizer(this, new GestureDetectedEventArgs(GestureType.SwipeLeft, speed, direction, timestamp));
                this.m_lastGestureTimestamp = new Tuple<GestureType, long>(GestureType.SwipeLeft, timestamp);
            }
        }

        private void RaiseSwipeRightGestureEvent(float speed, Vector direction, long timestamp)
        {
            if (SwipeRightGestureDetected_GestureRecognizer != null)
            {
                SwipeRightGestureDetected_GestureRecognizer(this, new GestureDetectedEventArgs(GestureType.SwipeRight, speed, direction, timestamp));
                this.m_lastGestureTimestamp = new Tuple<GestureType, long>(GestureType.SwipeRight, timestamp);
            }
        }

        private void RaiseTapGestureEvent(float speed, Vector direction, long timestamp)
        {
            if (TapGestureDetected_GestureRecognizer != null)
            {
                TapGestureDetected_GestureRecognizer(this, new GestureDetectedEventArgs(GestureType.Tap, speed, direction, timestamp));
                this.m_lastGestureTimestamp = new Tuple<GestureType, long>(GestureType.Tap, timestamp);
            }
        }

        private void RaisePushGestureEvent(float speed, Vector direction, long timestamp)
        {
            if (PushGestureDetected_GestureRecognizer != null)
            {
                PushGestureDetected_GestureRecognizer(this, new GestureDetectedEventArgs(GestureType.Push, speed, direction, timestamp));
                this.m_lastGestureTimestamp = new Tuple<GestureType, long>(GestureType.Push, timestamp);
            }
        }
        /***********************************************************************************************************************/


        /*********************** Events for gestures start *********************************************************************/
        public event GestureDetectedEventHandler SwipeLeftGestureBegin;
        public event GestureDetectedEventHandler SwipeRightGestureBegin;
        public event GestureDetectedEventHandler TapGestureBegin;
        public event GestureDetectedEventHandler PushGestureBegin;

        private void RaiseSwipeLeftGestureBegin(float speed, Vector direction, long timestamp)
        {
            if (SwipeLeftGestureBegin != null)
            {
                SwipeLeftGestureBegin(this, new GestureDetectedEventArgs(GestureType.SwipeLeft, speed, direction, timestamp));
                this.m_lastGestureTimestamp = new Tuple<GestureType, long>(GestureType.SwipeLeft, timestamp);
            }
        }

        private void RaiseSwipeRightGestureBegin(float speed, Vector direction, long timestamp)
        {
            if (SwipeRightGestureBegin != null)
            {
                SwipeRightGestureBegin(this, new GestureDetectedEventArgs(GestureType.SwipeRight, speed, direction, timestamp));
                this.m_lastGestureTimestamp = new Tuple<GestureType, long>(GestureType.SwipeRight, timestamp);
            }
        }

        private void RaiseTapGestureBegin(float speed, Vector direction, long timestamp)
        {
            if (TapGestureBegin != null)
            {
                TapGestureBegin(this, new GestureDetectedEventArgs(GestureType.Tap, speed, direction, timestamp));
                this.m_lastGestureTimestamp = new Tuple<GestureType, long>(GestureType.Tap, timestamp);
            }
        }

        private void RaisePushGestureBegin(float speed, Vector direction, long timestamp)
        {
            if (PushGestureBegin != null)
            {
                PushGestureBegin(this, new GestureDetectedEventArgs(GestureType.Push, speed, direction, timestamp));
                this.m_lastGestureTimestamp = new Tuple<GestureType, long>(GestureType.Push, timestamp);
            }
        }
        /***********************************************************************************************************************/


        /*********************** Events for gestures end ***********************************************************************/
        public event GestureDetectedEventHandler SwipeLeftGestureEnd;
        public event GestureDetectedEventHandler SwipeRightGestureEnd;
        public event GestureDetectedEventHandler TapGestureEnd;
        public event GestureDetectedEventHandler PushGestureEnd;

        private void RaiseSwipeLeftGestureEnd(float speed, Vector direction, long timestamp)
        {
            if (SwipeLeftGestureEnd != null)
            {
                SwipeLeftGestureEnd(this, new GestureDetectedEventArgs(GestureType.SwipeLeft, speed, direction, timestamp));
            }
        }

        private void RaiseSwipeRightGestureEnd(float speed, Vector direction, long timestamp)
        {
            if (SwipeRightGestureEnd != null)
            {
                SwipeRightGestureEnd(this, new GestureDetectedEventArgs(GestureType.SwipeRight, speed, direction, timestamp));
            }
        }

        private void RaiseTapGestureEnd(float speed, Vector direction, long timestamp)
        {
            if (TapGestureEnd != null)
            {
                TapGestureEnd(this, new GestureDetectedEventArgs(GestureType.Tap, speed, direction, timestamp));
            }
        }

        private void RaisePushGestureEnd(float speed, Vector direction, long timestamp)
        {
            if (PushGestureEnd != null)
            {
                PushGestureEnd(this, new GestureDetectedEventArgs(GestureType.Push, speed, direction, timestamp));
            }
        }
        /***********************************************************************************************************************/

        #endregion

        #region Constructor

        public GestureRecognizer(bool verboseMode)
        {
            this.m_VerboseMode = verboseMode;
            this.m_lastGestureTimestamp = new Tuple<GestureType, long>(GestureType.None, 0L);
        }

        #endregion

        /// <summary>
        /// Detects if a gesture is beginning or in progress.
        /// </summary>
        /// <param name="frames">The list of last frames.</param>
        /// <param name="fingerId">The id of the finger to check</param>
        public void Process(List<IFrame> frames, int fingerId)
        {
            List<Vector> directions = new List<Vector>();

            for (int i = 1; i < ParametersGesture.Instance.NbMinFrames; i++)
            {
                // we store all the direction vector of the movement between one frame and the previous
                directions.Add(LeapMath.Direction(fingerId, frames[i - 1], frames[i]));
            }

            IFrame actualFrame = frames[0];
            IFrame oldFrame = frames[ParametersGesture.Instance.NbMinFrames - 1];

            // we check if the movement is linear. If it is, we go on
            if (!LeapMath.IsLinearDirection(directions, m_VerboseMode))
                return;

            // we calculate the global speed of the movement
            float globalSpeed = LeapMath.Speed(fingerId, oldFrame, actualFrame); // mm/s

            // we calculate the global direction of the movement
            Vector globalDirection = LeapMath.Direction(fingerId, oldFrame, actualFrame);

            if (m_VerboseMode)
            {
                Console.WriteLine("global speed={0} mm/s", globalSpeed);
                Console.WriteLine("global direction=" + globalDirection);
                Console.WriteLine("position=" + actualFrame.Finger(fingerId).TipPosition);
                Console.WriteLine("duration={0} us", (actualFrame.Timestamp - oldFrame.Timestamp));
                Console.WriteLine("oldFrame id={0}, actualFrame id={1}", oldFrame.Id, actualFrame.Id);
                Console.WriteLine("timestamp=" + actualFrame.Timestamp);
            }

            // we check if the conditions (speed, displacement, etc) for gestures are valid
            // to have a priority on gesture, we call the condition checker of the priority gesture first
            if (TapConditionIsValid(globalSpeed, globalDirection))
            {
                if (this.m_lastGestureTimestamp.Item1 != GestureType.Tap || Math.Abs(actualFrame.Timestamp - this.m_lastGestureTimestamp.Item2) > ParametersGesture.Instance.MaxTimeBetweenTwoFrames)
                    RaiseTapGestureBegin(globalSpeed, globalDirection, actualFrame.Timestamp);
                else
                    RaiseTapGestureEvent(globalSpeed, globalDirection, actualFrame.Timestamp);
            }
            else if (PushConditionIsValid(globalSpeed, globalDirection, actualFrame.Finger(fingerId).TipPosition))
            {
                if (this.m_lastGestureTimestamp.Item1 != GestureType.Push || Math.Abs(actualFrame.Timestamp - this.m_lastGestureTimestamp.Item2) > ParametersGesture.Instance.MaxTimeBetweenTwoFrames)
                    RaisePushGestureBegin(globalSpeed, globalDirection, actualFrame.Timestamp);
                else
                    RaisePushGestureEvent(globalSpeed, globalDirection, actualFrame.Timestamp);
            }
            else if (SwipeRightConditionIsValid(globalSpeed, globalDirection))
            {
                if (this.m_lastGestureTimestamp.Item1 != GestureType.SwipeRight || Math.Abs(actualFrame.Timestamp - this.m_lastGestureTimestamp.Item2) > ParametersGesture.Instance.MaxTimeBetweenTwoFrames)
                    RaiseSwipeRightGestureBegin(globalSpeed, globalDirection, actualFrame.Timestamp);
                else
                    RaiseSwipeRightGestureEvent(globalSpeed, globalDirection, actualFrame.Timestamp);
            }
            else if (SwipeLeftConditionIsValid(globalSpeed, globalDirection))
            {
                if (this.m_lastGestureTimestamp.Item1 != GestureType.SwipeLeft || Math.Abs(actualFrame.Timestamp - this.m_lastGestureTimestamp.Item2) > ParametersGesture.Instance.MaxTimeBetweenTwoFrames)
                    RaiseSwipeLeftGestureBegin(globalSpeed, globalDirection, actualFrame.Timestamp);
                else
                    RaiseSwipeLeftGestureEvent(globalSpeed, globalDirection, actualFrame.Timestamp);
            }
            else
            {
                RaiseNoGestureDetectedEvent(globalSpeed, globalDirection, actualFrame.Timestamp);
            }

        }

        #region Stop Checker

        /// <summary>
        /// Checks if the gesture is ending.
        /// </summary>
        /// <param name="frames">The list of the two last frames.</param>
        /// <param name="fingerId">The id of the finger in movement.</param>
        public void CheckGestureEnd(List<IFrame> frames, int fingerId)
        {
            IFrame actualFrame = frames[0];
            IFrame previousFrame = frames[1];

            // we calculate the instant speed of the finger
            float instantSpeed = (actualFrame.Finger(fingerId).IsValid) ? LeapMath.Speed(fingerId, previousFrame, actualFrame) : 0; // mm/s

            if (m_VerboseMode)
            {
                Console.WriteLine("instant speed=" + instantSpeed);
            }

            // if the instant speed is too small, we consider that the gesture is finished
            if (instantSpeed < ParametersGesture.Instance.StopSpeed)
            {
                switch (m_lastGestureTimestamp.Item1)
                {
                    case GestureType.Tap:
                        {
                            RaiseSwipeLeftGestureEnd(instantSpeed, null, actualFrame.Timestamp);
                        }
                        break;
                    case GestureType.Push:
                        {
                            RaisePushGestureEnd(instantSpeed, null, actualFrame.Timestamp);
                        }
                        break;
                    case GestureType.SwipeRight:
                        {
                            RaiseSwipeRightGestureEnd(instantSpeed, null, actualFrame.Timestamp);
                        }
                        break;
                    case GestureType.SwipeLeft:
                        {
                            RaiseSwipeLeftGestureEnd(instantSpeed, null, actualFrame.Timestamp);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion


        #region Conditions Checkers
        
        // A tap gesture has to have a linear trajectory, its speed must be in a specific interval, the displacement along the x axis must be small and the one along the y axis big.
        // The movement has to be directed downward (dy < 0)

        /// <summary>
        /// Checks if the conditions for a Tap gesture are valid
        /// </summary>
        private bool TapConditionIsValid(float speed, Vector globalDirection)
        {
            if (speed > ParametersGesture.Instance.Tap_minSpeed && speed < ParametersGesture.Instance.Tap_maxSpeed)
            {
                if (globalDirection.x > -ParametersGesture.Instance.Tap_maxDx && globalDirection.x < ParametersGesture.Instance.Tap_maxDx && globalDirection.y < ParametersGesture.Instance.Tap_minDy)
                    return true;
            }
            return false;
        }

        
        // A swipe must have a linear direction, and its speed must be in a specific interval.
        
        /// <summary>
        /// Check is the conditions for a Swipe Right gesture are valid
        /// </summary>
        private bool SwipeRightConditionIsValid(float speed, Vector globalDirection)
        {
            if (speed > ParametersGesture.Instance.Swipe_minSpeed && speed < ParametersGesture.Instance.Swipe_maxSpeed)
            {
                if (globalDirection.x >= 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the conditions for a Swipe Left gesture are valid
        /// </summary>
        private bool SwipeLeftConditionIsValid(float speed, Vector globalDirection)
        {
            if (speed > ParametersGesture.Instance.Swipe_minSpeed && speed < ParametersGesture.Instance.Swipe_maxSpeed)
            {
                if (globalDirection.x < 0)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Checks if the conditions for a Push gesture is valid
        /// </summary>
        public bool PushConditionIsValid(float speed, Vector globalDirection, Vector lastPosition)
        {
            if (speed > ParametersGesture.Instance.Push_minSpeed && speed < ParametersGesture.Instance.Push_maxSpeed)
            {
                if (globalDirection.x > -ParametersGesture.Instance.Push_maxDx && globalDirection.x < ParametersGesture.Instance.Push_maxDx && globalDirection.z < ParametersGesture.Instance.Push_minDz
                    && globalDirection.y > ParametersGesture.Instance.Push_minDy && lastPosition.z < ParametersGesture.Instance.Push_maxZ) // The last condition is that the finger must pass a certain limit for the push to be recognized, but it greatly reduces the success rate.
                    return true;                                                                                                                 // Comment it if you want the push to be detected without having to pass the limit, or set the parameter higher.
            }
            return false;
        }

        #endregion
    }
}
