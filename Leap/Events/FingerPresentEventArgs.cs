            // ****************************************************************************
            // <copyright file="FingerPresentEventArgs.cs" company="IntuiLab">
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

    public delegate void FingerPresentEventHandler(object sender, FingerPresentEventArgs e);

    #endregion

    /// <summary>
    /// Event when a finger is present
    /// </summary>
    public class FingerPresentEventArgs : EventArgs
    {
        private int m_id;
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        private float m_x;
        public float X
        {
            get
            {
                return m_x;
            }
        }

        private float m_y;
        public float Y
        {
            get
            {
                return m_y;
            }
        }

        private float m_z;
        public float Z
        {
            get
            {
                return m_z;
            }
        }

        /// <summary>
        /// X coordinate of the intersection point between the finger and the screen, in pixels
        /// </summary>
        private int m_xOnScreen;
        public int XOnScreen
        {
            get
            {
                return m_xOnScreen;
            }
        }

        /// <summary>
        /// Y coordinate of the intersection point between the finger and the screen, in pixels
        /// </summary>
        private int m_yOnScreen;
        public int YOnScreen
        {
            get
            {
                return m_yOnScreen;
            }
        }

        public FingerPresentEventArgs(int id, float x, float y, float z, int xOnScreen, int yOnScreen)
        {
            m_id = id;
            m_x = x;
            m_y = y;
            m_z = z;
            m_xOnScreen = xOnScreen;
            m_yOnScreen = yOnScreen;
        }

        public FingerPresentEventArgs(int xOnScreen, int yOnScreen)
        {
            m_id = -1;
            m_x = -1;
            m_y = -1;
            m_z = -1;
            m_xOnScreen = xOnScreen;
            m_yOnScreen = yOnScreen;
        }
    }
}
