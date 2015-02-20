using System;

namespace TC.Tools.DbUtility.Commands
{
    public interface ICmd
    {
        
        /// <summary>
        ///   Holds exception details, if any
        /// </summary>
        Exception Exception { get; }

        
        /// <summary>
        ///   Runs command without exception handling
        /// </summary>
        void Run();

        /// <summary>
        ///   Runs command in a try-catch block
        /// </summary>
        /// <returns>false if an exception occurs</returns>
        bool TryRun();

        
    }
}