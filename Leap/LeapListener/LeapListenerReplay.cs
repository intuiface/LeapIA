            // ****************************************************************************
            // <copyright file="LeapListenerReplay.cs" company="IntuiLab">
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
using IntuiLab.Leap.DataStructures;

namespace IntuiLab.Leap
{
    public partial class LeapListener
    {
        #region Fields
        
        /// <summary>
        /// To activate replay so the frames of the Leap controller are blocked
        /// </summary>
        private bool m_ReplayEnabled;

        #endregion

        #region Methods For Replay

        /// <summary>
        /// Permits to replay the recording movements frame by frame to verify if there are recognized
        /// </summary>
        /// <param name="id">The id of the frame to replay</param>
        /// <param name="fingers">This is the information about the fingers, the dictionary key represents the finger id and the list of object is in the correct order :
        /// bool isFrontmost, float tipPosition_x, float tipPosition_y, float tipPosition_z, bool isValid</param>
        /// <param name="timestamp">The timestamp of the frame to replay</param>
        /// <param name="isValid">if the frame is valid or not</param>
        public void Replay(long id, Dictionary<int, List<object>> fingers, long timestamp, bool isValid)
        {
            // if the replay is enable, we create a frame and add it to the scheduler
            if (m_ReplayEnabled)
            {
                IFrame frame = new ReplayFrame(id, fingers, timestamp, isValid);
                m_Scheduler.AddFrameGesture(frame);
            }
        }

        /// <summary>
        /// Enable the replay mode
        /// </summary>
        public void EnableReplay()
        {
            this.m_ReplayEnabled = true;
        }

        /// <summary>
        /// Disable the replay mode
        /// </summary>
        public void DisableReplay()
        {
            this.m_ReplayEnabled = false;
        }

        #endregion
    }
}
