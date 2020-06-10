using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Lib.Camera.Core
{
    /// <summary>
    /// An interface responsible for acquisition device manipulations and configurations
    /// </summary>
    public interface IAcquirer
    {
        /// <summary>
        /// Opens and initializes acquisition device
        /// </summary>
        void Open();

        /// <summary>
        /// Sets acquisition device and program to be active and ready for acquiring 
        /// </summary>
        void Start();

        /// <summary>
        /// Commits an acquisition and returns out the result 
        /// </summary>
        /// <returns>Acquisition result</returns>
        DataItem Acquire();

        /// <summary>
        /// Sets acquisition device and program to be inactive and ready for closing 
        /// </summary>
        void Stop();

        /// <summary>
        /// Closes acquisition device and releases relative resources
        /// </summary>
        void Close();

        /// <summary>
        /// Starts a test to verify that acquisition device hardware is working properly and configurations are correct
        /// </summary>
        /// <param name="OutputData">Additional outputs as the product of testing</param>
        /// <returns>Outputs as the product of testing</returns>
        byte[] Test(out object[] OutputData);

        /// <summary>
        /// Retrieves and returns out parameters or configurations of acquisition device
        /// </summary>
        /// <returns>Parameters or configurations of acquisition device</returns>
        object[] Get();
    }
}
