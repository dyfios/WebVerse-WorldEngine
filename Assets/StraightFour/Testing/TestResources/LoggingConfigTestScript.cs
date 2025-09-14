// Copyright (c) 2019-2025 Five Squared Interactive. All rights reserved.

using UnityEngine;
using FiveSQD.StraightFour.Utilities;

namespace FiveSQD.StraightFour.Testing
{
    /// <summary>
    /// Simple test script to manually verify logging configuration functionality.
    /// Attach to a GameObject and run in play mode to see the logging behavior.
    /// </summary>
    public class LoggingConfigTestScript : MonoBehaviour
    {
        [Header("Test Configuration")]
        public bool enableDebug = false;
        public bool enableWarning = true;
        public bool enableError = true;
        public bool enableDefault = false;

        void Start()
        {
            // Test default logging (all enabled)
            Debug.Log("=== Testing Default Logging (All Enabled) ===");
            LogSystem.SetLoggingConfig(null);
            TestLogging("Default config");

            // Test custom configuration
            Debug.Log("=== Testing Custom Configuration ===");
            LoggingConfig customConfig = new LoggingConfig(enableDebug, enableWarning, enableError, enableDefault);
            LogSystem.SetLoggingConfig(customConfig);
            TestLogging("Custom config");

            // Test production-like configuration (only warnings and errors)
            Debug.Log("=== Testing Production Configuration (Warnings/Errors Only) ===");
            LoggingConfig prodConfig = new LoggingConfig(false, true, true, false);
            LogSystem.SetLoggingConfig(prodConfig);
            TestLogging("Production config");

            // Test development configuration (all enabled)
            Debug.Log("=== Testing Development Configuration (All Enabled) ===");
            LoggingConfig devConfig = new LoggingConfig(true, true, true, true);
            LogSystem.SetLoggingConfig(devConfig);
            TestLogging("Development config");

            // Reset to default
            LogSystem.SetLoggingConfig(null);
        }

        private void TestLogging(string testName)
        {
            string prefix = $"[{testName}]";
            LogSystem.LogDebug($"{prefix} This is a DEBUG message");
            LogSystem.LogWarning($"{prefix} This is a WARNING message");
            LogSystem.LogError($"{prefix} This is an ERROR message");
            LogSystem.Log($"{prefix} This is a DEFAULT/INFO message");
        }
    }
}