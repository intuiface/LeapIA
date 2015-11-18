using Leap;

namespace IntuiLab.Leap.DataStructures
{
    /// <summary>
    /// Structure which stores data of fingers
    /// </summary>
    internal class FingerData
    {
        /// <summary>
        /// The id of this finger
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
        /// If this finger is the frontmost
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
        /// The tip position of this finger
        /// </summary>
        private Vector m_tipPosition;
        public Vector TipPosition
        {
            get
            {
                return m_tipPosition;
            }
        }

        /// <summary>
        /// The length of the finger
        /// </summary>
        private float m_length;
        public float Length
        {
            get
            {
                return m_length;
            }
        }

        /// <summary>
        /// If this finger is valid
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
        /// An invalid finger
        /// </summary>
        public static FingerData Invalid
        {
            get
            {
                return new FingerData(-1, false, 0, 0, 0, false);
            }
        }

        // Constructor for ReplayFrame
        public FingerData(int id, bool isFrontmost, float tipPosition_x, float tipPosition_y, float tipPosition_z, bool isValid)
        {
            m_id = id;
            m_isFrontmost = isFrontmost;
            m_tipPosition = new Vector(tipPosition_x, tipPosition_y, tipPosition_z);
            m_isValid = isValid;
        }

        //Constructor for LeapFrame
        public FingerData(Finger finger)
        {
            m_id = finger.Id;
            m_isFrontmost = (finger.Id == finger.Frame.Fingers.Frontmost.Id);
            m_tipPosition = finger.TipPosition;
            m_length = finger.Length;
            m_isValid = finger.IsValid;
        }
    }
}
