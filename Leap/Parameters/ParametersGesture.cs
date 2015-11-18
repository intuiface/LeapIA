            // ****************************************************************************
            // <copyright file="ParametersGesture.cs" company="IntuiLab">
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
using IntuiLab.Leap.Exceptions;

namespace IntuiLab.Leap
{
    /// <summary>
    /// This class contains the configurable parameters of Gestures
    /// </summary>
    public class ParametersGesture
    {
        #region Singleton Pattern

        private static volatile ParametersGesture m_Instance = null;
        private static object syncRoot = new Object();

        /// <summary>
        /// Singleton
        /// </summary>
        public static ParametersGesture Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new ParametersGesture();
                        }
                    }
                }
                return m_Instance;
            }
        }

        public ParametersGesture()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
            }
            else
            {
                throw new LeapException("ParametersGesture is already instancied");
            }
        }

        #endregion

        #region Constants

        const int DEFAULT_NB_MIN_FRAMES = 5;
        const int DEFAULT_STOP_SPEED = 400; // mm/s;
        const int DEFAULT_MAX_TIME_BETWEEN_TWO_FRAMES = 40000; // 40000 µs ~= max time between each frame
        const int DEFAULT_SWIPE_MIN_SPEED = 1000; // mm/s
        const int DEFAULT_SWIPE_MAX_SPEED = 4000; // mm/s
        const int DEFAULT_TAP_MIN_SPEED = 1000; // mm/s
        const int DEFAULT_TAP_MAX_SPEED = 4000; // mm/s
        const float DEFAULT_TAP_MAX_DX = 15; // mm
        const float DEFAULT_TAP_MIN_DY = -25; // mm
        const int DEFAULT_PUSH_MIN_SPEED = 500; // mm/s
        const int DEFAULT_PUSH_MAX_SPEED = 2000; // mm/s
        const float DEFAULT_PUSH_MAX_DX = 15; // mm
        const float DEFAULT_PUSH_MIN_DY = -15; // mm
        const float DEFAULT_PUSH_MIN_DZ = -16; // mm
        const float DEFAULT_PUSH_MAX_Z = -30; // mm

        #endregion

        #region GestureRecognizer Parameters
        public readonly int NbMinFrames = DEFAULT_NB_MIN_FRAMES;
        public readonly int StopSpeed = DEFAULT_STOP_SPEED;
        public readonly int MaxTimeBetweenTwoFrames = DEFAULT_MAX_TIME_BETWEEN_TWO_FRAMES;
        #endregion

        #region Swipe Conditions
        public readonly int Swipe_minSpeed = DEFAULT_SWIPE_MIN_SPEED;
        public readonly int Swipe_maxSpeed = DEFAULT_SWIPE_MAX_SPEED;
        #endregion

        #region Tap Conditions
        public readonly int Tap_minSpeed = DEFAULT_TAP_MIN_SPEED;
        public readonly int Tap_maxSpeed = DEFAULT_TAP_MAX_SPEED;
        public readonly float Tap_maxDx = DEFAULT_TAP_MAX_DX;
        public readonly float Tap_minDy = DEFAULT_TAP_MIN_DY;
        #endregion

        #region Push Conditions
        public readonly int Push_minSpeed = DEFAULT_PUSH_MIN_SPEED;
        public readonly int Push_maxSpeed = DEFAULT_PUSH_MAX_SPEED;
        public readonly float Push_maxDx = DEFAULT_PUSH_MAX_DX;
        public readonly float Push_minDy = DEFAULT_PUSH_MIN_DY;
        public readonly float Push_minDz = DEFAULT_PUSH_MIN_DZ;
        public readonly float Push_maxZ = DEFAULT_PUSH_MAX_Z;
        #endregion
    }
}
