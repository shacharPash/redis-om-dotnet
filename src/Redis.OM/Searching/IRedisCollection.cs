﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Redis.OM.Modeling;

namespace Redis.OM.Searching
{
    /// <summary>
    /// A collection of items in redis that you can use to look up items in redis, or perform queries on indexed documents.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public interface IRedisCollection<T> : IOrderedQueryable<T>, IAsyncEnumerable<T>
    {
        /// <summary>
        /// Gets a value indicating whether gets whether the collection is meant to save the state of the records enumerated into it.
        /// </summary>
        bool SaveState { get; }

        /// <summary>
        /// Gets the collection state manager.
        /// </summary>
        RedisCollectionStateManager StateManager { get; }

        /// <summary>
        /// Gets the size of chunks to use when paginating.
        /// </summary>
        int ChunkSize { get; }

        /// <summary>
        /// Saves the current state of the collection, overriding what was initially materialized.
        /// </summary>
        void Save();

        /// <summary>
        /// Saves the current state of the collection, overriding what was initially materialized.
        /// </summary>
        /// <returns>a value task.</returns>
        ValueTask SaveAsync();

        /// <summary>
        /// Inserts an item into redis.
        /// </summary>
        /// <param name="item">an item.</param>
        /// <returns>the key.</returns>
        string Insert(T item);

        /// <summary>
        /// Inserts an item into redis.
        /// </summary>
        /// <param name="item">an item.</param>
        /// <param name="timeSpan">The timespan of the document's (TTL).</param>
        /// <returns>the key.</returns>
        string Insert(T item, TimeSpan timeSpan);

        /// <summary>
        /// Inserts an item into redis.
        /// </summary>
        /// <param name="item">an item.</param>
        /// <returns>the key.</returns>
        Task<string> InsertAsync(T item);

        /// <summary>
        /// Inserts an item into redis.
        /// </summary>
        /// <param name="item">an item.</param>
        /// <param name="timeSpan">The timespan of the document's (TTL).</param>
        /// <returns>the key.</returns>
        Task<string> InsertAsync(T item, TimeSpan timeSpan);

        /// <summary>
        /// Inserts an item into redis.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="when">Condition to insert the document under.</param>
        /// <param name="timeSpan">The expiration time of the document (TTL).</param>
        /// <returns>the Id of the newly inserted item, or null if not inserted.</returns>
        Task<string?> InsertAsync(T item, WhenKey when, TimeSpan? timeSpan = null);

        /// <summary>
        /// Inserts an item into redis.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="when">Condition to insert the document under.</param>
        /// <param name="timeSpan">The expiration time of the document (TTL).</param>
        /// <returns>the Id of the newly inserted item, or null if not inserted.</returns>
        string? Insert(T item, WhenKey when, TimeSpan? timeSpan = null);

        /// <summary>
        /// Inserts list of items into redis.
        /// </summary>
        /// <param name="items">The items to insert.</param>
        /// <returns>The list of Keys.</returns>
        Task<List<string>> Insert(IEnumerable<T> items);

        /// <summary>
        /// Inserts list of items into redis.
        /// </summary>
        /// <param name="items">The items to insert.</param>
        /// <param name="timeSpan">The timespan of the document's (TTL).</param>
        /// /// <returns>The list of Keys.</returns>
        Task<List<string>> Insert(IEnumerable<T> items, TimeSpan timeSpan);

        /// <summary>
        /// finds an item by it's ID or keyname.
        /// </summary>
        /// <param name="id">the id to lookup.</param>
        /// <returns>the item if it's present.</returns>
        Task<T?> FindByIdAsync(string id);

        /// <summary>
        /// finds an item by it's ID or keyname.
        /// </summary>
        /// <param name="id">the id to lookup.</param>
        /// <returns>the item if it's present.</returns>
        T? FindById(string id);

        /// <summary>
        /// Checks to see if the collection contains any.
        /// </summary>
        /// <returns>Whether anything matching the expression was found.</returns>
        bool Any();

        /// <summary>
        /// Checks to see if anything matching the expression exists.
        /// </summary>
        /// <param name="expression">the expression to be matched.</param>
        /// <returns>Whether anything matching the expression was found.</returns>
        bool Any(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Updates the provided item in Redis. Document must have a property marked with the <see cref="RedisIdFieldAttribute"/>.
        /// </summary>
        /// <param name="item">The item to update.</param>
        void Update(T item);

        /// <summary>
        /// Updates the provided item in Redis. Document must have a property marked with the <see cref="RedisIdFieldAttribute"/>.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(T item);

        /// <summary>
        /// Updates the provided items in Redis. Document must have a property marked with the <see cref="RedisIdFieldAttribute"/>.
        /// </summary>
        /// <param name="items">The items to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        ValueTask UpdateAsync(IEnumerable<T> items);

        /// <summary>
        /// Deletes the item from Redis.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        void Delete(T item);

        /// <summary>
        /// Deletes the List of items from Redis.
        /// </summary>
        /// <param name="items">The items to be deleted.</param>
        void Delete(IEnumerable<T> items);

        /// <summary>
        /// Deletes the item from Redis.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(T item);

        /// <summary>
        /// Deletes the List of items from Redis.
        /// </summary>
        /// <param name="items">The items to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(IEnumerable<T> items);

        /// <summary>
        /// Async method for enumerating the collection to a list.
        /// </summary>
        /// <returns>The enumerated collection as a list.</returns>
        Task<IList<T>> ToListAsync();

        /// <summary>
        /// Retrieves the count of the collection async.
        /// </summary>
        /// <returns>The Collection's count.</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Retrieves the count of the collection async.
        /// </summary>
        /// <param name="expression">The predicate match.</param>
        /// <returns>The Collection's count.</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// returns if there's any items in the colleciton.
        /// </summary>
        /// <returns>True if there are items present.</returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// returns if there's any items in the colleciton.
        /// </summary>
        /// <returns>True if there are items present.</returns>
        /// <param name="expression">The predicate match.</param>
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns the first item asynchronously.
        /// </summary>
        /// <returns>First or default result.</returns>
        Task<T> FirstAsync();

        /// <summary>
        /// Returns the first item asynchronously.
        /// </summary>
        /// <param name="expression">The predicate match.</param>
        /// <returns>First or default result.</returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns the first or default asynchronously.
        /// </summary>
        /// <returns>First or default result.</returns>
        Task<T?> FirstOrDefaultAsync();

        /// <summary>
        /// Returns the first or default asynchronously.
        /// </summary>
        /// <param name="expression">The predicate match.</param>
        /// <returns>First or default result.</returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns a single record or throws a <see cref="InvalidOperationException"/> if the sequence is empty or contains more than 1 record.
        /// </summary>
        /// <returns>The single instance.</returns>
        Task<T> SingleAsync();

        /// <summary>
        /// Returns a single record or throws a <see cref="InvalidOperationException"/> if the sequence is empty or contains more than 1 record.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The single instance.</returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns a single record or the default if there are none, or more than 1.
        /// </summary>
        /// <returns>The single instance.</returns>
        Task<T?> SingleOrDefaultAsync();

        /// <summary>
        /// Returns a single record or the default if there are none, or more than 1.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The single instance.</returns>
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Retrieves the count of the collection async.
        /// </summary>
        /// <param name="expression">The predicate match.</param>
        /// <returns>The Collection's count.</returns>
        int Count(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns the first item asynchronously.
        /// </summary>
        /// <param name="expression">The predicate match.</param>
        /// <returns>First or default result.</returns>
        T First(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns the first or default asynchronously.
        /// </summary>
        /// <param name="expression">The predicate match.</param>
        /// <returns>First or default result.</returns>
        T? FirstOrDefault(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns a single record or throws a <see cref="InvalidOperationException"/> if the sequence is empty or contains more than 1 record.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The single instance.</returns>
        T Single(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns a single record or the default if there are none, or more than 1.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The single instance.</returns>
        T? SingleOrDefault(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Retrieves the objects from redis at the given IDs,
        /// if there is no such Object in Redis, null is returned in the KVP.
        /// </summary>
        /// <param name="ids">The Ids to look up.</param>
        /// <returns>A dictionary correlating the ids provided to the objects in Redis.</returns>
        Task<IDictionary<string, T?>> FindByIdsAsync(IEnumerable<string> ids);
    }
}
