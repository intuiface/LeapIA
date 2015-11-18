            // ****************************************************************************
            // <copyright file="PostureDetectedEventArgs.cs" company="IntuiLab">
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

namespace IntuiLab.Leap.Recognition.Postures
{
    #region Delegate

    internal delegate void PostureDetectedEventHandler(object sender, PostureDetectedEventArgs e);

    #endregion

    /// <summary>
    /// Event for posture detected
    /// </summary>
    internal class PostureDetectedEventArgs : EventArgs
    {
        private PostureType m_type;
        public PostureType Type
        {
            get
            {
                return m_type;
            }
        }

        private int m_percentage;
        public int Percentage
        {
            get
            {
                return m_percentage;
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

        public PostureDetectedEventArgs(PostureType type, long timestamp)
        {
            this.m_type = type;
            this.m_percentage = -1;
            this.m_timestamp = timestamp;
        }

        public PostureDetectedEventArgs(PostureType type, int percentage, long timestamp)
        {
            this.m_type = type;
            this.m_percentage = percentage;
            this.m_timestamp = timestamp;
        }
    }
}
