using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by their unique Id. Returns null if no user with that Id exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> user if found , null if not </returns>
        Task<User?> GetByIdAsync(Guid id);


        /// <summary>
        /// Retrieves a user by their email address. Returns null if no matching user exists.
        /// </summary>
        /// <param name="email"></param>
        /// <returns> user if found , null if not </returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Returns the full list of users in the system
        /// Used by the admin "view users list" endpoint — restricted to Admin role at the controller level
        /// </summary>
        /// <returns>list of all the users</returns>

        Task<List<User>> GetAllAsync();

        /// <summary>
        /// Adds a new user entity to the data store (registration or admin-created user).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddAsync(User user);

        /// <summary>
        /// Removes an existing user from the data store, identified by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Delete(User user);

        /// <summary>
        /// get user by the refresh token in case we need to make a new access token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<User?> GetByRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Persists any pending changes tracked by the DbContext to the database.
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();


    }
}
