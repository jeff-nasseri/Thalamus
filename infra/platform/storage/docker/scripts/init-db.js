// MongoDB Database Initialization Script for Thalamus
// This script runs automatically when the MongoDB container starts for the first time
// It creates the database and all collections based on the domain entities

print('=========================================');
print('Starting Thalamus Database Initialization');
print('=========================================');

// Switch to the Thalamus database
db = db.getSiblingDB(process.env.MONGO_INITDB_DATABASE || 'thalamus');

print('Database: ' + db.getName());

// Collection names based on domain entities
const collections = [
    'agents',
    'agentNodes',
    'agentStatuses',
    'mcpPlugins',
    'memories',
    'memoryThreads',
    'plans',
    'prompts',
    'users'
];

print('\n--- Creating Collections ---');

// Create collections
collections.forEach(function(collectionName) {
    try {
        db.createCollection(collectionName, {
            validator: {
                $jsonSchema: {
                    bsonType: "object",
                    required: ["_id", "CreatedDateTime"],
                    properties: {
                        _id: {
                            bsonType: "string",
                            description: "Must be a string (GUID) and is required"
                        },
                        CreatedDateTime: {
                            bsonType: "date",
                            description: "Must be a date and is required"
                        },
                        ModifiedDateTime: {
                            bsonType: ["date", "null"],
                            description: "Must be a date or null"
                        },
                        DeletedDateTime: {
                            bsonType: ["date", "null"],
                            description: "Must be a date or null for soft delete functionality"
                        }
                    }
                }
            },
            validationLevel: "moderate",
            validationAction: "warn"
        });
        print('✓ Created collection: ' + collectionName);
    } catch (e) {
        if (e.codeName === 'NamespaceExists') {
            print('⚠ Collection already exists: ' + collectionName);
        } else {
            print('✗ Error creating collection ' + collectionName + ': ' + e.message);
        }
    }
});

// Create basic indexes on all collections for common fields
print('\n--- Creating Basic Indexes on All Collections ---');

collections.forEach(function(collectionName) {
    try {
        // Index on CreatedDateTime for sorting and filtering
        db.getCollection(collectionName).createIndex(
            { "CreatedDateTime": -1 },
            { name: "idx_createdDateTime" }
        );

        // Index on ModifiedDateTime for change tracking
        db.getCollection(collectionName).createIndex(
            { "ModifiedDateTime": -1 },
            { name: "idx_modifiedDateTime", sparse: true }
        );

        // Index on DeletedDateTime for soft delete queries (exclude deleted items)
        db.getCollection(collectionName).createIndex(
            { "DeletedDateTime": 1 },
            { name: "idx_deletedDateTime", sparse: true }
        );

        print('✓ Created basic indexes for: ' + collectionName);
    } catch (e) {
        print('✗ Error creating indexes for ' + collectionName + ': ' + e.message);
    }
});

// Create specific indexes for entity relationships
print('\n--- Creating Relationship Indexes ---');

try {
    // Agent -> Memory relationship
    db.agents.createIndex(
        { "Memories._id": 1 },
        { name: "idx_agent_memories" }
    );
    print('✓ Created index: agents.Memories._id');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Agent -> McpPlugin relationship
    db.agents.createIndex(
        { "Plugins._id": 1 },
        { name: "idx_agent_plugins", sparse: true }
    );
    print('✓ Created index: agents.Plugins._id');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Agent -> AgentStatus
    db.agents.createIndex(
        { "Status": 1 },
        { name: "idx_agent_status" }
    );
    print('✓ Created index: agents.Status');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // AgentNode -> Agent relationship
    db.agentNodes.createIndex(
        { "Agents._id": 1 },
        { name: "idx_agentNode_agents", sparse: true }
    );
    print('✓ Created index: agentNodes.Agents._id');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Memory -> MemoryThread relationship
    db.memories.createIndex(
        { "Threads._id": 1 },
        { name: "idx_memory_threads" }
    );
    print('✓ Created index: memories.Threads._id');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // MemoryThread -> Prompt relationship
    db.memoryThreads.createIndex(
        { "Prompts._id": 1 },
        { name: "idx_thread_prompts" }
    );
    print('✓ Created index: memoryThreads.Prompts._id');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Prompt -> ParentPrompt relationship (hierarchical prompts)
    db.prompts.createIndex(
        { "ParentPromptId": 1 },
        { name: "idx_prompt_parent", sparse: true }
    );
    print('✓ Created index: prompts.ParentPromptId');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Prompt -> Plan relationship
    db.prompts.createIndex(
        { "Plan._id": 1 },
        { name: "idx_prompt_plan", sparse: true }
    );
    print('✓ Created index: prompts.Plan._id');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Plan status for filtering by lifecycle
    db.plans.createIndex(
        { "Status": 1 },
        { name: "idx_plan_status" }
    );
    print('✓ Created index: plans.Status');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // User email for lookup
    db.users.createIndex(
        { "EmailAddress.Address": 1 },
        { name: "idx_user_email", unique: true, sparse: true }
    );
    print('✓ Created index: users.EmailAddress (unique)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // User username for lookup
    db.users.createIndex(
        { "Username": 1 },
        { name: "idx_user_username", sparse: true }
    );
    print('✓ Created index: users.Username');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // McpPlugin code for lookup (unique identifier)
    db.mcpPlugins.createIndex(
        { "Code": 1 },
        { name: "idx_mcpPlugin_code", unique: true }
    );
    print('✓ Created index: mcpPlugins.Code (unique)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // AgentNode registered name for lookup
    db.agentNodes.createIndex(
        { "RegisteredName": 1 },
        { name: "idx_agentNode_name", unique: true }
    );
    print('✓ Created index: agentNodes.RegisteredName (unique)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Agent name for lookup
    db.agents.createIndex(
        { "Name": 1 },
        { name: "idx_agent_name" }
    );
    print('✓ Created index: agents.Name');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // MemoryThread title for search
    db.memoryThreads.createIndex(
        { "Title": "text" },
        { name: "idx_thread_title_text" }
    );
    print('✓ Created text index: memoryThreads.Title');
} catch (e) {
    print('✗ Error: ' + e.message);
}

print('\n=========================================');
print('Database Initialization Completed');
print('=========================================\n');
