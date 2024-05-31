# Service Monitor Application

## Overview

The Service Monitor Application is a Windows Forms application designed to monitor and manage Windows services. The application allows you to start and stop monitoring services, ensuring they are always running and restarting them if they stop unexpectedly.

## Features

- **Service Monitoring**: Automatically monitor and restart services if they stop.
- **Add/Remove Services**: Easily add or remove services to be monitored.
- **Persistent Configuration**: The list of services to monitor is saved and loaded automatically.
- **System Tray Integration**: The application minimizes to the system tray and can be closed from there.

## Requirements

- Windows OS
- .NET Framework 4.8 or later (if using .NET Framework)
- Administrator privileges to manage services

## Installation

1. Download the executable file from the releases section.
2. Run the executable. Ensure you run it as an administrator for proper functionality.

## Usage

1. **Start Monitoring**: Click the "Start Monitoring" button to begin monitoring the listed services.
2. **Add Service**: Click the "Add Service" button to open a dialog and select services to monitor.
3. **Remove Service**: Select a service from the list and click the "Remove Service" button to stop monitoring it.
4. **Stop Monitoring**: Click the "Stop Monitoring" button to stop all monitoring activities.

## How It Works

- The application uses a timer to periodically check the status of the listed services.
- If a service is found to be stopped, the application attempts to restart it.
- The list of services is saved to the Windows registry, ensuring persistence between application restarts.

## Running as Administrator

This application requires administrator privileges to manage services. Ensure you run the application as an administrator by right-clicking the executable and selecting "Run as administrator".

## Building from Source

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Build the project in Release mode.
4. Run the application.

## License

This project is licensed under the MIT License.

