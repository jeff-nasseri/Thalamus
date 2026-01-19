// MongoDB Indexing Script for High-Volume Collections
// This script creates optimized indexes for collections expected to handle large datasets
// Target collections: memories, memoryThreads, prompts, plans

print('=========================================');
print('Creating Indexes for High-Volume Collections');
print('=========================================');

db = db.getSiblingDB(process.env.MONGO_INITDB_DATABASE || 'thalamus');

print('Database: ' + db.getName());
print('\n--- Optimizing High-Volume Collections ---\n');

// ============================================
// MEMORY COLLECTION INDEXES
// ============================================
print('--- Memory Collection ---');

try {
    // Compound index for keyword-based memory retrieval
    // This enables efficient keyword lookups for memory recall
    db.memories.createIndex(
        {
            "Keywords.Keyword": 1,
            "CreatedDateTime": -1
        },
        {
            name: "idx_memory_keywords_created",
            background: true
        }
    );
    print('✓ Created compound index: memories.Keywords.Keyword + CreatedDateTime');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index on keyword root for semantic similarity search
    db.memories.createIndex(
        { "Keywords.Root": 1 },
        {
            name: "idx_memory_keyword_root",
            background: true
        }
    );
    print('✓ Created index: memories.Keywords.Root');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Text index on all keyword fields for full-text search
    db.memories.createIndex(
        {
            "Keywords.Keyword": "text",
            "Keywords.Root": "text"
        },
        {
            name: "idx_memory_keywords_fulltext",
            weights: {
                "Keywords.Keyword": 10,
                "Keywords.Root": 5
            }
        }
    );
    print('✓ Created text index: memories.Keywords (full-text search)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index for retrieving recent memories (excludes soft-deleted)
    db.memories.createIndex(
        {
            "DeletedDateTime": 1,
            "CreatedDateTime": -1
        },
        {
            name: "idx_memory_active_recent",
            background: true,
            partialFilterExpression: { "DeletedDateTime": null }
        }
    );
    print('✓ Created partial index: memories (active + recent)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

// ============================================
// MEMORY THREAD COLLECTION INDEXES
// ============================================
print('\n--- Memory Thread Collection ---');

try {
    // Index on thread title for searching conversations
    db.memoryThreads.createIndex(
        { "Title": 1 },
        {
            name: "idx_thread_title",
            background: true
        }
    );
    print('✓ Created index: memoryThreads.Title');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Compound index for retrieving recent active threads
    db.memoryThreads.createIndex(
        {
            "DeletedDateTime": 1,
            "ModifiedDateTime": -1,
            "CreatedDateTime": -1
        },
        {
            name: "idx_thread_active_recent",
            background: true,
            partialFilterExpression: { "DeletedDateTime": null }
        }
    );
    print('✓ Created compound index: memoryThreads (active + recent)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index on prompt count for analyzing thread size
    db.memoryThreads.createIndex(
        { "Prompts": 1 },
        {
            name: "idx_thread_prompt_count",
            background: true,
            sparse: true
        }
    );
    print('✓ Created index: memoryThreads.Prompts (count)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

// ============================================
// PROMPT COLLECTION INDEXES
// ============================================
print('\n--- Prompt Collection ---');

try {
    // Compound index for hierarchical prompt navigation
    db.prompts.createIndex(
        {
            "ParentPromptId": 1,
            "CreatedDateTime": 1
        },
        {
            name: "idx_prompt_hierarchy",
            background: true,
            sparse: true
        }
    );
    print('✓ Created compound index: prompts.ParentPromptId + CreatedDateTime');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index on message content for text search
    db.prompts.createIndex(
        { "Message.Message": "text" },
        {
            name: "idx_prompt_message_text",
            background: true
        }
    );
    print('✓ Created text index: prompts.Message.Message');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index on token count for analyzing conversation length
    db.prompts.createIndex(
        { "Message.NumberOfTokens": -1 },
        {
            name: "idx_prompt_token_count",
            background: true
        }
    );
    print('✓ Created index: prompts.Message.NumberOfTokens');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Compound index for retrieving prompts with plans
    db.prompts.createIndex(
        {
            "Plan._id": 1,
            "CreatedDateTime": -1
        },
        {
            name: "idx_prompt_plan_created",
            background: true,
            sparse: true
        }
    );
    print('✓ Created compound index: prompts.Plan._id + CreatedDateTime');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index for root-level prompts (prompts without parents)
    db.prompts.createIndex(
        {
            "ParentPromptId": 1,
            "CreatedDateTime": -1
        },
        {
            name: "idx_prompt_root_level",
            background: true,
            partialFilterExpression: { "ParentPromptId": null }
        }
    );
    print('✓ Created partial index: prompts (root level)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index for recent active prompts
    db.prompts.createIndex(
        {
            "DeletedDateTime": 1,
            "CreatedDateTime": -1
        },
        {
            name: "idx_prompt_active_recent",
            background: true,
            partialFilterExpression: { "DeletedDateTime": null }
        }
    );
    print('✓ Created partial index: prompts (active + recent)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

// ============================================
// PLAN COLLECTION INDEXES
// ============================================
print('\n--- Plan Collection ---');

try {
    // Compound index for filtering plans by status and time
    db.plans.createIndex(
        {
            "Status": 1,
            "CreatedDateTime": -1
        },
        {
            name: "idx_plan_status_created",
            background: true
        }
    );
    print('✓ Created compound index: plans.Status + CreatedDateTime');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Compound index for filtering plans by status and modification time
    db.plans.createIndex(
        {
            "Status": 1,
            "ModifiedDateTime": -1
        },
        {
            name: "idx_plan_status_modified",
            background: true
        }
    );
    print('✓ Created compound index: plans.Status + ModifiedDateTime');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index on task completion for progress tracking
    db.plans.createIndex(
        {
            "Tasks.IsDone": 1,
            "Status": 1
        },
        {
            name: "idx_plan_task_completion",
            background: true
        }
    );
    print('✓ Created compound index: plans.Tasks.IsDone + Status');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index on task order for sequential execution
    db.plans.createIndex(
        {
            "Tasks.Order": 1,
            "Status": 1
        },
        {
            name: "idx_plan_task_order",
            background: true
        }
    );
    print('✓ Created compound index: plans.Tasks.Order + Status');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index for active in-progress plans
    db.plans.createIndex(
        {
            "Status": 1,
            "ModifiedDateTime": -1
        },
        {
            name: "idx_plan_in_progress",
            background: true,
            partialFilterExpression: {
                "Status": {
                    $in: ["IN_PROGRESS", "JUST_STARTED"]
                },
                "DeletedDateTime": null
            }
        }
    );
    print('✓ Created partial index: plans (in-progress)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Index for completed plans
    db.plans.createIndex(
        {
            "Status": 1,
            "CreatedDateTime": -1
        },
        {
            name: "idx_plan_completed",
            background: true,
            partialFilterExpression: {
                "Status": "COMPLETED",
                "DeletedDateTime": null
            }
        }
    );
    print('✓ Created partial index: plans (completed)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Text index on plan result title for searching
    db.plans.createIndex(
        { "Result.Title": "text" },
        {
            name: "idx_plan_result_title_text",
            background: true
        }
    );
    print('✓ Created text index: plans.Result.Title');
} catch (e) {
    print('✗ Error: ' + e.message);
}

try {
    // Text index on task names and descriptions for searching
    db.plans.createIndex(
        {
            "Tasks.Name": "text",
            "Tasks.Description": "text"
        },
        {
            name: "idx_plan_task_text",
            weights: {
                "Tasks.Name": 10,
                "Tasks.Description": 5
            },
            background: true
        }
    );
    print('✓ Created text index: plans.Tasks (name + description)');
} catch (e) {
    print('✗ Error: ' + e.message);
}

// ============================================
// SUMMARY AND STATISTICS
// ============================================
print('\n=========================================');
print('Index Creation Summary');
print('=========================================');

function printCollectionStats(collectionName) {
    try {
        const stats = db.getCollection(collectionName).stats();
        const indexes = db.getCollection(collectionName).getIndexes();
        print('\n' + collectionName + ':');
        print('  - Document count: ' + stats.count);
        print('  - Index count: ' + indexes.length);
        print('  - Total index size: ' + (stats.totalIndexSize / 1024).toFixed(2) + ' KB');
    } catch (e) {
        print('✗ Error getting stats for ' + collectionName + ': ' + e.message);
    }
}

printCollectionStats('memories');
printCollectionStats('memoryThreads');
printCollectionStats('prompts');
printCollectionStats('plans');

print('\n=========================================');
print('High-Volume Collection Indexing Completed');
print('=========================================\n');
