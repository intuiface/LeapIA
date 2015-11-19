using IntuiLab.Leap.DataStructures;
using IntuiLab.Leap.Utils;
using Leap;
using System;
using System.Collections.Generic;
using System.Timers;

namespace IntuiLab.Leap.Recognition.Postures
{
    /// <summary>
    /// This class contains the algorithm of postures recognition
    /// </summary>
    internal class PostureRecognizer
    {
        #region Fields

        private bool m_VerboseMode;

        /// <summary>
        /// It indicates if a posture is being held (not only the detection, but also the holding of the posture after the succeed event is raised)
        /// </summary>
        private Dictionary<PostureType, bool> m_FlagPosture;

        /// <summary>
        /// Permits to detect if the posture is not held anymore
        /// </summary>
        private Timer m_PostureEndTimer;

        #endregion

        #region Properties

        /// <summary>
        /// Contains the timestamp of the last event raised for a posture
        /// </summary>
        private Tuple<PostureType, long> m_lastPostureTimestamp;
        public Tuple<PostureType, long> LastPostureTimestamp
        {
            get
            {
                return m_lastPostureTimestamp;
            }
        }

        #endregion

        #region Events

        /***************************** Events for posture's start ****************************************************************/
        public event PostureDetectedEventHandler PostureRockBegin;
        public event PostureDetectedEventHandler PostureScissorsBegin;
        public event PostureDetectedEventHandler PosturePaperBegin;

        public event PostureDetectedEventHandler PostureZeroBegin;
        public event PostureDetectedEventHandler PostureOneBegin;
        public event PostureDetectedEventHandler PostureTwoBegin;
        public event PostureDetectedEventHandler PostureThreeBegin;
        public event PostureDetectedEventHandler PostureFourBegin;
        public event PostureDetectedEventHandler PostureFiveBegin;


        // This function raise an event if the posture just begin to be detected.
        // It also set the flag to true, fill the last posture timestamp and start the postureEndTimer.
        // If this timer elapsed, it means that a posture is no more recognize (too much time elapsed since the last raise of event) so that the posture ends.

        private void RaisePostureBegin(PostureType postureType, long timestamp)
        {
            switch (postureType)
            {
                case PostureType.Rock:
                    if (PostureRockBegin != null)
                    {
                        this.m_FlagPosture[postureType] = true;
                        this.m_PostureEndTimer.Start();
                        PostureRockBegin(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Scissors:
                    if (PostureScissorsBegin != null)
                    {
                        this.m_FlagPosture[postureType] = true;
                        this.m_PostureEndTimer.Start();
                        PostureScissorsBegin(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Paper:
                    if (PosturePaperBegin != null)
                    {
                        this.m_FlagPosture[postureType] = true;
                        this.m_PostureEndTimer.Start();
                        PosturePaperBegin(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Zero:
                    if (PostureZeroBegin != null)
                    {
                        this.m_FlagPosture[postureType] = true;
                        this.m_PostureEndTimer.Start();
                        PostureZeroBegin(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.One:
                    if (PostureOneBegin != null)
                    {
                        this.m_FlagPosture[postureType] = true;
                        this.m_PostureEndTimer.Start();
                        PostureOneBegin(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Two:
                    if (PostureTwoBegin != null)
                    {
                        this.m_FlagPosture[postureType] = true;
                        this.m_PostureEndTimer.Start();
                        PostureTwoBegin(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Three:
                    if (PostureThreeBegin != null)
                    {
                        this.m_FlagPosture[postureType] = true;
                        this.m_PostureEndTimer.Start();
                        PostureThreeBegin(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Four:
                    if (PostureFourBegin != null)
                    {
                        this.m_FlagPosture[postureType] = true;
                        this.m_PostureEndTimer.Start();
                        PostureFourBegin(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Five:
                    if (PostureFiveBegin != null)
                    {
                        this.m_FlagPosture[postureType] = true;
                        this.m_PostureEndTimer.Start();
                        PostureFiveBegin(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                default:
                    break;
            }
        }
        /**************************************************************************************************************************/

        /******************* Events for posture in progress ***********************************************************************/
        public event PostureDetectedEventHandler PostureRockHeld;
        public event PostureDetectedEventHandler PostureScissorsHeld;
        public event PostureDetectedEventHandler PosturePaperHeld;

        public event PostureDetectedEventHandler PostureZeroHeld;
        public event PostureDetectedEventHandler PostureOneHeld;
        public event PostureDetectedEventHandler PostureTwoHeld;
        public event PostureDetectedEventHandler PostureThreeHeld;
        public event PostureDetectedEventHandler PostureFourHeld;
        public event PostureDetectedEventHandler PostureFiveHeld;

       
        // This function raise an event is a posture is held, restart the postureEndTimer and fill the last posture timestamp
        
        private void RaisePostureHeld(PostureType postureType, long timestamp)
        {
            switch (postureType)
            {
                case PostureType.Rock:
                    if (PostureRockHeld != null)
                    {
                        RestartTimer(this.m_PostureEndTimer);
                        PostureRockHeld(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Scissors:
                    if (PostureScissorsHeld != null)
                    {
                        RestartTimer(this.m_PostureEndTimer);
                        PostureScissorsHeld(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Paper:
                    if (PosturePaperHeld != null)
                    {
                        RestartTimer(this.m_PostureEndTimer);
                        PosturePaperHeld(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Zero:
                    if (PostureZeroHeld != null)
                    {
                        RestartTimer(this.m_PostureEndTimer);
                        PostureZeroHeld(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.One:
                    if (PostureOneHeld != null)
                    {
                        RestartTimer(this.m_PostureEndTimer);
                        PostureOneHeld(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Two:
                    if (PostureTwoHeld != null)
                    {
                        RestartTimer(this.m_PostureEndTimer);
                        PostureTwoHeld(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Three:
                    if (PostureThreeHeld != null)
                    {
                        RestartTimer(this.m_PostureEndTimer);
                        PostureThreeHeld(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Four:
                    if (PostureFourHeld != null)
                    {
                        RestartTimer(this.m_PostureEndTimer);
                        PostureFourHeld(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                case PostureType.Five:
                    if (PostureFiveHeld != null)
                    {
                        RestartTimer(this.m_PostureEndTimer);
                        PostureFiveHeld(this, new PostureDetectedEventArgs(postureType, timestamp));
                        this.m_lastPostureTimestamp = new Tuple<PostureType, long>(postureType, timestamp);
                    }
                    break;
                default:
                    break;
            }
        }
        /**************************************************************************************************************************/
        
        /// <summary>
        /// This function restart a timer
        /// </summary>
        /// <param name="timer">The timer to restart</param>
        private void RestartTimer(Timer timer)
        {
            timer.Stop();
            timer.Start();
        }

        /// <summary>
        /// When this timer elapses, it means that postures are not held anymore
        /// </summary>
        private void OnPostureEndTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // we just set all the value of the flags to false
            this.m_PostureEndTimer.Stop();
            this.m_FlagPosture = new Dictionary<PostureType, bool>(8)
            {
                {PostureType.Rock, false},
                {PostureType.Scissors, false},
                {PostureType.Paper, false},
                {PostureType.Zero, false},
                {PostureType.One, false},
                {PostureType.Two, false},
                {PostureType.Three, false},
                {PostureType.Four, false},
                {PostureType.Five, false}
            };
        }
        #endregion

        #region Constructor

        public PostureRecognizer(bool verboseMode)
        {
            // initialisation of the fields
            this.m_VerboseMode = verboseMode;
            this.m_lastPostureTimestamp = new Tuple<PostureType, long>(PostureType.None, 0L);
            this.m_FlagPosture = new Dictionary<PostureType, bool>(8)
            {
                {PostureType.Rock, false},
                {PostureType.Scissors, false},
                {PostureType.Paper, false},
                {PostureType.Zero, false},
                {PostureType.One, false},
                {PostureType.Two, false},
                {PostureType.Three, false},
                {PostureType.Four, false},
                {PostureType.Five, false}
            };
            this.m_PostureEndTimer = new Timer(ParametersPosture.Instance.PostureEndTimerInterval);
            this.m_PostureEndTimer.Elapsed += OnPostureEndTimerElapsed;
        }

        #endregion

        /// <summary>
        /// Check if a posture is recognized
        /// </summary>
        /// <param name="frames">The list of frames to test, already preprocessing (the hand is present in each of them)</param>
        /// <param name="handId">The id of the hand supposed to performing the posture</param>
        public void Process(List<IFrame> frames, int handId)
        {
            List<Vector> palmPositions = new List<Vector>();

            // we store all the palm position in a list
            for (int i = 0; i < frames.Count; i++)
            {
                HandData hand = frames[i].Hand(handId);
                palmPositions.Add(hand.PalmPosition);
            }

            // we check if this hand is immobile (if the palm position stay immobile)
            // if yes, we go on
            if (!LeapMath.IsImmobile(palmPositions))
                return;

            if (m_VerboseMode)
            {
                Console.WriteLine("palm positions=");
                foreach (Vector p in palmPositions)
                {
                    Console.WriteLine(p);
                }
            }

            IFrame actualFrame = frames[0];
            int fingersCount;

            // we count the number of fingers to determine the posture type, and raise the corresponding event
            if (LeapMath.FingersCount(frames, out fingersCount))
            {
                switch (fingersCount)
                {
                    case 0:
                        if (this.m_FlagPosture[PostureType.Rock] == false) // if the posture begin
                            RaisePostureBegin(PostureType.Rock, actualFrame.Timestamp);
                        else // if the posture is held
                            RaisePostureHeld(PostureType.Rock, actualFrame.Timestamp);

                        if (this.m_FlagPosture[PostureType.Zero] == false)
                            RaisePostureBegin(PostureType.Zero, actualFrame.Timestamp);
                        else
                            RaisePostureHeld(PostureType.Zero, actualFrame.Timestamp);
                        break;

                    case 1:
                        if (actualFrame.Fingers[0].Length <= 40)
                        {
                            if (this.m_FlagPosture[PostureType.Rock] == false) // if the posture begin
                                RaisePostureBegin(PostureType.Rock, actualFrame.Timestamp);
                            else // if the posture is held
                                RaisePostureHeld(PostureType.Rock, actualFrame.Timestamp);

                            if (this.m_FlagPosture[PostureType.Zero] == false)
                                RaisePostureBegin(PostureType.Zero, actualFrame.Timestamp);
                            else
                                RaisePostureHeld(PostureType.Zero, actualFrame.Timestamp);
                        }
                        else
                        {
                            if (this.m_FlagPosture[PostureType.One] == false)
                                RaisePostureBegin(PostureType.One, actualFrame.Timestamp);
                            else
                                RaisePostureHeld(PostureType.One, actualFrame.Timestamp);
                        }
                        break;

                    case 2:
                        if (this.m_FlagPosture[PostureType.Scissors] == false)
                            RaisePostureBegin(PostureType.Scissors, actualFrame.Timestamp);
                        else
                            RaisePostureHeld(PostureType.Scissors, actualFrame.Timestamp);

                        if (this.m_FlagPosture[PostureType.Two] == false)
                            RaisePostureBegin(PostureType.Two, actualFrame.Timestamp);
                        else
                            RaisePostureHeld(PostureType.Two, actualFrame.Timestamp);
                        break;

                    case 3:
                        if (this.m_FlagPosture[PostureType.Three] == false)
                            RaisePostureBegin(PostureType.Three, actualFrame.Timestamp);
                        else
                            RaisePostureHeld(PostureType.Three, actualFrame.Timestamp);
                        break;

                    case 4:
                        if (this.m_FlagPosture[PostureType.Four] == false)
                            RaisePostureBegin(PostureType.Four, actualFrame.Timestamp);
                        else
                            RaisePostureHeld(PostureType.Four, actualFrame.Timestamp);
                        break;

                    case 5:
                        if (this.m_FlagPosture[PostureType.Paper] == false)
                            RaisePostureBegin(PostureType.Paper, actualFrame.Timestamp);
                        else
                            RaisePostureHeld(PostureType.Paper, actualFrame.Timestamp);

                        if (this.m_FlagPosture[PostureType.Five] == false)
                            RaisePostureBegin(PostureType.Five, actualFrame.Timestamp);
                        else
                            RaisePostureHeld(PostureType.Five, actualFrame.Timestamp);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
