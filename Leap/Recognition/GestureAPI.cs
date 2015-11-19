using IntuiLab.Leap.Events;
using IntuiLab.Leap.Recognition.Gestures;
using Leap;

namespace IntuiLab.Leap.Recognition
{
    /// <summary>
    /// An abstract class to permit to chose between Leap Gesture API and IntuiLab Gesture API
    /// </summary>
    internal abstract class GestureAPI
    {
        #region Events

        /************ Events raised when gestures are detected **************************************/

        /// <summary>
        /// Event raised when a Tap is detected
        /// </summary>
        public event LinearGestureEventHandler TapGestureDetected;

        /// <summary>
        /// Event raised when a Push is detected
        /// </summary>
        public event LinearGestureEventHandler PushGestureDetected;

        /// <summary>
        /// Events raised when Swipes are detected
        /// </summary>
        public event LinearGestureEventHandler SwipeRightGestureDetected;
        public event LinearGestureEventHandler SwipeLeftGestureDetected;

        /// <summary>
        /// Event raised when a Circle is detected
        /// </summary>
        public event CircleGestureEventHandler CircleGestureDetected;

        /// <summary>
        /// Event raised when no gesture is detected (for debug)
        /// </summary>
        public event LinearGestureEventHandler NoGestureDetected;


        protected void RaiseTapGestureEvent(float speed, Vector direction, long timestamp)
        {
            if (TapGestureDetected != null)
                TapGestureDetected(this, new LinearGestureEventArgs(speed, direction, timestamp));
        }

        protected void RaisePushGestureEvent(float speed, Vector direction, long timestamp)
        {
            if (PushGestureDetected != null)
                PushGestureDetected(this, new LinearGestureEventArgs(speed, direction, timestamp));
        }

        protected void RaiseSwipeRightGestureEvent(float speed, Vector direction, long timestamp)
        {
            if (SwipeRightGestureDetected != null)
                SwipeRightGestureDetected(this, new LinearGestureEventArgs(speed, direction, timestamp));
        }

        protected void RaiseSwipeLeftGestureEvent(float speed, Vector direction, long timestamp)
        {
            if (SwipeLeftGestureDetected != null)
                SwipeLeftGestureDetected(this, new LinearGestureEventArgs(speed, direction, timestamp));
        }

        protected void RaiseCircleGestureEvent(int id, float progress, string clockwiseness, float angle)
        {
            if (CircleGestureDetected != null)
                CircleGestureDetected(this, new CircleGestureEventArgs(id, progress, clockwiseness, angle));
        }

        protected void RaiseNoGestureEvent(float speed, Vector direction, long timestamp)
        {
            if (NoGestureDetected != null)
                NoGestureDetected(this, new LinearGestureEventArgs(speed, direction, timestamp));
        }

        /********************************************************************************************/

        #endregion

        public abstract void EnableGestureRecognition(GestureType gestureType);
        public abstract void DisableGestureRecognition(GestureType gestureType);
        public abstract void GestureDetection(Frame frame);
    }
}
