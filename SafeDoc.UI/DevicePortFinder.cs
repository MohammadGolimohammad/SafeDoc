using System;
using System.IO.Ports;
using System.Management;

namespace SafeDoc.UI
{
    internal static class DevicePortFinder
    {
        private const string ApprovedVendorId = "VID_CAFE";
        private const string ApprovedProductId = "PID_4014";

        internal static string FindApprovedDevicePort()
        {
            try
            {
                using (
                    ManagementObjectSearcher serialPortSearcher = new ManagementObjectSearcher(
                        "SELECT DeviceID, PNPDeviceID FROM Win32_SerialPort"
                    )
                )
                {
                    foreach (ManagementObject serialPort in serialPortSearcher.Get())
                    {
                        string deviceId = Convert.ToString(serialPort["DeviceID"]);
                        string pnpDeviceId = Convert.ToString(serialPort["PNPDeviceID"]);
                        if (IsApprovedDevice(pnpDeviceId) && IsAvailableSerialPort(deviceId))
                        {
                            return deviceId;
                        }
                    }
                }

                using (
                    ManagementObjectSearcher plugAndPlaySearcher = new ManagementObjectSearcher(
                        "SELECT Name, PNPDeviceID FROM Win32_PnPEntity"
                    )
                )
                {
                    foreach (ManagementObject device in plugAndPlaySearcher.Get())
                    {
                        string deviceName = Convert.ToString(device["Name"]);
                        string pnpDeviceId = Convert.ToString(device["PNPDeviceID"]);
                        if (IsApprovedDevice(pnpDeviceId) == false)
                        {
                            continue;
                        }

                        foreach (string availablePort in SerialPort.GetPortNames())
                        {
                            if (deviceName.IndexOf("(" + availablePort + ")", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                return availablePort;
                            }
                        }
                    }
                }
            }
            catch (ManagementException)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        private static bool IsApprovedDevice(string pnpDeviceId)
        {
            if (string.IsNullOrEmpty(pnpDeviceId))
            {
                return false;
            }

            return pnpDeviceId.IndexOf(ApprovedVendorId, StringComparison.OrdinalIgnoreCase) >= 0
                && pnpDeviceId.IndexOf(ApprovedProductId, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsAvailableSerialPort(string portName)
        {
            foreach (string availablePort in SerialPort.GetPortNames())
            {
                if (string.Equals(availablePort, portName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
