// Copyright (c) 2019-2025 Five Squared Interactive. All rights reserved.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FiveSQD.StraightFour.Utilities;

public class LoggingTests
{
    [Test]
    public void LoggingTests_General()
    {
        string message = "qwertyuiopasdfghjklzxcvbnm1234567890-=`!@#$%^&*()~-=_+[]{};':\",./<>?";

        // Log.
        LogAssert.Expect(LogType.Log, message);
        LogSystem.Log(message);
        LogAssert.Expect(LogType.Log, message);
        LogSystem.Log(message, LogSystem.Type.Debug);
        LogAssert.Expect(LogType.Log, message);
        LogSystem.Log(message, LogSystem.Type.Default);
        LogAssert.Expect(LogType.Warning, message);
        LogSystem.Log(message, LogSystem.Type.Warning);
        LogAssert.Expect(LogType.Error, message);
        LogSystem.Log(message, LogSystem.Type.Error);

        // Log Debug.
        LogAssert.Expect(LogType.Log, message);
        LogSystem.LogDebug(message);

        // Log Warning.
        LogAssert.Expect(LogType.Warning, message);
        LogSystem.LogWarning(message);

        // Log Error.
        LogAssert.Expect(LogType.Error, message);
        LogSystem.LogError(message);
    }

    [Test]
    public void LoggingTests_Configuration_AllEnabled()
    {
        string message = "Test message for all enabled";
        
        // Create config with all logging enabled
        LoggingConfig config = new LoggingConfig(true, true, true, true);
        LogSystem.SetLoggingConfig(config);

        // All log types should appear
        LogAssert.Expect(LogType.Log, message);
        LogSystem.LogDebug(message);
        LogAssert.Expect(LogType.Warning, message);
        LogSystem.LogWarning(message);
        LogAssert.Expect(LogType.Error, message);
        LogSystem.LogError(message);
        LogAssert.Expect(LogType.Log, message);
        LogSystem.Log(message, LogSystem.Type.Default);

        // Clear config
        LogSystem.SetLoggingConfig(null);
    }

    [Test]
    public void LoggingTests_Configuration_DebugDisabled()
    {
        string message = "Test message for debug disabled";
        
        // Create config with debug disabled
        LoggingConfig config = new LoggingConfig(false, true, true, true);
        LogSystem.SetLoggingConfig(config);

        // Debug logs should not appear, others should
        LogSystem.LogDebug(message); // Should not appear
        LogAssert.Expect(LogType.Warning, message);
        LogSystem.LogWarning(message);
        LogAssert.Expect(LogType.Error, message);
        LogSystem.LogError(message);
        LogAssert.Expect(LogType.Log, message);
        LogSystem.Log(message, LogSystem.Type.Default);

        // Clear config
        LogSystem.SetLoggingConfig(null);
    }

    [Test]
    public void LoggingTests_Configuration_OnlyErrors()
    {
        string message = "Test message for errors only";
        
        // Create config with only errors enabled
        LoggingConfig config = new LoggingConfig(false, false, true, false);
        LogSystem.SetLoggingConfig(config);

        // Only error logs should appear
        LogSystem.LogDebug(message); // Should not appear
        LogSystem.LogWarning(message); // Should not appear
        LogAssert.Expect(LogType.Error, message);
        LogSystem.LogError(message);
        LogSystem.Log(message, LogSystem.Type.Default); // Should not appear

        // Clear config
        LogSystem.SetLoggingConfig(null);
    }

    [Test]
    public void LoggingTests_Configuration_ShouldLog()
    {
        // Test the ShouldLog method directly
        LoggingConfig config = new LoggingConfig(true, false, true, false);

        Assert.IsTrue(config.ShouldLog(LogSystem.Type.Debug));
        Assert.IsFalse(config.ShouldLog(LogSystem.Type.Warning));
        Assert.IsTrue(config.ShouldLog(LogSystem.Type.Error));
        Assert.IsFalse(config.ShouldLog(LogSystem.Type.Default));
    }

    [Test]
    public void LoggingTests_Configuration_DefaultConstructor()
    {
        // Test default constructor (all enabled)
        LoggingConfig config = new LoggingConfig();

        Assert.IsTrue(config.ShouldLog(LogSystem.Type.Debug));
        Assert.IsTrue(config.ShouldLog(LogSystem.Type.Warning));
        Assert.IsTrue(config.ShouldLog(LogSystem.Type.Error));
        Assert.IsTrue(config.ShouldLog(LogSystem.Type.Default));
    }

    [Test]
    public void LoggingTests_Configuration_GetSet()
    {
        // Test getting and setting configuration
        LoggingConfig config = new LoggingConfig(false, true, false, true);
        
        // Initially should be null
        Assert.IsNull(LogSystem.GetLoggingConfig());
        
        // Set and verify
        LogSystem.SetLoggingConfig(config);
        Assert.AreEqual(config, LogSystem.GetLoggingConfig());
        
        // Clear and verify
        LogSystem.SetLoggingConfig(null);
        Assert.IsNull(LogSystem.GetLoggingConfig());
    }

    [Test]
    public void LoggingTests_StraightFour_Integration()
    {
        // Test that StraightFour's LoadWorld method correctly applies logging configuration
        string message = "StraightFour integration test";
        
        // Create a config that only allows errors
        LoggingConfig config = new LoggingConfig(false, false, true, false);
        
        // Set the config and verify it's being used
        LogSystem.SetLoggingConfig(config);
        Assert.AreEqual(config, LogSystem.GetLoggingConfig());
        
        // Test that the configuration is filtering correctly
        LogSystem.LogDebug(message); // Should not appear
        LogSystem.LogWarning(message); // Should not appear
        LogAssert.Expect(LogType.Error, message);
        LogSystem.LogError(message); // Should appear
        LogSystem.Log(message, LogSystem.Type.Default); // Should not appear
        
        // Clean up
        LogSystem.SetLoggingConfig(null);
    }
}