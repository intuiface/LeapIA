            // ****************************************************************************
            // <copyright file="TouchEventArgs.cs" company="IntuiLab">
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

    public delegate void TouchEventHandler(object sender, TouchEventArgs e);

    #endregion

    /// <summary>
    /// Event for touch detected during pointing
    /// </summary>
    public class TouchEventArgs : EventArgs
    {
        /// <summary>
        /// X coordinate of the touch
        /// </summary>
        private int m_x;
        public int X
        {
            get
            {
                return m_x;
            }
        }

        /// <summary>
        /// Y coordinate of the touch
        /// </summary>
        private int m_y;
        public int Y
        {
            get
            {
                return m_y;
            }
        }

        public TouchEventArgs(int x, int y)
        {
            m_x = x;
            m_y = y;
        }
    }
}
