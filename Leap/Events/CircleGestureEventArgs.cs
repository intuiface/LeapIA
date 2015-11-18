            // ****************************************************************************
            // <copyright file="CircleGestureEventArgs.cs" company="IntuiLab">
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

namespace IntuiLab.Leap.Events
{
    #region Delegate

    public delegate void CircleGestureEventHandler(object sender, CircleGestureEventArgs e);

    #endregion

    /// <summary>
    /// Event for the circle gesture detection
    /// </summary>
    public class CircleGestureEventArgs : EventArgs
    {
        private int m_id;
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        private float m_progress;
        public float Progress
        {
            get
            {
                return m_progress;
            }
        }

        private string m_clockwiseness;
        public string Clockwiseness
        {
            get
            {
                return m_clockwiseness;
            }
        }

        /// <summary>
        /// The angle done since the beginning of the movement
        /// </summary>
        private float m_angle;
        public float Angle
        {
            get
            {
                return m_angle;
            }
        }

        public CircleGestureEventArgs(int id, float progress, string clockwisness, float angle)
        {
            m_id = id;
            m_progress = progress;
            m_clockwiseness = clockwisness;
            m_angle = angle;
        }
    }
}
