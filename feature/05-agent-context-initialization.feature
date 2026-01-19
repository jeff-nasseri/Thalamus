Feature: after the agent configuration seeded (idempotent) in the storage, the platform need to initialize the agents context based on the existing configuration from the storage
  """
  this means we need a sort of the context which will get the existing configuration from the storage and initialize the agent based on the configuration
  like doing a foreach in each agent and setup the agent with required mcps and connect it to the agent network across the nodes
  this is only for the agent context + network initialization which should be aalso idempotent, the context and also agents are **STATE LESS**, this means no cache or data should be 
  stored in any agent context, the context is the platform for the agent communication and all of the data should store in the related storage and memory, so memory is actually statefull
  and based on the memory we should be able to trigger any agents and agent context with proper ocnfiguration and mcps
  """
  
  Background: 
    Given the plafrom already has the following agent configuration
    """
    claude please fill here
    """

    Scenario: the agent context will be creaetd and each agent are in the realted network
