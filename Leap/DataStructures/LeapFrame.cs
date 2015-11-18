using Leap;
using System.Collections.Generic;

namespace IntuiLab.Leap.DataStructures
{
    /// <summary>
    /// Structure which stores Leap frame data
    /// </summary>
    internal class LeapFrame : IFrame
    {
        private Frame m_frame; // the frame directly from the Leap device

        #region Fields

        /// <summary>
        /// The frame id
        /// </summary>
        public long Id
        {
            get
            {
                return m_frame.Id;
            }
        }

        /// <summary>
        /// The list of hands in this frame
        /// </summary>
        public List<HandData> Hands
        {
            get
            {
                List<HandData> hands = new List<HandData>();
                foreach (Hand h in m_frame.Hands)
                {
                    hands.Add(new HandData(h));
                }
                return hands;
            }
        }

        /// <summary>
        /// The frontmost hand of this frame
        /// </summary>
        public HandData FrontmostHand
        {
            get
            {
                return new HandData(m_frame.Hands.Frontmost);
            }
        }

        /// <summary>
        /// The list of fingers of this frame
        /// </summary>
        public List<FingerData> Fingers
        {
            get
            {
                List<FingerData> fingers = new List<FingerData>();
                foreach (Finger f in m_frame.Fingers)
                {
                    fingers.Add(new FingerData(f));
                }
                return fingers;
            }
        }

        /// <summary>
        /// The frontmost finger of this frame
        /// </summary>
        public FingerData FrontmostFinger
        {
            get
            {
                return new FingerData(m_frame.Fingers.Frontmost);
            }
        }

        /// <summary>
        /// The timestamp of this frame
        /// </summary>
        public long Timestamp
        {
            get
            {
                return m_frame.Timestamp;
            }
        }

        /// <summary>
        /// If this frame is valid or not
        /// </summary>
        public bool IsValid
        {
            get
            {
                return m_frame.IsValid;
            }
        }

        #endregion

        public LeapFrame(Frame frame)
        {
            this.m_frame = frame;
        }

        #region Methods

        /// <summary>
        /// The finger with the specified id in this frame
        /// </summary>
        /// <param name="id">The id of the finger</param>
        /// <returns>The finger if it exists, an invalid finger otherwise</returns>
        public FingerData Finger(int id)
        {
            return new FingerData(m_frame.Finger(id));
        }
        
        /// <summary>
        /// The hand with the specified id in this frame
        /// </summary>
        /// <param name="id">The id of the hand</param>
        /// <returns>The hand if it exists, an invalid hand otherwise</returns>
        public HandData Hand(int id)
        {
            return new HandData(m_frame.Hand(id));
        }

        #endregion
    }
}
