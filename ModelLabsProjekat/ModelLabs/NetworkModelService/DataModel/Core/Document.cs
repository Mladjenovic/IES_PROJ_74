using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Document : IdentifiedObject
    {
        private DateTime createdDateTime;
        private DateTime lastModifiedDateTime;
        private string   revisionNumber;
        private string   subject ;
        private string   title;
        private string   type;

        public Document(long globalId) : base(globalId)
        {
        }
        public DateTime CreatedDateTime { get => createdDateTime; set => createdDateTime = value; }
        public DateTime LastModifiedDateTime { get => lastModifiedDateTime; set => lastModifiedDateTime = value; }
        public string RevisionNumber { get => revisionNumber; set => revisionNumber = value; }
        public string Subject { get => subject; set => subject = value; }
        public string Title { get => title; set => title = value; }
        public string Type { get => type; set => type = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Document x = (Document)obj;
                return ((x.CreatedDateTime == this.CreatedDateTime) &&
                        (x.LastModifiedDateTime == this.LastModifiedDateTime) &&
                        (x.RevisionNumber == this.RevisionNumber) &&
                        (x.Subject == this.Subject) &&
                        (x.Title == this.Title) &&
                        (x.Type == this.Type));
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
                case ModelCode.DOCUMENT_CREATEDDATETIME:
                case ModelCode.DOCUMENT_LASTMODIFIEDDATETIME:
                case ModelCode.DOCUMENT_REVISIONNUMBER:
                case ModelCode.DOCUMENT_SUBJECT:
                case ModelCode.DOCUMENT_TITLE:
                case ModelCode.DOCUMENT_TYPE:

                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.DOCUMENT_CREATEDDATETIME:
                    prop.SetValue(createdDateTime);
                    break;
                case ModelCode.DOCUMENT_LASTMODIFIEDDATETIME:
                    prop.SetValue(lastModifiedDateTime);
                    break;
                case ModelCode.DOCUMENT_REVISIONNUMBER:
                    prop.SetValue(revisionNumber);
                    break;

                case ModelCode.DOCUMENT_SUBJECT:
                    prop.SetValue(subject);
                    break;
                case ModelCode.DOCUMENT_TITLE:
                    prop.SetValue(title);
                    break;
                case ModelCode.DOCUMENT_TYPE:
                    prop.SetValue(type);
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
                case ModelCode.DOCUMENT_CREATEDDATETIME:
                    createdDateTime = property.AsDateTime();
                    break;
                case ModelCode.DOCUMENT_LASTMODIFIEDDATETIME:
                    lastModifiedDateTime = property.AsDateTime();
                    break;
                case ModelCode.DOCUMENT_REVISIONNUMBER:
                    revisionNumber = property.AsString();
                    break;
                case ModelCode.DOCUMENT_SUBJECT:
                    subject = property.AsString();
                    break;
                case ModelCode.DOCUMENT_TITLE:
                    title = property.AsString();
                    break;
                case ModelCode.DOCUMENT_TYPE:
                    type = property.AsString();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
