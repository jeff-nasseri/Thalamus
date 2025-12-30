Feature: Platform Configuration and Rules
  As a platform administrator
  I want to define the core configuration and rules
  So that the Thalamus network operates securely and efficiently

  Background:
    Given the following platform rules are defined in the application

  @platform-rules
  Scenario: Platform rule hierarchy is established
    Given the following rule levels exist:
      | Rule Type      | Description                                                                                         | Firewall Access |
      | God Rule       | Direct access to all nodes and configuration using Thalamus. Can pass any firewall rule             | Bypass          |
      | Human Rule     | Communicate with master node only. Master node handles slave node deployment based on human request | Standard        |
      | Technician Rule| Direct communication with slave nodes via Thalamus CLI. Cannot bypass firewall                      | Standard        |

  @node-types
  Scenario: Node types are configured in the platform
    Given the following node types are available:
      """
      {
        "nodes": [
          {
            "type": "master",
            "description": "Responsible for deploying slave nodes to related destinations",
            "requirements": [
              "Valid network configuration observable by slave nodes",
              "Destination configuration for slave deployment",
              "Firewall settings for security filtering"
            ]
          },
          {
            "type": "slave",
            "description": "Hosts slave agents deployed by master node",
            "requirements": [
              "Valid network configuration with IPv4/IPv6",
              "Firewall settings for security filtering",
              "Accessible by master node"
            ]
          }
        ]
      }
      """

  @agent-types
  Scenario: Agent hierarchy is defined
    Given the following agent types exist:
      """
      {
        "agents": [
          {
            "type": "master_agent",
            "location": "master_node",
            "role": "Core coordinator",
            "responsibilities": [
              "Communicate with all agents",
              "Process human requests",
              "Coordinate slave agent operations"
            ]
          },
          {
            "type": "slave_agent",
            "location": "slave_node",
            "role": "Task executor",
            "team_structure": {
              "description": "Multiple agents can work together as a team",
              "lead_agent_required": "When team has 3 or more agents, one becomes slave lead agent"
            }
          }
        ]
      }
      """

  @firewall
  Scenario: Firewall configuration protects all nodes
    Given each node has firewall settings configured
    And all agents are powered with MCP
    When a request arrives at any agent
    Then the firewall must validate the request
    And check agent permissions from configuration
    And only pass validated requests to the agent for MCP execution
    
    And the firewall configuration includes:
      """
      {
        "firewall": {
          "permission_model": {
            "description": "Permissions are defined per server in configuration",
            "validation_flow": [
              "Request arrives at agent",
              "Firewall checks agent permissions from configuration",
              "If valid, pass to agent for MCP execution",
              "If invalid, reject request"
            ]
          }
        }
      }
      """
