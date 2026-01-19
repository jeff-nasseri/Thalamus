# Thalamus MongoDB Infrastructure

This directory contains the Docker-based MongoDB infrastructure for the Thalamus system.

## Overview

The setup includes:
- **MongoDB 8.0**: Linux-based database instance for persistent storage
- **Mongo Express**: Web-based database management dashboard
- **Auto-initialization**: Automatic database and collection setup on first run
- **Health checks**: MongoDB availability monitoring

## Quick Start

### 1. First-time Setup

Copy the environment template and configure your credentials:

```bash
cd infra/platform/storage/docker
cp .env.template .env
```

Edit the `.env` file with your desired credentials:

```bash
# MongoDB Configuration
MONGO_ROOT_USERNAME=your_username
MONGO_ROOT_PASSWORD=your_secure_password
MONGO_DATABASE=thalamus
MONGO_PORT=27017

# Mongo Express Configuration
MONGO_EXPRESS_USERNAME=your_username
MONGO_EXPRESS_PASSWORD=your_secure_password
MONGO_EXPRESS_PORT=8081
MONGO_EXPRESS_BASEURL=/
```

### 2. Start the Services

```bash
docker compose up -d
```

This command will:
1. Pull the MongoDB and Mongo Express images
2. Start the MongoDB container
3. Run initialization scripts to create database and collections
4. Create optimized indexes for high-volume collections
5. Start Mongo Express dashboard
6. Perform health checks to confirm MongoDB is ready

### 3. Access the Services

- **MongoDB**: `mongodb://localhost:27017`
- **Mongo Express Dashboard**: `http://localhost:8081`

### 4. Stop the Services

```bash
docker compose down
```

To also remove volumes (⚠️ this deletes all data):

```bash
docker compose down -v
```

## Architecture

### Collections

Based on the domain entities, the following collections are created:

| Collection | Description | Volume |
|------------|-------------|--------|
| `agents` | Agent entities with memories and plugins | Medium |
| `agentNodes` | Physical/virtual nodes hosting agents | Low |
| `agentStatuses` | Agent operational states | Low |
| `mcpPlugins` | Model Context Protocol plugins | Low |
| `memories` | Memory storage with keyword indexing | **High** |
| `memoryThreads` | Conversation threads | **High** |
| `plans` | Execution plans with tasks | **High** |
| `prompts` | Messages in conversations | **High** |
| `users` | User entities | Low |

### High-Volume Collections

The following collections are optimized with specialized indexes for query performance:

#### 1. **memories** Collection
- Keyword-based retrieval indexes
- Semantic similarity search (root/phoneme indexes)
- Full-text search on keywords
- Partial index for active recent memories

#### 2. **memoryThreads** Collection
- Title search index
- Recent active threads compound index
- Prompt count analysis

#### 3. **prompts** Collection
- Hierarchical navigation (parent-child relationships)
- Message full-text search
- Token count analysis
- Plan association indexes
- Root-level and active prompts partial indexes

#### 4. **plans** Collection
- Status and time-based filtering
- Task completion tracking
- Task order for sequential execution
- In-progress and completed plans partial indexes
- Full-text search on results and tasks

### Base Entity Schema

All collections inherit base entity fields:

```typescript
{
  _id: string,              // GUID
  CreatedDateTime: Date,    // Required
  ModifiedDateTime?: Date,  // Optional
  DeletedDateTime?: Date    // Soft delete support
}
```

### Indexes

#### Common Indexes (All Collections)
- `idx_createdDateTime`: For sorting by creation time
- `idx_modifiedDateTime`: For tracking changes
- `idx_deletedDateTime`: For soft delete queries

#### Relationship Indexes
- Agent ↔ Memory, Agent ↔ Plugin, Agent ↔ Status
- AgentNode ↔ Agent
- Memory ↔ MemoryThread ↔ Prompt ↔ Plan
- Prompt parent-child hierarchy

#### Unique Indexes
- `users.EmailAddress`: Unique email addresses
- `mcpPlugins.Code`: Unique plugin codes
- `agentNodes.RegisteredName`: Unique node names

## Initialization Scripts

### 1. `init-db.js`
- Creates all collections based on domain entities
- Applies JSON schema validation (moderate level)
- Creates basic indexes on common fields
- Sets up relationship indexes

### 2. `create-indexes.js`
- Creates specialized indexes for high-volume collections
- Optimizes query performance with compound indexes
- Implements partial indexes for common query patterns
- Provides text search capabilities

## Health Checks

MongoDB health is monitored using:

```yaml
healthcheck:
  test: ["CMD", "mongosh", "--quiet", "--eval", "db.adminCommand('ping').ok"]
  interval: 10s
  timeout: 5s
  retries: 5
  start_period: 30s
```

Mongo Express waits for MongoDB to be healthy before starting.

## Data Persistence

Data is persisted in Docker volumes:

- `mongodb_data`: Database files
- `mongodb_config`: MongoDB configuration

## Connection Strings

### From Host Machine

```
mongodb://admin:password@localhost:27017/thalamus
```

### From Docker Network

```
mongodb://admin:password@mongodb:27017/thalamus
```

### .NET Connection String

```csharp
"mongodb://admin:password@localhost:27017/thalamus?authSource=admin"
```

## Development Tips

### Viewing Logs

```bash
# MongoDB logs
docker compose logs -f mongodb

# Mongo Express logs
docker compose logs -f mongo-express
```

### Accessing MongoDB Shell

```bash
docker compose exec mongodb mongosh -u admin -p
```

### Rebuilding from Scratch

```bash
# Stop and remove everything including volumes
docker compose down -v

# Start fresh
docker compose up -d
```

### Backup Database

```bash
docker compose exec mongodb mongodump -u admin -p password --authenticationDatabase admin --db thalamus --out /data/backup
```

### Restore Database

```bash
docker compose exec mongodb mongorestore -u admin -p password --authenticationDatabase admin --db thalamus /data/backup/thalamus
```

## Troubleshooting

### MongoDB won't start

Check logs for errors:
```bash
docker compose logs mongodb
```

Common issues:
- Port 27017 already in use
- Insufficient disk space
- Permission issues with volumes

### Mongo Express can't connect

Ensure MongoDB is healthy:
```bash
docker compose ps
```

Verify credentials in `.env` match between services.

### Initialization scripts didn't run

Scripts only run on first container start. To re-run:
```bash
docker compose down -v
docker compose up -d
```

## Security Notes

- The `.env` file is gitignored to prevent credential leaks
- Use `.env.template` as a reference for required variables
- Change default passwords in production environments
- Consider using Docker secrets for production deployments
- Mongo Express should not be exposed in production

## Next Steps

After setting up MongoDB, you'll need to:

1. Install MongoDB driver in the Infrastructure project:
   ```bash
   dotnet add package MongoDB.Driver
   ```

2. Create repository implementations in `src/data-access/Infrastructure/Storage/`

3. Configure MongoDB connection in your application startup

4. Implement data access patterns for your domain entities
