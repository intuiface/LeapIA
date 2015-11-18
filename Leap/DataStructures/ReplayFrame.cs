using System.Collections.Generic;

namespace IntuiLab.Leap.DataStructures
{
    /// <summary>
    /// Structure which stores replay frame data
    /// </summary>
    internal class ReplayFrame : IFrame
    {
        /// <summary>
        /// The frame id
        /// </summary>
        private long m_id;
        public long Id
        {
            get
            {
                return m_id;
            }
        }

        /// <summary>
        /// The list of hands in this frame
        /// </summary>
        private List<HandData> m_hands;
        public List<HandData> Hands
        {
            get
            {
                return m_hands;
            }
        }

        /// <summary>
        /// The frontmost hand of this frame
        /// </summary>
        public HandData FrontmostHand
        {
            get
            {
                foreach (HandData hd in Hands)
                {
                    if (hd.IsFrontmost)
                        return hd;
                }
                return HandData.Invalid;
            }
        }

        /// <summary>
        /// The list of fingers of this frame
        /// </summary>
        private List<FingerData> m_fingers;
        public List<FingerData> Fingers
        {
            get
            {
                return m_fingers;
            }
        }

        /// <summary>
        /// The frontmost finger of this frame
        /// </summary>
        public FingerData FrontmostFinger
        {
            get
            {
                foreach (FingerData fd in Fingers)
                {
                    if (fd.IsFrontmost)
                        return fd;
                }
                return FingerData.Invalid;
            }
        }

        /// <summary>
        /// The timestamp of this frame
        /// </summary>
        private long m_timestamp;
        public long Timestamp
        {
            get
            {
                return m_timestamp;
            }
        }

        /// <summary>
        /// If this frame is valid or not
        /// </summary>
        private bool m_isValid;
        public bool IsValid
        {
            get
            {
                return m_isValid;
            }
        }

        /// <summary>
        /// Constructor for frame from replay.
        /// </summary>
        /// <param name="id">The id of the frame</param>
        /// <param name="fingers">This is the information for a finger, the key of the dictionary reprensents the finger id, and the list of objet is in the correct order :
        ///  bool isFrontmost, float tipPosition_x, float tipPosition_y, float tipPosition_z, bool isValid</param>
        /// <param name="timestamp">The timestamp of the frame</param>
        /// <param name="isValid">Whether the frame is valid or not</param>
        public ReplayFrame(long id, Dictionary<int, List<object>> fingers, long timestamp, bool isValid)
        {
            m_id = id;

            m_hands = new List<HandData>();

            m_fingers = new List<FingerData>();
            foreach (var f in fingers)
            {
                if (f.Value.Count == 5)
                {
                    m_fingers.Add(new FingerData(f.Key, (bool)f.Value[0], (float)f.Value[1], (float)f.Value[2], (float)f.Value[3], (bool)f.Value[4]));
                }
            }

            m_timestamp = timestamp;
            m_isValid = isValid;
        }


        #region Methods

        /// <summary>
        /// The finger with the specified id in this frame
        /// </summary>
        /// <param name="id">The id of the finger</param>
        /// <returns>The finger if it exists, an invalid finger otherwise</returns>
        public FingerData Finger(int id)
        {
            foreach (FingerData fd in Fingers)
            {
                if (fd.Id == id)
                    return fd;
            }
            return FingerData.Invalid;
        }

        /// <summary>
        /// The hand with the specified id in this frame
        /// </summary>
        /// <param name="id">The id of the hand</param>
        /// <returns>The hand if it exists, an invalid hand otherwise</returns>
        public HandData Hand(int id)
        {
            foreach (HandData hd in Hands)
            {
                if (hd.Id == id)
                    return hd;
            }
            return HandData.Invalid;
        }

        #endregion
    }
}
