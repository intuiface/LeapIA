            // ****************************************************************************
            // <copyright file="PostureEventArgs.cs" company="IntuiLab">
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

    public delegate void PostureEventHandler(object sender, PostureEventArgs e);

    #endregion

    /// <summary>
    /// Event for posture detected
    /// </summary>
    public class PostureEventArgs : EventArgs
    {
        private int m_percentage;
        public int Percentage
        {
            get
            {
                return m_percentage;
            }
        }

        public PostureEventArgs(int percentage)
        {
            this.m_percentage = percentage;
        }

        public PostureEventArgs()
        {
            this.m_percentage = -1;
        }
    }
}
