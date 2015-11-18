            // ****************************************************************************
            // <copyright file="HandPresentEventArgs.cs" company="IntuiLab">
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

namespace LeapPlugin.Events
{
    #region Delegate

    public delegate void HandPresentEventHandler(object sender, HandPresentEventArgs e);
    
    #endregion

    /// <summary>
    /// Evènement levé lorsqu'une main est détectée ou lorsqu'elle n'est plus présente
    /// </summary>
    public class HandPresentEventArgs : EventArgs
    {
        private bool m_isHandPresent;

        /// <summary>
        /// Valeur : true si une main est détectée, false au moment où elle part
        /// </summary>
        public bool IsHandPresent
        {
            get
            {
                return m_isHandPresent;
            }
        }

        public HandPresentEventArgs(bool isHandPresent)
        {
            m_isHandPresent = isHandPresent;
        }
    }
}
