            // ****************************************************************************
            // <copyright file="FingerRemovedEventArgs.cs" company="IntuiLab">
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

    public delegate void FingerRemovedEventHandler(object sender, FingerRemovedEventArgs e);

    #endregion

    /// <summary>
    /// Event when a finger is removed
    /// </summary>
    public class FingerRemovedEventArgs : EventArgs
    {
        private int m_id;
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        public FingerRemovedEventArgs(int id)
        {
            m_id = id;
        }

        public FingerRemovedEventArgs()
        {
            m_id = -1;
        }
    }
}
