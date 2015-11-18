using Leap;
using System.Collections.Generic;

namespace IntuiLab.Leap.DataStructures
{
    /// <summary>
    /// Structure which stores data of hands
    /// </summary>
    internal class HandData
    {
        /// <summary>
        /// The id of this hand
        /// </summary>
        private int m_id;
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        /// <summary>
        /// The list of fingers of this hand
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
        /// The number of fingers of this hand
        /// </summary>
        private int m_nbOfFingers;
        public int NbOfFingers
        {
            get
            {
                return m_nbOfFingers;
            }
        }

        /// <summary>
        /// If this hand is the frontmost
        /// </summary>
        private bool m_isFrontmost;
        public bool IsFrontmost
        {
            get
            {
                return m_isFrontmost;
            }
        }

        /// <summary>
        /// The palm postion of this hand
        /// </summary>
        private Vector m_palmPosition;
        public Vector PalmPosition
        {
            get
            {
                return m_palmPosition;
            }
        }

        /// <summary>
        /// If this hand is valid
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
        /// An invalid hand
        /// </summary>
        public static HandData Invalid
        {
            get
            {
                return new HandData(-1, 0, false, 0, 0, 0, false);
            }
        }

        // Constructor for ReplayFrame
        public HandData(int id, int nbOfFingers, bool isFrontmost, float palmPosition_x, float palmPosition_y, float palmPosition_z, bool isValid)
        {
            m_id = id;
            m_nbOfFingers = nbOfFingers;
            m_isFrontmost = isFrontmost;
            m_palmPosition = new Vector(palmPosition_x, palmPosition_y, palmPosition_z);
            m_isValid = isValid;
        }

        // Constructor for LeapFrame
        public HandData(Hand hand)
        {
            m_id = hand.Id;

            m_fingers = new List<FingerData>();
            foreach (Finger f in hand.Fingers)
            {
                m_fingers.Add(new FingerData(f));
            }

            m_isFrontmost = (hand.Id == hand.Frame.Hands.Frontmost.Id);
            m_palmPosition = hand.PalmPosition;
            m_isValid = hand.IsValid;
        }
    }
}
