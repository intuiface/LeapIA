            // ****************************************************************************
            // <copyright file="IntuiLabPriorityFingerPointing.cs" company="IntuiLab">
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
using System.Windows;

namespace IntuiLab.Leap.Pointing
{
    /// <summary>
    /// Detection of pointing (finger position on screen, touch events) with only the priority finger (the frontmost) without using the Leap API
    /// </summary>
    internal class IntuiLabPriorityFingerPointing : FingerPointing
    {
        public override void FingerDetection(Frame currentFrame, Frame lastFrame, Controller controller = null)
        {
            FingerList fingers = currentFrame.Fingers;

            if (!fingers.IsEmpty)
            {
                //this version of finger detection raises an event only for the frontmost finger
                Finger priorityFinger = fingers.Frontmost;

                Vector coordinates3D = priorityFinger.TipPosition;

                // To perform pointing, we define a virtual plane in front of the finger corresponding to the screen. If the x and y coordinates of the finger tip are inside this plane,
                // it is like they are on the real screen.

                // we calculate the x and y coordinates in the virtual plane corresponding to the screen and normalized it (between 0 and 1)
                float normalizedX = (coordinates3D.x - ParametersPointing.Instance.MinXOfVirtualScreen) / (ParametersPointing.Instance.MaxXOfVirtualScreen - ParametersPointing.Instance.MinXOfVirtualScreen);
                float normalizedY = (coordinates3D.y - ParametersPointing.Instance.MinYOfVirtualScreen) / (ParametersPointing.Instance.MaxYOfVirtualScreen - ParametersPointing.Instance.MinYOfVirtualScreen);

                // we clamp the coordinates to the virtual screen boundaries
                if (normalizedX < 0) { normalizedX = 0; }
                if (normalizedX > 1) { normalizedX = 1; }
                if (normalizedY < 0) { normalizedY = 0; }
                if (normalizedY > 1) { normalizedY = 1; }

                // we calculate the coordinates from the virtual screen to the real screen
                int xPixel = (int)(normalizedX * SystemParameters.PrimaryScreenWidth);
                int yPixel = (int)(SystemParameters.PrimaryScreenHeight - normalizedY * SystemParameters.PrimaryScreenHeight);

                // we raise the events
                if (lastFrame.Fingers.IsEmpty) // if there was no finger in the previous frame -> the finger appears in this frame
                    RaiseFingerAppearsEvent(priorityFinger.Id, priorityFinger.TipPosition.x, priorityFinger.TipPosition.y, priorityFinger.TipPosition.z, xPixel, yPixel);
                else // otherwise, it just moves
                    RaiseFingerMovesEvent(priorityFinger.Id, priorityFinger.TipPosition.x, priorityFinger.TipPosition.y, priorityFinger.TipPosition.z, xPixel, yPixel);

                // touch detection
                if (priorityFinger.TipPosition.z <= ParametersPointing.Instance.ZLimitForTouch && !this.m_Touching)
                    RaiseTouchDownEvent(xPixel, yPixel);
                if (priorityFinger.TipPosition.z > ParametersPointing.Instance.ZLimitForTouch && this.m_Touching)
                    RaiseTouchUpEvent(xPixel, yPixel);
            }
            else if (!lastFrame.Fingers.IsEmpty) // if there was a finger in the last frame but not in this frame -> the finger just disappears
            {
                RaiseFingerRemovedEvent(0);
            }
        }
    }
}
