            // ****************************************************************************
            // <copyright file="LeapAllFingersPointing.cs" company="IntuiLab">
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

namespace IntuiLab.Leap.Pointing
{
    /// <summary>
    /// Detection of pointing with all fingers
    /// </summary>
    internal class LeapAllFingersPointing : FingerPointing
    {
        public override void FingerDetection(Frame currentFrame, Frame lastFrame, Controller controller)
        {
            FingerList fingers = currentFrame.Fingers;
            ScreenList screens = controller.LocatedScreens;

            // to decrease the frame rate of the device -> if it is bound to graphical applications, it permits not to refresh the display to many times
            m_FrameCounter++;
            if (m_FrameCounter > ParametersOther.Instance.LeapListener_maxNbFrames) { m_FrameCounter = 1; }


            /********************** FINGERS DETECTION **********************************/
            // this version of fingers detection raises an event for each detected fingers

            // we look for the fingers which disappear, if there is any
            foreach (Finger f in lastFrame.Fingers)
            {
                if (!currentFrame.Finger(f.Id).IsValid) // if the finger was present in the previous frame but is no longer in that frame
                    RaiseFingerRemovedEvent(f.Id); // we raise the FingerRemoved event
            }

            if (!fingers.IsEmpty)
            {
                // for each fingers in the frame, we calculate the finger coordinates within the screen coordinate system
                foreach (Finger finger in fingers)
                {
                    Screen screen = screens.ClosestScreenHit(finger);

                    // we take the normalized intersection coordinates so that they correspond to the 2D plane of the screen
                    // coordinates E [0,1]
                    Vector normalizedCoordinates = screen.Intersect(finger, true);

                    // we transform the coordinates to have the ones in pixels
                    int xPixel = (int)(normalizedCoordinates.x * screen.WidthPixels);
                    int yPixel = screen.HeightPixels - (int)(normalizedCoordinates.y * screen.HeightPixels);

                    if (!lastFrame.Finger(finger.Id).IsValid) // if the finger just appears then we raise de FingerAppears event
                        RaiseFingerAppearsEvent(finger.Id, finger.TipPosition.x, finger.TipPosition.y, finger.TipPosition.z, xPixel, yPixel);
                    else if (m_FrameCounter % 4 == 0 || m_FrameCounter == ParametersOther.Instance.LeapListener_maxNbFrames) // we decrease the frame rate (ie we don't raise the event which serves to update the finger coordinates at each frames, but about every 4 frames)
                        RaiseFingerMovesEvent(finger.Id, finger.TipPosition.x, finger.TipPosition.y, finger.TipPosition.z, xPixel, yPixel);
                }
            }
            /***************************************************************************/
        }
    }
}
