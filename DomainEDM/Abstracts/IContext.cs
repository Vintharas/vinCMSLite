using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace DomainStorage.Abstracts
{
    public interface IContext : IDisposable
    {

        IObjectSet<Category> Categories { get; }
        IObjectSet<Comment> Comments { get; }
        IObjectSet<ContentContainer> ContentContainers { get; }
        IObjectSet<Position> Positions { get; }
        IObjectSet<RolePermission> RolePermissions { get; }
        IObjectSet<Tag> Tags { get; }
        IObjectSet<User> Users { get; }
        IObjectSet<UserRole> UserRoles { get; }
        IObjectSet<Media> Medias { get; }
        IObjectSet<MimeType> MimeTypes { get; }

        IQueryable<BlogPost> BlogPosts { get; }
        IQueryable<Page> Pages { get; }
        IQueryable<GenericContentBlock> GenericContentBlocks { get; }

        string SubmitChanges();
        IEnumerable<T> ManagedEntities<T>();
        bool ValidateBeforeSave(out string validationErrors);

    }
}
