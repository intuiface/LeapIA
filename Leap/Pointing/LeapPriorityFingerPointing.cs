using Leap;

namespace IntuiLab.Leap.Pointing
{
    /// <summary>
    /// Detection of pointing (finger position on screen, touch events) with only the priority finger (the frontmost) using the Leap API
    /// </summary>
    internal class LeapPriorityFingerPointing : FingerPointing
    {
        public override void FingerDetection(Frame currentFrame, Frame lastFrame, Controller controller)
        {
            FingerList fingers = currentFrame.Fingers;
            ScreenList screens = controller.LocatedScreens;

            // to decrease the frame rate of the device -> if it is bound to graphical applications, it permits not to refresh the display to many times
            m_FrameCounter++;
            if (m_FrameCounter > ParametersOther.Instance.LeapListener_maxNbFrames) { m_FrameCounter = 1; }


            /********************** FINGERS DETECTION **********************************/
            //this version of finger detection raises an event only for the frontmost finger
            if (!fingers.IsEmpty)
            {
                // we look for the priority finger and we apply the same calculation as in the previous version
                Finger priorityFinger = fingers.Frontmost;

                // intersection of the finger on the sreen
                Screen screen = screens.ClosestScreenHit(priorityFinger);
                Vector normalizedCoordinates = screen.Intersect(priorityFinger, true);

                int xPixel = (int)(normalizedCoordinates.x * screen.WidthPixels);
                int yPixel = screen.HeightPixels - (int)(normalizedCoordinates.y * screen.HeightPixels);

                if (lastFrame.Fingers.IsEmpty)
                    RaiseFingerAppearsEvent(priorityFinger.Id, priorityFinger.TipPosition.x, priorityFinger.TipPosition.y, priorityFinger.TipPosition.z, xPixel, yPixel);
                else if (m_FrameCounter % 4 == 0 || m_FrameCounter == ParametersOther.Instance.LeapListener_maxNbFrames)
                    RaiseFingerMovesEvent(priorityFinger.Id, priorityFinger.TipPosition.x, priorityFinger.TipPosition.y, priorityFinger.TipPosition.z, xPixel, yPixel);

                // touch detection
                if (priorityFinger.TipPosition.z <= ParametersPointing.Instance.ZLimitForTouch && !m_Touching)
                    RaiseTouchDownEvent(xPixel, yPixel);
                if (priorityFinger.TipPosition.z > ParametersPointing.Instance.ZLimitForTouch && m_Touching)
                    RaiseTouchUpEvent(xPixel, yPixel);
            }
            else if (!lastFrame.Fingers.IsEmpty)
            {
                RaiseFingerRemovedEvent(0);
            }
            /***************************************************************************/

        }
    }
}
