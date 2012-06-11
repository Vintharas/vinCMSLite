
using System;
using System.Data;
using System.Data.Objects;
using System.Data.EntityClient;
using Domain.Entities;
using DomainStorage.Abstracts;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DomainStorage
{
    public partial class VinCMSEntities : ObjectContext, IContext
    {
        public const string ConnectionString = "name=VinCMSEntities";
        public const string ContainerName = "VinCMSEntities";

        #region Constructors

        public VinCMSEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        public VinCMSEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        public VinCMSEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        #endregion

        #region IObjectSet Properties

        public IObjectSet<Category> Categories
        {
            get { return _categories ?? (_categories = CreateObjectSet<Category>("Categories")); }
        }
        private IObjectSet<Category> _categories;

        public IObjectSet<Comment> Comments
        {
            get { return _comments ?? (_comments = CreateObjectSet<Comment>("Comments")); }
        }
        private IObjectSet<Comment> _comments;

        public IObjectSet<ContentContainer> ContentContainers
        {
            get { return _contentContainers ?? (_contentContainers = CreateObjectSet<ContentContainer>("ContentContainers")); }
        }
        private IObjectSet<ContentContainer> _contentContainers;

        public IObjectSet<Position> Positions
        {
            get { return _positions ?? (_positions = CreateObjectSet<Position>("Positions")); }
        }
        private IObjectSet<Position> _positions;

        public IObjectSet<RolePermission> RolePermissions
        {
            get { return _rolePermissions ?? (_rolePermissions = CreateObjectSet<RolePermission>("RolePermissions")); }
        }
        private IObjectSet<RolePermission> _rolePermissions;

        public IObjectSet<Tag> Tags
        {
            get { return _tags ?? (_tags = CreateObjectSet<Tag>("Tags")); }
        }
        private IObjectSet<Tag> _tags;

        public IObjectSet<User> Users
        {
            get { return _users ?? (_users = CreateObjectSet<User>("Users")); }
        }
        private IObjectSet<User> _users;

        public IObjectSet<UserRole> UserRoles
        {
            get { return _userRoles ?? (_userRoles = CreateObjectSet<UserRole>("UserRoles")); }
        }
        private IObjectSet<UserRole> _userRoles;

        public IObjectSet<Media> Medias
        {
            get { return _medias ?? (_medias = CreateObjectSet<Media>("Medias")); }
        }
        private IObjectSet<Media> _medias;

        public IObjectSet<MimeType> MimeTypes
        {
            get { return _mimeTypes ?? (_mimeTypes = CreateObjectSet<MimeType>("MimeTypes")); }
        }
        private IObjectSet<MimeType> _mimeTypes;

        public IQueryable<BlogPost> BlogPosts
        {
            get { return _blogPosts ?? (_blogPosts = CreateObjectSet<ContentContainer>("ContentContainers").OfType<BlogPost>()); }
        }
        private IQueryable<BlogPost> _blogPosts;

        public IQueryable<Page> Pages
        {
            get { return _pages ?? (_pages = CreateObjectSet<ContentContainer>("ContentContainers").OfType<Page>()); }
        }
        private IQueryable<Page> _pages;

        public IQueryable<GenericContentBlock> GenericContentBlocks
        {
            get
            {
                return _genericContentBlocks ??
                       (_genericContentBlocks =
                        CreateObjectSet<ContentContainer>("ContentContainers").OfType<GenericContentBlock>());
            }
        }
        private IQueryable<GenericContentBlock> _genericContentBlocks;

        #endregion

        /// <summary>
        /// Method that checks validation and submit changes to the database
        /// </summary>
        /// <returns></returns>
        public string SubmitChanges()
        {
            string validationErrors;
            if (ValidateBeforeSave(out validationErrors))
            {
                SaveChanges();
                return string.Empty;
            }
            return "Data not saved due to validation errors : " + validationErrors;
        }

        /// <summary>
        /// Method that returns a IEnumerable collection of entities of type T being managed
        /// by the context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public System.Collections.Generic.IEnumerable<T> ManagedEntities<T>()
        {
            var managedEntities = this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | 
                                                                                EntityState.Deleted |
                                                                                EntityState.Modified |
                                                                                EntityState.Unchanged);
            return managedEntities.Where(x => x.Entity is T).Select(x => (T) x.Entity);
        }

        /// <summary>
        /// Method that validates the entities that will be saved on the database
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <returns></returns>
        public bool ValidateBeforeSave(out string validationErrors)
        {
            validationErrors = string.Empty;
            return true;
        }

    }
}
