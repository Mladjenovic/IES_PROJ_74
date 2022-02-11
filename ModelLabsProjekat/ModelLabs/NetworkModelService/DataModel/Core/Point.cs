using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Point : IdentifiedObject
    {
        private int position;
        private float quantity;
        private long period;
        public Point(long globalId) : base(globalId)
        {
        }

        public int Position { get => position; set => position = value; }
        public float Quantity { get => quantity; set => quantity = value; }
        public long Period { get => period; set => period = value; }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Point x = (Point)obj;
                return ((x.position == this.position) &&
                        (x.quantity== this.quantity) &&
                        (x.period == this.period));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.POINT_POSITION:
                case ModelCode.POINT_QUANTITY:
                case ModelCode.POINT_PERIOD:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }
        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.POINT_POSITION:
                    prop.SetValue(position);
                    break;
                case ModelCode.POINT_QUANTITY:
                    prop.SetValue(quantity);
                    break;
                case ModelCode.POINT_PERIOD:
                    prop.SetValue(period);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.POINT_POSITION:
                    position = property.AsInt();
                    break;
                case ModelCode.POINT_QUANTITY:
                    quantity = property.AsFloat();
                    break;
                case ModelCode.POINT_PERIOD:
                    period = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
