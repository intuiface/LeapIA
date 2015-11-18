using Leap;
using System.IO;

namespace IntuiLab.Leap.DataRecording
{
    /// <summary>
    /// Permits to record frame data in a CSV file.
    /// </summary>
    internal class CSVWriter
    {
        /// <summary>
        /// Streamwriter for hand data
        /// </summary>
        private StreamWriter m_HandSw;
        /// <summary>
        /// Streamwriter for finger data
        /// </summary>
        private StreamWriter m_FingerSw;

        /// <summary>
        /// The path for the files storing hands
        /// </summary>
        private string m_HandPath;
        /// <summary>
        /// The path for the files storing fingers
        /// </summary>
        private string m_FingerPath;

        public CSVWriter(string fileName)
        {
            // creation of the storage directories if they do not exist
            DirectoryInfo CSVdataDirectoryInfo = new DirectoryInfo(ParametersOther.Instance.CSVWriter_path);
            DirectoryInfo fingerDirectoryInfo = new DirectoryInfo(ParametersOther.Instance.CSVWriter_path + @"\fingers");
            DirectoryInfo handDirectoryInfo = new DirectoryInfo(ParametersOther.Instance.CSVWriter_path + @"\hands");
            if (!CSVdataDirectoryInfo.Exists)
            {
                CSVdataDirectoryInfo.Create();
                fingerDirectoryInfo.Create();
                handDirectoryInfo.Create();
            }

            // calculation of the paths
            m_HandPath = ParametersOther.Instance.CSVWriter_path + @"\hands\" + fileName + "_h.csv";
            m_FingerPath = ParametersOther.Instance.CSVWriter_path + @"\fingers\" + fileName + "_f.csv";

            // initialisation of the streamwriters
            m_HandSw = new StreamWriter(m_HandPath, true);
            m_FingerSw = new StreamWriter(m_FingerPath, true);

            // writing of the first line (titles of the columns)
            FileInfo handFile = new FileInfo(m_HandPath);

            if (handFile.Length == 0)
            {
                m_HandSw.WriteLine("Frame id;Hand id;Nb of fingers;Palm position;Palm normal;Palm velocity;Sphere center;Sphere radius;Timestamp");
            }

            FileInfo fingerFile = new FileInfo(m_FingerPath);

            if (fingerFile.Length == 0)
            {
                m_FingerSw.WriteLine("Frame id;Finger id;Associated hand id;Is frontmost;Direction;Tip position;Tip velocity;Length;Width;Timestamp");
            }
        }

        /// <summary>
        /// Records hand data in the file named "hands.csv".
        /// </summary>
        public void RecordHand(long frameId, int handId, int nbOfFingers, Vector palmPosition, Vector palmNormal, Vector palmVelocity, Vector sphereCenter, float sphereRadius, long timestamp)
        {
            string record = frameId + ";" + handId + ";" + nbOfFingers + ";" + palmPosition + ";" + palmNormal + ";" + palmVelocity + ";" + sphereCenter + ";" + sphereRadius + ";" + timestamp;
            m_HandSw.WriteLine(record);
        }

        /// <summary>
        /// Records finger data in the file named "fingers.csv".
        /// </summary>
        public void RecordFinger(long frameId, int fingerId, int associatedHandId, bool isFrontmost, Vector direction, Vector tipPosition, Vector tipVelocity, float length, float width, long timestamp)
        {
            string record = frameId + ";" + fingerId + ";" + associatedHandId + ";" + isFrontmost + ";" + direction + ";" + tipPosition + ";" + tipVelocity + ";" + length + ";" + width + ";" + timestamp;
            m_FingerSw.WriteLine(record);
        }

        public void Close()
        {
            m_HandSw.Close();
            m_FingerSw.Close();
        }
    }
}
