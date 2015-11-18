using System.Collections.Generic;

namespace IntuiLab.Leap.DataStructures
{
    /// <summary>
    /// Interface to manipulate frame data
    /// </summary>
    interface IFrame
    {
        long Id { get; }
        List<HandData> Hands { get; }
        HandData FrontmostHand { get; }
        List<FingerData> Fingers { get; }
        FingerData FrontmostFinger { get; }
        long Timestamp { get; }
        bool IsValid { get; }

        /// <summary>
        /// Permits to access the FingerData with the corresponding id.
        /// </summary>
        /// <param name="id">The id of the FingerData wanted</param>
        /// <returns>The instance of FingerData or an invalid finger</returns>
        FingerData Finger(int id);

        /// <summary>
        /// Permits to access the HandData with the corresponding id
        /// </summary>
        /// <param name="id">The id of the HandData wanted</param>
        /// <returns>The instance of HandData or an invalid hand</returns>
        HandData Hand(int id);
    }
}
