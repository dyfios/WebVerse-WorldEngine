// Copyright (c) 2019-2025 Five Squared Interactive. All rights reserved.

using UnityEngine;
using FiveSQD.StraightFour.Utilities;

namespace FiveSQD.StraightFour.Examples
{
    /// <summary>
    /// Example script demonstrating how to use the new logging configuration feature
    /// with the StraightFour World Engine.
    /// </summary>
    public class LoggingConfigurationExample : MonoBehaviour
    {
        [Header("Example Logging Configurations")]
        [Tooltip("Enable debug logging for development builds")]
        public bool developmentMode = true;

        void Start()
        {
            // Example 1: Configure logging based on development/production mode
            ConfigureLoggingForEnvironment();

            // Example 2: Load a world with specific logging configuration
            LoadWorldWithCustomLogging();

            // Example 3: Change logging configuration at runtime
            ChangeLoggingAtRuntime();
        }

        /// <summary>
        /// Configure logging based on whether we're in development or production mode.
        /// </summary>
        void ConfigureLoggingForEnvironment()
        {
            LoggingConfig config;

            if (developmentMode || Debug.isDebugBuild)
            {
                // Development: Enable all logging for full debugging
                config = new LoggingConfig(
                    enableDebug: true,
                    enableWarning: true, 
                    enableError: true,
                    enableDefault: true
                );
                Debug.Log("[Example] Configured logging for DEVELOPMENT mode (all levels enabled)");
            }
            else
            {
                // Production: Only warnings and errors to reduce noise
                config = new LoggingConfig(
                    enableDebug: false,
                    enableWarning: true,
                    enableError: true, 
                    enableDefault: false
                );
                Debug.Log("[Example] Configured logging for PRODUCTION mode (warnings/errors only)");
            }

            // Apply the configuration globally
            LogSystem.SetLoggingConfig(config);
        }

        /// <summary>
        /// Example of loading a world with a specific logging configuration.
        /// </summary>
        void LoadWorldWithCustomLogging()
        {
            // Create a logging config that only shows errors (useful for performance testing)
            var errorOnlyConfig = new LoggingConfig(
                enableDebug: false,
                enableWarning: false,
                enableError: true,
                enableDefault: false
            );

            // Note: This is just an example - in a real scenario you would have
            // proper world loading setup
            /*
            bool success = StraightFour.LoadWorld(
                worldName: "ExampleWorld",
                queryParams: null,
                loggingConfig: errorOnlyConfig
            );
            
            if (success)
            {
                LogSystem.LogError("[Example] World loaded with error-only logging");
            }
            */
            
            Debug.Log("[Example] Would load world with error-only logging configuration");
        }

        /// <summary>
        /// Example of changing logging configuration at runtime.
        /// </summary>
        void ChangeLoggingAtRuntime()
        {
            // Get current configuration
            LoggingConfig currentConfig = LogSystem.GetLoggingConfig();
            
            if (currentConfig != null)
            {
                Debug.Log($"[Example] Current config - Debug: {currentConfig.enableDebug}, " +
                         $"Warning: {currentConfig.enableWarning}, " +
                         $"Error: {currentConfig.enableError}, " +
                         $"Default: {currentConfig.enableDefault}");
            }
            else
            {
                Debug.Log("[Example] No logging configuration currently set (all logging enabled)");
            }

            // Example: Temporarily disable debug logging for performance-critical section
            var tempConfig = new LoggingConfig(false, true, true, true);
            LogSystem.SetLoggingConfig(tempConfig);
            
            // Simulate some performance-critical work
            LogSystem.LogDebug("[Example] This debug message should not appear");
            LogSystem.LogWarning("[Example] This warning should appear");
            
            // Restore previous configuration
            LogSystem.SetLoggingConfig(currentConfig);
            LogSystem.LogDebug("[Example] Debug logging restored");
        }

        /// <summary>
        /// Example method showing how to create preset configurations for common scenarios.
        /// </summary>
        public static class LoggingPresets
        {
            /// <summary>
            /// Development configuration - all logging enabled.
            /// </summary>
            public static LoggingConfig Development => new LoggingConfig(true, true, true, true);

            /// <summary>
            /// Production configuration - only warnings and errors.
            /// </summary>
            public static LoggingConfig Production => new LoggingConfig(false, true, true, false);

            /// <summary>
            /// Silent configuration - only errors.
            /// </summary>
            public static LoggingConfig Silent => new LoggingConfig(false, false, true, false);

            /// <summary>
            /// Debug configuration - only debug and error messages.
            /// </summary>
            public static LoggingConfig DebugOnly => new LoggingConfig(true, false, true, false);

            /// <summary>
            /// Quiet configuration - no debug or default messages.
            /// </summary>
            public static LoggingConfig Quiet => new LoggingConfig(false, true, true, false);
        }

        /// <summary>
        /// Example of using preset configurations.
        /// </summary>
        [ContextMenu("Apply Development Logging")]
        void ApplyDevelopmentLogging()
        {
            LogSystem.SetLoggingConfig(LoggingPresets.Development);
            Debug.Log("[Example] Applied development logging preset");
        }

        [ContextMenu("Apply Production Logging")]
        void ApplyProductionLogging()
        {
            LogSystem.SetLoggingConfig(LoggingPresets.Production);
            Debug.Log("[Example] Applied production logging preset");
        }

        [ContextMenu("Apply Silent Logging")]
        void ApplySilentLogging()
        {
            LogSystem.SetLoggingConfig(LoggingPresets.Silent);
            LogSystem.LogError("[Example] Applied silent logging preset (only this error should show)");
        }
    }
}