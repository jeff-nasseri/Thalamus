Feature: Slave Agent Deployment to Network Devices
  As a master node
  I want to deploy slave agents to slave devices in a network
  So that I can coordinate distributed operations

  Background:
    Given the master node is running on a dedicated server
    And the following node configuration exists in the master:
      """
      {
        "slave_nodes": [
          {
            "device_name": "Jeff House Server",
            "category": "Physical Server",
            "details": {
              "region": "nl-NL",
              "location": "Netherlands-Denbosch",
              "timezone": "Europe/Amsterdam"
            },
            "vms": [
              {
                "vm_name": "Windows Production VM",
                "os": "Windows Server 2022",
                "network": {
                  "ipv4": "192.168.1.100",
                  "ipv6": "fe80::1",
                  "subnet": "255.255.255.0",
                  "gateway": "192.168.1.1",
                  "dns": ["8.8.8.8", "1.1.1.1"]
                },
                "resources": {
                  "cpu_cores": 8,
                  "ram_gb": 32,
                  "storage_gb": 500,
                  "network_bandwidth_mbps": 1000
                },
                "firewall": {
                  "enabled": true,
                  "allowed_ports": [22, 80, 443, 3389],
                  "allowed_ips": ["10.0.0.0/8", "172.16.0.0/12"],
                  "mcp_validation": true
                }
              },
              {
                "vm_name": "Linux Development VM",
                "os": "Ubuntu 22.04 LTS",
                "network": {
                  "ipv4": "192.168.1.101",
                  "ipv6": "fe80::2",
                  "subnet": "255.255.255.0",
                  "gateway": "192.168.1.1",
                  "dns": ["8.8.8.8", "1.1.1.1"]
                },
                "resources": {
                  "cpu_cores": 4,
                  "ram_gb": 16,
                  "storage_gb": 250,
                  "network_bandwidth_mbps": 1000
                },
                "firewall": {
                  "enabled": true,
                  "allowed_ports": [22, 80, 443, 8080],
                  "allowed_ips": ["10.0.0.0/8"],
                  "mcp_validation": true
                }
              }
            ]
          },
          {
            "device_name": "Laboratory Microscope",
            "category": "Laboratory Device",
            "details": {
              "region": "nl-NL",
              "location": "Netherlands-Denbosch",
              "manufacturer": "Zeiss",
              "model": "Axio Observer",
              "description": "Smart microscope with embedded computing capability"
            },
            "embedded_system": {
              "os": "Embedded Linux (Yocto)",
              "architecture": "ARM64",
              "network": {
                "ipv4": "192.168.1.150",
                "subnet": "255.255.255.0",
                "gateway": "192.168.1.1",
                "connection_type": "Ethernet"
              },
              "resources": {
                "cpu_cores": 2,
                "ram_mb": 2048,
                "storage_gb": 32,
                "network_bandwidth_mbps": 100
              },
              "capabilities": [
                "Image capture and processing",
                "Real-time analysis",
                "Remote control via API",
                "Data storage and retrieval"
              ],
              "firewall": {
                "enabled": true,
                "allowed_ports": [22, 8080],
                "allowed_ips": ["192.168.1.0/24"],
                "mcp_validation": true
              }
            }
          }
        ]
      }
      """

  @deployment-validation
  Scenario: Master node validates slave node connectivity before deployment
    Given a slave node is configured in the master
    When the infrastructure agent attempts to deploy a slave agent
    Then the agent must verify network connectivity
    And the agent must verify SSH/FTP access
    And the agent must verify firewall rules allow communication
    And the agent must verify sufficient resources are available

  @deployment-success
  Scenario: Slave agent is successfully deployed to a slave node
    Given the "Jeff House Server" node is configured
    And the "Windows Production VM" is accessible
    And the infrastructure agent has valid credentials
    When the master agent requests slave agent deployment
    Then the infrastructure agent connects to the VM via SSH
    And installs required packages and dependencies
    And deploys the slave agent binary
    And configures the slave agent with master node connection details
    And starts the slave agent service
    And the monitor agent confirms the slave agent is healthy
    And the master agent receives deployment confirmation

  @deployment-failure
  Scenario: Slave agent deployment fails due to network issues
    Given a slave node is configured
    But the node is not accessible via network
    When the infrastructure agent attempts deployment
    Then the deployment fails with network error
    And the master agent is notified of the failure
    And the failure is logged for investigation
