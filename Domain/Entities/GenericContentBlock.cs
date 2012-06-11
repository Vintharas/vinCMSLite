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
    public partial class GenericContentBlock : ContentContainer
    {
        #region Primitive Properties
    
        public int Weight
        {
            get;
            set;
        }
    
        public int PositionID
        {
            get { return _positionID; }
            set
            {
                if (_positionID != value)
                {
                    if (Position != null && Position.PositionID != value)
                    {
                        Position = null;
                    }
                    _positionID = value;
                }
            }
        }
        private int _positionID;

        #endregion
        #region Navigation Properties
    
        public Position Position
        {
            get { return _position; }
            set
            {
                if (!ReferenceEquals(_position, value))
                {
                    var previousValue = _position;
                    _position = value;
                    FixupPosition(previousValue);
                }
            }
        }
        private Position _position;

        #endregion
        #region Association Fixup
    
        private void FixupPosition(Position previousValue)
        {
            if (previousValue != null && previousValue.GenericContentBlocks.Contains(this))
            {
                previousValue.GenericContentBlocks.Remove(this);
            }
    
            if (Position != null)
            {
                if (!Position.GenericContentBlocks.Contains(this))
                {
                    Position.GenericContentBlocks.Add(this);
                }
                if (PositionID != Position.PositionID)
                {
                    PositionID = Position.PositionID;
                }
            }
        }

        #endregion
    }
}
