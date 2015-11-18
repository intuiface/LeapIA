            // ****************************************************************************
            // <copyright file="LinearGestureEventArgs.cs" company="IntuiLab">
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

namespace IntuiLab.Leap.Events
{
    #region Delegate

    public delegate void LinearGestureEventHandler(object sender, LinearGestureEventArgs e);

    #endregion

    /// <summary>
    /// Event for a linear gesture detection (swipe, tap, push)
    /// </summary>
    public class LinearGestureEventArgs : EventArgs
    {
        private float m_speed;
        public float Speed
        {
            get
            {
                return m_speed;
            }
        }

        private Vector m_direction;
        public Vector Direction
        {
            get
            {
                return m_direction;
            }
        }

        private long m_timestamp;
        public long Timestamp
        {
            get
            {
                return m_timestamp;
            }
        }

        public LinearGestureEventArgs(float speed, Vector direction, long timestamp)
        {
            m_speed = speed;
            m_direction = direction;
            m_timestamp = timestamp;
        }

        public LinearGestureEventArgs()
        {
            m_speed = 0;
            m_direction = Vector.Zero;
            m_timestamp = 0;
        }
    }
}
