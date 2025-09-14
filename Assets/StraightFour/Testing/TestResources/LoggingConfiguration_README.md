# Logging Configuration for StraightFour

This document describes the logging configuration feature added to the StraightFour World Engine, which allows users to control log output based on their needs or environment (development vs. production).

## Overview

The logging configuration system provides fine-grained control over which types of log messages are displayed. This is particularly useful for:

- **Development environments**: Enable all logging for debugging
- **Production environments**: Show only warnings and errors to reduce noise
- **Performance testing**: Disable verbose logging to avoid impacting performance
- **Specific debugging scenarios**: Enable only certain log types

## Core Components

### LoggingConfig Class

The `LoggingConfig` class (`FiveSQD.StraightFour.Utilities.LoggingConfig`) controls which log types are enabled:

```csharp
public class LoggingConfig
{
    public bool enableDebug = true;    // Debug messages
    public bool enableWarning = true;  // Warning messages  
    public bool enableError = true;    // Error messages
    public bool enableDefault = true;  // Default/Info messages
}
```

### LogSystem Enhancement

The existing `LogSystem` class has been enhanced with configuration support:

```csharp
// Set logging configuration
LogSystem.SetLoggingConfig(config);

// Get current configuration
LoggingConfig current = LogSystem.GetLoggingConfig();

// Clear configuration (enables all logging)
LogSystem.SetLoggingConfig(null);
```

### StraightFour Integration

The `StraightFour` class now supports logging configuration in two ways:

1. **Instance Default Configuration**: Set via the inspector or code
2. **Per-World Configuration**: Passed to `LoadWorld()` method

## Usage Examples

### Basic Configuration

```csharp
// Create a configuration that only shows warnings and errors
var config = new LoggingConfig(
    enableDebug: false,
    enableWarning: true, 
    enableError: true,
    enableDefault: false
);

// Apply globally
LogSystem.SetLoggingConfig(config);
```

### World Loading with Custom Configuration

```csharp
// Load world with specific logging configuration
var prodConfig = new LoggingConfig(false, true, true, false);
StraightFour.LoadWorld("MyWorld", null, prodConfig);

// Load world using instance default configuration
StraightFour.LoadWorld("MyWorld"); // Uses StraightFour.loggingConfig
```

### Environment-Based Configuration

```csharp
LoggingConfig config;

if (Debug.isDebugBuild)
{
    // Development: Enable all logging
    config = new LoggingConfig(true, true, true, true);
}
else  
{
    // Production: Only warnings and errors
    config = new LoggingConfig(false, true, true, false);
}

LogSystem.SetLoggingConfig(config);
```

### Runtime Configuration Changes

```csharp
// Save current config
var originalConfig = LogSystem.GetLoggingConfig();

// Temporarily disable debug logging for performance-critical section
var tempConfig = new LoggingConfig(false, true, true, true);
LogSystem.SetLoggingConfig(tempConfig);

// ... performance-critical code ...

// Restore original configuration
LogSystem.SetLoggingConfig(originalConfig);
```

## Common Configuration Patterns

### Development Configuration
```csharp
var devConfig = new LoggingConfig(true, true, true, true);
```
- **Use case**: Full debugging information
- **Performance**: May impact performance due to verbose logging
- **Recommended for**: Development, debugging, testing

### Production Configuration  
```csharp
var prodConfig = new LoggingConfig(false, true, true, false);
```
- **Use case**: Important messages only
- **Performance**: Minimal performance impact
- **Recommended for**: Production builds, live environments

### Silent Configuration
```csharp
var silentConfig = new LoggingConfig(false, false, true, false);
```
- **Use case**: Critical errors only
- **Performance**: Minimal performance impact
- **Recommended for**: Performance testing, embedded systems

### Debug-Only Configuration
```csharp
var debugConfig = new LoggingConfig(true, false, true, false);
```
- **Use case**: Debugging specific issues
- **Performance**: Moderate performance impact
- **Recommended for**: Troubleshooting specific problems

## Inspector Configuration

You can configure logging directly in the Unity Inspector:

1. Select your StraightFour GameObject
2. In the StraightFour component, find the "Logging Config" section
3. Configure the boolean flags for each log type
4. The configuration will be used as the default for world loading

## Backward Compatibility

The logging configuration system is fully backward compatible:

- **No configuration set**: All logging is enabled (existing behavior)
- **Existing code**: Continues to work without modification
- **New parameter**: The `loggingConfig` parameter in `LoadWorld()` is optional

## Performance Considerations

- **Filtering overhead**: Minimal - simple boolean checks
- **Log message creation**: Prevented entirely when log type is disabled
- **String formatting**: Avoided for disabled log types
- **Unity console**: Reduced message volume improves console performance

## Testing

The implementation includes comprehensive tests in `LoggingTests.cs`:

- Configuration creation and validation
- Log filtering behavior
- StraightFour integration
- Edge cases and error handling

To run tests manually, use the `LoggingConfigTestScript` component or the `LoggingConfigurationExample` script provided in the TestResources folder.

## Best Practices

1. **Use environment-based configuration**: Automatically configure based on build type
2. **Preserve important messages**: Always enable error logging in production
3. **Document custom configurations**: Clearly comment why specific configurations are used
4. **Test with different configurations**: Ensure your application works with various logging levels
5. **Consider performance**: Disable verbose logging in performance-critical scenarios
6. **Use presets**: Create standard configurations for common scenarios