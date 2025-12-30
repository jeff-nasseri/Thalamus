Feature: End-to-End Application Deployment
  As a human user
  I want to request applications to be developed and deployed
  So that I can access them via the internet

  Background:
    Given the master node is running with the following agents:
      | Agent Name           | Status  |
      | Master Agent         | Running |
      | Maintenance Agent    | Running |
      | Infrastructure Agent | Running |
      | Monitor Agent        | Running |
    And the "Jeff House Server" is configured with available VMs
    And the domain "thalamus.network" is configured for deployments

  @application-development
  Scenario: Human requests an online calculator application
    Given the human is connected to the master agent
    When the human sends the following request:
      """
      I need an online calculator application. It should be fully written in HTML, JS, and CSS.
      When you are done, please give me a URL so I can visit my application.
      """
    Then the master agent responds with:
      """
      {
        "status": "in-progress",
        "progress": "0%",
        "message": "Processing your request. Analyzing requirements and creating tasks."
      }
      """
    And the master agent creates the following task breakdown:
      """
      {
        "tasks": [
          {
            "task_id": "DEV-001",
            "category": "Code Development",
            "assigned_to": "Maintenance Agent",
            "description": "Develop calculator application",
            "subtasks": [
              "Create HTML structure for calculator",
              "Implement calculator logic in JavaScript",
              "Style calculator with CSS",
              "Create Dockerfile for application",
              "Create docker-compose.yml for deployment"
            ],
            "estimated_duration": "30 minutes"
          },
          {
            "task_id": "INFRA-001",
            "category": "Infrastructure",
            "assigned_to": "Infrastructure Agent",
            "description": "Deploy application to server",
            "subtasks": [
              "Connect to Jeff House Server VM via SSH",
              "Install required packages (nginx, docker, docker-compose)",
              "Upload application code via FTP/SSH",
              "Deploy application using docker-compose",
              "Configure nginx reverse proxy",
              "Configure DNS for calculator.thalamus.network"
            ],
            "estimated_duration": "20 minutes"
          },
          {
            "task_id": "MON-001",
            "category": "Monitoring",
            "assigned_to": "Monitor Agent",
            "description": "Verify application health",
            "subtasks": [
              "Check application availability at calculator.thalamus.network",
              "Verify HTTP response codes",
              "Test calculator functionality",
              "Report status to master agent"
            ],
            "estimated_duration": "10 minutes"
          }
        ]
      }
      """

  @code-development-phase
  Scenario: Maintenance agent develops the calculator application
    Given the maintenance agent receives task "DEV-001"
    When the maintenance agent starts code development
    Then the agent creates an HTML file with calculator interface:
      """
      <!DOCTYPE html>
      <html lang="en">
      <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <title>Online Calculator</title>
          <link rel="stylesheet" href="styles.css">
      </head>
      <body>
          <div class="calculator">
              <input type="text" id="display" readonly>
              <!-- Calculator buttons -->
          </div>
          <script src="script.js"></script>
      </body>
      </html>
      """
    And creates a JavaScript file with calculator logic
    And creates a CSS file with calculator styling
    And creates a Dockerfile:
      """
      FROM nginx:alpine
      COPY . /usr/share/nginx/html
      EXPOSE 80
      CMD ["nginx", "-g", "daemon off;"]
      """
    And creates a docker-compose.yml:
      """
      version: '3.8'
      services:
        calculator:
          build: .
          ports:
            - "8080:80"
          restart: unless-stopped
      """
    And the agent updates task status:
      """
      {
        "task_id": "DEV-001",
        "status": "completed",
        "artifacts": [
          "index.html",
          "script.js",
          "styles.css",
          "Dockerfile",
          "docker-compose.yml"
        ]
      }
      """
    And the master agent updates progress to "33%"

  @infrastructure-deployment-phase
  Scenario: Infrastructure agent deploys the application
    Given the infrastructure agent receives task "INFRA-001"
    And the code development is completed
    When the infrastructure agent starts deployment
    Then the agent connects to the VM at "192.168.1.101" via SSH
    And executes the following commands:
      """
      # Install docker if not present
      sudo apt-get update
      sudo apt-get install -y docker.io docker-compose nginx

      # Create deployment directory
      sudo mkdir -p /opt/thalamus/calculator
      cd /opt/thalamus/calculator
      """
    And uploads application files to "/opt/thalamus/calculator"
    And executes deployment:
      """
      # Deploy with docker-compose
      sudo docker-compose up -d

      # Configure nginx reverse proxy
      sudo tee /etc/nginx/sites-available/calculator << EOF
      server {
          listen 80;
          server_name calculator.thalamus.network;
          
          location / {
              proxy_pass http://localhost:8080;
              proxy_set_header Host \$host;
              proxy_set_header X-Real-IP \$remote_addr;
          }
      }
      EOF

      sudo ln -s /etc/nginx/sites-available/calculator /etc/nginx/sites-enabled/
      sudo nginx -t
      sudo systemctl reload nginx
      """
    And configures DNS A record:
      """
      {
        "domain": "calculator.thalamus.network",
        "type": "A",
        "value": "<public_ip_of_jeff_house_server>",
        "ttl": 300
      }
      """
    And the agent updates task status:
      """
      {
        "task_id": "INFRA-001",
        "status": "completed",
        "deployment_url": "http://calculator.thalamus.network"
      }
      """
    And the master agent updates progress to "66%"

  @monitoring-phase
  Scenario: Monitor agent verifies application health
    Given the monitor agent receives task "MON-001"
    And the application is deployed
    When the monitor agent starts health checks
    Then the agent performs HTTP GET to "http://calculator.thalamus.network"
    And verifies HTTP status code is 200
    And verifies response contains calculator HTML
    And tests basic calculator operations
    And the agent reports to master agent:
      """
      {
        "task_id": "MON-001",
        "status": "completed",
        "health_check": {
          "url": "calculator.thalamus.network",
          "status": "ok",
          "response_time_ms": 45,
          "availability": "100%",
          "functional_tests": "passed"
        }
      }
      """
    And the master agent updates progress to "100%"

  @completion-response
  Scenario: Master agent responds to human with completed application
    Given all tasks are completed successfully
    And the monitor agent confirms application is healthy
    When the master agent compiles the final response
    Then the master agent responds to the human:
      """
      {
        "status": "completed",
        "progress": "100%",
        "application": {
          "name": "Online Calculator",
          "url": "http://calculator.thalamus.network",
          "status": "running",
          "health": "ok"
        },
        "message": "Your online calculator application is ready and accessible at calculator.thalamus.network"
      }
      """
    And the human can access the calculator at the provided URL

  @error-handling
  Scenario: Deployment fails and master agent reports error
    Given the maintenance agent completes code development
    But the infrastructure agent cannot connect to the VM
    When the infrastructure agent attempts deployment
    Then the deployment fails with connection error
    And the master agent is notified of the failure
    And the master agent responds to the human:
      """
      {
        "status": "failed",
        "progress": "33%",
        "error": {
          "phase": "infrastructure",
          "message": "Unable to connect to deployment server. Please check network connectivity.",
          "task_id": "INFRA-001"
        }
      }
      """
    And the human is informed of the issue
