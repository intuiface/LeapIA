            // ****************************************************************************
            // <copyright file="FingerPointing.cs" company="IntuiLab">
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
using IntuiLab.Leap.Events;
using Leap;

namespace IntuiLab.Leap.Pointing
{
    /// <summary>
    /// The class of base for finger pointing
    /// </summary>
    internal abstract class FingerPointing
    {
        #region Fields

        /// <summary>
        /// To reduce the frame rate
        /// </summary>
        protected int m_FrameCounter;

        /// <summary>
        // To know if a finger is performing a touch
        /// </summary>
        protected bool m_Touching;

        #endregion

        #region Events

        /********************* Finger Detection **********************************************************/
        /// <summary>
        /// Event raised when a finger appears
        /// </summary>
        public event FingerPresentEventHandler FingerAppears;

        /// <summary>
        /// Event raised when a finger is present (carry data of tip position)
        /// </summary>
        public event FingerPresentEventHandler FingerMoves;

        /// <summary>
        /// Event raised when a finger disappears
        /// </summary>
        public event FingerRemovedEventHandler FingerRemoved;

        protected void RaiseFingerAppearsEvent(int id, float x, float y, float z, int xOnScreen, int yOnScreen)
        {
            if (FingerAppears != null)
                FingerAppears(this, new FingerPresentEventArgs(id, x, y, z, xOnScreen, yOnScreen));
        }

        protected void RaiseFingerMovesEvent(int id, float x, float y, float z, int xOnScreen, int yOnScreen)
        {
            if (FingerMoves != null)
                FingerMoves(this, new FingerPresentEventArgs(id, x, y, z, xOnScreen, yOnScreen));
        }

        protected void RaiseFingerRemovedEvent(int id)
        {
            if (FingerRemoved != null)
            {
                this.m_Touching = false;
                FingerRemoved(this, new FingerRemovedEventArgs(id));
            }
        }
        /***********************************************************************************************/

        /*************************** Touch detection ***************************************************/

        /// <summary>
        /// Event when a touch down (~=touch the screen) is performed
        /// </summary>
        public event TouchEventHandler TouchDown;

        /// <summary>
        /// Event when a touch up (~=finger removed from the screen) is performed
        /// </summary>
        public event TouchEventHandler TouchUp;

        protected void RaiseTouchDownEvent(int x, int y)
        {
            if (TouchDown != null)
            {
                this.m_Touching = true;
                TouchDown(this, new TouchEventArgs(x, y));
            }
        }

        protected void RaiseTouchUpEvent(int x, int y)
        {
            if (TouchUp != null)
            {
                this.m_Touching = false;
                TouchUp(this, new TouchEventArgs(x, y));
            }
        }

        /***********************************************************************************************/

        #endregion

        #region Constructor

        public FingerPointing()
        {
            this.m_FrameCounter = 0;
            this.m_Touching = false;
        }

        #endregion

        /// <summary>
        /// Detects the finger apparition, movement and touch
        /// </summary>
        /// <param name="currentFrame">The actual frame</param>
        /// <param name="lastFrame">The last frame before the actual</param>
        /// <param name="controller">The Leap controller</param>
        public abstract void FingerDetection(Frame currentFrame, Frame lastFrame, Controller controller);
    }
}
