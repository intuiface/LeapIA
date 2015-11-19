using IntuiLab.Leap.DataStructures;
using Leap;
using System.Collections.Generic;

namespace IntuiLab.Leap.Utils
{
    /// <summary>
    /// This class contains utility functions for recognition algorithms
    /// </summary>
    internal static class LeapMath
    {
        /// <summary>
        /// Calculate the direction vector of the movement perming by the finger from the startFrame frame to the endFrame frame.
        /// </summary>
        /// <param name="pointableId">The id of the finger performing the movement.</param>
        /// <param name="startFrame">The frame containing the start position of the direction vector.</param>
        /// <param name="endFrame">The frame containing the end position of the direction vector.</param>
        /// <returns>The direction vector.</returns>
        public static Vector Direction(int pointableId, IFrame startFrame, IFrame endFrame)
        {
            Vector startPosition = startFrame.Finger(pointableId).TipPosition;
            Vector endPosition = endFrame.Finger(pointableId).TipPosition;
            return (endPosition - startPosition);
        }

        /// <summary>
        /// Calculate the speed of the finger between the startFrame frame and the endFrame frame in mm/s.
        /// </summary>
        /// <param name="finger">The finger which we want to calculate the speed.</param>
        /// <param name="startFrame">The start frame.</param>
        /// <param name="endFrame">The end frame.</param>
        /// <returns>The speed in mm/s.</returns>
        public static float Speed(int pointableId, IFrame startFrame, IFrame endFrame)
        {
            Vector startPosition = startFrame.Finger(pointableId).TipPosition;
            Vector endPosition = endFrame.Finger(pointableId).TipPosition;
            return ((endPosition - startPosition).Magnitude / (endFrame.Timestamp - startFrame.Timestamp)) * 1000000; // mm/s
        }

        /// <summary>
        /// Recognizes if the movement represented by the list of directions is linear.
        /// </summary>
        /// <param name="directions">The list of the direction vectors between each point of the discretized movement.</param>
        /// <param name="verboseMode">To display the log.</param>
        /// <returns>If the movement is linear or not.</returns>
        public static bool IsLinearDirection(List<Vector> directions, bool verboseMode)
        {
            List<float> angleBetweenDirection = new List<float>();

            // We calculate the angle between each direction vector to know the trajectory of the movement
            for (int i = 1; i < directions.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    angleBetweenDirection.Add(directions[i].AngleTo(directions[j]));
                }
            }

            // We test if the movement is linear
            // To do so, we compare the angle between the different directions, if it is small, the direction is linear
            foreach (var angle in angleBetweenDirection)
            {
                if (angle > ParametersOther.Instance.LeapMath_maxAngleForLinearDirection) // 0.5 =~ pi/6
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Recognizes if a hand is motionless for the postures.
        /// </summary>
        /// <param name="positions">The list of the successive positions of the hand.</param>
        /// <returns>If the hand is motionless or not.</returns>
        public static bool IsImmobile(List<Vector> positions)
        {
            float minX = positions[0].x;
            float maxX = positions[0].x;
            float minY = positions[0].y;
            float maxY = positions[0].y;
            float minZ = positions[0].z;
            float maxZ = positions[0].z;

            // we calculate the min and max coordinates along each axis to define a kind of box which have to be small for considering the hand as motionless
            for (int i = 1; i < positions.Count; i++)
            {
                if (positions[i].x < minX)
                    minX = positions[i].x;
                else if (positions[i].x > maxX)
                    maxX = positions[i].x;

                if (positions[i].y < minY)
                    minY = positions[i].y;
                else if (positions[i].y > maxY)
                    maxY = positions[i].y;

                if (positions[i].z < minZ)
                    minZ = positions[i].z;
                else if (positions[i].z > maxZ)
                    maxZ = positions[i].z;
            }

            float maxDisplacementForImmobility = ParametersOther.Instance.LeapMath_maxDisplacementForImmobility;
            float detectionAreaBoundary = ParametersOther.Instance.LeapMath_areaBoundaryForImmobility; // the hand have also to be more or less above the Leap to avoid untimely detection

            if (maxX - minX < maxDisplacementForImmobility && maxY - minY < maxDisplacementForImmobility && maxZ - minZ < maxDisplacementForImmobility && minX > -detectionAreaBoundary && maxX < detectionAreaBoundary && minZ > -detectionAreaBoundary && maxZ < detectionAreaBoundary)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Tests if there is the same count of fingers in all frames and assigns this count to a variable.
        /// </summary>
        /// <param name="frames">The list of frames to test.</param>
        /// <param name="fingersCount">The variable which will contain the count of fingers.</param>
        /// <returns>If there is the same fingers count in each frame.</returns>
        public static bool FingersCount(List<IFrame> frames, out int fingersCount)
        {
            fingersCount = frames[0].Fingers.Count;
            foreach (IFrame frame in frames)
            {
                if (frame.Fingers.Count != fingersCount)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Test if at least one hand is above the Leap. Permits to reduce the Leap's field of detection and to avoid untimely detection of hands.
        /// This function is implemented because the Leap detects hand when there is nothing, for exemple it considers my head as a hand when I'm just sitting on my chair.
        /// </summary>
        /// <param name="hands">The list of hands to test</param>
        /// <returns>If at least one hand is above the Leap</returns>
        public static bool IsAboveTheLeap(HandList hands)
        {
            foreach (Hand hand in hands)
            {
                if (hand.PalmPosition.x > -ParametersOther.Instance.LeapMath_xBoundaryForHandDetection 
                    && hand.PalmPosition.x < ParametersOther.Instance.LeapMath_xBoundaryForHandDetection
                    && hand.PalmPosition.z > -ParametersOther.Instance.LeapMath_zBoundaryForHandDetection 
                    && hand.PalmPosition.z < ParametersOther.Instance.LeapMath_zBoundaryForHandDetection)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Test if at least one finger is above the Leap. Permits to reduce the Leap's field of detection and to avoid untimely detection of hands (for HandPresent/HandLost events).
        /// </summary>
        /// <param name="fingers">The list of finger to test</param>
        /// <returns>If at least one finger is above the Leap</returns>
        public static bool IsAboveTheLeap(FingerList fingers)
        {
            foreach (Finger finger in fingers)
            {
                if (finger.TipPosition.x > -ParametersOther.Instance.LeapMath_xBoundaryForHandDetection
                    && finger.TipPosition.x < ParametersOther.Instance.LeapMath_xBoundaryForHandDetection
                    && finger.TipPosition.z > -ParametersOther.Instance.LeapMath_zBoundaryForHandDetection
                    && finger.TipPosition.z < ParametersOther.Instance.LeapMath_zBoundaryForHandDetection)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
