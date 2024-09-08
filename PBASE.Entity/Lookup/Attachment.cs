using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class Attachment : TrackingEntity
    {
        [Key]
        public int AttachmentId { get; set; }
        public string AttachmentFileName { get; set; }
        public decimal? AttachmentFileSize { get; set; }
        public string AttachmentDescription { get; set; }
        public string AttachmentFileHandle { get; set; }
        public string AttachmentFileType { get; set; }
        public bool? IsArchived { get; set; }
        public int? TypeId { get; set; }
        public int? ReferenceId { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public byte[] AttachmentFile { get; set; }
        public int? QuotationId { get; set; }
        public int? ApplicationId { get; set; }
        public int? ClaimId { get; set; }
        public string SerialNo { get; set; }
        public bool? IsDealerVisible { get; set; }
        public bool? IsProcessed { get; set; }
        public string ConnectedTable { get; set; }
        public string ConnectedField { get; set; }
        public int? ConnectedId { get; set; }
        public bool? IsConnectedRecordDeleted { get; set; }
        public DateTime? ConnectedRecordDeletedDate { get; set; }
        public bool? IsFilePickerAttachmentDeleted { get; set; }
        public DateTime? FilePickerAttachmentDeletedDate { get; set; }

    }
}

