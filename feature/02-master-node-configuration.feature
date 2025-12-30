Feature: Master Node Configuration
  As a platform administrator
  I want to configure the master node with required agents
  So that it can manage slave nodes and coordinate operations

  @master-agents
  Scenario: Master node has minimum required agents
    Given the master node is running
    Then the following agents must be present:
      """
      {
        "required_agents": [
          {
            "name": "Infrastructure Agent",
            "responsibilities": [
              "Deploy slave agents to slave nodes",
              "Access slave nodes via FTP, SSH protocols",
              "Manage node connectivity"
            ],
            "required_access": ["FTP", "SSH", "slave_node_credentials"]
          },
          {
            "name": "Maintenance Agent",
            "responsibilities": [
              "Create slave agents",
              "Prepare agents for deployment",
              "Test and fix agents",
              "Ensure agent quality"
            ],
            "required_access": ["agent_templates", "testing_environment"]
          },
          {
            "name": "Monitor Agent",
            "responsibilities": [
              "Monitor slave agent health checks",
              "Report status to master agent",
              "Alert on failures"
            ],
            "required_access": ["health_check_endpoints", "logging_system"]
          },
          {
            "name": "Master Agent",
            "responsibilities": [
              "Main core of Thalamus network",
              "Communicate with all master and slave agents",
              "End-to-end communication with humans",
              "Receive formatted reports from all agents",
              "Coordinate all operations"
            ],
            "required_access": ["all_agents", "human_interface", "configuration"]
          }
        ]
      }
      """

  @agent-configuration
  Scenario: Agent configuration includes responsibilities and permissions
    Given an agent is being configured
    Then the configuration must include:
      """
      {
        "agent_configuration": {
          "agent_name": "IO Operation Agent",
          "responsibilities": [
            {
              "responsibility_type": "IO",
              "description": [
                {
                  "title": "Write files",
                  "supported_extensions": ["cs", "js", "ts", "jsx", "tsx", "json", "xml"],
                  "required_permissions": ["write"]
                },
                {
                  "title": "Read files",
                  "supported_extensions": ["cs", "js", "ts", "jsx", "tsx", "json", "xml", "txt"],
                  "required_permissions": ["read"]
                },
                {
                  "title": "Delete files",
                  "supported_extensions": ["*"],
                  "required_permissions": ["delete", "write"]
                },
                {
                  "title": "Create directories",
                  "supported_extensions": ["*"],
                  "required_permissions": ["write"]
                }
              ]
            }
          ],
          "required_mcps": [
            {
              "title": "IO MCP",
              "mcp_code": "io-mcp-v1",
              "mcp_configurations": [
                {
                  "platform": "docker",
                  "configuration": {
                    "image": "thalamus/io-mcp:latest",
                    "volumes": ["/data:/app/data"],
                    "environment": {
                      "MAX_FILE_SIZE": "100MB",
                      "ALLOWED_PATHS": "/app/data"
                    }
                  }
                },
                {
                  "platform": "kubernetes",
                  "configuration": {
                    "deployment": "io-mcp-deployment",
                    "namespace": "thalamus-agents",
                    "replicas": 1
                  }
                }
              ]
            }
          ]
        }
      }
      """

  @cli-tool
  Scenario: Thalamus CLI is available for cross-platform administration
    Given the Thalamus CLI is installed
    When an administrator runs "thalamus --help"
    Then the CLI displays available commands
    And all rules and configurations can be managed via CLI
    And the CLI supports cross-platform operation (Windows, Linux, macOS)
