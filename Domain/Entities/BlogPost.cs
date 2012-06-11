//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Domain.Entities
{
    public partial class BlogPost : ContentContainer
    {
        #region Primitive Properties
    
        public System.DateTime CreationDate
        {
            get;
            set;
        }
    
        public System.DateTime PublishingDate
        {
            get;
            set;
        }
    
        public bool IsDraft
        {
            get;
            set;
        }
    
        public int AuthorID
        {
            get { return _authorID; }
            set
            {
                if (_authorID != value)
                {
                    if (User != null && User.UserID != value)
                    {
                        User = null;
                    }
                    _authorID = value;
                }
            }
        }
        private int _authorID;
    
        public string Summary
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public User User
        {
            get { return _user; }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user;
                    _user = value;
                    FixupUser(previousValue);
                }
            }
        }
        private User _user;
    
        public FixupCollection<Comment> Comments
        {
            get
            {
                if (_comments == null)
                {
                    var newCollection = new FixupCollection<Comment>();
                    newCollection.CollectionChanged += FixupComments;
                    _comments = newCollection;
                }
                return _comments;
            }
            set
            {
                if (!ReferenceEquals(_comments, value))
                {
                    var previousValue = _comments as FixupCollection<Comment>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupComments;
                    }
                    _comments = value;
                    var newValue = value as FixupCollection<Comment>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupComments;
                    }
                }
            }
        }
        private FixupCollection<Comment> _comments;

        #endregion
        #region Association Fixup
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.BlogPosts.Contains(this))
            {
                previousValue.BlogPosts.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.BlogPosts.Contains(this))
                {
                    User.BlogPosts.Add(this);
                }
                if (AuthorID != User.UserID)
                {
                    AuthorID = User.UserID;
                }
            }
        }
    
        private void FixupComments(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Comment item in e.NewItems)
                {
                    item.BlogPost = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Comment item in e.OldItems)
                {
                    if (ReferenceEquals(item.BlogPost, this))
                    {
                        item.BlogPost = null;
                    }
                }
            }
        }

        #endregion
    }
}