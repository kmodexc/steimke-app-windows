// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace HeySteimke.Services.Rest.HeySteimkeUser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for HeySteimkeUserClient.
    /// </summary>
    public static partial class HeySteimkeUserClientExtensions
    {
            /// <summary>
            /// Returns ids of existing Users
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<int?> GetAvaiUserIds(this IHeySteimkeUserClient operations)
            {
                return Task.Factory.StartNew(s => ((IHeySteimkeUserClient)s).GetAvaiUserIdsAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Returns ids of existing Users
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<int?>> GetAvaiUserIdsAsync(this IHeySteimkeUserClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAvaiUserIdsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Returns Specific User by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of User to get
            /// </param>
            public static User GetUser(this IHeySteimkeUserClient operations, int id)
            {
                return Task.Factory.StartNew(s => ((IHeySteimkeUserClient)s).GetUserAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Returns Specific User by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of User to get
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<User> GetUserAsync(this IHeySteimkeUserClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetUserWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Replace existing User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of User to get
            /// </param>
            /// <param name='user'>
            /// User to replace
            /// </param>
            public static void ReplaceUser(this IHeySteimkeUserClient operations, int id, User user)
            {
                Task.Factory.StartNew(s => ((IHeySteimkeUserClient)s).ReplaceUserAsync(id, user), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Replace existing User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of User to get
            /// </param>
            /// <param name='user'>
            /// User to replace
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ReplaceUserAsync(this IHeySteimkeUserClient operations, int id, User user, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.ReplaceUserWithHttpMessagesAsync(id, user, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// Remove User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of User to delete
            /// </param>
            public static void DeleteUser(this IHeySteimkeUserClient operations, int id)
            {
                Task.Factory.StartNew(s => ((IHeySteimkeUserClient)s).DeleteUserAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Remove User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of User to delete
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteUserAsync(this IHeySteimkeUserClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DeleteUserWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// Add User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='user'>
            /// User to add
            /// </param>
            public static void AddUser(this IHeySteimkeUserClient operations, User user)
            {
                Task.Factory.StartNew(s => ((IHeySteimkeUserClient)s).AddUserAsync(user), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Add User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='user'>
            /// User to add
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task AddUserAsync(this IHeySteimkeUserClient operations, User user, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.AddUserWithHttpMessagesAsync(user, null, cancellationToken).ConfigureAwait(false);
            }

    }
}
