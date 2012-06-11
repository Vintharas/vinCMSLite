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
    public abstract partial class ContentContainer : StateObject
    {
        #region Primitive Properties
    
        public int ContentID
        {
            get;
            set;
        }
    
        public string Title
        {
            get;
            set;
        }
    
        public string BodyContent
        {
            get;
            set;
        }
    
        public string MetaDescription
        {
            get;
            set;
        }
    
        public string MetaAuthor
        {
            get;
            set;
        }
    
        public string Path
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public FixupCollection<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    var newCollection = new FixupCollection<Category>();
                    newCollection.CollectionChanged += FixupCategories;
                    _categories = newCollection;
                }
                return _categories;
            }
            set
            {
                if (!ReferenceEquals(_categories, value))
                {
                    var previousValue = _categories as FixupCollection<Category>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupCategories;
                    }
                    _categories = value;
                    var newValue = value as FixupCollection<Category>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupCategories;
                    }
                }
            }
        }
        private FixupCollection<Category> _categories;
    
        public FixupCollection<Tag> Tags
        {
            get
            {
                if (_tags == null)
                {
                    var newCollection = new FixupCollection<Tag>();
                    newCollection.CollectionChanged += FixupTags;
                    _tags = newCollection;
                }
                return _tags;
            }
            set
            {
                if (!ReferenceEquals(_tags, value))
                {
                    var previousValue = _tags as FixupCollection<Tag>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTags;
                    }
                    _tags = value;
                    var newValue = value as FixupCollection<Tag>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTags;
                    }
                }
            }
        }
        private FixupCollection<Tag> _tags;
    
        public FixupCollection<Media> Media
        {
            get
            {
                if (_media == null)
                {
                    var newCollection = new FixupCollection<Media>();
                    newCollection.CollectionChanged += FixupMedia;
                    _media = newCollection;
                }
                return _media;
            }
            set
            {
                if (!ReferenceEquals(_media, value))
                {
                    var previousValue = _media as FixupCollection<Media>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMedia;
                    }
                    _media = value;
                    var newValue = value as FixupCollection<Media>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMedia;
                    }
                }
            }
        }
        private FixupCollection<Media> _media;

        #endregion
        #region Association Fixup
    
        private void FixupCategories(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Category item in e.NewItems)
                {
                    if (!item.ContentContainers.Contains(this))
                    {
                        item.ContentContainers.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Category item in e.OldItems)
                {
                    if (item.ContentContainers.Contains(this))
                    {
                        item.ContentContainers.Remove(this);
                    }
                }
            }
        }
    
        private void FixupTags(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Tag item in e.NewItems)
                {
                    if (!item.ContentContainers.Contains(this))
                    {
                        item.ContentContainers.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Tag item in e.OldItems)
                {
                    if (item.ContentContainers.Contains(this))
                    {
                        item.ContentContainers.Remove(this);
                    }
                }
            }
        }
    
        private void FixupMedia(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Media item in e.NewItems)
                {
                    if (!item.ContentContainers.Contains(this))
                    {
                        item.ContentContainers.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Media item in e.OldItems)
                {
                    if (item.ContentContainers.Contains(this))
                    {
                        item.ContentContainers.Remove(this);
                    }
                }
            }
        }

        #endregion
    }
}
