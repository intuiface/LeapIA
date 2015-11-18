            // ****************************************************************************
            // <copyright file="GestureDetectedEventArgs.cs" company="IntuiLab">
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

namespace IntuiLab.Leap.Recognition.Gestures
{
    #region Delegate

    internal delegate void GestureDetectedEventHandler(object sender, GestureDetectedEventArgs e);

    #endregion

    /// <summary>
    /// Event for gesture detected
    /// </summary>
    internal class GestureDetectedEventArgs : EventArgs
    {
        private GestureType m_type;
        public GestureType Type
        {
            get
            {
                return m_type;
            }
        }

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

        public GestureDetectedEventArgs(GestureType type, float speed, Vector direction, long timestamp)
        {
            m_type = type;
            m_speed = speed;
            m_direction = direction;
            m_timestamp = timestamp;
        }
    }
}
