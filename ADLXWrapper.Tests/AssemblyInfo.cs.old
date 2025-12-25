using Xunit;

// ADLX hardware-backed calls are not thread-safe; run tests sequentially to avoid driver deadlocks.
[assembly: CollectionBehavior(DisableTestParallelization = true, MaxParallelThreads = 1)]
