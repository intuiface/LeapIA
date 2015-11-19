using IntuiLab.Leap.Events;
using IntuiLab.Leap.Recognition.Postures;
using System;
using System.ComponentModel;

namespace IntuiLab.Leap
{
    /// <summary>
    /// A facade for the fingers count posture Enable/Disable properties and events
    /// </summary>
    public class PostureFingerCountFacade : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(String strInfo)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strInfo));
            }
        }

        #region Properties

        #region Enable/Disable Properties

        /// <summary>
        /// Enable or disable the Zero posture
        /// </summary>
        private bool m_enableZeroPosture;
        public bool EnableZeroPosture
        {
            get
            {
                return m_enableZeroPosture;
            }
            set
            {
                if (value && !m_enableZeroPosture)
                {
                    LeapPlugin.Instance.LeapListener.EnablePostureRecognition(PostureType.Zero);
                    LeapPlugin.Instance.LeapListener.ZeroFingerPostureSucceed += OnZeroFingerPostureSucceed;
                    LeapPlugin.Instance.LeapListener.ZeroFingerPostureDetectionInProgress += OnZeroFingerPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.ZeroFingerPostureDetectionLost += OnZeroFingerPostureDetectionLost;
                }
                else if (!value && m_enableZeroPosture)
                {
                    LeapPlugin.Instance.LeapListener.DisablePostureRecognition(PostureType.Zero);
                    LeapPlugin.Instance.LeapListener.ZeroFingerPostureSucceed -= OnZeroFingerPostureSucceed;
                    LeapPlugin.Instance.LeapListener.ZeroFingerPostureDetectionInProgress -= OnZeroFingerPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.ZeroFingerPostureDetectionLost -= OnZeroFingerPostureDetectionLost;
                }
                m_enableZeroPosture = value;
                NotifyPropertyChanged("EnableZeroPosture");
            }
        }

        /// <summary>
        /// Enable or disable the One posture
        /// </summary>
        private bool m_enableOnePosture;
        public bool EnableOnePosture
        {
            get
            {
                return m_enableOnePosture;
            }
            set
            {
                if (value && !m_enableOnePosture)
                {
                    LeapPlugin.Instance.LeapListener.EnablePostureRecognition(PostureType.One);
                    LeapPlugin.Instance.LeapListener.OneFingerPostureSucceed += OnOneFingerPostureSucceed;
                    LeapPlugin.Instance.LeapListener.OneFingerPostureDetectionInProgress += OnOneFingerPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.OneFingerPostureDetectionLost += OnOneFingerPostureDetectionLost;
                }
                else if (!value && m_enableOnePosture)
                {
                    LeapPlugin.Instance.LeapListener.DisablePostureRecognition(PostureType.One);
                    LeapPlugin.Instance.LeapListener.OneFingerPostureSucceed -= OnOneFingerPostureSucceed;
                    LeapPlugin.Instance.LeapListener.OneFingerPostureDetectionInProgress -= OnOneFingerPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.OneFingerPostureDetectionLost -= OnOneFingerPostureDetectionLost;
                }
                m_enableOnePosture = value;
                NotifyPropertyChanged("EnableOnePosture");
            }
        }

        /// <summary>
        /// Enable or disable the Two posture
        /// </summary>
        private bool m_enableTwoPosture;
        public bool EnableTwoPosture
        {
            get
            {
                return m_enableTwoPosture;
            }
            set
            {
                if (value && !m_enableTwoPosture)
                {
                    LeapPlugin.Instance.LeapListener.EnablePostureRecognition(PostureType.Two);
                    LeapPlugin.Instance.LeapListener.TwoFingersPostureSucceed += OnTwoFingersPostureSucceed;
                    LeapPlugin.Instance.LeapListener.TwoFingersPostureDetectionInProgress += OnTwoFingersPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.TwoFingersPostureDetectionLost += OnTwoFingersPostureDetectionLost;
                }
                else if (!value && m_enableTwoPosture)
                {
                    LeapPlugin.Instance.LeapListener.DisablePostureRecognition(PostureType.Two);
                    LeapPlugin.Instance.LeapListener.TwoFingersPostureSucceed -= OnTwoFingersPostureSucceed;
                    LeapPlugin.Instance.LeapListener.TwoFingersPostureDetectionInProgress -= OnTwoFingersPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.TwoFingersPostureDetectionLost -= OnTwoFingersPostureDetectionLost;
                }
                m_enableTwoPosture = value;
                NotifyPropertyChanged("EnableTwoPosture");
            }
        }

        /// <summary>
        /// Enable or disable the Three posture
        /// </summary>
        private bool m_enableThreePosture;
        public bool EnableThreePosture
        {
            get
            {
                return m_enableThreePosture;
            }
            set
            {
                if (value && !m_enableThreePosture)
                {
                    LeapPlugin.Instance.LeapListener.EnablePostureRecognition(PostureType.Three);
                    LeapPlugin.Instance.LeapListener.ThreeFingersPostureSucceed += OnThreeFingersPostureSucceed;
                    LeapPlugin.Instance.LeapListener.ThreeFingersPostureDetectionInProgress += OnThreeFingersPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.ThreeFingersPostureDetectionLost += OnThreeFingersPostureDetectionLost;
                }
                else if (!value && m_enableThreePosture)
                {
                    LeapPlugin.Instance.LeapListener.DisablePostureRecognition(PostureType.Three);
                    LeapPlugin.Instance.LeapListener.ThreeFingersPostureSucceed -= OnThreeFingersPostureSucceed;
                    LeapPlugin.Instance.LeapListener.ThreeFingersPostureDetectionInProgress -= OnThreeFingersPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.ThreeFingersPostureDetectionLost -= OnThreeFingersPostureDetectionLost;
                }
                m_enableThreePosture = value;
                NotifyPropertyChanged("EnableThreePosture");
            }
        }

        /// <summary>
        /// Enable or disable the Four posture
        /// </summary>
        private bool m_enableFourPosture;
        public bool EnableFourPosture
        {
            get
            {
                return m_enableFourPosture;
            }
            set
            {
                if (value && !m_enableFourPosture)
                {
                    LeapPlugin.Instance.LeapListener.EnablePostureRecognition(PostureType.Four);
                    LeapPlugin.Instance.LeapListener.FourFingersPostureSucceed += OnFourFingersPostureSucceed;
                    LeapPlugin.Instance.LeapListener.FourFingersPostureDetectionInProgress += OnFourFingersPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.FourFingersPostureDetectionLost += OnFourFingersPostureDetectionLost;
                }
                else if (!value && m_enableFourPosture)
                {
                    LeapPlugin.Instance.LeapListener.DisablePostureRecognition(PostureType.Four);
                    LeapPlugin.Instance.LeapListener.FourFingersPostureSucceed -= OnFourFingersPostureSucceed;
                    LeapPlugin.Instance.LeapListener.FourFingersPostureDetectionInProgress -= OnFourFingersPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.FourFingersPostureDetectionLost -= OnFourFingersPostureDetectionLost;
                }
                m_enableFourPosture = value;
                NotifyPropertyChanged("EnableFourPosture");
            }
        }

        /// <summary>
        /// Enable or disable the Five posture
        /// </summary>
        private bool m_enableFivePosture;
        public bool EnableFivePosture
        {
            get
            {
                return m_enableFivePosture;
            }
            set
            {
                if (value && !m_enableFivePosture)
                {
                    LeapPlugin.Instance.LeapListener.EnablePostureRecognition(PostureType.Five);
                    LeapPlugin.Instance.LeapListener.FiveFingersPostureSucceed += OnFiveFingersPostureSucceed;
                    LeapPlugin.Instance.LeapListener.FiveFingersPostureDetectionInProgress += OnFiveFingersPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.FiveFingersPostureDetectionLost += OnFiveFingersPostureDetectionLost;
                }
                else if (!value && m_enableFivePosture)
                {
                    LeapPlugin.Instance.LeapListener.DisablePostureRecognition(PostureType.Five);
                    LeapPlugin.Instance.LeapListener.FiveFingersPostureSucceed -= OnFiveFingersPostureSucceed;
                    LeapPlugin.Instance.LeapListener.FiveFingersPostureDetectionInProgress -= OnFiveFingersPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.FiveFingersPostureDetectionLost -= OnFiveFingersPostureDetectionLost;
                }
                m_enableFivePosture = value;
                NotifyPropertyChanged("EnableFivePosture");
            }
        }

        #endregion

        #region Detection Time

        /// <summary>
        /// The detection time of the zero posture in milliseconds
        /// </summary>
        public int DetectionTimeForZeroInMilliseconds
        {
            get
            {
                return ParametersScheduler.Instance.NbEventsForZeroPostureToBeDetected * 10;
            }
            set
            {
                ParametersScheduler.Instance.NbEventsForZeroPostureToBeDetected = value / 10;
                NotifyPropertyChanged("DetectionTimeForZeroInMilliseconds");
            }
        }

        /// <summary>
        /// The detection time of the one posture in milliseconds
        /// </summary>
        public int DetectionTimeForOneInMilliseconds
        {
            get
            {
                return ParametersScheduler.Instance.NbEventsForOnePostureToBeDetected * 10;
            }
            set
            {
                ParametersScheduler.Instance.NbEventsForOnePostureToBeDetected = value / 10;
                NotifyPropertyChanged("DetectionTimeForOneInMilliseconds");
            }
        }

        /// <summary>
        /// The detection time of the two posture in milliseconds
        /// </summary>
        public int DetectionTimeForTwoInMilliseconds
        {
            get
            {
                return ParametersScheduler.Instance.NbEventsForTwoPostureToBeDetected * 10;
            }
            set
            {
                ParametersScheduler.Instance.NbEventsForTwoPostureToBeDetected = value / 10;
                NotifyPropertyChanged("DetectionTimeForTwoInMilliseconds");
            }
        }

        /// <summary>
        /// The detection time of the three posture in milliseconds
        /// </summary>
        public int DetectionTimeForThreeInMilliseconds
        {
            get
            {
                return ParametersScheduler.Instance.NbEventsForThreePostureToBeDetected * 10;
            }
            set
            {
                ParametersScheduler.Instance.NbEventsForThreePostureToBeDetected = value / 10;
                NotifyPropertyChanged("DetectionTimeForThreeInMilliseconds");
            }
        }

        /// <summary>
        /// The detection time of the four posture in milliseconds
        /// </summary>
        public int DetectionTimeForFourInMilliseconds
        {
            get
            {
                return ParametersScheduler.Instance.NbEventsForFourPostureToBeDetected * 10;
            }
            set
            {
                ParametersScheduler.Instance.NbEventsForFourPostureToBeDetected = value / 10;
                NotifyPropertyChanged("DetectionTimeForFourInMilliseconds");
            }
        }

        /// <summary>
        /// The detection time of the five posture in milliseconds
        /// </summary>
        public int DetectionTimeForFiveInMilliseconds
        {
            get
            {
                return ParametersScheduler.Instance.NbEventsForFivePostureToBeDetected * 10;
            }
            set
            {
                ParametersScheduler.Instance.NbEventsForFivePostureToBeDetected = value / 10;
                NotifyPropertyChanged("DetectionTimeForFiveInMilliseconds");
            }
        }

        #endregion

        #endregion

        #region Events

        public event PostureEventHandler ZeroFingerPostureSucceed;
        public event PostureEventHandler OneFingerPostureSucceed;
        public event PostureEventHandler TwoFingersPostureSucceed;
        public event PostureEventHandler ThreeFingersPostureSucceed;
        public event PostureEventHandler FourFingersPostureSucceed;
        public event PostureEventHandler FiveFingersPostureSucceed;

        public event PostureEventHandler ZeroFingerPostureDetectionInProgress;
        public event PostureEventHandler OneFingerPostureDetectionInProgress;
        public event PostureEventHandler TwoFingersPostureDetectionInProgress;
        public event PostureEventHandler ThreeFingersPostureDetectionInProgress;
        public event PostureEventHandler FourFingersPostureDetectionInProgress;
        public event PostureEventHandler FiveFingersPostureDetectionInProgress;

        public event PostureEventHandler ZeroFingerPostureDetectionLost;
        public event PostureEventHandler OneFingerPostureDetectionLost;
        public event PostureEventHandler TwoFingersPostureDetectionLost;
        public event PostureEventHandler ThreeFingersPostureDetectionLost;
        public event PostureEventHandler FourFingersPostureDetectionLost;
        public event PostureEventHandler FiveFingersPostureDetectionLost;


        private void RaiseZeroPostureSucceed(PostureEventArgs e)
        {
            if (ZeroFingerPostureSucceed != null)
            {
                ZeroFingerPostureSucceed(this, e);
            }
        }
        private void RaiseZeroPostureDetectionInProgress(PostureEventArgs e)
        {
            if (ZeroFingerPostureDetectionInProgress != null)
            {
                ZeroFingerPostureDetectionInProgress(this, e);
            }
        }
        private void RaiseZeroPostureDetectionLost(PostureEventArgs e)
        {
            if (ZeroFingerPostureDetectionLost != null)
                ZeroFingerPostureDetectionLost(this, e);
        }


        private void RaiseOnePostureSucceed(PostureEventArgs e)
        {
            if (OneFingerPostureSucceed != null)
            {
                OneFingerPostureSucceed(this, e);
            }
        }
        private void RaiseOnePostureDetectionInProgress(PostureEventArgs e)
        {
            if (OneFingerPostureDetectionInProgress != null)
            {
                OneFingerPostureDetectionInProgress(this, e);
            }
        }
        private void RaiseOnePostureDetectionLost(PostureEventArgs e)
        {
            if (OneFingerPostureDetectionLost != null)
                OneFingerPostureDetectionLost(this, e);
        }


        private void RaiseTwoPostureSucceed(PostureEventArgs e)
        {
            if (TwoFingersPostureSucceed != null)
            {
                TwoFingersPostureSucceed(this, e);
            }
        }
        private void RaiseTwoPostureDetectionInProgress(PostureEventArgs e)
        {
            if (TwoFingersPostureDetectionInProgress != null)
            {
                TwoFingersPostureDetectionInProgress(this, e);
            }
        }
        private void RaiseTwoPostureDetectionLost(PostureEventArgs e)
        {
            if (TwoFingersPostureDetectionLost != null)
                TwoFingersPostureDetectionLost(this, e);
        }


        private void RaiseThreePostureSucceed(PostureEventArgs e)
        {
            if (ThreeFingersPostureSucceed != null)
            {
                ThreeFingersPostureSucceed(this, e);
            }
        }
        private void RaiseThreePostureDetectionInProgress(PostureEventArgs e)
        {
            if (ThreeFingersPostureDetectionInProgress != null)
            {
                ThreeFingersPostureDetectionInProgress(this, e);
            }
        }
        private void RaiseThreePostureDetectionLost(PostureEventArgs e)
        {
            if (ThreeFingersPostureDetectionLost != null)
                ThreeFingersPostureDetectionLost(this, e);
        }


        private void RaiseFourPostureSucceed(PostureEventArgs e)
        {
            if (FourFingersPostureSucceed != null)
            {
                FourFingersPostureSucceed(this, e);
            }
        }
        private void RaiseFourPostureDetectionInProgress(PostureEventArgs e)
        {
            if (FourFingersPostureDetectionInProgress != null)
            {
                FourFingersPostureDetectionInProgress(this, e);
            }
        }
        private void RaiseFourPostureDetectionLost(PostureEventArgs e)
        {
            if (FourFingersPostureDetectionLost != null)
                FourFingersPostureDetectionLost(this, e);
        }


        private void RaiseFivePostureSucceed(PostureEventArgs e)
        {
            if (FiveFingersPostureSucceed != null)
            {
                FiveFingersPostureSucceed(this, e);
            }
        }
        private void RaiseFivePostureDetectionInProgress(PostureEventArgs e)
        {
            if (FiveFingersPostureDetectionInProgress != null)
            {
                FiveFingersPostureDetectionInProgress(this, e);
            }
        }
        private void RaiseFivePostureDetectionLost(PostureEventArgs e)
        {
            if (FiveFingersPostureDetectionLost != null)
                FiveFingersPostureDetectionLost(this, e);
        }


        private void OnZeroFingerPostureSucceed(object sender, PostureEventArgs e)
        {
            RaiseZeroPostureSucceed(e);
        }
        private void OnZeroFingerPostureDetectionInProgress(object sender, PostureEventArgs e)
        {
            RaiseZeroPostureDetectionInProgress(e);
        }
        private void OnZeroFingerPostureDetectionLost(object sender, PostureEventArgs e)
        {
            RaiseZeroPostureDetectionLost(e);
        }


        private void OnOneFingerPostureSucceed(object sender, PostureEventArgs e)
        {
            RaiseOnePostureSucceed(e);
        }
        private void OnOneFingerPostureDetectionInProgress(object sender, PostureEventArgs e)
        {
            RaiseOnePostureDetectionInProgress(e);
        }
        private void OnOneFingerPostureDetectionLost(object sender, PostureEventArgs e)
        {
            RaiseOnePostureDetectionLost(e);
        }


        private void OnTwoFingersPostureSucceed(object sender, PostureEventArgs e)
        {
            RaiseTwoPostureSucceed(e);
        }
        private void OnTwoFingersPostureDetectionInProgress(object sender, PostureEventArgs e)
        {
            RaiseTwoPostureDetectionInProgress(e);
        }
        private void OnTwoFingersPostureDetectionLost(object sender, PostureEventArgs e)
        {
            RaiseTwoPostureDetectionLost(e);
        }


        private void OnThreeFingersPostureSucceed(object sender, PostureEventArgs e)
        {
            RaiseThreePostureSucceed(e);
        }
        private void OnThreeFingersPostureDetectionInProgress(object sender, PostureEventArgs e)
        {
            RaiseThreePostureDetectionInProgress(e);
        }
        private void OnThreeFingersPostureDetectionLost(object sender, PostureEventArgs e)
        {
            RaiseThreePostureDetectionLost(e);
        }


        private void OnFourFingersPostureSucceed(object sender, PostureEventArgs e)
        {
            RaiseFourPostureSucceed(e);
        }
        private void OnFourFingersPostureDetectionInProgress(object sender, PostureEventArgs e)
        {
            RaiseFourPostureDetectionInProgress(e);
        }
        private void OnFourFingersPostureDetectionLost(object sender, PostureEventArgs e)
        {
            RaiseFourPostureDetectionLost(e);
        }


        private void OnFiveFingersPostureSucceed(object sender, PostureEventArgs e)
        {
            RaiseFivePostureSucceed(e);
        }
        private void OnFiveFingersPostureDetectionInProgress(object sender, PostureEventArgs e)
        {
            RaiseFivePostureDetectionInProgress(e);
        }
        private void OnFiveFingersPostureDetectionLost(object sender, PostureEventArgs e)
        {
            RaiseFivePostureDetectionLost(e);
        }

        #endregion

        #region Constructor

        public PostureFingerCountFacade()
        {
            // if the LeapPlugin is not already instantiated, we do it
            if (LeapPlugin.Instance == null)
            {
                LeapPlugin temp = new LeapPlugin();
            }

            this.m_enableZeroPosture = false;
            this.m_enableOnePosture = false;
            this.m_enableTwoPosture = false;
            this.m_enableThreePosture = false;
            this.m_enableFourPosture = false;
            this.m_enableFivePosture = false;

            EnableZeroPosture = true;
            EnableOnePosture = true;
            EnableTwoPosture = true;
            EnableThreePosture = true;
            EnableFourPosture = true;
            EnableFivePosture = true;
            
            Main.RegisterFacade(this);
        }

        #endregion
        
        /// <summary>
        /// Clear the resources
        /// </summary>
        public void Dispose()
        {
           EnableZeroPosture = false;
           EnableOnePosture = false;
           EnableTwoPosture = false;
           EnableThreePosture = false;
           EnableFourPosture = false;
           EnableFivePosture = false;
        }
    }
}
